using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CivicLens
{
    public partial class MyComplaintsForm : Form
    {
        private readonly SqlConnection con =
            new SqlConnection("Data Source=LAPTOP-368QC6MP\\SQLEXPRESS;Initial Catalog=CivicLensDB;Integrated Security=True;");

        private readonly int _currentUserId;
        private const string Placeholder = "Search by title or status...";

        public MyComplaintsForm(int currentUserId = 0)
        {
            _currentUserId = currentUserId;
            InitializeComponent();
            this.Load += MyComplaintsForm_Load;
        }

        private void MyComplaintsForm_Load(object sender, EventArgs e)
        {
            if (_currentUserId <= 0)
            {
                MessageBox.Show("Missing user context.");
                Close();
                return;
            }

            
            if ((txtSearch.Text ?? "").Trim()
                .Equals(Placeholder, StringComparison.OrdinalIgnoreCase))
            {
                txtSearch.Clear();
            }

            LoadGrid("");  
        }

       
        private void txtSearch_GotFocus(object sender, EventArgs e)
        {
            if (txtSearch.Text == Placeholder) txtSearch.Clear();
        }

        private void txtSearch_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text)) txtSearch.Text = Placeholder;
        }

     
        private void btnSearch_Click(object sender, EventArgs e)
        {
            var filter = (txtSearch.Text ?? "").Trim();
            if (filter.Equals(Placeholder, StringComparison.OrdinalIgnoreCase)) filter = "";
            LoadGrid(filter);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            LoadGrid("");
        }

        private void btnClose_Click(object sender, EventArgs e) => Close();

        private void dgvComplaints_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            string colName = dgvComplaints.Columns[e.ColumnIndex].Name;
            if (colName != "colView" && colName != "colEdit") return;

            object idObj = dgvComplaints.Rows[e.RowIndex].Cells["colId"].Value;
            if (idObj == null) return;

            int complaintId = Convert.ToInt32(idObj);
            bool editable = (colName == "colEdit");

          
            using (var f = new ComplaintDetailForm(complaintId, editable, currentUserId: _currentUserId))
            {
                f.StartPosition = FormStartPosition.CenterParent;
                var result = f.ShowDialog(this);

                
                if (editable && result == DialogResult.OK)
                {
                    var currentFilter = (txtSearch.Text ?? "").Trim();
                    if (currentFilter.Equals(Placeholder, StringComparison.OrdinalIgnoreCase))
                        currentFilter = "";
                    LoadGrid(currentFilter);
                }
            }

        }

        
        private void LoadGrid(string filter)
        {
            try
            {
                dgvComplaints.Rows.Clear();

                string q = @"
                    SELECT c.ComplaintId, c.Title, cat.CategoryName, c.Status, c.CreatedAt
                    FROM Complaints c
                    JOIN Categories cat ON cat.CategoryId = c.CategoryId
                    WHERE c.CreatedByUserId = @uid
                      AND (@kw = '' OR c.Title LIKE @kwLike OR c.Status LIKE @kwLike OR cat.CategoryName LIKE @kwLike)
                    ORDER BY c.CreatedAt DESC;";

                using (var cmd = new SqlCommand(q, con))
                {
                    cmd.Parameters.AddWithValue("@uid", _currentUserId);
                    cmd.Parameters.AddWithValue("@kw", filter ?? "");
                    cmd.Parameters.AddWithValue("@kwLike", "%" + (filter ?? "") + "%");

                    var da = new SqlDataAdapter(cmd);
                    var dt = new DataTable();
                    da.Fill(dt);

                    foreach (DataRow r in dt.Rows)
                    {
                        dgvComplaints.Rows.Add(
                            Convert.ToInt32(r["ComplaintId"]),
                            r["Title"]?.ToString(),
                            r["CategoryName"]?.ToString(),
                            r["Status"]?.ToString(),
                            Convert.ToDateTime(r["CreatedAt"]).ToString("yyyy-MM-dd HH:mm")
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load complaints: " + ex.Message, "CivicLens");
            }
        }
    }
}