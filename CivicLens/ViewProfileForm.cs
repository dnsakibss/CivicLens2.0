using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CivicLens
{
    public partial class ViewProfileForm : Form
    {
        private readonly SqlConnection _con =
            new SqlConnection("Data Source=LAPTOP-368QC6MP\\SQLEXPRESS;Initial Catalog=CivicLensDB;Integrated Security=True;");

        private readonly int _userId;
        private readonly string _fullNameHint;

        private bool _hasFullName, _hasEmail, _hasPhone, _hasCreatedAt, _hasApprovedAt, _hasIsApproved, _hasRole, _hasRoleId, _hasRolesTableAndName;
        private string _addressCol; 

        public ViewProfileForm(int userId = 0, string fullName = null)
        {
            _userId = userId;
            _fullNameHint = fullName;
            InitializeComponent();
            this.Load += ViewProfileForm_Load;
        }

        private void ViewProfileForm_Load(object sender, EventArgs e)
        {
            try
            {
                if (_userId <= 0) { MessageBox.Show("Invalid user id.", "CivicLens", MessageBoxButtons.OK, MessageBoxIcon.Error); Close(); return; }
                DetectSchema();
                LoadProfileFromDb();
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
            _hasCreatedAt = cols.Contains("CreatedAt");
            _hasApprovedAt = cols.Contains("ApprovedAt");
            _hasIsApproved = cols.Contains("IsApproved");
            _hasRole = cols.Contains("Role");
            _hasRoleId = cols.Contains("RoleId");

            if (cols.Contains("Address")) _addressCol = "Address";
            else if (cols.Contains("AddressLine")) _addressCol = "AddressLine";
            else if (cols.Contains("Address1")) _addressCol = "Address1";
            else _addressCol = null;

            bool hasRolesTable = false, hasRoleNameCol = false;
            using (var daT = new SqlDataAdapter(
                       "SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME='Roles'", _con))
            {
                var dt = new DataTable();
                daT.Fill(dt);
                hasRolesTable = dt.Rows.Count > 0;
            }
            if (hasRolesTable)
            {
                using (var daC = new SqlDataAdapter(
                           "SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME='Roles' AND COLUMN_NAME='RoleName'", _con))
                {
                    var dt = new DataTable();
                    daC.Fill(dt);
                    hasRoleNameCol = dt.Rows.Count > 0;
                }
            }
            _hasRolesTableAndName = hasRolesTable && hasRoleNameCol;
        }

        private void LoadProfileFromDb()
        {
            var select = new List<string> { "u.UserId" };

            if (_hasFullName) select.Add("ISNULL(u.FullName,'') AS FullName");
            if (_hasEmail) select.Add("ISNULL(u.Email,'') AS Email");
            if (_hasPhone) select.Add("ISNULL(u.Phone,'') AS Phone");
            if (!string.IsNullOrEmpty(_addressCol)) select.Add($"ISNULL(u.{_addressCol},'') AS Address");
            if (_hasCreatedAt) select.Add("u.CreatedAt AS CreatedAt");
            if (_hasApprovedAt) select.Add("u.ApprovedAt AS ApprovedAt");
            if (_hasIsApproved) select.Add("u.IsApproved AS IsApproved");

            string join = "";
            if (_hasRoleId && _hasRolesTableAndName)
            {
                select.Add("ISNULL(r.RoleName,'') AS RoleName");
                join = " LEFT JOIN Roles r ON r.RoleId = u.RoleId ";
            }
            else if (_hasRole)
            {
                select.Add("ISNULL(u.Role,'') AS RoleName");
            }
            else
            {
                select.Add("'' AS RoleName");
            }

            string sql = "SELECT " + string.Join(", ", select) + " FROM Users u " + join + " WHERE u.UserId=@id";

            var da = new SqlDataAdapter(sql, _con);
            da.SelectCommand.Parameters.AddWithValue("@id", _userId);

            var dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count == 0)
            {
                txtFullName.Text = _fullNameHint ?? "";
                txtEmail.Text = txtPhone.Text = txtAddress.Text = txtRole.Text = txtApproval.Text = "";
                txtCreatedAt.Text = txtApprovedAt.Text = "";
                MessageBox.Show("User not found.", "CivicLens");
                return;
            }

            var r = dt.Rows[0];

            txtFullName.Text = _hasFullName ? Convert.ToString(r["FullName"]) : (_fullNameHint ?? "");
            txtEmail.Text = _hasEmail ? Convert.ToString(r["Email"]) : "";
            txtPhone.Text = _hasPhone ? Convert.ToString(r["Phone"]) : "";
            txtAddress.Text = (!string.IsNullOrEmpty(_addressCol) && r.Table.Columns.Contains("Address")) ? Convert.ToString(r["Address"]) : "";
            txtRole.Text = Convert.ToString(r["RoleName"]);

            if (_hasCreatedAt && r["CreatedAt"] != DBNull.Value)
                txtCreatedAt.Text = Convert.ToDateTime(r["CreatedAt"]).ToString("yyyy-MM-dd HH:mm");
            else
                txtCreatedAt.Text = "";

            if (_hasApprovedAt && r["ApprovedAt"] != DBNull.Value)
                txtApprovedAt.Text = Convert.ToDateTime(r["ApprovedAt"]).ToString("yyyy-MM-dd HH:mm");
            else
                txtApprovedAt.Text = "";

            if (_hasIsApproved && r["IsApproved"] != DBNull.Value)
            {
                bool ok = false; try { ok = Convert.ToBoolean(r["IsApproved"]); } catch { }
                txtApproval.Text = ok ? "Approved" : "Pending";
            }
            else if (_hasApprovedAt)
            {
                txtApproval.Text = (r["ApprovedAt"] != DBNull.Value) ? "Approved" : "Pending";
            }
            else
            {
                txtApproval.Text = "";
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try { LoadProfileFromDb(); }
            catch (Exception ex) { MessageBox.Show("Refresh error: " + ex.Message, "CivicLens"); }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                using (var f = new EditProfileForm(_userId))
                {
                    if (f.ShowDialog(this) == DialogResult.OK)
                        LoadProfileFromDb();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Open edit form failed: " + ex.Message, "CivicLens");
            }
        }

        private void btnClose_Click(object sender, EventArgs e) => Close();
    }
}
