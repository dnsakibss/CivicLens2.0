using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;

namespace CivicLens
{
    public partial class ModeratorQueueForm : Form
    {
        private readonly int _currentUserId; 
        
        SqlConnection con = new SqlConnection(
            "Data Source=LAPTOP-368QC6MP\\SQLEXPRESS;Initial Catalog=CivicLensDB;Integrated Security=True;");

        
        public ModeratorQueueForm(int currentUserId)
        {
            _currentUserId = currentUserId;
            InitializeComponent();
            this.Load += ModeratorQueueForm_Load;
        }

        private void ModeratorQueueForm_Load(object sender, EventArgs e)
        {
            try
            {
                if (_currentUserId <= 0)
                {
                    MessageBox.Show("Current moderator/admin userId is not set. Pass it when opening this form.",
                        "CivicLens", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                    return;
                }

                LoadFilterLists();
                LoadQueueFromDb();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Load error: " + ex.Message, "CivicLens");
            }
        }

        
        private void LoadFilterLists()
        {
            cmbStatus.Items.Clear();
            cmbStatus.Items.AddRange(new object[] { "Any", "New", "Pending", "Assigned", "InProgress", "OnHold", "Resolved", "Rejected", "Closed" });
            cmbStatus.SelectedItem = "Any";

            cmbCategory.Items.Clear();
            cmbCategory.Items.Add("Any");
            using (var da = new SqlDataAdapter("SELECT CategoryName FROM Categories ORDER BY CategoryName", con))
            {
                var dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow r in dt.Rows) cmbCategory.Items.Add(r["CategoryName"]?.ToString());
            }
            if (cmbCategory.Items.Count == 0) cmbCategory.Items.Add("Any");
            cmbCategory.SelectedItem = "Any";
        }

        private void LoadQueueFromDb()
        {
            dgvQueue.Rows.Clear();

            string q = (txtSearch.Text ?? "").Trim();
            string cat = (cmbCategory.SelectedItem?.ToString() ?? "Any").Trim();
            string status = (cmbStatus.SelectedItem?.ToString() ?? "Any").Trim();
            bool onlyUnassigned = chkOnlyUnassigned.Checked;

            var where = new StringBuilder("WHERE 1=1 ");

            if (!string.IsNullOrWhiteSpace(q))
            {
                string eq = Escape(q);
                where.Append("AND (c.Title LIKE '%" + eq + "%' OR c.Status LIKE '%" + eq + "%') ");
            }
            if (!string.Equals(cat, "Any", StringComparison.OrdinalIgnoreCase))
                where.Append("AND cat.CategoryName = '" + Escape(cat) + "' ");
            if (!string.Equals(status, "Any", StringComparison.OrdinalIgnoreCase))
                where.Append("AND c.Status = '" + Escape(status) + "' ");

          
            if (onlyUnassigned)
                where.Append("AND c.Status IN ('New','Pending') ");

            string sql =
                "SELECT c.ComplaintId, c.Title, c.Priority, c.Status, c.CreatedAt, " +
                "       cat.CategoryName, u.FullName AS Reporter, " +
                "       ISNULL(l.District,'') + '/' + ISNULL(l.City,'') + '/' + ISNULL(l.Area,'') AS LocationStr " +
                "FROM Complaints c " +
                "JOIN Categories cat ON cat.CategoryId = c.CategoryId " +
                "JOIN Users u ON u.UserId = c.CreatedByUserId " +
                "LEFT JOIN Locations l ON l.LocationId = c.LocationId " +
                where.ToString() +
                "ORDER BY c.CreatedAt DESC;";

            var da = new SqlDataAdapter(sql, con);
            var dt = new DataTable();
            da.Fill(dt);

            foreach (DataRow r in dt.Rows)
            {
                dgvQueue.Rows.Add(
                    r["ComplaintId"],
                    r["Title"],
                    r["CategoryName"],
                    r["Priority"],
                    r["Status"],
                    Convert.ToDateTime(r["CreatedAt"]).ToString("yyyy-MM-dd HH:mm"),
                    r["Reporter"],
                    r["LocationStr"]
                );
            }
        }

        private void btnSearch_Click(object sender, EventArgs e) => SafeReload();

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            cmbStatus.SelectedItem = "Any";
            cmbCategory.SelectedItem = "Any";
            chkOnlyUnassigned.Checked = false;
            SafeReload();
        }

        private void btnAssignSelected_Click(object sender, EventArgs e)
        {
            if (dgvQueue.CurrentRow == null)
            {
                MessageBox.Show("Select a complaint to assign.", "CivicLens");
                return;
            }
            OpenAssignDialogFromRow(dgvQueue.CurrentRow);
        }

        private void btnClose_Click(object sender, EventArgs e) => Close();

        private void dgvQueue_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dgvQueue.Rows[e.RowIndex];
            string colName = dgvQueue.Columns[e.ColumnIndex].Name;

            int id = Convert.ToInt32(row.Cells["colId"].Value);
            string title = row.Cells["colTitle"].Value?.ToString();
            string category = row.Cells["colCategory"].Value?.ToString();
            string priority = row.Cells["colPriority"].Value?.ToString();
            string status = row.Cells["colStatus"].Value?.ToString();
            string createdAt = row.Cells["colCreatedAt"].Value?.ToString();
            string reporter = row.Cells["colReporter"].Value?.ToString();
            string location = row.Cells["colLocation"].Value?.ToString();

            if (colName == "colView")
            {
                using (var f = new ComplaintDetailForm(id, editable: false, currentUserId: _currentUserId))
                    f.ShowDialog(this);
            }
            else if (colName == "colAssign")
            {
                OpenAssignDialog(id, title, category, priority, status, createdAt, reporter, location);
            }
        }

        private void OpenAssignDialogFromRow(DataGridViewRow row)
        {
            int id = Convert.ToInt32(row.Cells["colId"].Value);
            string title = row.Cells["colTitle"].Value?.ToString();
            string category = row.Cells["colCategory"].Value?.ToString();
            string priority = row.Cells["colPriority"].Value?.ToString();
            string status = row.Cells["colStatus"].Value?.ToString();
            string createdAt = row.Cells["colCreatedAt"].Value?.ToString();
            string reporter = row.Cells["colReporter"].Value?.ToString();
            string location = row.Cells["colLocation"].Value?.ToString();

            OpenAssignDialog(id, title, category, priority, status, createdAt, reporter, location);
        }

        private void OpenAssignDialog(
            int complaintId,
            string title = null,
            string category = null,
            string priority = null,
            string status = null,
            string createdAt = null,
            string reporter = null,
            string location = null)
        {
            if (_currentUserId <= 0)
            {
                MessageBox.Show("Current moderator/admin userId is not set. Pass it when opening this form.",
                    "CivicLens", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (var f = new AssignComplaintForm(
                complaintId,
                _currentUserId,     
                title, category, priority, status, createdAt, reporter, location))
            {
                if (f.ShowDialog(this) == DialogResult.OK)
                    LoadQueueFromDb();
            }
        }

        private void SafeReload()
        {
            try { LoadQueueFromDb(); }
            catch (Exception ex) { MessageBox.Show("Reload error: " + ex.Message, "CivicLens"); }
        }

        private static string Escape(string s) => s?.Replace("'", "''") ?? "";
    }
}
