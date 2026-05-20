using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace CivicLens
{
    public partial class SubmitComplaintForm : Form
    {
        private class MediaItem
        {
            public string FilePath { get; set; }
            public string MediaType { get; set; } 
            public bool IsPrimary { get; set; }
            public int SortOrder { get; set; }
        }

        
        SqlConnection con = new SqlConnection(
            "Data Source=LAPTOP-368QC6MP\\SQLEXPRESS;Initial Catalog=CivicLensDB;Integrated Security=True;");

        private readonly int _currentUserId; 
        private readonly List<MediaItem> _media = new List<MediaItem>();
        private bool _isSubmitting = false;   

        public SubmitComplaintForm(int currentUserId = 0)
        {
            _currentUserId = currentUserId;
            InitializeComponent();

           

            this.Load += SubmitComplaintForm_Load;

       
            this.cmbDistrict.SelectedIndexChanged += (s, e) => LoadCities();
            this.cmbCity.SelectedIndexChanged += (s, e) => LoadAreas();
        }

        
        private void SubmitComplaintForm_Load(object sender, EventArgs e)
        {
            try
            {
                LoadCategories();
                LoadDistricts();
                LoadPriority();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Load error: " + ex.Message);
            }
        }

        private void LoadCategories()
        {
            string q = "SELECT CategoryId, CategoryName FROM Categories ORDER BY CategoryName";
            SqlDataAdapter da = new SqlDataAdapter(q, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            cmbCategory.DataSource = dt;
            cmbCategory.DisplayMember = "CategoryName";
            cmbCategory.ValueMember = "CategoryId";
            if (cmbCategory.Items.Count > 0) cmbCategory.SelectedIndex = 0;
        }

        private void LoadDistricts()
        {
            string q = "SELECT DISTINCT District FROM Locations ORDER BY District";
            SqlDataAdapter da = new SqlDataAdapter(q, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            cmbDistrict.Items.Clear();
            foreach (DataRow r in dt.Rows)
            {
                var d = r["District"]?.ToString();
                if (!string.IsNullOrWhiteSpace(d)) cmbDistrict.Items.Add(d);
            }
            if (cmbDistrict.Items.Count == 0)
                cmbDistrict.Items.AddRange(new object[] { "Dhaka", "Chattogram", "Khulna" });

            cmbDistrict.SelectedIndex = 0;
            LoadCities();
        }

        private void LoadCities()
        {
            string district = cmbDistrict.SelectedItem?.ToString() ?? "";
            cmbCity.Items.Clear();

            if (!string.IsNullOrWhiteSpace(district))
            {
                string q = "SELECT DISTINCT City FROM Locations WHERE District='" + Escape(district) + "' ORDER BY City";
                SqlDataAdapter da = new SqlDataAdapter(q, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow r in dt.Rows)
                {
                    var c = r["City"]?.ToString();
                    if (!string.IsNullOrWhiteSpace(c)) cmbCity.Items.Add(c);
                }
            }
            if (cmbCity.Items.Count == 0)
                cmbCity.Items.AddRange(new object[] { "Dhaka", "Chattogram", "Khulna" });

            cmbCity.SelectedIndex = 0;
            LoadAreas();
        }

        private void LoadAreas()
        {
            string district = cmbDistrict.SelectedItem?.ToString() ?? "";
            string city = cmbCity.SelectedItem?.ToString() ?? "";
            cmbArea.Items.Clear();

            if (!string.IsNullOrWhiteSpace(district) && !string.IsNullOrWhiteSpace(city))
            {
                string q = "SELECT DISTINCT Area FROM Locations " +
                           "WHERE District='" + Escape(district) + "' AND City='" + Escape(city) + "' ORDER BY Area";
                SqlDataAdapter da = new SqlDataAdapter(q, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow r in dt.Rows)
                {
                    var a = r["Area"]?.ToString();
                    if (!string.IsNullOrWhiteSpace(a)) cmbArea.Items.Add(a);
                }
            }
            if (cmbArea.Items.Count == 0)
                cmbArea.Items.AddRange(new object[] { "Dhanmondi", "Pahartali", "Sonadanga" });

            cmbArea.SelectedIndex = 0;
        }

        private void LoadPriority()
        {
            cmbPriority.Items.Clear();
            cmbPriority.Items.AddRange(new object[] { "Low", "Normal", "High" });
            cmbPriority.SelectedItem = "Normal";
        }

       
        private void btnAddMedia_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.Multiselect = true;
                ofd.Filter = "Images & Videos|*.jpg;*.jpeg;*.png;*.bmp;*.gif;*.mp4;*.mov;*.avi;*.mkv";
                ofd.Title = "Select media files";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    foreach (var file in ofd.FileNames)
                    {
                        var ext = Path.GetExtension(file).ToLowerInvariant();
                        var type = IsVideoExt(ext) ? "Video" : "Image";
                        _media.Add(new MediaItem
                        {
                            FilePath = file,
                            MediaType = type,
                            IsPrimary = _media.Count == 0,
                            SortOrder = _media.Count
                        });
                    }
                    RefreshMediaUI();
                }
            }
        }

        private void btnRemoveMedia_Click(object sender, EventArgs e)
        {
            if (lvMedia.SelectedItems.Count == 0)
            {
                MessageBox.Show("Select a media item to remove.");
                return;
            }
            var index = lvMedia.SelectedItems[0].Index;
            _media.RemoveAt(index);
            for (int i = 0; i < _media.Count; i++) _media[i].SortOrder = i;
            if (_media.Count > 0 && !_media.Any(m => m.IsPrimary)) _media[0].IsPrimary = true;
            RefreshMediaUI();
        }

        private void btnSetPrimary_Click(object sender, EventArgs e)
        {
            if (lvMedia.SelectedItems.Count == 0)
            {
                MessageBox.Show("Select a media item to set as primary.");
                return;
            }
            var index = lvMedia.SelectedItems[0].Index;
            for (int i = 0; i < _media.Count; i++) _media[i].IsPrimary = (i == index);
            RefreshMediaUI();
        }

        private void btnMoveUp_Click(object sender, EventArgs e)
        {
            if (lvMedia.SelectedItems.Count == 0) return;
            var index = lvMedia.SelectedItems[0].Index;
            if (index <= 0) return;

            var item = _media[index];
            _media.RemoveAt(index);
            _media.Insert(index - 1, item);
            for (int i = 0; i < _media.Count; i++) _media[i].SortOrder = i;

            RefreshMediaUI();
            lvMedia.Items[index - 1].Selected = true;
        }

        private void btnMoveDown_Click(object sender, EventArgs e)
        {
            if (lvMedia.SelectedItems.Count == 0) return;
            var index = lvMedia.SelectedItems[0].Index;
            if (index >= _media.Count - 1) return;

            var item = _media[index];
            _media.RemoveAt(index);
            _media.Insert(index + 1, item);
            for (int i = 0; i < _media.Count; i++) _media[i].SortOrder = i;

            RefreshMediaUI();
            lvMedia.Items[index + 1].Selected = true;
        }

        private void RefreshMediaUI()
        {
            lvMedia.BeginUpdate();
            lvMedia.Items.Clear();
            foreach (var m in _media.OrderBy(x => x.SortOrder))
            {
                var li = new ListViewItem(m.FilePath);
                li.SubItems.Add(m.MediaType);
                li.SubItems.Add(m.IsPrimary ? "Yes" : "No");
                li.SubItems.Add(m.SortOrder.ToString());
                lvMedia.Items.Add(li);
            }
            lvMedia.EndUpdate();
        }

        private static bool IsVideoExt(string ext)
        {
            switch (ext)
            {
                case ".mp4":
                case ".mov":
                case ".avi":
                case ".mkv":
                    return true;
                default:
                    return false;
            }
        }

       
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (_isSubmitting) return;     
            _isSubmitting = true;
            btnSubmit.Enabled = false;

            var title = (txtTitle.Text ?? "").Trim();
            var desc = (txtDescription.Text ?? "").Trim();
            var priority = (cmbPriority.SelectedItem?.ToString() ?? "Normal").Trim();

            if (string.IsNullOrWhiteSpace(title))
            {
                MessageBox.Show("Title is required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTitle.Focus();
                _isSubmitting = false; btnSubmit.Enabled = true; return;
            }
            if (cmbCategory.SelectedValue == null)
            {
                MessageBox.Show("Please select a category.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbCategory.Focus();
                _isSubmitting = false; btnSubmit.Enabled = true; return;
            }

            int categoryId = Convert.ToInt32(cmbCategory.SelectedValue);
            string district = cmbDistrict.SelectedItem?.ToString() ?? "";
            string city = cmbCity.SelectedItem?.ToString() ?? "";
            string area = cmbArea.SelectedItem?.ToString() ?? "";

            if (string.IsNullOrWhiteSpace(district) || string.IsNullOrWhiteSpace(city) || string.IsNullOrWhiteSpace(area))
            {
                MessageBox.Show("Please choose District, City and Area.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _isSubmitting = false; btnSubmit.Enabled = true; return;
            }

            SqlTransaction tx = null;

            try
            {
                int locationId = GetOrCreateLocationId(district, city, area);

                con.Open();
                tx = con.BeginTransaction();

               
                string insertComplaint =
                    "INSERT INTO Complaints(CreatedByUserId, Title, Description, CategoryId, LocationId, Priority, Status) " +
                    "VALUES (" + _currentUserId + ", '" + Escape(title) + "', " +
                    (string.IsNullOrWhiteSpace(desc) ? "NULL" : "'" + Escape(desc) + "'") + ", " +
                    categoryId + ", " + locationId + ", '" + Escape(priority) + "', 'New'); " +
                    "SELECT CAST(SCOPE_IDENTITY() AS INT);";

                int newComplaintId = 0;
                using (SqlCommand cmd = new SqlCommand(insertComplaint, con, tx))
                {
                    object idObj = cmd.ExecuteScalar();
                    if (idObj != null && idObj != DBNull.Value) newComplaintId = Convert.ToInt32(idObj);
                }
                if (newComplaintId <= 0) throw new Exception("Failed to create complaint.");

                
                string insertHistory =
                    "INSERT INTO StatusHistory(ComplaintId, OldStatus, NewStatus, Note, ChangedByUserId) " +
                    "VALUES (" + newComplaintId + ", NULL, 'New', N'Complaint submitted', " + _currentUserId + ")";
                using (SqlCommand cmd2 = new SqlCommand(insertHistory, con, tx))
                {
                    cmd2.ExecuteNonQuery();
                }

             
                foreach (var m in _media.OrderBy(x => x.SortOrder))
                {
                    string insertMedia =
                        "INSERT INTO ComplaintMedia(ComplaintId, FilePath, MediaType, ThumbnailPath, IsPrimary, SortOrder) " +
                        "VALUES (" + newComplaintId + ", '" + Escape(m.FilePath) + "', '" + Escape(m.MediaType) + "', NULL, " +
                        (m.IsPrimary ? 1 : 0) + ", " + m.SortOrder + ")";
                    using (SqlCommand cmd3 = new SqlCommand(insertMedia, con, tx))
                    {
                        cmd3.ExecuteNonQuery();
                    }
                }

                tx.Commit();
                con.Close();

                MessageBox.Show("Complaint submitted successfully.\nStatus: New",
                    "CivicLens", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                try { tx?.Rollback(); } catch { }
                if (con.State == ConnectionState.Open) con.Close();
                MessageBox.Show("Submit failed: " + ex.Message);
                _isSubmitting = false; btnSubmit.Enabled = true;
            }
        }

        
        private int GetOrCreateLocationId(string district, string city, string area)
        {
            string findQ = "SELECT TOP 1 LocationId FROM Locations " +
                           "WHERE District='" + Escape(district) + "' AND City='" + Escape(city) + "' AND Area='" + Escape(area) + "'";
            SqlDataAdapter da = new SqlDataAdapter(findQ, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0) return Convert.ToInt32(dt.Rows[0]["LocationId"]);

            string insertQ =
                "INSERT INTO Locations(District, City, Area) " +
                "VALUES ('" + Escape(district) + "', '" + Escape(city) + "', '" + Escape(area) + "'); " +
                "SELECT CAST(SCOPE_IDENTITY() AS INT);";

            using (SqlCommand cmd = new SqlCommand(insertQ, con))
            {
                con.Open();
                object idObj = cmd.ExecuteScalar();
                con.Close();
                return (idObj == null || idObj == DBNull.Value) ? 0 : Convert.ToInt32(idObj);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    
        private static string Escape(string s) => s?.Replace("'", "''") ?? "";
    }
}
