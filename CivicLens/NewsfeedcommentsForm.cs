using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace CivicLens
{
    public partial class NewsfeedCommentsForm : Form
    {
       
        private readonly SqlConnection con = new SqlConnection(
            "Data Source=LAPTOP-368QC6MP\\SQLEXPRESS;Initial Catalog=CivicLensDB;Integrated Security=True;");

     
        private readonly int _complaintId;
        private readonly int _currentUserId;
        private string _currentUserName;   
        private static readonly Color C_Navy = Color.FromArgb(18, 78, 148);
        private static readonly Color C_Blue = Color.FromArgb(37, 99, 235);
        private static readonly Color C_PageBg = Color.FromArgb(245, 247, 252);
        private static readonly Color C_White = Color.White;
        private static readonly Color C_Border = Color.FromArgb(203, 213, 225);
        private static readonly Color C_TextDark = Color.FromArgb(32, 56, 100);
        private static readonly Color C_TextMuted = Color.FromArgb(120, 150, 185);

      
        public NewsfeedCommentsForm(int complaintId, int userId)
            : this(complaintId, userId, null) { }

      
        public NewsfeedCommentsForm(int complaintId, int userId, string fullName)
        {
            _complaintId = complaintId;
            _currentUserId = userId;
            _currentUserName = fullName ?? "";

            InitializeComponent();

            this.Load += NewsfeedCommentsForm_Load;
            this.Resize += (s, e) => ArrangeInputBar();
        }

      
        private void NewsfeedCommentsForm_Load(object sender, EventArgs e)
        {
            // Resolve username if not supplied
            if (string.IsNullOrWhiteSpace(_currentUserName))
                _currentUserName = ResolveUserName(_currentUserId);

            lblHeaderTitle.Text = $"💬  Comments — Complaint #{_complaintId}";
            lblHeaderSub.Text = $"Public discussion thread";

            ArrangeInputBar();
            LoadComments();
        }

      
        private string ResolveUserName(int userId)
        {
            try
            {
                string sql = "SELECT FullName FROM Users WHERE UserId=@uid";
                using (var da = new SqlDataAdapter(sql, con))
                {
                    da.SelectCommand.Parameters.AddWithValue("@uid", userId);
                    var dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                        return dt.Rows[0]["FullName"].ToString();
                }
            }
            catch { }
            return "You";
        }

      
        private void LoadComments()
        {
            flowComments.Controls.Clear();
            flowComments.SuspendLayout();

            try
            {
                using (var cmd = new SqlCommand("sp_GetComplaintComments", con)
                { CommandType = CommandType.StoredProcedure })
                {
                    cmd.Parameters.AddWithValue("@ComplaintId", _complaintId);
                    var da = new SqlDataAdapter(cmd);
                    var dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count == 0)
                    {
                        flowComments.Controls.Add(new Label
                        {
                            Text = "📭  No comments yet. Be the first to comment!",
                            ForeColor = C_TextMuted,
                            Font = new Font("Segoe UI", 10f),
                            AutoSize = true,
                            Margin = new Padding(10, 20, 10, 10),
                            BackColor = Color.Transparent
                        });
                    }
                    else
                    {
                        foreach (DataRow r in dt.Rows)
                            flowComments.Controls.Add(BuildBubble(r));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load comments: " + ex.Message, "CivicLens",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                flowComments.ResumeLayout(true);
                // Scroll to bottom
                flowComments.ScrollControlIntoView(
                    flowComments.Controls.Count > 0
                        ? flowComments.Controls[flowComments.Controls.Count - 1]
                        : flowComments);
            }
        }

       
        private Panel BuildBubble(DataRow r)
        {
            string author = r["AuthorName"]?.ToString() ?? "Unknown";
            string role = r["AuthorRole"]?.ToString() ?? "";
            string text = r["CommentText"]?.ToString() ?? "";
            DateTime dt = r["CreatedAt"] == DBNull.Value
                                ? DateTime.Now
                                : Convert.ToDateTime(r["CreatedAt"]);

          
            bool isMine = string.Equals(author, _currentUserName,
                              StringComparison.OrdinalIgnoreCase);

            int availW = Math.Max(300, flowComments.ClientSize.Width - 28);
            int bubbleW = (int)(availW * 0.72);

        
            var wrapper = new Panel
            {
                Width = availW,
                BackColor = Color.Transparent,
                Margin = new Padding(0, 0, 0, 6)
            };

           
            var bubble = new Panel
            {
                BackColor = isMine ? C_Blue : C_White,
                Width = bubbleW,
                Padding = new Padding(12, 8, 12, 8)
            };

         
            bubble.Left = isMine ? (availW - bubbleW - 2) : 2;
            bubble.Top = 0;

           
            var lblAuthor = new Label
            {
                Text = isMine ? "You" : $"{author}  ·  {role}",
                Font = new Font("Segoe UI", 8f, FontStyle.Bold),
                ForeColor = isMine ? Color.FromArgb(191, 219, 254) : C_Blue,
                AutoSize = true,
                Location = new Point(12, 8),
                BackColor = Color.Transparent
            };

            
            var lblText = new Label
            {
                Text = text,
                Font = new Font("Segoe UI", 9.5f),
                ForeColor = isMine ? Color.White : C_TextDark,
                Location = new Point(12, lblAuthor.Bottom + 4),
                Width = bubbleW - 24,
                AutoSize = false,
                BackColor = Color.Transparent
            };

            
            lblText.Height = Math.Max(20,
                TextRenderer.MeasureText(
                    text,
                    lblText.Font,
                    new Size(lblText.Width, int.MaxValue),
                    TextFormatFlags.WordBreak).Height + 4);

            
            var lblTime = new Label
            {
                Text = dt.ToLocalTime().ToString("MMM dd, HH:mm"),
                Font = new Font("Segoe UI", 7.5f),
                ForeColor = isMine ? Color.FromArgb(191, 219, 254) : C_TextMuted,
                AutoSize = true,
                Location = new Point(12, lblText.Bottom + 4),
                BackColor = Color.Transparent
            };

            bubble.Height = lblTime.Bottom + 10;
            wrapper.Height = bubble.Height + 6;

            bubble.Controls.Add(lblAuthor);
            bubble.Controls.Add(lblText);
            bubble.Controls.Add(lblTime);

            
            if (!isMine)
            {
                bubble.Paint += (s, e) =>
                {
                    using (var pen = new Pen(C_Border, 1f))
                        e.Graphics.DrawRectangle(pen, 0, 0, bubble.Width - 1, bubble.Height - 1);
                };
            }

            wrapper.Controls.Add(bubble);
            return wrapper;
        }

        
        private void PostComment()
        {
            string text = (txtComment.Text ?? "").Trim();

            if (string.IsNullOrWhiteSpace(text))
            {
                MessageBox.Show("Please enter a comment before posting.", "CivicLens",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (text.Length > 1000)
            {
                MessageBox.Show("Comment must be 1 000 characters or fewer.", "CivicLens",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string sql =
                    "INSERT INTO NewsfeedComments (ComplaintId, UserId, CommentText) " +
                    "VALUES (@cid, @uid, @txt)";

                using (var cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@cid", _complaintId);
                    cmd.Parameters.AddWithValue("@uid", _currentUserId);
                    cmd.Parameters.AddWithValue("@txt", text);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                txtComment.Clear();
                txtComment.Focus();
                LoadComments();
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open) con.Close();
                MessageBox.Show("Post failed: " + ex.Message, "CivicLens",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

 
        private void ArrangeInputBar()
        {
            if (panelInput == null || txtComment == null) return;
            int right = panelInput.Width - 14;
            txtComment.Width = right - btnPost.Width - 14 - 14;
            btnPost.Left = txtComment.Right + 8;
            btnClose.Left = txtComment.Right + 8;
        }

        private void btnPost_Click(object sender, EventArgs e) => PostComment();
        private void btnClose_Click(object sender, EventArgs e) => this.Close();

        private void txtComment_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !e.Shift)
            {
                e.SuppressKeyPress = true;
                PostComment();
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            if (con.State == ConnectionState.Open) con.Close();
            base.OnFormClosed(e);
        }
    }
}