using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace CivicLens
{
    public partial class ComplaintDetailForm : Form
    {
        SqlConnection con = new SqlConnection(
            "Data Source=LAPTOP-368QC6MP\\SQLEXPRESS;Initial Catalog=CivicLensDB;Integrated Security=True;");

        private readonly int _complaintId;
        private readonly bool _editable;
        private readonly int _currentUserId;

        public ComplaintDetailForm(int complaintId = 0, bool editable = false, int currentUserId = 0)
        {
            _complaintId = complaintId;
            _editable = editable;
            _currentUserId = currentUserId;
            InitializeComponent();
            this.Load += ComplaintDetailForm_Load;
        }

        private void ComplaintDetailForm_Load(object sender, EventArgs e)
        {
            if (_complaintId <= 0)
            {
                MessageBox.Show("Invalid complaint id.", "CivicLens");
                Close();
                return;
            }

            try
            {
                LoadDetails();
                LoadMedia();
                LoadTimeline();
                ApplyEditMode();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load complaint: " + ex.Message, "CivicLens");
            }
        }

      
        private void ApplyEditMode()
        {
           
            btnSave.Visible = _editable;
            btnSave.Enabled = _editable;

           
            txtTitle.ReadOnly = !_editable;
            txtDescription.ReadOnly = !_editable;
            txtPriority.ReadOnly = !_editable;

            
            txtCategory.ReadOnly = true;
            txtStatus.ReadOnly = true;
            txtCreatedAt.ReadOnly = true;
            txtDistrict.ReadOnly = true;
            txtCity.ReadOnly = true;
            txtArea.ReadOnly = true;
        }

   
        private void LoadDetails()
        {
            string q =
                "SELECT TOP 1 c.Title, cat.CategoryName, c.Priority, c.Status, c.CreatedAt, " +
                "       l.District, l.City, l.Area, c.Description " +
                "FROM Complaints c " +
                "JOIN Categories cat ON cat.CategoryId = c.CategoryId " +
                "LEFT JOIN Locations l ON l.LocationId = c.LocationId " +
                "WHERE c.ComplaintId = " + _complaintId;

            SqlDataAdapter da = new SqlDataAdapter(q, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count == 0) throw new Exception("Complaint not found.");

            var r = dt.Rows[0];
            txtTitle.Text = r["Title"]?.ToString();
            txtCategory.Text = r["CategoryName"]?.ToString();
            txtPriority.Text = r["Priority"]?.ToString();
            txtStatus.Text = r["Status"]?.ToString();
            txtCreatedAt.Text = Convert.ToDateTime(r["CreatedAt"]).ToString("yyyy-MM-dd HH:mm");
            txtDistrict.Text = r["District"]?.ToString();
            txtCity.Text = r["City"]?.ToString();
            txtArea.Text = r["Area"]?.ToString();
            txtDescription.Text = r["Description"]?.ToString();
        }

        private void LoadMedia()
        {
            lvMedia.Items.Clear();
            pbPreview.Image = null;

            string q =
                "SELECT FilePath, MediaType, IsPrimary " +
                "FROM ComplaintMedia " +
                "WHERE ComplaintId = " + _complaintId + " " +
                "ORDER BY SortOrder, MediaId";

            SqlDataAdapter da = new SqlDataAdapter(q, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            foreach (DataRow r in dt.Rows)
            {
                var li = new ListViewItem(r["FilePath"]?.ToString() ?? "");
                li.SubItems.Add(r["MediaType"]?.ToString() ?? "Image");
                li.SubItems.Add(r["IsPrimary"].ToString() == "True" ? "Yes" : "No");
                lvMedia.Items.Add(li);
            }

            var firstImage = lvMedia.Items.Cast<ListViewItem>()
                                 .FirstOrDefault(i => i.SubItems[1].Text.Equals("Image", StringComparison.OrdinalIgnoreCase));
            if (firstImage != null)
            {
                TryPreview(firstImage.SubItems[0].Text, isImage: true);
                firstImage.Selected = true;
            }
        }

        private void LoadTimeline()
        {
            lstTimeline.Items.Clear();

            bool hasHistory;
            using (var cmd = new SqlCommand(
                "SELECT COUNT(1) FROM sys.tables WHERE name='StatusHistory' AND type='U'", con))
            {
                con.Open();
                hasHistory = Convert.ToInt32(cmd.ExecuteScalar()) > 0;
                con.Close();
            }
            if (!hasHistory)
            {
                lstTimeline.Items.Add("No history available.");
                return;
            }

            string q =
                "SELECT h.OldStatus, h.NewStatus, h.Note, u.FullName, h.ChangedAt " +
                "FROM StatusHistory h " +
                "JOIN Users u ON u.UserId = h.ChangedByUserId " +
                "WHERE h.ComplaintId = " + _complaintId + " " +
                "ORDER BY h.ChangedAt ASC";

            SqlDataAdapter da = new SqlDataAdapter(q, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count == 0)
            {
                lstTimeline.Items.Add("No history recorded yet.");
                return;
            }

            foreach (DataRow r in dt.Rows)
            {
                string dtStr = (r["ChangedAt"] == DBNull.Value)
                                ? ""
                                : Convert.ToDateTime(r["ChangedAt"]).ToString("yyyy-MM-dd HH:mm");

                string oldS = r["OldStatus"] == DBNull.Value ? "(none)" : r["OldStatus"].ToString();
                string newS = r["NewStatus"]?.ToString() ?? "";
                string by = r["FullName"]?.ToString() ?? "System";
                string note = r["Note"] == DBNull.Value ? "" : r["Note"].ToString();

                lstTimeline.Items.Add($"[{dtStr}] {oldS} → {newS} by {by}" +
                                      (string.IsNullOrWhiteSpace(note) ? "" : $" — {note}"));
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string newTitle = (txtTitle.Text ?? "").Trim();
                string newDesc = (txtDescription.Text ?? "").Trim();
                string newPrio = (txtPriority.Text ?? "Normal").Trim();

                if (string.IsNullOrWhiteSpace(newTitle))
                {
                    MessageBox.Show("Title is required.", "CivicLens");
                    return;
                }

                string q =
                    "UPDATE Complaints " +
                    "SET Title = @t, Description = @d, Priority = @p " +
                    "WHERE ComplaintId = @id;";

                using (SqlCommand cmd = new SqlCommand(q, con))
                {
                    cmd.Parameters.AddWithValue("@t", newTitle);
                    cmd.Parameters.AddWithValue("@d", (object)newDesc ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@p", newPrio);
                    cmd.Parameters.AddWithValue("@id", _complaintId);

                    con.Open();
                    int rows = cmd.ExecuteNonQuery();
                    con.Close();

                    if (rows > 0)
                    {
                        MessageBox.Show("Saved successfully.", "CivicLens");
                        LoadDetails();
                        ApplyEditMode();
                        this.DialogResult = DialogResult.OK; 
                    }
                    else
                    {
                        MessageBox.Show("No changes were made.", "CivicLens");
                    }
                }
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open) con.Close();
                MessageBox.Show("Save failed: " + ex.Message, "CivicLens");
            }
        }

       
        private void lvMedia_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvMedia.SelectedItems.Count == 0)
            {
                pbPreview.Image = null;
                return;
            }

            var sel = lvMedia.SelectedItems[0];
            var path = sel.SubItems[0].Text;
            var type = sel.SubItems[1].Text;
            if (type.Equals("Image", StringComparison.OrdinalIgnoreCase))
                TryPreview(path, isImage: true);
            else
                pbPreview.Image = SystemIcons.Information.ToBitmap();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                LoadDetails();
                LoadMedia();
                LoadTimeline();
                ApplyEditMode();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Refresh failed: " + ex.Message, "CivicLens");
            }
        }

        private void btnClose_Click(object sender, EventArgs e) => Close();

       
        private void TryPreview(string path, bool isImage)
        {
            try
            {
                if (isImage && File.Exists(path))
                {
                    using (var bmpTemp = new Bitmap(path))
                        pbPreview.Image = new Bitmap(bmpTemp);
                }
                else
                {
                    pbPreview.Image = SystemIcons.Warning.ToBitmap();
                }
            }
            catch
            {
                pbPreview.Image = SystemIcons.Warning.ToBitmap();
            }
        }

        private static string Escape(string s) => s?.Replace("'", "''") ?? "";
        private void btnChat_Click(object sender, EventArgs e)
        {
            using (var f = new ChatForm(_complaintId, _currentUserId, "Me"))
                f.ShowDialog(this);
        }
    }
}
