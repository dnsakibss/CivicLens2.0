using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CivicLens
{
    public partial class SignupForm : Form
    {
       
        SqlConnection con = new SqlConnection(
            "Data Source=LAPTOP-368QC6MP\\SQLEXPRESS;Initial Catalog=CivicLensDB;Integrated Security=True;");

        public SignupForm()
        {
            InitializeComponent();
        }

        private void btnSignup_Click(object sender, EventArgs e)
        {
            string fullName = (txtFullName.Text ?? "").Trim();
            string email = (txtEmail.Text ?? "").Trim();
            string phone = (txtPhone.Text ?? "").Trim();
            string address = (txtAddress.Text ?? "").Trim();
            string role = cmbRole.SelectedItem == null ? "" : cmbRole.SelectedItem.ToString().Trim();
            string username = (txtUsername.Text ?? "").Trim();
            string password = txtPassword.Text ?? "";
            string confirm = txtConfirm.Text ?? "";

            if (string.IsNullOrWhiteSpace(fullName) ||
                string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(confirm) ||
                string.IsNullOrWhiteSpace(role))
            {
                MessageBox.Show("Please fill all required fields.", "Signup", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!password.Equals(confirm))
            {
                MessageBox.Show("Passwords do not match.", "Signup", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
               
                string existsQuery = "SELECT COUNT(1) FROM Logins WHERE Username = '" + Escape(username) + "'";
                SqlDataAdapter sda = new SqlDataAdapter(existsQuery, con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (Convert.ToInt32(dt.Rows[0][0]) > 0)
                {
                    MessageBox.Show("Username already exists. Choose another.");
                    return;
                }

              
                string insertUser =
                    "INSERT INTO Users(FullName, Email, Phone, AddressLine, RoleId, ApprovalStatus) " +
                    "SELECT '" + Escape(fullName) + "', " +
                           (string.IsNullOrWhiteSpace(email) ? "NULL" : "'" + Escape(email) + "'") + ", " +
                           (string.IsNullOrWhiteSpace(phone) ? "NULL" : "'" + Escape(phone) + "'") + ", " +
                           (string.IsNullOrWhiteSpace(address) ? "NULL" : "'" + Escape(address) + "'") + ", " +
                           "r.RoleId, 'Pending' " +
                    "FROM Roles r WHERE r.RoleName = '" + Escape(role) + "'; " +
                    "SELECT CAST(SCOPE_IDENTITY() AS INT);";

                SqlCommand cmdInsertUser = new SqlCommand(insertUser, con);
                con.Open();
                object newIdObj = cmdInsertUser.ExecuteScalar();
                con.Close();

                int newUserId = (newIdObj == null || newIdObj == DBNull.Value) ? 0 : Convert.ToInt32(newIdObj);
                if (newUserId <= 0)
                {
                    MessageBox.Show("Failed to create user. (Check if the selected role exists.)",
                        "Signup", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

               
                string insertLogin =
                    "INSERT INTO Logins(UserId, Username, Password, IsLocked) " +
                    "VALUES (" + newUserId + ", '" + Escape(username) + "', '" + Escape(password) + "', 0)";

                SqlCommand cmdInsertLogin = new SqlCommand(insertLogin, con);
                con.Open();
                int rows = cmdInsertLogin.ExecuteNonQuery();
                con.Close();

                if (rows > 0)
                {
                    MessageBox.Show(
                        "Account created successfully.\nYour account is pending admin approval.",
                        "CivicLens", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Signup failed while creating login.", "Signup", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open) con.Close();
                MessageBox.Show("Signup failed: " + ex.Message, "Signup", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

   
        private static string Escape(string s) => s?.Replace("'", "''") ?? "";
    }
}