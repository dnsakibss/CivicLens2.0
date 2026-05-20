using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CivicLens
{
    public partial class AdminManageAdminsForm : Form
    {
        // ✅ Adjust server if needed
        private readonly SqlConnection _con = new SqlConnection(
            "Data Source=LAPTOP-368QC6MP\\SQLEXPRESS;Initial Catalog=CivicLensDB;Integrated Security=True;");

        public AdminManageAdminsForm()
        {
            InitializeComponent();

            // Ensure we hook up Search button handlers even if designer didn’t
            this.Load += AdminManageAdminsForm_Load;
            this.btnSearchAdmin.Click += btnSearchAdmin_Click;
            this.btnSearchUsers.Click += btnSearchUsers_Click;
        }

        // -------------------- FORM LOAD --------------------
        private void AdminManageAdminsForm_Load(object sender, EventArgs e)
        {
            // Right pane role filter (non-admin roles)
            if (cmbRoleFilter.Items.Count == 0)
                cmbRoleFilter.Items.AddRange(new object[] { "Any", "Moderator", "Police", "Journalist", "Citizen" });
            cmbRoleFilter.SelectedItem = "Any";

            SafeLoadAdmins();
            SafeLoadUsers();
        }

        // -------------------- LEFT: ADMINS --------------------
        private void btnSearchAdmin_Click(object sender, EventArgs e) => SafeLoadAdmins();

        private void btnRefreshAdmins_Click(object sender, EventArgs e)
        {
            txtSearchAdmin.Clear();
            SafeLoadAdmins();
        }

        private void dgvAdmins_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var col = dgvAdmins.Columns[e.ColumnIndex].Name;
            var id = Convert.ToInt32(dgvAdmins.Rows[e.RowIndex].Cells["colAdminId"].Value);
            var name = dgvAdmins.Rows[e.RowIndex].Cells["colAdminName"].Value?.ToString();

            if (col == "colAdminView")
            {
                try
                {
                    using (var f = new ViewProfileForm(id, name))
                        f.ShowDialog(this);
                }
                catch
                {
                    MessageBox.Show($"Open ViewProfileForm for admin #{id} ({name}).", "CivicLens");
                }
            }
            else if (col == "colAdminDemote")
            {
                var ok = MessageBox.Show($"Demote admin #{id} ({name}) to Citizen?", "Confirm",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (ok != DialogResult.Yes) return;

                try
                {
                    ChangeUserRole(id, "Citizen");
                    MessageBox.Show("Admin demoted to Citizen.", "CivicLens");
                    SafeLoadAdmins();
                    SafeLoadUsers(); // in case they move to right list
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Demote error: " + ex.Message, "CivicLens");
                }
            }
        }

        private void btnDemoteSelected_Click(object sender, EventArgs e)
        {
            var ids = GetSelectedIds(dgvAdmins, "colAdminId");
            if (ids.Count == 0) { MessageBox.Show("Select one or more admins to demote."); return; }

            var ok = MessageBox.Show($"Demote {ids.Count} admin(s) to Citizen?", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (ok != DialogResult.Yes) return;

            try
            {
                BulkChangeRole(ids, "Citizen");
                MessageBox.Show("Selected admin(s) demoted to Citizen.", "CivicLens");
                SafeLoadAdmins();
                SafeLoadUsers();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bulk demote error: " + ex.Message, "CivicLens");
            }
        }

        // -------------------- RIGHT: USERS (PROMOTE) --------------------
        private void btnSearchUsers_Click(object sender, EventArgs e) => SafeLoadUsers();

        private void dgvUsers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var col = dgvUsers.Columns[e.ColumnIndex].Name;
            var id = Convert.ToInt32(dgvUsers.Rows[e.RowIndex].Cells["colUserId"].Value);
            var name = dgvUsers.Rows[e.RowIndex].Cells["colUserName"].Value?.ToString();

            if (col == "colUserView")
            {
                try
                {
                    using (var f = new ViewProfileForm(id, name))
                        f.ShowDialog(this);
                }
                catch
                {
                    MessageBox.Show($"Open ViewProfileForm for user #{id} ({name}).", "CivicLens");
                }
            }
            else if (col == "colUserPromote")
            {
                var ok = MessageBox.Show($"Promote user #{id} ({name}) to Admin?", "Confirm",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (ok != DialogResult.Yes) return;

                try
                {
                    ChangeUserRole(id, "Admin");
                    MessageBox.Show("User promoted to Admin.", "CivicLens");
                    SafeLoadUsers();
                    SafeLoadAdmins();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Promote error: " + ex.Message, "CivicLens");
                }
            }
        }

        private void btnPromoteSelected_Click(object sender, EventArgs e)
        {
            var ids = GetSelectedIds(dgvUsers, "colUserId");
            if (ids.Count == 0) { MessageBox.Show("Select one or more users to promote."); return; }

            var ok = MessageBox.Show($"Promote {ids.Count} user(s) to Admin?", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (ok != DialogResult.Yes) return;

            try
            {
                BulkChangeRole(ids, "Admin");
                MessageBox.Show("Selected user(s) promoted to Admin.", "CivicLens");
                SafeLoadUsers();
                SafeLoadAdmins();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bulk promote error: " + ex.Message, "CivicLens");
            }
        }

        private void btnAddNewAdmin_Click(object sender, EventArgs e)
        {
            // If you have an AddUserForm, open it with defaults. Otherwise, leave this as-is or implement later.
            MessageBox.Show("Open AddUserForm pre-filled with Role=Admin (Approved).", "CivicLens");
        }

        private void btnClose_Click(object sender, EventArgs e) => this.Close();

        // -------------------- LOADERS --------------------
        private void SafeLoadAdmins()
        {
            try { LoadAdminsFromDb(); }
            catch (Exception ex) { MessageBox.Show("Load admins error: " + ex.Message, "CivicLens"); }
        }

        private void SafeLoadUsers()
        {
            try { LoadUsersFromDb(); }
            catch (Exception ex) { MessageBox.Show("Load users error: " + ex.Message, "CivicLens"); }
        }

        private void LoadAdminsFromDb()
        {
            dgvAdmins.Rows.Clear();
            string q = (txtSearchAdmin.Text ?? "").Trim();

            // Show current Admins (approved, not deleted)
            // Adjust columns/joins if your schema differs
            var sql = @"SELECT 
                            u.UserId,
                            u.FullName,
                            u.Email,
                            u.Phone,
                            ISNULL(u.ApprovedAt, u.CreatedAt) AS AdminSince
                        FROM Users u
                        JOIN Roles r ON r.RoleId = u.RoleId
                        WHERE r.RoleName = 'Admin'
                          AND u.IsDeleted = 0
                        ";

            if (!string.IsNullOrWhiteSpace(q))
            {
                sql += @" AND (u.FullName LIKE @q OR u.Email LIKE @q OR u.Phone LIKE @q) ";
            }

            sql += " ORDER BY AdminSince ASC;";

            using (var da = new SqlDataAdapter(sql, _con))
            {
                if (!string.IsNullOrWhiteSpace(q))
                    da.SelectCommand.Parameters.AddWithValue("@q", "%" + q + "%");

                var dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow r in dt.Rows)
                {
                    dgvAdmins.Rows.Add(
                        r["UserId"],
                        r["FullName"],
                        r["Email"],
                        r["Phone"],
                        Convert.ToDateTime(r["AdminSince"]).ToString("yyyy-MM-dd HH:mm")
                    );
                }
            }
        }

        private void LoadUsersFromDb()
        {
            dgvUsers.Rows.Clear();

            string q = (txtUserSearch.Text ?? "").Trim();
            string roleFilter = (cmbRoleFilter.SelectedItem?.ToString() ?? "Any");

            // Non-admin users only
            var sb = new StringBuilder(@"
SELECT 
    u.UserId,
    u.FullName,
    u.Email,
    u.Phone,
    r.RoleName,
    CASE WHEN u.IsActive = 1 THEN 'Active' ELSE 'Inactive' END AS Status
FROM Users u
JOIN Roles r ON r.RoleId = u.RoleId
WHERE u.IsDeleted = 0
  AND r.RoleName <> 'Admin'
");

            if (!string.Equals(roleFilter, "Any", StringComparison.OrdinalIgnoreCase))
            {
                sb.Append(" AND r.RoleName = @role ");
            }
            if (!string.IsNullOrWhiteSpace(q))
            {
                sb.Append(" AND (u.FullName LIKE @q OR u.Email LIKE @q OR u.Phone LIKE @q) ");
            }

            sb.Append(" ORDER BY Status DESC, r.RoleName ASC, u.FullName ASC; ");

            using (var da = new SqlDataAdapter(sb.ToString(), _con))
            {
                if (!string.Equals(roleFilter, "Any", StringComparison.OrdinalIgnoreCase))
                    da.SelectCommand.Parameters.AddWithValue("@role", roleFilter);

                if (!string.IsNullOrWhiteSpace(q))
                    da.SelectCommand.Parameters.AddWithValue("@q", "%" + q + "%");

                var dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow r in dt.Rows)
                {
                    dgvUsers.Rows.Add(
                        r["UserId"],
                        r["FullName"],
                        r["Email"],
                        r["Phone"],
                        r["RoleName"],
                        r["Status"]
                    );
                }
            }
        }

        // -------------------- ROLE HELPERS --------------------
        private int GetRoleIdByName(string roleName, SqlConnection con, SqlTransaction tx = null)
        {
            using (var cmd = new SqlCommand("SELECT RoleId FROM Roles WHERE RoleName=@name", con, tx))
            {
                cmd.Parameters.AddWithValue("@name", roleName);
                var o = cmd.ExecuteScalar();
                if (o == null || o == DBNull.Value)
                    throw new Exception($"Role '{roleName}' not found.");
                return Convert.ToInt32(o);
            }
        }

        private void ChangeUserRole(int userId, string roleName)
        {
            if (_con.State != ConnectionState.Open) _con.Open();
            SqlTransaction tx = _con.BeginTransaction();
            try
            {
                int roleId = GetRoleIdByName(roleName, _con, tx);
                using (var cmd = new SqlCommand(
                    "UPDATE Users SET RoleId=@rid WHERE UserId=@uid;", _con, tx))
                {
                    cmd.Parameters.AddWithValue("@rid", roleId);
                    cmd.Parameters.AddWithValue("@uid", userId);
                    cmd.ExecuteNonQuery();
                }
                tx.Commit();
            }
            catch
            {
                try { tx.Rollback(); } catch { /* ignore */ }
                throw;
            }
            finally
            {
                if (_con.State == ConnectionState.Open) _con.Close();
            }
        }

        private void BulkChangeRole(List<int> userIds, string roleName)
        {
            if (userIds == null || userIds.Count == 0) return;

            if (_con.State != ConnectionState.Open) _con.Open();
            SqlTransaction tx = _con.BeginTransaction();
            try
            {
                int roleId = GetRoleIdByName(roleName, _con, tx);

                using (var cmd = new SqlCommand(
                    "UPDATE Users SET RoleId=@rid WHERE UserId=@uid;", _con, tx))
                {
                    cmd.Parameters.Add("@rid", SqlDbType.Int).Value = roleId;
                    var pUid = cmd.Parameters.Add("@uid", SqlDbType.Int);

                    foreach (var id in userIds)
                    {
                        pUid.Value = id;
                        cmd.ExecuteNonQuery();
                    }
                }

                tx.Commit();
            }
            catch
            {
                try { tx.Rollback(); } catch { /* ignore */ }
                throw;
            }
            finally
            {
                if (_con.State == ConnectionState.Open) _con.Close();
            }
        }

        // -------------------- UTILS --------------------
        private List<int> GetSelectedIds(DataGridView grid, string idColName)
        {
            var ids = new List<int>();
            foreach (DataGridViewRow row in grid.SelectedRows)
            {
                var v = row.Cells[idColName].Value?.ToString();
                if (int.TryParse(v, out var id)) ids.Add(id);
            }
            return ids;
        }
    }
}
