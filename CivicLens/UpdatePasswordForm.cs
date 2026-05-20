using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace CivicLens
{
    public enum UpdatePasswordMode { Forgot, Change }

    public partial class UpdatePasswordForm : Form
    {
        private readonly SqlConnection _con =
            new SqlConnection("Data Source=LAPTOP-368QC6MP\\SQLEXPRESS;Initial Catalog=CivicLensDB;Integrated Security=True;");

        private readonly UpdatePasswordMode _mode;
        private readonly int _currentUserId;

        public UpdatePasswordForm(UpdatePasswordMode mode, int currentUserId = 0)
        {
            _mode = mode;
            _currentUserId = currentUserId;
            InitializeComponent();
            Load += UpdatePasswordForm_Load;
        }

        private void UpdatePasswordForm_Load(object sender, EventArgs e)
        {
            try
            {
                LoadRoles();
                bool isChange = _mode == UpdatePasswordMode.Change;
                lblTitle.Text = isChange ? "Change Password" : "Reset / Forgot Password";
                lblCurrentPassword.Visible = txtCurrentPassword.Visible = isChange;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Load error: " + ex.Message, "CivicLens");
            }
        }

        private void LoadRoles()
        {
            cmbRole.Items.Clear();
            string sql = "SELECT RoleName FROM Roles ORDER BY RoleName";
            using (var da = new SqlDataAdapter(sql, _con))
            {
                var dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow r in dt.Rows)
                    cmbRole.Items.Add(Convert.ToString(r["RoleName"]));
            }
            if (cmbRole.Items.Count > 0) cmbRole.SelectedIndex = 0;
        }

        private void chkShowNew_CheckedChanged(object sender, EventArgs e)
        {
            bool show = chkShowNew.Checked;
            txtNewPassword.UseSystemPasswordChar = !show;
            txtConfirmPassword.UseSystemPasswordChar = !show;
            txtCurrentPassword.UseSystemPasswordChar = !show;
        }

        private void btnCancel_Click(object sender, EventArgs e) => Close();

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string fullName = txtFullName.Text.Trim();
            string email = txtEmail.Text.Trim();
            string phone = txtPhone.Text.Trim();
            string role = (cmbRole.SelectedItem?.ToString() ?? "").Trim();
            string username = txtUsername.Text.Trim();
            string newPw = txtNewPassword.Text.Trim();
            string confirm = txtConfirmPassword.Text.Trim();

            if (string.IsNullOrWhiteSpace(fullName) ||
                string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(phone) ||
                string.IsNullOrWhiteSpace(role) ||
                string.IsNullOrWhiteSpace(username))
            {
                MessageBox.Show("Please fill all required fields in the identity section.", "CivicLens");
                return;
            }
            if (string.IsNullOrWhiteSpace(newPw) || string.IsNullOrWhiteSpace(confirm))
            {
                MessageBox.Show("Please enter and confirm the new password.", "CivicLens");
                return;
            }
            if (newPw != confirm)
            {
                MessageBox.Show("New & Confirm password do not match.", "CivicLens");
                txtConfirmPassword.Focus();
                return;
            }
            if (!ValidatePasswordPolicy(newPw))
            {
                MessageBox.Show("Password must be at least 8 characters and contain a number.", "CivicLens");
                txtNewPassword.Focus();
                return;
            }

            try
            {
                int userId = GetUserId(fullName, email, phone, role, username);
                if (userId == 0)
                {
                    MessageBox.Show("Identity information did not match our records.", "CivicLens");
                    return;
                }

                if (_mode == UpdatePasswordMode.Change)
                {
                    string current = txtCurrentPassword.Text.Trim();
                    if (string.IsNullOrWhiteSpace(current))
                    {
                        MessageBox.Show("Current password is required.", "CivicLens");
                        return;
                    }
                    if (!VerifyCurrentPassword(userId, current))
                    {
                        MessageBox.Show("Current password is incorrect.", "CivicLens");
                        return;
                    }
                }

                UpdatePassword(userId, newPw);
                if (_con.State == ConnectionState.Open) _con.Close();

                MessageBox.Show("Password updated successfully. Please log in again.", "CivicLens");

                ReturnToLogin();
            }
            catch (Exception ex)
            {
                if (_con.State == ConnectionState.Open) _con.Close();
                MessageBox.Show("Update failed: " + ex.Message, "CivicLens");
            }
        }

        private int GetUserId(string fullName, string email, string phone, string role, string username)
        {
            string sql = @"
                SELECT TOP 1 u.UserId
                FROM Users u
                JOIN Roles r ON r.RoleId = u.RoleId
                JOIN Logins l ON l.UserId = u.UserId
                WHERE u.FullName = @fn
                  AND u.Email    = @em
                  AND u.Phone    = @ph
                  AND r.RoleName = @role
                  AND l.Username = @un";

            using (var cmd = new SqlCommand(sql, _con))
            {
                cmd.Parameters.AddWithValue("@fn", fullName);
                cmd.Parameters.AddWithValue("@em", email);
                cmd.Parameters.AddWithValue("@ph", phone);
                cmd.Parameters.AddWithValue("@role", role);
                cmd.Parameters.AddWithValue("@un", username);

                _con.Open();
                var result = cmd.ExecuteScalar();
                _con.Close();

                return result == null ? 0 : Convert.ToInt32(result);
            }
        }

        private bool VerifyCurrentPassword(int userId, string current)
        {
            string sql = "SELECT [Password] FROM Logins WHERE UserId = @id";
            using (var cmd = new SqlCommand(sql, _con))
            {
                cmd.Parameters.AddWithValue("@id", userId);
                _con.Open();
                var obj = cmd.ExecuteScalar();
                _con.Close();

                string stored = obj == null ? "" : Convert.ToString(obj);
                return string.Equals(stored, current);
            }
        }

        private void UpdatePassword(int userId, string newPw)
        {
            string sql = "UPDATE Logins SET [Password] = @p WHERE UserId = @id";
            using (var cmd = new SqlCommand(sql, _con))
            {
                cmd.Parameters.AddWithValue("@p", newPw);
                cmd.Parameters.AddWithValue("@id", userId);
                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
            }
        }

        private void ReturnToLogin()
        {
            
            this.Close();
            var formsToClose = Application.OpenForms
                                          .Cast<Form>()
                                          .Where(f => !(f is LoginForm))
                                          .ToList();

            foreach (var f in formsToClose)
                try { f.Close(); } catch { }
            var login = Application.OpenForms
                                   .Cast<Form>()
                                   .OfType<LoginForm>()
                                   .FirstOrDefault();

            if (login != null && !login.Visible)
                login.Show();
        }

        private bool ValidatePasswordPolicy(string pw)
        {
            if (string.IsNullOrWhiteSpace(pw) || pw.Length < 8) return false;
            return pw.Any(char.IsDigit);
        }
    }
}