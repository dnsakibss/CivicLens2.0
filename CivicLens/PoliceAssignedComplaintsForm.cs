using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;

namespace CivicLens
{
    public partial class PoliceAssignedComplaintsForm : Form
    {
        private readonly SqlConnection con =
            new SqlConnection("Data Source=LAPTOP-368QC6MP\\SQLEXPRESS;Initial Catalog=CivicLensDB;Integrated Security=True;");

        private readonly int _currentUserId;

        public PoliceAssignedComplaintsForm(int currentUserId = 0)
        {
            _currentUserId = currentUserId;
            InitializeComponent();
            this.Load += PoliceAssignedComplaintsForm_Load;
        }

        private void PoliceAssignedComplaintsForm_Load(object sender, EventArgs e)
        {
            try
            {
                if (_currentUserId <= 0)
                {
                    MessageBox.Show("Current police userId is not set. Pass it when opening this form.",
                        "CivicLens", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                    return;
                }

                LoadFilterLists();
                LoadGridFromDb();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Load error: " + ex.Message, "CivicLens");
            }
        }

        private void LoadFilterLists()
        {
            cmbStatus.Items.Clear();
            cmbStatus.Items.AddRange(new object[]
            {
                "Any", "Assigned", "InProgress", "OnHold", "Resolved", "Rejected", "Closed"
            });
            cmbStatus.SelectedItem = "Any";
        }

        private void LoadGridFromDb()
        {
            dgvAssigned.Rows.Clear();

            string q = (txtSearch.Text ?? "").Trim();
            string status = (cmbStatus.SelectedItem?.ToString() ?? "Any").Trim();

            var where = new StringBuilder();
            where.Append("WHERE a.IsActive = 1 AND a.AssigneeUserId = " + _currentUserId + " ");

            if (!string.IsNullOrWhiteSpace(q))
            {
                string eq = Escape(q);
                where.Append("AND (c.Title LIKE '%" + eq + "%' OR c.Status LIKE '%" + eq + "%') ");
            }

            if (!string.Equals(status, "Any", StringComparison.OrdinalIgnoreCase))
                where.Append("AND c.Status = '" + Escape(status) + "' ");

            string sql =
                "SELECT c.ComplaintId, c.Title, cat.CategoryName, c.Priority, c.Status, c.CreatedAt, " +
                "       u.FullName AS Reporter, " +
                "       ISNULL(l.District,'') + '/' + ISNULL(l.City,'') + '/' + ISNULL(l.Area,'') AS LocationStr " +
                "FROM Assignments a " +
                "JOIN Complaints c ON c.ComplaintId = a.ComplaintId " +
                "JOIN Users u ON u.UserId = c.CreatedByUserId " +
                "JOIN Categories cat ON cat.CategoryId = c.CategoryId " +
                "LEFT JOIN Locations l ON l.LocationId = c.LocationId " +
                where.ToString() +
                "ORDER BY c.CreatedAt DESC;";

            using (var da = new SqlDataAdapter(sql, con))
            {
                var dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow r in dt.Rows)
                {
                    dgvAssigned.Rows.Add(
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
        }

        private void btnSearch_Click(object sender, EventArgs e) => SafeReload();

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            cmbStatus.SelectedItem = "Any";
            SafeReload();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dgvAssigned_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            string colName = dgvAssigned.Columns[e.ColumnIndex].Name;

            if (colName == "colView")
            {
                int id = Convert.ToInt32(dgvAssigned.Rows[e.RowIndex].Cells["colId"].Value);
                try
                {
                    using (var f = new ComplaintDetailForm(id, editable: false, currentUserId: _currentUserId))
                        f.ShowDialog(this);
                }
                catch
                {
                    using (var f = new ComplaintDetailForm(id))
                        f.ShowDialog(this);
                }
            }
            else if (colName == "colUpdate")
            {
                var row = dgvAssigned.Rows[e.RowIndex];

                int id = Convert.ToInt32(row.Cells["colId"].Value);
                string title = row.Cells["colTitle"].Value?.ToString() ?? "";
                string category = row.Cells["colCategory"].Value?.ToString() ?? "";
                string priority = row.Cells["colPriority"].Value?.ToString() ?? "";
                string status = row.Cells["colStatus"].Value?.ToString() ?? "Assigned";
                string reporter = row.Cells["colReporter"].Value?.ToString() ?? "";
                DateTime createdAt = DateTime.UtcNow;
                DateTime.TryParse(row.Cells["colCreatedAt"].Value?.ToString(), out createdAt);

                using (var f = new UpdateStatusForm(
                    complaintId: id,
                    changedByUserId: _currentUserId,
                    title: title,
                    category: category,
                    priority: priority,
                    reporter: reporter,
                    createdAt: createdAt,
                    currentStatus: status))
                {
                    if (f.ShowDialog(this) == DialogResult.OK)
                        LoadGridFromDb();
                }
            }
            else if (colName == "colChat")                                          // ← added
            {
                int id = Convert.ToInt32(dgvAssigned.Rows[e.RowIndex].Cells["colId"].Value);
                using (var f = new ChatForm(id, _currentUserId, "Police Officer", "Police"))
                    f.ShowDialog(this);
            }
        }

        private void SafeReload()
        {
            try { LoadGridFromDb(); }
            catch (Exception ex) { MessageBox.Show("Reload error: " + ex.Message, "CivicLens"); }
        }

        private static string Escape(string s) => s?.Replace("'", "''") ?? "";
    }
}