using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace CivicLens
{
    public partial class AdminUserApprovalsForm : Form
    {
        // ⬇️ Change instance if needed
        SqlConnection con = new SqlConnection(
            "Data Source=LAPTOP-368QC6MP\\SQLEXPRESS;Initial Catalog=CivicLensDB;Integrated Security=True;");

        private int adminUserId = 1; // fallback (better: pass the real admin id from Dashboard)

        // Default constructor (kept for compatibility)
        public AdminUserApprovalsForm()
        {
            InitializeComponent();
            this.Load += AdminUserApprovalsForm_Load;

            // wire events that were commented in designer
            this.btnSearch.Click += btnSearch_Click;
            this.btnApproveSelected.Click += btnApproveSelected_Click;
            this.btnRejectSelected.Click += btnRejectSelected_Click;
        }

        // Preferred constructor — pass in the current admin user id
        public AdminUserApprovalsForm(int currentAdminUserId) : this()
        {
            if (currentAdminUserId > 0) adminUserId = currentAdminUserId;
        }

        private void AdminUserApprovalsForm_Load(object sender, EventArgs e)
        {
            LoadRoleFilter();
            LoadGridFromDb(); // initial load
        }

        private void LoadRoleFilter()
        {
            try
            {
                // Load roles from DB (add "Any" first)
                cmbRole.Items.Clear();
                cmbRole.Items.Add("Any");

                string q = "SELECT RoleName FROM Roles ORDER BY RoleName";
                SqlDataAdapter da = new SqlDataAdapter(q, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow r in dt.Rows)
                    cmbRole.Items.Add(r["RoleName"].ToString());

                // Default: show only non-admin pending users; but admin may filter to Admin if needed
                cmbRole.SelectedItem = "Any";
            }
            catch
            {
                // fallback
                if (cmbRole.Items.Count == 0)
                    cmbRole.Items.AddRange(new object[] { "Any", "Admin", "Moderator", "Police", "Journalist", "Citizen" });
                cmbRole.SelectedItem = "Any";
            }
        }

        // ===================== Grid Loading =====================
        private void LoadGridFromDb()
        {
            dgvPending.Rows.Clear();

            string search = (txtSearch.Text ?? "").Trim();
            string role = (cmbRole.SelectedItem == null ? "Any" : cmbRole.SelectedItem.ToString());

            // base query
            string q =
                "SELECT u.UserId, u.FullName, u.Email, u.Phone, r.RoleName, u.CreatedAt " +
                "FROM Users u " +
                "JOIN Roles r ON r.RoleId = u.RoleId " +
                "WHERE u.ApprovalStatus = 'Pending' AND ISNULL(u.IsDeleted,0) = 0 ";

            // role filter
            if (!string.Equals(role, "Any", StringComparison.OrdinalIgnoreCase))
            {
                q += "AND r.RoleName = '" + Escape(role) + "' ";
            }

            // search filter (name/email/phone)
            if (!string.IsNullOrWhiteSpace(search))
            {
                string s = Escape(search);
                q += "AND (u.FullName LIKE '%" + s + "%' OR u.Email LIKE '%" + s + "%' OR u.Phone LIKE '%" + s + "%') ";
            }

            q += "ORDER BY u.CreatedAt ASC";

            SqlDataAdapter da = new SqlDataAdapter(q, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            foreach (DataRow r in dt.Rows)
            {
                dgvPending.Rows.Add(
                    r["UserId"],
                    r["FullName"],
                    r["Email"],
                    r["Phone"],
                    r["RoleName"],
                    Convert.ToDateTime(r["CreatedAt"]).ToString("yyyy-MM-dd HH:mm")
                );
            }
        }

        // ===================== Buttons =====================
        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadGridFromDb();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            cmbRole.SelectedItem = "Any";
            LoadGridFromDb();
        }

        private void btnApproveSelected_Click(object sender, EventArgs e)
        {
            var ids = GetSelectedUserIds();
            if (ids.Count == 0) { MessageBox.Show("Select one or more rows to approve."); return; }

            int ok = 0, fail = 0;
            foreach (var id in ids)
            {
                if (ApproveUser(id)) ok++; else fail++;
            }
            MessageBox.Show($"Approved: {ok}, Failed: {fail}");
            LoadGridFromDb();
        }

        private void btnRejectSelected_Click(object sender, EventArgs e)
        {
            var ids = GetSelectedUserIds();
            if (ids.Count == 0) { MessageBox.Show("Select one or more rows to reject."); return; }

            int ok = 0, fail = 0;
            foreach (var id in ids)
            {
                if (RejectUser(id)) ok++; else fail++;
            }
            MessageBox.Show($"Rejected: {ok}, Failed: {fail}");
            LoadGridFromDb();
        }

        private void btnClose_Click(object sender, EventArgs e) => Close();

        // ===================== Grid Button Clicks =====================
        private void dgvPending_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var colName = dgvPending.Columns[e.ColumnIndex].Name;
            int userId = Convert.ToInt32(dgvPending.Rows[e.RowIndex].Cells["colUserId"].Value);

            if (colName == "colApprove")
            {
                if (ApproveUser(userId))
                    MessageBox.Show($"Approved user #{userId}");
                else
                    MessageBox.Show($"Failed to approve user #{userId}");
                LoadGridFromDb();
            }
            else if (colName == "colReject")
            {
                if (RejectUser(userId))
                    MessageBox.Show($"Rejected user #{userId}");
                else
                    MessageBox.Show($"Failed to reject user #{userId}");
                LoadGridFromDb();
            }
        }

        // ===================== Actions =====================
        private bool ApproveUser(int userId)
        {
            try
            {
                string q =
                    "UPDATE Users SET " +
                    "  ApprovalStatus='Approved', " +
                    "  ApprovedAt = SYSUTCDATETIME(), " +
                    "  ApprovedByAdminId = " + adminUserId + " " +
                    "WHERE UserId = " + userId + " AND ApprovalStatus='Pending'";

                SqlCommand cmd = new SqlCommand(q, con);
                con.Open();
                int rows = cmd.ExecuteNonQuery();
                con.Close();

                return rows > 0;
            }
            catch (Exception ex)
            {
                if (con.State == System.Data.ConnectionState.Open) con.Close();
                MessageBox.Show("Approve error: " + ex.Message);
                return false;
            }
        }

        private bool RejectUser(int userId)
        {
            try
            {
                string q =
                    "UPDATE Users SET " +
                    "  ApprovalStatus='Rejected' " +
                    "WHERE UserId = " + userId + " AND ApprovalStatus='Pending'";

                SqlCommand cmd = new SqlCommand(q, con);
                con.Open();
                int rows = cmd.ExecuteNonQuery();
                con.Close();

                return rows > 0;
            }
            catch (Exception ex)
            {
                if (con.State == System.Data.ConnectionState.Open) con.Close();
                MessageBox.Show("Reject error: " + ex.Message);
                return false;
            }
        }

        // ===================== Helpers =====================
        private List<int> GetSelectedUserIds()
        {
            var list = new List<int>();
            foreach (DataGridViewRow row in dgvPending.SelectedRows)
            {
                if (row.Cells["colUserId"].Value != null &&
                    int.TryParse(row.Cells["colUserId"].Value.ToString(), out int id))
                {
                    list.Add(id);
                }
            }
            return list.Distinct().ToList();
        }

        private static string Escape(string s) => s?.Replace("'", "''") ?? "";
    }
}
