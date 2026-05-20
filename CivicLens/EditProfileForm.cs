using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace CivicLens
{
    public partial class EditProfileForm : Form
    {
        private readonly int _userId;
        private string _photoPath;

        private readonly SqlConnection _con =
            new SqlConnection("Data Source=LAPTOP-368QC6MP\\SQLEXPRESS;Initial Catalog=CivicLensDB;Integrated Security=True;");

        private bool _hasFullName, _hasEmail, _hasPhone, _hasPassword;
        private string _addressCol; 

        public EditProfileForm(int userId = 0)
        {
            _userId = userId;
            InitializeComponent();
            this.Load += EditProfileForm_Load;
        }

        private void EditProfileForm_Load(object sender, EventArgs e)
        {
            try
            {
                DetectSchema();
                LoadCurrentValues();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Load error: " + ex.Message, "CivicLens");
            }
        }

        private void DetectSchema()
        {
            var cols = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            using (var da = new SqlDataAdapter(
                       "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME='Users'", _con))
            {
                var dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow r in dt.Rows) cols.Add(Convert.ToString(r["COLUMN_NAME"]));
            }

            _hasFullName = cols.Contains("FullName");
            _hasEmail = cols.Contains("Email");
            _hasPhone = cols.Contains("Phone");

            if (cols.Contains("Address")) _addressCol = "Address";
            else if (cols.Contains("AddressLine")) _addressCol = "AddressLine";
            else if (cols.Contains("Address1")) _addressCol = "Address1";
            else _addressCol = null;

            _hasPassword = TableHasColumn("Logins", "Password");
            if (!_hasPassword)
            {
                MessageBox.Show(
                    "Warning: Your Logins table has no 'Password' column.\nPassword verification will be skipped.",
                    "CivicLens", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private bool TableHasColumn(string table, string col)
        {
            using (var da = new SqlDataAdapter(
                       "SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME=@t AND COLUMN_NAME=@c", _con))
            {
                da.SelectCommand.Parameters.AddWithValue("@t", table);
                da.SelectCommand.Parameters.AddWithValue("@c", col);
                var dt = new DataTable();
                da.Fill(dt);
                return dt.Rows.Count > 0;
            }
        }

        private void LoadCurrentValues()
        {
            if (_userId <= 0)
            {
                txtFullName.Text = "";
                txtEmail.Text = "";
                txtPhone.Text = "";
                txtAddress.Text = "";
                txtPassword.Text = "";
                _photoPath = string.Empty;
                return;
            }

            var select = new List<string> { "UserId" };
            if (_hasFullName) select.Add("ISNULL(FullName,'') AS FullName");
            if (_hasEmail) select.Add("ISNULL(Email,'') AS Email");
            if (_hasPhone) select.Add("ISNULL(Phone,'') AS Phone");
            if (!string.IsNullOrEmpty(_addressCol)) select.Add($"ISNULL({_addressCol},'') AS Address");

            var sql = "SELECT " + string.Join(", ", select) + " FROM Users WHERE UserId=@id";
            var da = new SqlDataAdapter(sql, _con);
            da.SelectCommand.Parameters.AddWithValue("@id", _userId);
            var dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count == 0)
            {
                txtFullName.Text = "";
                txtEmail.Text = "";
                txtPhone.Text = "";
                txtAddress.Text = "";
                txtPassword.Text = "";
                _photoPath = string.Empty;
                return;
            }

            var r = dt.Rows[0];
            txtFullName.Text = dt.Columns.Contains("FullName") ? Convert.ToString(r["FullName"]) : "";
            txtEmail.Text = dt.Columns.Contains("Email") ? Convert.ToString(r["Email"]) : "";
            txtPhone.Text = dt.Columns.Contains("Phone") ? Convert.ToString(r["Phone"]) : "";
            txtAddress.Text = dt.Columns.Contains("Address") ? Convert.ToString(r["Address"]) : "";
            txtPassword.Text = ""; 
            _photoPath = string.Empty;
        }

        private void btnUploadPhoto_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                ofd.Title = "Select Profile Picture";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    _photoPath = ofd.FileName;
                    pbAvatar.ImageLocation = _photoPath;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                MessageBox.Show("Full name cannot be empty.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            
            if (_hasPassword)
            {
                var entered = (txtPassword.Text ?? "").Trim();
                if (entered.Length == 0)
                {
                    MessageBox.Show("Please enter your current password to confirm edits.", "CivicLens",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPassword.Focus();
                    return;
                }

                try
                {
                    var dbPw = GetCurrentPasswordFromLogins();
                    if (dbPw == null)
                    {
                        MessageBox.Show("Could not fetch your stored password for verification.", "CivicLens",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (!string.Equals(entered, dbPw, StringComparison.Ordinal))
                    {
                        MessageBox.Show("Incorrect password! Profile update cancelled.", "CivicLens",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtPassword.Focus();
                        txtPassword.SelectAll();
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Password check failed: " + ex.Message, "CivicLens",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            try
            {
                var setList = new List<string>();
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = _con;

                    if (_hasFullName)
                    {
                        setList.Add("FullName=@n");
                        cmd.Parameters.AddWithValue("@n", (txtFullName.Text ?? "").Trim());
                    }
                    if (_hasEmail)
                    {
                        setList.Add("Email=@e");
                        cmd.Parameters.AddWithValue("@e", (txtEmail.Text ?? "").Trim());
                    }
                    if (_hasPhone)
                    {
                        setList.Add("Phone=@p");
                        cmd.Parameters.AddWithValue("@p", (txtPhone.Text ?? "").Trim());
                    }
                    if (!string.IsNullOrEmpty(_addressCol))
                    {
                        setList.Add(_addressCol + "=@a");
                        cmd.Parameters.AddWithValue("@a", (txtAddress.Text ?? "").Trim());
                    }

                    if (setList.Count == 0)
                    {
                        MessageBox.Show("No writable fields found in schema.", "CivicLens");
                        return;
                    }

                    cmd.CommandText = "UPDATE Users SET " + string.Join(", ", setList) + " WHERE UserId=@id";
                    cmd.Parameters.AddWithValue("@id", _userId);

                    _con.Open();
                    int rows = cmd.ExecuteNonQuery();
                    _con.Close();

                    if (rows <= 0)
                    {
                        MessageBox.Show("No rows were updated.", "CivicLens");
                        return;
                    }

                    MessageBox.Show("Profile updated successfully.", "CivicLens",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    Close();
                }
            }
            catch (Exception ex)
            {
                if (_con.State == ConnectionState.Open) _con.Close();
                MessageBox.Show("Save failed: " + ex.Message, "CivicLens");
            }
        }
        private string GetCurrentPasswordFromLogins()
        {
            using (var cmd = new SqlCommand("SELECT [Password] FROM Logins WHERE UserId=@id", _con))
            {
                cmd.Parameters.AddWithValue("@id", _userId);
                _con.Open();
                object obj = cmd.ExecuteScalar();
                _con.Close();
                if (obj == null || obj == DBNull.Value) return null;
                return Convert.ToString(obj);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e) => Close();

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            if (_userId <= 0)
            {
                MessageBox.Show("Invalid user context for changing password.", "CivicLens");
                return;
            }

            using (var f = new UpdatePasswordForm(UpdatePasswordMode.Change, _userId))
            {
                f.StartPosition = FormStartPosition.CenterParent;
                f.ShowDialog(this);
                txtPassword.Clear();
            }
        }
    }
}
