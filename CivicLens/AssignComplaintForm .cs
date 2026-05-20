using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CivicLens
{
    public partial class AssignComplaintForm : Form
    {
        private readonly int _complaintId;
        private readonly int _currentModeratorUserId; 

        
        private readonly SqlConnection _con = new SqlConnection(
            "Data Source=LAPTOP-368QC6MP\\SQLEXPRESS;Initial Catalog=CivicLensDB;Integrated Security=True;");

        public AssignComplaintForm(
            int complaintId = 0,
            int currentModeratorUserId = 0,
            string title = null,
            string category = null,
            string priority = null,
            string status = null,
            string createdAt = null,
            string reporter = null,
            string location = null)
        {
            _complaintId = complaintId;
            _currentModeratorUserId = currentModeratorUserId;

            InitializeComponent();

            lblIdValue.Text = complaintId > 0 ? $"#{complaintId}" : "#?";
            txtTitle.Text = title ?? "";
            txtCategory.Text = category ?? "";
            txtPriority.Text = priority ?? "";
            txtStatus.Text = status ?? "New";
            txtCreatedAt.Text = createdAt ?? DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm");
            txtReporter.Text = reporter ?? "Citizen";
            txtLocation.Text = location ?? "";

        
            cmbRole.SelectedIndexChanged += (s, e) => LoadCandidatesForSelectedRole();

            this.Load += AssignComplaintForm_Load;
        }

        private void AssignComplaintForm_Load(object sender, EventArgs e)
        {
            if (_complaintId <= 0)
            {
                MessageBox.Show("Invalid ComplaintId.", "CivicLens", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
                return;
            }
            if (_currentModeratorUserId <= 0)
            {
                MessageBox.Show("Current moderator/admin userId is not set. Pass it when opening this form.",
                                "CivicLens", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
                return;
            }

            // Roles available for assignment
            cmbRole.Items.Clear();
            cmbRole.Items.AddRange(new object[] { "Police", "Journalist" });
            cmbRole.SelectedIndex = 0; // triggers candidates load
        }

        private void btnRefreshCandidates_Click(object sender, EventArgs e)
        {
            LoadCandidatesForSelectedRole();
            MessageBox.Show("Candidates refreshed.", "CivicLens");
        }

        private void LoadCandidatesForSelectedRole()
        {
            try
            {
                var role = (cmbRole.SelectedItem?.ToString() ?? "Police").Trim();

                string sql =
                    "SELECT u.UserId, u.FullName " +
                    "FROM Users u " +
                    "JOIN Roles r ON r.RoleId = u.RoleId " +
                    "WHERE r.RoleName = @role AND u.IsActive = 1 AND u.ApprovalStatus = 'Approved' " +
                    "ORDER BY u.FullName;";

                var da = new SqlDataAdapter(sql, _con);
                da.SelectCommand.Parameters.AddWithValue("@role", role);
                var dt = new DataTable();
                da.Fill(dt);

                cmbAssignee.DataSource = null;
                cmbAssignee.DisplayMember = "FullName";
                cmbAssignee.ValueMember = "UserId";
                cmbAssignee.DataSource = dt;

                if (cmbAssignee.Items.Count > 0)
                    cmbAssignee.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Load candidates failed: " + ex.Message, "CivicLens");
            }
        }

        private void btnAssign_Click(object sender, EventArgs e)
        {
            if (cmbRole.SelectedItem == null)
            {
                MessageBox.Show("Please select a role.");
                return;
            }
            if (cmbAssignee.SelectedValue == null || !int.TryParse(cmbAssignee.SelectedValue.ToString(), out int assigneeId) || assigneeId <= 0)
            {
                MessageBox.Show("Please select a valid assignee.");
                return;
            }

            string note = (txtNote.Text ?? "").Trim();
            string role = cmbRole.SelectedItem.ToString();

            try
            {
                _con.Open();
                using (var tx = _con.BeginTransaction())
                {
                    
                    if (!UserExists(_currentModeratorUserId, tx))
                        throw new Exception("Current moderator/admin user not found in Users table.");
                    if (!UserExists(assigneeId, tx))
                        throw new Exception("Selected assignee not found in Users table.");

                   
                    using (var cmdDeactivate = new SqlCommand(
                        "UPDATE Assignments SET IsActive = 0 WHERE ComplaintId = @cid AND IsActive = 1;", _con, tx))
                    {
                        cmdDeactivate.Parameters.AddWithValue("@cid", _complaintId);
                        cmdDeactivate.ExecuteNonQuery();
                    }

             
                    using (var cmdIns = new SqlCommand(
                        "INSERT INTO Assignments(ComplaintId, AssigneeUserId, AssignedByUserId, Note, IsActive) " +
                        "VALUES (@cid, @assignee, @by, @note, 1);", _con, tx))
                    {
                        cmdIns.Parameters.AddWithValue("@cid", _complaintId);
                        cmdIns.Parameters.AddWithValue("@assignee", assigneeId);
                        cmdIns.Parameters.AddWithValue("@by", _currentModeratorUserId);
                        cmdIns.Parameters.AddWithValue("@note", (object)note ?? DBNull.Value);
                        cmdIns.ExecuteNonQuery();
                    }

                    
                    using (var cmdUpd = new SqlCommand(
                        "UPDATE Complaints SET Status = 'Assigned' WHERE ComplaintId = @cid AND Status IN ('New','Pending');",
                        _con, tx))
                    {
                        cmdUpd.Parameters.AddWithValue("@cid", _complaintId);
                        cmdUpd.ExecuteNonQuery();
                    }

                    // 4) Log status history
                    using (var cmdHist = new SqlCommand(
                        "INSERT INTO StatusHistory(ComplaintId, OldStatus, NewStatus, Note, ChangedByUserId) " +
                        "VALUES (@cid, NULL, 'Assigned', @hnote, @by);", _con, tx))
                    {
                        cmdHist.Parameters.AddWithValue("@cid", _complaintId);
                        cmdHist.Parameters.AddWithValue("@hnote",
                            string.IsNullOrWhiteSpace(note) ? (object)$"Assigned to {role} (UserId={assigneeId})." : note);
                        cmdHist.Parameters.AddWithValue("@by", _currentModeratorUserId);
                        cmdHist.ExecuteNonQuery();
                    }

                    tx.Commit();
                }

                MessageBox.Show("Complaint assigned successfully.", "CivicLens",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Assign failed: " + ex.Message, "CivicLens");
            }
            finally
            {
                if (_con.State == ConnectionState.Open) _con.Close();
            }
        }

        private bool UserExists(int userId, SqlTransaction tx)
        {
            using (var cmd = new SqlCommand("SELECT COUNT(1) FROM Users WHERE UserId=@id;", _con, tx))
            {
                cmd.Parameters.AddWithValue("@id", userId);
                var n = Convert.ToInt32(cmd.ExecuteScalar());
                return n > 0;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e) => Close();
    }
}
