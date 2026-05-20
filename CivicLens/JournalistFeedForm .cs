using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;

namespace CivicLens
{
    public partial class JournalistFeedForm : Form
    {
        private readonly SqlConnection _con = new SqlConnection(
            "Data Source=LAPTOP-368QC6MP\\SQLEXPRESS;Initial Catalog=CivicLensDB;Integrated Security=True;");

        private readonly int _currentUserId;

        public JournalistFeedForm(int currentUserId = 0)
        {
            _currentUserId = currentUserId;
            InitializeComponent();
            this.Load += JournalistFeedForm_Load;
        }

        private void JournalistFeedForm_Load(object sender, EventArgs e)
        {
            try
            {
                if (_currentUserId <= 0)
                {
                    MessageBox.Show("Current journalist userId is not set. Pass it when opening this form.",
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
                "Any", "Assigned", "InProgress", "OnHold", "Covered", "Rejected", "Closed", "Resolved"
            });
            cmbStatus.SelectedItem = "Any";
        }

        private void LoadGridFromDb()
        {
            dgvAssigned.Rows.Clear();

            var q = (txtSearch.Text ?? "").Trim();
            var status = (cmbStatus.SelectedItem?.ToString() ?? "Any").Trim();

            var sql = new StringBuilder(@"
            SELECT
                c.ComplaintId,
                c.Title,
                cat.CategoryName,
                c.Priority,
                c.Status,
                c.CreatedAt,
                u.FullName AS Reporter,
                ISNULL(l.District,'') + '/' + ISNULL(l.City,'') + '/' + ISNULL(l.Area,'') AS LocationStr
            FROM Assignments a
            JOIN Complaints c  ON c.ComplaintId = a.ComplaintId
            JOIN Users u       ON u.UserId = c.CreatedByUserId
            JOIN Categories cat ON cat.CategoryId = c.CategoryId
            LEFT JOIN Locations l ON l.LocationId = c.LocationId
            WHERE a.IsActive = 1
              AND a.AssigneeUserId = @uid
            ");

            if (!string.IsNullOrWhiteSpace(q))
            {
                sql.Append(@" AND (c.Title LIKE @q OR c.Status LIKE @q OR cat.CategoryName LIKE @q) ");
            }

            if (!string.Equals(status, "Any", StringComparison.OrdinalIgnoreCase))
            {
                sql.Append(@" AND c.Status = @status ");
            }

            sql.Append(" ORDER BY c.CreatedAt DESC; ");

            using (var da = new SqlDataAdapter(sql.ToString(), _con))
            {
                da.SelectCommand.Parameters.AddWithValue("@uid", _currentUserId);
                if (!string.IsNullOrWhiteSpace(q))
                    da.SelectCommand.Parameters.AddWithValue("@q", "%" + q + "%");
                if (!string.Equals(status, "Any", StringComparison.OrdinalIgnoreCase))
                    da.SelectCommand.Parameters.AddWithValue("@status", status);

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

        private void btnClose_Click(object sender, EventArgs e) => Close();

        private void dgvAssigned_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var colName = dgvAssigned.Columns[e.ColumnIndex].Name;
            var id = Convert.ToInt32(dgvAssigned.Rows[e.RowIndex].Cells["colId"].Value);

            if (colName == "colView")
            {
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
            else if (colName == "colCovered")
            {
                MarkCovered(id);
            }
            else if (colName == "colChat")
            {
                using (var f = new ChatForm(id, _currentUserId, "Journalist", "Journalist"))
                    f.ShowDialog(this);
            }
        }

        private void MarkCovered(int complaintId)
        {
            var ok = MessageBox.Show(
                "Mark this complaint as Covered? This will update the complaint status and add a status history entry.",
                "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (ok != DialogResult.Yes) return;

            string oldStatus = GetCurrentStatus(complaintId);
            if (string.IsNullOrEmpty(oldStatus)) oldStatus = null;

            string note = PromptNote("Mark as Covered", "Enter an optional note (press OK to continue):");

            using (var cmd = new SqlCommand(@"
            UPDATE Complaints
            SET Status = @new
            WHERE ComplaintId = @id;
            
            INSERT INTO StatusHistory
            (ComplaintId, OldStatus, NewStatus, Note, ChangedByUserId, ChangedAt)
            VALUES
            (@id, @old, @new, @note, @by, SYSUTCDATETIME());
            ", _con))
            {
                cmd.Parameters.AddWithValue("@id", complaintId);
                cmd.Parameters.AddWithValue("@old", (object)oldStatus ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@new", "Covered");
                cmd.Parameters.AddWithValue("@note", string.IsNullOrWhiteSpace(note) ? (object)DBNull.Value : note);
                cmd.Parameters.AddWithValue("@by", _currentUserId);

                try
                {
                    if (_con.State != ConnectionState.Open) _con.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Marked as Covered.", "CivicLens", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    SafeReload();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Update error: " + ex.Message, "CivicLens");
                }
                finally
                {
                    if (_con.State == ConnectionState.Open) _con.Close();
                }
            }
        }

        private string GetCurrentStatus(int complaintId)
        {
            using (var cmd = new SqlCommand("SELECT Status FROM Complaints WHERE ComplaintId=@id", _con))
            {
                cmd.Parameters.AddWithValue("@id", complaintId);
                try
                {
                    if (_con.State != ConnectionState.Open) _con.Open();
                    var o = cmd.ExecuteScalar();
                    return o?.ToString();
                }
                catch
                {
                    return null;
                }
                finally
                {
                    if (_con.State == ConnectionState.Open) _con.Close();
                }
            }
        }

        private void SafeReload()
        {
            try { LoadGridFromDb(); }
            catch (Exception ex) { MessageBox.Show("Reload error: " + ex.Message, "CivicLens"); }
        }

        private string PromptNote(string title, string label)
        {
            using (var f = new Form()
            {
                Width = 460,
                Height = 220,
                Text = title,
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false
            })
            {
                var lbl = new Label { Left = 16, Top = 14, Width = 420, Text = label };
                var txt = new TextBox { Left = 16, Top = 40, Width = 420, Height = 80, Multiline = true, ScrollBars = ScrollBars.Vertical };
                var ok = new Button { Text = "OK", Left = 270, Width = 80, Top = 132, DialogResult = DialogResult.OK };
                var cancel = new Button { Text = "Cancel", Left = 356, Width = 80, Top = 132, DialogResult = DialogResult.Cancel };
                f.Controls.Add(lbl); f.Controls.Add(txt); f.Controls.Add(ok); f.Controls.Add(cancel);
                f.AcceptButton = ok; f.CancelButton = cancel;

                return f.ShowDialog(this) == DialogResult.OK ? txt.Text.Trim() : null;
            }
        }
    }
}