using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CivicLens
{
    public partial class UpdateStatusForm : Form
    {
       
        private readonly SqlConnection con =
            new SqlConnection("Data Source=LAPTOP-368QC6MP\\SQLEXPRESS;Initial Catalog=CivicLensDB;Integrated Security=True;");

        private readonly int _complaintId;
        private readonly int _changedByUserId;  
        private readonly string _currentStatus;

        public UpdateStatusForm(
            int complaintId = 0,
            int changedByUserId = 0,
            string title = null,
            string category = null,
            string priority = null,
            string reporter = null,
            DateTime? createdAt = null,
            string currentStatus = "Assigned")
        {
            _complaintId = complaintId;
            _changedByUserId = changedByUserId;
            _currentStatus = currentStatus ?? "Assigned";

            InitializeComponent();
            this.Load += UpdateStatusForm_Load;

          
            lblIdValue.Text = complaintId > 0 ? $"#{complaintId}" : "#?";
            txtTitle.Text = title ?? "";
            txtCategory.Text = category ?? "";
            txtPriority.Text = priority ?? "";
            txtReporter.Text = reporter ?? "";
            txtCreatedAt.Text = (createdAt ?? DateTime.UtcNow).ToString("yyyy-MM-dd HH:mm");
        }

        private void UpdateStatusForm_Load(object sender, EventArgs e)
        {
            txtCurrentStatus.Text = _currentStatus;

            cmbNewStatus.Items.Clear();
            cmbNewStatus.Items.AddRange(new object[]
            {
                "Assigned", "InProgress", "OnHold", "Resolved", "Rejected", "Closed"
            });

            if (_currentStatus.Equals("Assigned", StringComparison.OrdinalIgnoreCase))
                cmbNewStatus.SelectedItem = "InProgress";
            else
                cmbNewStatus.SelectedItem = _currentStatus;

            dtWhen.Value = DateTime.Now;

           
            if (_complaintId > 0 && string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                LoadSummaryFromDb();
            }
        }

        private void LoadSummaryFromDb()
        {
            try
            {
                string sql =
                    "SELECT c.Title, cat.CategoryName, c.Priority, u.FullName AS Reporter, c.CreatedAt, c.Status " +
                    "FROM Complaints c " +
                    "JOIN Categories cat ON cat.CategoryId = c.CategoryId " +
                    "JOIN Users u ON u.UserId = c.CreatedByUserId " +
                    "WHERE c.ComplaintId = " + _complaintId;

                using (var da = new SqlDataAdapter(sql, con))
                {
                    var dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("Invalid complaint.", "CivicLens", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    var r = dt.Rows[0];
                    txtTitle.Text = r["Title"].ToString();
                    txtCategory.Text = r["CategoryName"].ToString();
                    txtPriority.Text = r["Priority"].ToString();
                    txtReporter.Text = r["Reporter"].ToString();
                    txtCreatedAt.Text = Convert.ToDateTime(r["CreatedAt"]).ToString("yyyy-MM-dd HH:mm");
                    txtCurrentStatus.Text = r["Status"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Load summary failed: " + ex.Message, "CivicLens");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_complaintId <= 0)
            {
                MessageBox.Show("Invalid complaint.", "CivicLens", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cmbNewStatus.SelectedItem == null)
            {
                MessageBox.Show("Please choose the new status.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var newStatus = cmbNewStatus.SelectedItem.ToString();
            var when = dtWhen.Value;
            var note = txtNote.Text.Trim();

            try
            {
                
                string upd = "UPDATE Complaints SET Status='" + Escape(newStatus) + "' WHERE ComplaintId=" + _complaintId;
                using (var cmd = new SqlCommand(upd, con))
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

            
                string ins =
                    "INSERT INTO StatusHistory(ComplaintId, OldStatus, NewStatus, Note, ChangedByUserId, ChangedAt) " +
                    "VALUES (" + _complaintId + ", " +
                    (_currentStatus == null ? "NULL" : "'" + Escape(_currentStatus) + "'") + ", " +
                    "'" + Escape(newStatus) + "', " +
                    (string.IsNullOrWhiteSpace(note) ? "NULL" : "N'" + Escape(note) + "'") + ", " +
                    (_changedByUserId <= 0 ? "NULL" : _changedByUserId.ToString()) + ", " +
                    "'" + when.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                using (var cmd2 = new SqlCommand(ins, con))
                {
                    con.Open();
                    cmd2.ExecuteNonQuery();
                    con.Close();
                }

                MessageBox.Show("Status updated successfully.", "CivicLens",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open) con.Close();
                MessageBox.Show("Update failed: " + ex.Message, "CivicLens");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e) => this.Close();

        private static string Escape(string s) => s?.Replace("'", "''") ?? "";
    }
}
