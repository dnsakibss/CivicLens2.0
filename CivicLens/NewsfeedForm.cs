using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace CivicLens
{
    public partial class NewsfeedForm : Form
    {
        private readonly SqlConnection con = new SqlConnection(
            "Data Source=LAPTOP-368QC6MP\\SQLEXPRESS;Initial Catalog=CivicLensDB;Integrated Security=True;");

        private readonly int _userId;
        private readonly string _fullName;
        private readonly string _role;

        private int _pageIndex = 0;
        private const int PAGE_SIZE = 10;
        private bool _hasMoreRows = true;

        private bool IsPrivileged => _role == "Admin" || _role == "Moderator" || _role == "Police";
        private bool IsCitizenOrJournalist => _role == "Citizen" || _role == "Journalist";

        private static readonly Color C_Navy = Color.FromArgb(18, 78, 148);
        private static readonly Color C_Blue = Color.FromArgb(37, 99, 235);
        private static readonly Color C_PageBg = Color.FromArgb(245, 247, 252);
        private static readonly Color C_White = Color.White;
        private static readonly Color C_Border = Color.FromArgb(203, 213, 225);
        private static readonly Color C_TextDark = Color.FromArgb(32, 56, 100);
        private static readonly Color C_TextMuted = Color.FromArgb(120, 150, 185);
        private static readonly Color C_Green = Color.FromArgb(22, 163, 74);
        private static readonly Color C_Red = Color.FromArgb(220, 38, 38);
        private static readonly Color C_Amber = Color.FromArgb(217, 119, 6);

        private int CardWidth => Math.Max(400, this.ClientSize.Width - 57);

        public NewsfeedForm(int userId, string fullName, string role)
        {
            _userId = userId;
            _fullName = fullName;
            _role = role;

            InitializeComponent();

            this.Text = "CivicLens 2.0 – Newsfeed";
            this.Load += NewsfeedForm_Load;
            this.Resize += NewsfeedForm_Resize;
        }

        private void NewsfeedForm_Load(object sender, EventArgs e)
        {
            lblUserInfo.Text = $"Signed in as  {_fullName}  ({_role})";

            cmbStatus.Items.Clear();
            cmbStatus.Items.AddRange(new object[]
            {
                "All", "Pending", "Assigned", "InProgress",
                "OnHold", "Resolved", "Rejected", "Closed"
            });

            cmbStatus.SelectedIndex = 0;

            cmbCategory.Items.Clear();
            cmbCategory.Items.Add("All");

            LoadCategories();

            cmbCategory.SelectedIndex = 0;

            LoadFeed(true);
        }

        private void NewsfeedForm_Resize(object sender, EventArgs e)
        {
            int cw = CardWidth;

            foreach (Control c in flowFeed.Controls)
            {
                if (c is Panel card && card.Name.StartsWith("card_"))
                    card.Width = cw;
            }
        }

        private void LoadCategories()
        {
            try
            {
                string sql = "SELECT CategoryName FROM Categories ORDER BY CategoryName";

                using (var da = new SqlDataAdapter(sql, con))
                {
                    var dt = new DataTable();
                    da.Fill(dt);

                    foreach (DataRow r in dt.Rows)
                        cmbCategory.Items.Add(r["CategoryName"].ToString());
                }
            }
            catch { }
        }

        private void LoadFeed(bool reset)
        {
            if (reset)
            {
                _pageIndex = 0;
                _hasMoreRows = true;

                flowFeed.SuspendLayout();
                flowFeed.Controls.Clear();
                flowFeed.ResumeLayout(false);
            }

            if (!_hasMoreRows) return;

            int offset = _pageIndex * PAGE_SIZE;

            string statusFilter = cmbStatus.SelectedItem?.ToString() ?? "All";
            string categoryFilter = cmbCategory.SelectedItem?.ToString() ?? "All";

            try
            {
                var dt = new DataTable();

                if (con.State != ConnectionState.Open)
                    con.Open();

                using (var cmd = new SqlCommand("sp_GetNewsfeed", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@PageSize", SqlDbType.Int).Value = PAGE_SIZE;
                    cmd.Parameters.Add("@Offset", SqlDbType.Int).Value = offset;

                    cmd.Parameters.Add("@StatusFilter", SqlDbType.NVarChar, 50).Value =
                        statusFilter == "All" ? (object)DBNull.Value : statusFilter;

                    cmd.Parameters.Add("@CategoryFilter", SqlDbType.NVarChar, 100).Value =
                        categoryFilter == "All" ? (object)DBNull.Value : categoryFilter;

                    using (var da = new SqlDataAdapter(cmd))
                        da.Fill(dt);
                }

                con.Close();

                flowFeed.SuspendLayout();

                if (dt.Rows.Count == 0 && _pageIndex == 0)
                {
                    flowFeed.Controls.Add(BuildEmptyCard());
                    _hasMoreRows = false;
                }
                else
                {
                    foreach (DataRow r in dt.Rows)
                        flowFeed.Controls.Add(BuildComplaintCard(r));

                    if (dt.Rows.Count < PAGE_SIZE)
                        _hasMoreRows = false;
                }

                flowFeed.ResumeLayout(true);
                flowFeed.PerformLayout();

                btnLoadMore.Visible = _hasMoreRows;

                _pageIndex++;
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                    con.Close();

                MessageBox.Show(
                    "Failed to load newsfeed:\n\n" + ex.Message,
                    "CivicLens – Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private string GetPrimaryImagePath(int complaintId)
        {
            try
            {
                const string sql = @"
                    SELECT TOP 1 FilePath
                    FROM ComplaintMedia
                    WHERE ComplaintId = @cid
                      AND MediaType = 'Image'
                    ORDER BY IsPrimary DESC, SortOrder ASC, MediaId ASC";

                using (var cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@cid", complaintId);

                    if (con.State != ConnectionState.Open)
                        con.Open();

                    var result = cmd.ExecuteScalar();

                    con.Close();

                    return result == null || result == DBNull.Value
                        ? null
                        : result.ToString();
                }
            }
            catch
            {
                if (con.State == ConnectionState.Open)
                    con.Close();

                return null;
            }
        }

        private bool CanAccessChat(int complaintId)
        {
            if (_role == "Admin" || _role == "Moderator")
                return true;

            try
            {
                const string sql = @"
                    SELECT 1 WHERE
                        EXISTS (
                            SELECT 1 FROM Complaints
                            WHERE ComplaintId = @cid
                              AND CreatedByUserId = @uid
                        )
                        OR EXISTS (
                            SELECT 1 FROM Assignments
                            WHERE ComplaintId = @cid
                              AND AssigneeUserId = @uid
                              AND IsActive = 1
                        )";

                using (var cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@cid", complaintId);
                    cmd.Parameters.AddWithValue("@uid", _userId);

                    if (con.State != ConnectionState.Open)
                        con.Open();

                    var result = cmd.ExecuteScalar();

                    con.Close();

                    return result != null;
                }
            }
            catch
            {
                if (con.State == ConnectionState.Open)
                    con.Close();

                return false;
            }
        }

        private Panel BuildComplaintCard(DataRow r)
        {
            int complaintId = Convert.ToInt32(r["ComplaintId"]);

            string title = r["Title"]?.ToString() ?? "(No title)";
            string description = r["Description"]?.ToString() ?? "";
            string postedByName = r["PostedByName"]?.ToString() ?? "Unknown";
            string userRole = r["PostedByRole"]?.ToString() ?? "";
            string status = r["Status"]?.ToString() ?? "";
            string category = r["CategoryName"]?.ToString() ?? "";
            string location = r["LocationLabel"]?.ToString() ?? "";

            int totalReactions = r["TotalReactions"] == DBNull.Value
                ? 0
                : Convert.ToInt32(r["TotalReactions"]);

            int totalComments = r["TotalComments"] == DBNull.Value
                ? 0
                : Convert.ToInt32(r["TotalComments"]);

            DateTime createdAt = r["CreatedAt"] == DBNull.Value
                ? DateTime.Now
                : Convert.ToDateTime(r["CreatedAt"]);

            string shortDesc = description.Length > 180
                ? description.Substring(0, 180) + "…"
                : description;

            int cardW = CardWidth;

            var card = new Panel
            {
                Name = "card_" + complaintId,
                Width = cardW,
                BackColor = C_White,
                Margin = new Padding(4, 6, 4, 0),
                Padding = new Padding(16, 14, 16, 12),
                Cursor = Cursors.Default
            };

            Color accentColor = GetStatusColor(status);

            card.Paint += (s, e) =>
            {
                var g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;

                using (var pen = new Pen(C_Border, 1f))
                    g.DrawRectangle(pen, 0, 0, card.Width - 1, card.Height - 1);

                using (var br = new SolidBrush(accentColor))
                    g.FillRectangle(br, 0, 0, 5, card.Height);
            };

            int y = 14;
            int innerW = cardW - 48;

            var lblTitle = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 11f, FontStyle.Bold),
                ForeColor = C_TextDark,
                Location = new Point(20, y),
                Width = innerW - 120,
                AutoSize = false,
                Height = 22,
                BackColor = Color.Transparent
            };

            card.Controls.Add(lblTitle);

            var lblStatus = new Label
            {
                Text = status,
                Font = new Font("Segoe UI", 8f, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = accentColor,
                AutoSize = false,
                Width = 100,
                Height = 20,
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(cardW - 130, y + 1)
            };

            card.Controls.Add(lblStatus);

            y += 26;

            var lblMeta = new Label
            {
                Text = $"{GetRoleEmoji(userRole)} {postedByName}  ·  {userRole}  ·  {createdAt:MMM dd, yyyy HH:mm}",
                Font = new Font("Segoe UI", 8.5f),
                ForeColor = C_TextMuted,
                Location = new Point(20, y),
                Width = innerW,
                AutoSize = false,
                Height = 18,
                BackColor = Color.Transparent
            };

            card.Controls.Add(lblMeta);

            y += 22;

            var lblCatLoc = new Label
            {
                Text = $"📁 {category}    📍 {location}",
                Font = new Font("Segoe UI", 8.5f),
                ForeColor = C_TextMuted,
                Location = new Point(20, y),
                Width = innerW,
                AutoSize = false,
                Height = 18,
                BackColor = Color.Transparent
            };

            card.Controls.Add(lblCatLoc);

            y += 24;

            card.Controls.Add(new Panel
            {
                Location = new Point(20, y),
                Width = innerW,
                Height = 1,
                BackColor = C_Border
            });

            y += 8;

            var lblDesc = new Label
            {
                Text = shortDesc,
                Font = new Font("Segoe UI", 9.5f),
                ForeColor = C_TextDark,
                Location = new Point(20, y),
                Width = innerW,
                AutoSize = false,
                Height = 54,
                BackColor = Color.Transparent
            };

            card.Controls.Add(lblDesc);

            y += 60;

            string imgPath = GetPrimaryImagePath(complaintId);

            if (!string.IsNullOrEmpty(imgPath) && System.IO.File.Exists(imgPath))
            {
                try
                {
                    var pb = new PictureBox
                    {
                        Location = new Point(20, y),
                        Width = innerW,
                        Height = 180,
                        SizeMode = PictureBoxSizeMode.Zoom,
                        BackColor = Color.FromArgb(245, 247, 252),
                        BorderStyle = BorderStyle.None,
                        Cursor = Cursors.Hand
                    };

                    pb.Image = Image.FromFile(imgPath);

                    pb.Click += (s, e) =>
                    {
                        using (var f = new ComplaintDetailForm(complaintId, false, _userId))
                            f.ShowDialog(this);
                    };

                    card.Controls.Add(pb);

                    y += 188;
                }
                catch { }
            }

            var lblCounts = new Label
            {
                Text = $"👍 {totalReactions}  reactions    💬 {totalComments}  comments",
                Font = new Font("Segoe UI", 8.5f),
                ForeColor = C_TextMuted,
                Location = new Point(20, y),
                Width = innerW,
                AutoSize = false,
                Height = 18,
                BackColor = Color.Transparent
            };

            card.Controls.Add(lblCounts);

            y += 24;

            int bx = 20;
            int bh = 30;

            var btnView = MakeCardButton("🔍 View", C_Blue, bx, y, bh);

            btnView.Click += (s, e) =>
            {
                using (var f = new ComplaintDetailForm(complaintId, false, _userId))
                    f.ShowDialog(this);
            };

            card.Controls.Add(btnView);

            bx += btnView.Width + 6;

            if (CanAccessChat(complaintId))
            {
                var btnChat = MakeCardButton("💬 Chat", Color.FromArgb(55, 65, 81), bx, y, bh);

                btnChat.Click += (s, e) =>
                {
                    using (var f = new ChatForm(complaintId, _userId, _fullName, _role))
                        f.ShowDialog(this);
                };

                card.Controls.Add(btnChat);

                bx += btnChat.Width + 6;
            }

            if (IsCitizenOrJournalist)
            {
                var btnReact = MakeCardButton("👍 React", Color.FromArgb(5, 150, 105), bx, y, bh);

                btnReact.Click += (s, e) => ToggleReaction(complaintId, btnReact);

                card.Controls.Add(btnReact);

                bx += btnReact.Width + 6;

                var btnComment = MakeCardButton("💬 Comment", Color.FromArgb(124, 58, 237), bx, y, bh);

                btnComment.Click += (s, e) =>
                {
                    using (var f = new NewsfeedCommentsForm(complaintId, _userId, _fullName))
                        f.ShowDialog(this);

                    RefreshCard(complaintId);
                };

                card.Controls.Add(btnComment);

                bx += btnComment.Width + 6;
            }

            if (IsPrivileged)
            {
                var btnStatus = MakeCardButton("📋 Update Status", C_Amber, bx, y, bh);

                btnStatus.Click += (s, e) =>
                {
                    using (var f = new UpdateStatusForm(
                        complaintId,
                        _userId,
                        title,
                        category,
                        null,
                        postedByName,
                        createdAt,
                        status))
                    {
                        if (f.ShowDialog(this) == DialogResult.OK)
                            RefreshCard(complaintId);
                    }
                };

                card.Controls.Add(btnStatus);

                bx += btnStatus.Width + 6;

                var btnResolve = MakeCardButton("✅ Resolve", C_Green, bx, y, bh);

                btnResolve.Click += (s, e) => ResolveComplaint(complaintId, title);

                card.Controls.Add(btnResolve);

                bx += btnResolve.Width + 6;
            }

            y += bh + 14;

            card.Height = y;

            return card;
        }

        private static Button MakeCardButton(string text, Color bg, int x, int y, int h)
        {
            int w = TextRenderer.MeasureText(
                text,
                new Font("Segoe UI", 8.5f, FontStyle.Bold)).Width + 22;

            var btn = new Button
            {
                Text = text,
                Font = new Font("Segoe UI", 8.5f, FontStyle.Bold),
                BackColor = bg,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Location = new Point(x, y),
                Size = new Size(Math.Max(80, w), h),
                Cursor = Cursors.Hand
            };

            btn.FlatAppearance.BorderSize = 0;

            return btn;
        }

        private static Color GetStatusColor(string status)
        {
            switch (status)
            {
                case "Resolved":
                    return Color.FromArgb(22, 163, 74);

                case "Closed":
                    return Color.FromArgb(75, 85, 99);

                case "Rejected":
                    return Color.FromArgb(220, 38, 38);

                case "InProgress":
                    return Color.FromArgb(37, 99, 235);

                case "OnHold":
                    return Color.FromArgb(217, 119, 6);

                case "Assigned":
                    return Color.FromArgb(124, 58, 237);

                default:
                    return Color.FromArgb(100, 116, 139);
            }
        }

        private static string GetRoleEmoji(string role)
        {
            switch (role)
            {
                case "Admin":
                    return "🛡";

                case "Moderator":
                    return "🔧";

                case "Police":
                    return "👮";

                case "Journalist":
                    return "📰";

                default:
                    return "👤";
            }
        }

        private Panel BuildEmptyCard()
        {
            var p = new Panel
            {
                Width = CardWidth,
                Height = 90,
                BackColor = C_White,
                Margin = new Padding(4, 6, 4, 0)
            };

            p.Controls.Add(new Label
            {
                Text = "📭  No complaints found for the selected filters.",
                Font = new Font("Segoe UI", 10.5f),
                ForeColor = C_TextMuted,
                AutoSize = true,
                Location = new Point(20, 30),
                BackColor = Color.Transparent
            });

            return p;
        }

        private void ToggleReaction(int complaintId, Button btnReact)
        {
            try
            {
                using (var cmd = new SqlCommand("sp_ToggleReaction", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ComplaintId", complaintId);
                    cmd.Parameters.AddWithValue("@UserId", _userId);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                btnReact.BackColor =
                    btnReact.BackColor == Color.FromArgb(5, 150, 105)
                    ? Color.FromArgb(3, 105, 64)
                    : Color.FromArgb(5, 150, 105);
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                    con.Close();

                MessageBox.Show("Reaction failed: " + ex.Message, "CivicLens");
            }
        }

        private void ResolveComplaint(int complaintId, string title)
        {
            if (MessageBox.Show(
                $"Mark complaint #{complaintId} as Resolved?\n\n\"{title}\"",
                "Confirm Resolve",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                using (var cmd = new SqlCommand(
                    "UPDATE Complaints SET Status='Resolved' WHERE ComplaintId=@id", con))
                {
                    cmd.Parameters.AddWithValue("@id", complaintId);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                MessageBox.Show(
                    "Complaint resolved successfully.",
                    "CivicLens",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                RefreshCard(complaintId);
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                    con.Close();

                MessageBox.Show("Resolve failed: " + ex.Message, "CivicLens");
            }
        }

        private void RefreshCard(int complaintId)
        {
            LoadFeed(true);
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            LoadFeed(true);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            cmbStatus.SelectedIndex = 0;
            cmbCategory.SelectedIndex = 0;

            LoadFeed(true);
        }

        private void btnLoadMore_Click(object sender, EventArgs e)
        {
            LoadFeed(false);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            if (con.State == ConnectionState.Open)
                con.Close();

            base.OnFormClosed(e);
        }
    }
}