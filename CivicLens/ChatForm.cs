using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace CivicLens
{
    public partial class ChatForm : Form
    {
       
        private readonly SqlConnection con = new SqlConnection(
            "Data Source=LAPTOP-368QC6MP\\SQLEXPRESS;Initial Catalog=CivicLensDB;Integrated Security=True;");

       
        private readonly int _complaintId;
        private readonly int _currentUserId;
        private readonly string _currentUserName;
        private readonly string _currentUserRole;

        private int _lastMessageId = 0;
        private Timer _refreshTimer;

      
        public ChatForm(int complaintId, int currentUserId,
                        string currentUserName, string currentUserRole = "")
        {
            _complaintId = complaintId;
            _currentUserId = currentUserId;
            _currentUserName = currentUserName;
            _currentUserRole = currentUserRole;

            InitializeComponent();

            this.Load += ChatForm_Load;
            this.Resize += (s, e) => ArrangeInputBar();
        }

        
        private void ChatForm_Load(object sender, EventArgs e)
        {
            try
            {
                lblComplaintBadge.Text = $"#{_complaintId:D4}";
                lblChatTitle.Text = "Complaint Chat";

                ArrangeInputBar();
                LoadComplaintMeta();
                LoadParticipants();
                LoadAllMessages();

                _refreshTimer = new Timer { Interval = 10_000, Enabled = true };
                _refreshTimer.Tick += (s2, e2) => FetchNewMessages();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Chat load error: " + ex.Message, "CivicLens");
            }
        }

        
        private void ArrangeInputBar()
        {
            if (panelInput == null) return;
            int right = panelInput.Width - 16;
            btnRefresh.Location = new Point(right - 40, 14);
            btnSend.Location = new Point(right - 40 - 4 - 110, 12);
            panelInputBox.Width = btnSend.Left - 16 - 8;
        }

        
        private void LoadComplaintMeta()
        {
            try
            {
                string sql =
                    "SELECT c.Title, c.Status, cat.CategoryName " +
                    "FROM Complaints c " +
                    "JOIN Categories cat ON cat.CategoryId = c.CategoryId " +
                    "WHERE c.ComplaintId = @id";

                using (var da = new SqlDataAdapter(sql, con))
                {
                    da.SelectCommand.Parameters.AddWithValue("@id", _complaintId);
                    var dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        var r = dt.Rows[0];
                        lblCardId.Text = $"ID: #{_complaintId:D4}";
                        lblCardStatus.Text = $"Status: {r["Status"]}";
                        lblCardCategory.Text = $"Category: {r["CategoryName"]}";
                        this.Text = $"CivicLens Chat — {r["Title"]}";
                    }
                }
            }
            catch { }
        }

      
        private void LoadParticipants()
        {
            try
            {
                lstParticipants.Items.Clear();

                string sql = @"
                    SELECT DISTINCT u.FullName, r.RoleName
                    FROM Users u
                    JOIN Roles r ON r.RoleId = u.RoleId
                    WHERE u.UserId = (SELECT CreatedByUserId FROM Complaints WHERE ComplaintId = @cid)
                    UNION
                    SELECT DISTINCT u.FullName, r.RoleName
                    FROM Assignments a
                    JOIN Users u ON u.UserId = a.AssigneeUserId
                    JOIN Roles r ON r.RoleId = u.RoleId
                    WHERE a.ComplaintId = @cid AND a.IsActive = 1
                    UNION
                    SELECT u.FullName, r.RoleName
                    FROM Users u
                    JOIN Roles r ON r.RoleId = u.RoleId
                    WHERE u.UserId = @uid";

                using (var da = new SqlDataAdapter(sql, con))
                {
                    da.SelectCommand.Parameters.AddWithValue("@cid", _complaintId);
                    da.SelectCommand.Parameters.AddWithValue("@uid", _currentUserId);
                    var dt = new DataTable();
                    da.Fill(dt);

                    foreach (DataRow row in dt.Rows)
                    {
                        string name = row["FullName"].ToString();
                        string role = row["RoleName"].ToString();
                        string you = name == _currentUserName ? " (You)" : "";
                        lstParticipants.Items.Add($"● {name}{you}  [{role}]");
                    }
                }
            }
            catch { }
        }

        
        private void LoadAllMessages()
        {
            flowMessages.Controls.Clear();
            _lastMessageId = 0;
            FetchNewMessages();
        }

    
        private void FetchNewMessages()
        {
            try
            {
                string sql = @"
                    SELECT m.MessageId, m.SentAt, u.FullName, r.RoleName,
                           m.SenderUserId, m.MessageText
                    FROM ComplaintMessages m
                    JOIN Users u ON u.UserId = m.SenderUserId
                    JOIN Roles r ON r.RoleId  = u.RoleId
                    WHERE m.ComplaintId = @cid AND m.MessageId > @lastId
                    ORDER BY m.SentAt ASC";

                using (var da = new SqlDataAdapter(sql, con))
                {
                    da.SelectCommand.Parameters.AddWithValue("@cid", _complaintId);
                    da.SelectCommand.Parameters.AddWithValue("@lastId", _lastMessageId);

                    var dt = new DataTable();
                    da.Fill(dt);

                    bool added = false;
                    foreach (DataRow row in dt.Rows)
                    {
                        bool isMine = Convert.ToInt32(row["SenderUserId"]) == _currentUserId;

                        var bubble = BuildBubble(
                            name: row["FullName"].ToString(),
                            role: row["RoleName"].ToString(),
                            text: row["MessageText"].ToString(),
                            time: Convert.ToDateTime(row["SentAt"]).ToLocalTime().ToString("HH:mm"),
                            isMine: isMine
                        );

                        if (this.InvokeRequired)
                            this.Invoke((Action)(() => flowMessages.Controls.Add(bubble)));
                        else
                            flowMessages.Controls.Add(bubble);

                        _lastMessageId = Convert.ToInt32(row["MessageId"]);
                        added = true;
                    }

                    if (added) ScrollToBottom();
                }
            }
            catch { }
        }


        private Panel BuildBubble(string name, string role, string text, string time, bool isMine)
        {
            int bubbleMaxW = Math.Max(300, flowMessages.Width - 80);

            var textFont = new Font("Segoe UI", 10.5f);
            var nameFont = new Font("Segoe UI", 8.5f, FontStyle.Bold);
            var timeFont = new Font("Segoe UI", 7.5f);

            SizeF textSize = MeasureText(text, textFont, bubbleMaxW - 24);
            int bubbleW = Math.Max(160, (int)textSize.Width + 28);
            int bubbleH = (int)textSize.Height + 52;

            var wrapper = new Panel();
            wrapper.Width = flowMessages.Width - 24;
            wrapper.Height = bubbleH + 12;
            wrapper.BackColor = Color.Transparent;
            wrapper.Margin = new Padding(0, 0, 0, 6);

       
            Color bubbleBg = isMine
                ? Color.FromArgb(37, 99, 235)         
                : Color.White;                         

            Color bubbleBorder = isMine
                ? Color.FromArgb(29, 78, 216)
                : Color.FromArgb(203, 213, 225);

            var bubble = new Panel();
            bubble.Width = bubbleW;
            bubble.Height = bubbleH;
            bubble.BackColor = bubbleBg;
            bubble.Left = isMine ? (wrapper.Width - bubbleW - 8) : 8;
            bubble.Top = 6;

            bubble.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                using (var path = RoundedRect(new Rectangle(0, 0, bubble.Width, bubble.Height), 12))
                using (var brush = new SolidBrush(bubble.BackColor))
                using (var pen = new Pen(bubbleBorder, 1f))
                {
                    e.Graphics.FillPath(brush, path);
                    e.Graphics.DrawPath(pen, path);
                }
            };
            bubble.Region = RoundedRegion(new Rectangle(0, 0, bubbleW, bubbleH), 12);

            var lblName = new Label();
            lblName.Text = isMine ? "You" : $"{name}  ·  {role}";
            lblName.Font = nameFont;
            lblName.ForeColor = isMine
                ? Color.FromArgb(191, 219, 254)
                : Color.FromArgb(37, 99, 235);
            lblName.Location = new Point(10, 8);
            lblName.AutoSize = true;
            lblName.BackColor = Color.Transparent;

            var lblText = new Label();
            lblText.Text = text;
            lblText.Font = textFont;
            lblText.ForeColor = isMine
                ? Color.White
                : Color.FromArgb(15, 23, 42);
            lblText.Location = new Point(10, 26);
            lblText.Size = new Size(bubbleW - 20, (int)textSize.Height + 4);
            lblText.BackColor = Color.Transparent;

            var lblTime = new Label();
            lblTime.Text = time;
            lblTime.Font = timeFont;
            lblTime.ForeColor = isMine
                ? Color.FromArgb(191, 219, 254)
                : Color.FromArgb(100, 116, 139);
            lblTime.Location = new Point(bubbleW - 44, bubbleH - 16);
            lblTime.AutoSize = true;
            lblTime.BackColor = Color.Transparent;

            bubble.Controls.Add(lblName);
            bubble.Controls.Add(lblText);
            bubble.Controls.Add(lblTime);
            wrapper.Controls.Add(bubble);

            return wrapper;
        }

       
        private static GraphicsPath RoundedRect(Rectangle rect, int radius)
        {
            int d = radius * 2;
            var path = new GraphicsPath();
            path.AddArc(rect.Left, rect.Top, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Top, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.Left, rect.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }

        private static Region RoundedRegion(Rectangle rect, int radius)
        {
            using (var path = RoundedRect(rect, radius))
                return new Region(path);
        }

        private static SizeF MeasureText(string text, Font font, int maxWidth)
        {
            using (var g = Graphics.FromImage(new Bitmap(1, 1)))
                return g.MeasureString(text, font, maxWidth);
        }

        private void ScrollToBottom()
        {
            if (flowMessages.InvokeRequired)
            {
                flowMessages.Invoke((Action)ScrollToBottom);
                return;
            }
            flowMessages.ScrollControlIntoView(
                flowMessages.Controls.Count > 0
                    ? flowMessages.Controls[flowMessages.Controls.Count - 1]
                    : (Control)flowMessages);
        }

        
        private void SendMessage()
        {
            string text = (rtxInput.Text ?? "").Trim();
            if (string.IsNullOrWhiteSpace(text)) return;

            EnsureTableExists();

            try
            {
                string sql = @"
                    INSERT INTO ComplaintMessages (ComplaintId, SenderUserId, MessageText)
                    VALUES (@cid, @uid, @msg)";

                using (var cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@cid", _complaintId);
                    cmd.Parameters.AddWithValue("@uid", _currentUserId);
                    cmd.Parameters.AddWithValue("@msg", text);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                rtxInput.Clear();
                rtxInput.Focus();
                FetchNewMessages();
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open) con.Close();
                MessageBox.Show("Send failed: " + ex.Message, "CivicLens");
            }
        }

       
        private void EnsureTableExists()
        {
            try
            {
                string sql = @"
                    IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name='ComplaintMessages')
                    CREATE TABLE ComplaintMessages (
                        MessageId      INT IDENTITY(1,1) PRIMARY KEY,
                        ComplaintId    INT NOT NULL,
                        SenderUserId   INT NOT NULL,
                        ReceiverUserId INT NULL,
                        MessageText    NVARCHAR(2000) NOT NULL,
                        SentAt         DATETIME2 DEFAULT SYSUTCDATETIME(),
                        IsRead         BIT DEFAULT 0
                    );";

                using (var cmd = new SqlCommand(sql, con))
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            catch
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
        }

     
        private void btnSend_Click(object sender, EventArgs e) => SendMessage();

     
        private void btnBack_Click(object sender, EventArgs e)
        {
            _refreshTimer?.Stop();
            this.Close();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadAllMessages();
            LoadParticipants();
            lblOnlineStatus.Text = "● Refreshed";
            lblOnlineStatus.ForeColor = Color.FromArgb(37, 99, 235);

            var t = new Timer { Interval = 2000 };
            t.Tick += (s2, e2) =>
            {
                lblOnlineStatus.Text = "● Connected";
                lblOnlineStatus.ForeColor = Color.FromArgb(22, 163, 74);
                t.Stop();
            };
            t.Start();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            _refreshTimer?.Stop();
            this.Close();
        }

      
        private void rtxInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !e.Shift)
            {
                e.SuppressKeyPress = true;
                SendMessage();
            }
        }

       
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            _refreshTimer?.Stop();
            _refreshTimer?.Dispose();
            if (con.State == ConnectionState.Open) con.Close();
            base.OnFormClosed(e);
        }
    }
}