using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CivicLens
{
    public partial class AdminUsersForm : Form
    {
        private readonly SqlConnection con =
            new SqlConnection("Data Source=LAPTOP-368QC6MP\\SQLEXPRESS;Initial Catalog=CivicLensDB;Integrated Security=True;");

        private bool _hasIsActive;

        public AdminUsersForm()
        {
            InitializeComponent();

            this.Load += AdminUsersForm_Load;
            this.btnSearch.Click += btnSearch_Click;
            this.btnRefresh.Click += btnRefresh_Click;
            this.btnBulkActivate.Click += btnBulkActivate_Click;
            this.btnBulkDeactivate.Click += btnBulkDeactivate_Click;
            this.btnBulkDelete.Click += btnBulkDelete_Click;
            this.dgvUsers.CellContentClick += dgvUsers_CellContentClick;
            this.btnClose.Click += btnClose_Click;
        }

        private void AdminUsersForm_Load(object sender, EventArgs e)
        {
            try
            {
                if (cmbRole.Items.Count == 0)
                    cmbRole.Items.AddRange(new object[] { "Any", "Admin", "Moderator", "Police", "Journalist", "Citizen" });
                cmbRole.SelectedItem = "Any";

                if (cmbStatus.Items.Count == 0)
                    cmbStatus.Items.AddRange(new object[] { "Any", "Active", "Inactive" });
                cmbStatus.SelectedItem = "Any";

                DetectSchema();
                LoadGridFromDb();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Load error: " + ex.Message, "CivicLens");
            }
        }

        private void DetectSchema()
        {
            _hasIsActive = false;
            using (var da = new SqlDataAdapter(
                "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME='Users'", con))
            {
                var dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow r in dt.Rows)
                    if (string.Equals(Convert.ToString(r["COLUMN_NAME"]), "IsActive", StringComparison.OrdinalIgnoreCase))
                        _hasIsActive = true;
            }
        }

        private void DeleteUserById(SqlConnection c, int userId)
        {
            void Exec(string sql, string param, int val)
            {
                using (var cmd = new SqlCommand(sql, c))
                {
                    cmd.Parameters.AddWithValue(param, val);
                    cmd.ExecuteNonQuery();
                }
            }

            var complaintIds = new List<int>();
            using (var cmd = new SqlCommand(
                "SELECT ComplaintId FROM Complaints WHERE CreatedByUserId=@uid", c))
            {
                cmd.Parameters.AddWithValue("@uid", userId);
                using (var rdr = cmd.ExecuteReader())
                    while (rdr.Read())
                        complaintIds.Add(rdr.GetInt32(0));
            }

            foreach (int cid in complaintIds)
            {
                Exec("DELETE FROM ComplaintReactions WHERE ComplaintId=@id", "@id", cid);
                Exec("DELETE FROM NewsfeedComments   WHERE ComplaintId=@id", "@id", cid);
                Exec("DELETE FROM ComplaintMessages  WHERE ComplaintId=@id", "@id", cid);
                Exec("DELETE FROM StatusHistory      WHERE ComplaintId=@id", "@id", cid);
                Exec("DELETE FROM Assignments        WHERE ComplaintId=@id", "@id", cid);
                Exec("DELETE FROM ComplaintMedia     WHERE ComplaintId=@id", "@id", cid);
            }

            Exec("DELETE FROM Assignments WHERE AssigneeUserId=@id", "@id", userId);
            Exec("DELETE FROM Assignments WHERE AssignedByUserId=@id", "@id", userId);
            Exec("DELETE FROM ComplaintMessages WHERE SenderUserId=@id", "@id", userId);
            Exec("DELETE FROM ComplaintMessages WHERE ReceiverUserId=@id", "@id", userId);
            Exec("DELETE FROM ComplaintReactions WHERE UserId=@id", "@id", userId);
            Exec("DELETE FROM NewsfeedComments WHERE UserId=@id", "@id", userId);
            Exec("DELETE FROM StatusHistory WHERE ChangedByUserId=@id", "@id", userId);
            Exec("DELETE FROM Complaints WHERE CreatedByUserId=@id", "@id", userId);
            Exec("DELETE FROM Logins WHERE UserId=@id", "@id", userId);
            Exec("DELETE FROM Users WHERE UserId=@id", "@id", userId);
        }

        private void LoadGridFromDb()
        {
            dgvUsers.Rows.Clear();

            string q = (txtSearch.Text ?? "").Trim();
            string roleFilter = (cmbRole.SelectedItem?.ToString() ?? "Any");
            string statusFilter = (cmbStatus.SelectedItem?.ToString() ?? "Any");

            string sql = @"
SELECT
    u.UserId,
    u.FullName,
    u.Email,
    u.Phone,
    r.RoleName,
    CASE WHEN u.IsActive = 1 THEN 'Active' ELSE 'Inactive' END AS StatusText,
    u.CreatedAt
FROM Users u
LEFT JOIN Roles r ON r.RoleId = u.RoleId
WHERE 1 = 1 ";

            if (!string.IsNullOrWhiteSpace(q))
                sql += "AND (u.FullName LIKE @q OR u.Email LIKE @q OR u.Phone LIKE @q) ";

            if (!string.Equals(roleFilter, "Any", StringComparison.OrdinalIgnoreCase))
                sql += "AND r.RoleName = @role ";

            if (!string.Equals(statusFilter, "Any", StringComparison.OrdinalIgnoreCase))
                sql += "AND u.IsActive = " + (statusFilter == "Active" ? "1 " : "0 ");

            sql += "ORDER BY u.CreatedAt DESC;";

            using (var da = new SqlDataAdapter(sql, con))
            {
                if (!string.IsNullOrWhiteSpace(q))
                    da.SelectCommand.Parameters.AddWithValue("@q", "%" + q + "%");
                if (!string.Equals(roleFilter, "Any", StringComparison.OrdinalIgnoreCase))
                    da.SelectCommand.Parameters.AddWithValue("@role", roleFilter);

                var dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow r in dt.Rows)
                {
                    dgvUsers.Rows.Add(
                        r["UserId"],
                        r["FullName"],
                        r["Email"],
                        r["Phone"],
                        r["RoleName"] is DBNull ? "Unknown" : Convert.ToString(r["RoleName"]),
                        r["StatusText"],
                        r["CreatedAt"] == DBNull.Value
                            ? ""
                            : Convert.ToDateTime(r["CreatedAt"]).ToString("yyyy-MM-dd HH:mm")
                    );
                }
            }
        }

        private void SafeReload()
        {
            try { LoadGridFromDb(); }
            catch (Exception ex) { MessageBox.Show("Reload error: " + ex.Message, "CivicLens"); }
        }

        private void btnSearch_Click(object sender, EventArgs e) => SafeReload();

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            cmbRole.SelectedItem = "Any";
            cmbStatus.SelectedItem = "Any";
            SafeReload();
        }

        private void btnClose_Click(object sender, EventArgs e) => Close();

        private List<int> GetSelectedIds()
        {
            var ids = new List<int>();
            foreach (DataGridViewRow r in dgvUsers.SelectedRows)
                if (r.Cells["colUserId"].Value != null &&
                    int.TryParse(r.Cells["colUserId"].Value.ToString(), out int id))
                    ids.Add(id);
            return ids;
        }

        private void BulkUpdateActive(List<int> ids, bool active)
        {
            if (!_hasIsActive)
            {
                MessageBox.Show("The Users table does not have an IsActive column.", "CivicLens");
                return;
            }
            try
            {
                if (con.State != ConnectionState.Open) con.Open();
                foreach (int id in ids)
                    using (var cmd = new SqlCommand("UPDATE Users SET IsActive=@val WHERE UserId=@id", con))
                    {
                        cmd.Parameters.AddWithValue("@val", active ? 1 : 0);
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                    }
                con.Close();
                MessageBox.Show($"Selected users {(active ? "activated" : "deactivated")} successfully.", "CivicLens");
                SafeReload();
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open) con.Close();
                MessageBox.Show("Update failed: " + ex.Message, "CivicLens");
            }
        }

        private void btnBulkActivate_Click(object sender, EventArgs e)
        {
            var ids = GetSelectedIds();
            if (ids.Count == 0) { MessageBox.Show("Select users to activate."); return; }
            BulkUpdateActive(ids, true);
        }

        private void btnBulkDeactivate_Click(object sender, EventArgs e)
        {
            var ids = GetSelectedIds();
            if (ids.Count == 0) { MessageBox.Show("Select users to deactivate."); return; }
            BulkUpdateActive(ids, false);
        }

        private void btnBulkDelete_Click(object sender, EventArgs e)
        {
            var ids = GetSelectedIds();
            if (ids.Count == 0) { MessageBox.Show("Select users to delete."); return; }

            if (MessageBox.Show($"Delete {ids.Count} user(s)? This will also remove all their complaints and data.",
                "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            try
            {
                if (con.State != ConnectionState.Open) con.Open();
                foreach (var id in ids)
                    DeleteUserById(con, id);
                con.Close();
                MessageBox.Show("Selected user(s) deleted.", "CivicLens");
                SafeReload();
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open) con.Close();
                MessageBox.Show("Delete error: " + ex.Message, "CivicLens");
            }
        }

        private void dgvUsers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var col = dgvUsers.Columns[e.ColumnIndex].Name;
            int id = Convert.ToInt32(dgvUsers.Rows[e.RowIndex].Cells["colUserId"].Value);
            string name = Convert.ToString(dgvUsers.Rows[e.RowIndex].Cells["colFullName"].Value);
            string status = Convert.ToString(dgvUsers.Rows[e.RowIndex].Cells["colStatusCol"].Value);

            if (col == "colView")
            {
                using (var f = new ViewProfileForm(id, name))
                    f.ShowDialog(this);
            }
            else if (col == "colToggleActive")
            {
                ToggleSingle(id, status);
            }
            else if (col == "colDelete")
            {
                if (MessageBox.Show($"Delete user '{name}'? This will also remove all their complaints and data.",
                        "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                    return;

                try
                {
                    if (con.State != ConnectionState.Open) con.Open();
                    DeleteUserById(con, id);
                    con.Close();
                    SafeReload();
                }
                catch (Exception ex)
                {
                    if (con.State == ConnectionState.Open) con.Close();
                    MessageBox.Show("Delete failed: " + ex.Message, "CivicLens");
                }
            }
            else if (col == "colChangeRole")
            {
                ChangeRole(id);
            }
        }

        private void ToggleSingle(int id, string currentStatus)
        {
            if (!_hasIsActive) { MessageBox.Show("The Users table does not have an IsActive column."); return; }

            bool active = string.Equals(currentStatus, "Active", StringComparison.OrdinalIgnoreCase);
            try
            {
                using (var cmd = new SqlCommand("UPDATE Users SET IsActive=@val WHERE UserId=@id", con))
                {
                    cmd.Parameters.AddWithValue("@val", active ? 0 : 1);
                    cmd.Parameters.AddWithValue("@id", id);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                SafeReload();
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open) con.Close();
                MessageBox.Show("Toggle failed: " + ex.Message);
            }
        }

        private void ChangeRole(int userId)
        {
            var roles = new List<string>();
            try
            {
                using (var da = new SqlDataAdapter("SELECT RoleName FROM Roles ORDER BY RoleName", con))
                {
                    var dt = new DataTable();
                    da.Fill(dt);
                    foreach (DataRow r in dt.Rows) roles.Add(Convert.ToString(r["RoleName"]));
                }
            }
            catch { MessageBox.Show("Could not read roles."); return; }

            if (roles.Count == 0) { MessageBox.Show("No roles found."); return; }

            var f = new Form
            {
                Width = 320,
                Height = 150,
                Text = "Change Role",
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterParent,
                MaximizeBox = false,
                MinimizeBox = false
            };
            var lbl = new Label { Left = 12, Top = 12, Width = 280, Text = "Select new role:" };
            var cmb = new ComboBox { Left = 12, Top = 36, Width = 280, DropDownStyle = ComboBoxStyle.DropDownList };
            cmb.Items.AddRange(roles.ToArray());
            cmb.SelectedIndex = 0;
            var ok = new Button { Text = "OK", Left = 136, Top = 72, Width = 70, DialogResult = DialogResult.OK };
            var cancel = new Button { Text = "Cancel", Left = 222, Top = 72, Width = 70, DialogResult = DialogResult.Cancel };
            f.Controls.AddRange(new Control[] { lbl, cmb, ok, cancel });
            f.AcceptButton = ok; f.CancelButton = cancel;

            if (f.ShowDialog(this) != DialogResult.OK) return;
            string role = Convert.ToString(cmb.SelectedItem);

            try
            {
                int roleId = -1;
                using (var cmdRole = new SqlCommand("SELECT RoleId FROM Roles WHERE RoleName=@n", con))
                {
                    cmdRole.Parameters.AddWithValue("@n", role);
                    if (con.State != ConnectionState.Open) con.Open();
                    var v = cmdRole.ExecuteScalar();
                    con.Close();
                    if (v != null && v != DBNull.Value) roleId = Convert.ToInt32(v);
                }
                if (roleId < 0) { MessageBox.Show("Role not found."); return; }

                using (var cmd = new SqlCommand("UPDATE Users SET RoleId=@rid WHERE UserId=@id", con))
                {
                    cmd.Parameters.AddWithValue("@rid", roleId);
                    cmd.Parameters.AddWithValue("@id", userId);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                MessageBox.Show("Role updated.");
                SafeReload();
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open) con.Close();
                MessageBox.Show("Change role failed: " + ex.Message);
            }
        }
    }
}