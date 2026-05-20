using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace CivicLens
{
    public partial class LoginForm : Form
    {
        private readonly SqlConnection con = new SqlConnection(
            "Data Source=LAPTOP-368QC6MP\\SQLEXPRESS;Initial Catalog=CivicLensDB;Integrated Security=True;");

        // Tracks the current dashboard so we never stack handlers
        private DashboardForm _currentDash = null;

        public LoginForm()
        {
            InitializeComponent();
            SetupCustomUI();
            this.Load += (s, e) => { txtPassword.UseSystemPasswordChar = !chkShowPassword.Checked; };
        }

        private void ResetFields()
        {
            txtUsername.Text = "Enter your username";
            txtUsername.ForeColor = Color.Silver;
            txtPassword.Text = "";
            chkShowPassword.Checked = false;
            txtPassword.UseSystemPasswordChar = true;
        }

        private void SetupCustomUI()
        {
            txtUsername.Enter += (s, e2) =>
            {
                if (txtUsername.Text == "Enter your username")
                { txtUsername.Text = ""; txtUsername.ForeColor = Color.FromArgb(20, 40, 80); }
            };
            txtUsername.Leave += (s, e2) =>
            {
                if (string.IsNullOrWhiteSpace(txtUsername.Text))
                { txtUsername.Text = "Enter your username"; txtUsername.ForeColor = Color.Silver; }
            };

            panelLeftBrand.Paint += PanelLeftBrand_Paint;
            panelCard.Paint += PanelCard_Paint;
            panelChatToast.Paint += PanelChatToast_Paint;

            panelCard.Resize += (s, e) => { LayoutCard(); panelCard.Invalidate(); };

            panelRight.Resize += (s, e) =>
            {
                int hInset = 60;
                panelCard.Left = hInset;
                panelCard.Top = 0;
                panelCard.Width = Math.Max(300, panelRight.ClientSize.Width - hInset * 2);
                panelCard.Height = panelRight.ClientSize.Height;
            };

            this.Resize += (s, e) => PositionToast();

            panelChatToast.Click += (s, e) => DismissToast();
            toastTimer.Tick += (s, e) => DismissToast();

            this.Load += LoginForm_Load;
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            int hInset = 60;
            panelCard.Left = hInset;
            panelCard.Top = 0;
            panelCard.Width = Math.Max(300, panelRight.ClientSize.Width - hInset * 2);
            panelCard.Height = panelRight.ClientSize.Height;

            LayoutCard();
            panelCard.Invalidate();
            PositionToast();
            ResetFields();

            var delay = new Timer(this.components) { Interval = 900 };
            delay.Tick += (ss, ee) =>
            {
                delay.Stop();
                panelChatToast.Visible = true;
                panelChatToast.BringToFront();
                toastTimer.Start();
            };
            delay.Start();
        }

        private void Dashboard_FormClosed(object sender, FormClosedEventArgs e)
        {
            _currentDash = null;

            if (!this.IsDisposed)
            {
                ResetFields();
                this.Show();
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = (txtUsername.Text ?? "").Trim();
            string password = txtPassword.Text ?? "";

            if (string.IsNullOrWhiteSpace(username) || username == "Enter your username")
            {
                MessageBox.Show("Please enter username.", "Login", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtUsername.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter password.", "Login", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPassword.Focus();
                return;
            }

            try
            {
                string sql = @"
                    SELECT TOP 1 
                        u.UserId,
                        u.FullName,
                        u.ApprovalStatus,
                        u.IsActive,
                        u.IsDeleted,
                        l.IsLocked,
                        r.RoleName
                    FROM Logins l
                    JOIN Users  u ON u.UserId = l.UserId
                    JOIN Roles  r ON r.RoleId = u.RoleId
                    WHERE l.Username = @u AND l.[Password] = @p;";

                using (var da = new SqlDataAdapter(sql, con))
                {
                    da.SelectCommand.Parameters.AddWithValue("@u", username);
                    da.SelectCommand.Parameters.AddWithValue("@p", password);

                    var dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count != 1)
                    {
                        MessageBox.Show("Wrong username or password!!", "Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    var row = dt.Rows[0];

                    bool TryBool(string col, bool d = false) =>
                        dt.Columns.Contains(col) && row[col] != DBNull.Value
                            ? Convert.ToBoolean(row[col]) : d;

                    string TryStr(string col, string d = "") =>
                        dt.Columns.Contains(col) && row[col] != DBNull.Value
                            ? row[col].ToString() : d;

                    int userId = Convert.ToInt32(row["UserId"]);
                    string fullName = TryStr("FullName", "User");
                    string role = TryStr("RoleName", "Citizen");
                    string approval = TryStr("ApprovalStatus", "Approved");

                    bool isLocked = TryBool("IsLocked", false);
                    bool isActive = TryBool("IsActive", true);
                    bool isDeleted = TryBool("IsDeleted", false);

                    if (isDeleted) { MessageBox.Show("Account is removed. Contact admin.", "Login"); return; }
                    if (!isActive) { MessageBox.Show("Account is disabled. Contact admin.", "Login"); return; }
                    if (isLocked) { MessageBox.Show("Account is locked. Contact admin.", "Login"); return; }
                    if (!approval.Equals("Approved", StringComparison.OrdinalIgnoreCase))
                    {
                        MessageBox.Show($"Your account is '{approval}'. Wait for admin approval.", "Login");
                        return;
                    }

                   
                    if (_currentDash != null)
                    {
                        _currentDash.FormClosed -= Dashboard_FormClosed;
                        _currentDash = null;
                    }

                    var dash = new DashboardForm(userId, fullName, role);
                    _currentDash = dash;

                    
                    dash.FormClosed += Dashboard_FormClosed;

                    this.Hide();
                    dash.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Login error: " + ex.Message, "Login");
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            
            if (_currentDash != null)
            {
                _currentDash.FormClosed -= Dashboard_FormClosed;
                _currentDash = null;
            }
            base.OnFormClosing(e);
        }

        private void LayoutCard()
        {
            int cardPad = 40;
            int w = panelCard.Width;
            int h = panelCard.Height;
            int cw = w - cardPad * 2;
            int x = cardPad;

            int titleH = 38;
            int subH = 22;
            int divH = 1;
            int labelH = 18;
            int boxH = Math.Max(32, (int)(h * 0.058));
            int chkH = 22;
            int btnH = Math.Max(42, (int)(h * 0.075));
            int linkH = 22;
            int footerH = 18;

            int fixedH = titleH + subH + divH + labelH + boxH + labelH + boxH + chkH + btnH + linkH + linkH + footerH;
            int remaining = Math.Max(h - fixedH - 8 - 10, 7 * 4);

            int g0 = (int)(remaining * 0.13);
            int g1 = (int)(remaining * 0.09);
            int g3 = (int)(remaining * 0.10);
            int g4 = (int)(remaining * 0.05);
            int g5 = (int)(remaining * 0.12);
            int g6 = (int)(remaining * 0.12);
            int g7 = Math.Max(4, remaining - g0 - g1 - g3 - g4 - g5 - g6);

            int y = 8;

            y += g0;
            lblTitle.Location = new Point(x, y);
            y += titleH;

            lblSubtitle.Location = new Point(x, y);
            y += subH + 4;

            divider.Location = new Point(x, y);
            divider.Width = cw;
            y += divH + g1;

            lblUsername.Location = new Point(x, y);
            y += labelH + 2;

            txtUsername.Location = new Point(x, y);
            txtUsername.Width = cw;
            txtUsername.Height = boxH;
            y += boxH + g3;

            lblPassword.Location = new Point(x, y);
            y += labelH + 2;

            txtPassword.Location = new Point(x, y);
            txtPassword.Width = cw;
            txtPassword.Height = boxH;
            y += boxH + g4;

            chkShowPassword.Location = new Point(x, y);
            y += chkH + g5;

            btnLogin.Location = new Point(x, y);
            btnLogin.Width = cw;
            btnLogin.Height = btnH;
            y += btnH + g6;

            linkForgot.Location = new Point(x, y);
            y += linkH + 6;

            linkSignup.Location = new Point(x, y);
            y += linkH + g7;

            divFooter.Location = new Point(x, y);
            divFooter.Width = cw;
            y += 1 + 6;

            lblFooter.Location = new Point(x, y);
        }

        private void PanelLeftBrand_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            int w = panelLeftBrand.Width;
            int h = panelLeftBrand.Height;

            using (var br = new LinearGradientBrush(
                new Point(0, 0), new Point(0, h),
                Color.FromArgb(30, 120, 210),
                Color.FromArgb(12, 60, 130)))
                g.FillRectangle(br, 0, 0, w, h);

            using (var br = new SolidBrush(Color.FromArgb(20, 255, 255, 255)))
                g.FillEllipse(br, w - 100, -80, 200, 200);
            using (var br = new SolidBrush(Color.FromArgb(14, 255, 255, 255)))
                g.FillEllipse(br, -55, h - 140, 190, 190);
            using (var br = new SolidBrush(Color.FromArgb(10, 255, 255, 255)))
                g.FillEllipse(br, w / 2 - 70, h / 2 + 10, 145, 145);

            int cx = w / 2, cy = h / 3;
            PointF[] shield = {
                new PointF(cx,      cy - 44),
                new PointF(cx + 36, cy - 22),
                new PointF(cx + 36, cy + 12),
                new PointF(cx,      cy + 44),
                new PointF(cx - 36, cy + 12),
                new PointF(cx - 36, cy - 22),
            };
            using (var pen = new Pen(Color.FromArgb(180, 255, 255, 255), 2f))
                g.DrawPolygon(pen, shield);

            using (var pen = new Pen(Color.FromArgb(200, 255, 255, 255), 2.2f)
            { StartCap = LineCap.Round, EndCap = LineCap.Round })
            {
                g.DrawLine(pen, cx - 14, cy + 2, cx - 3, cy + 14);
                g.DrawLine(pen, cx - 3, cy + 14, cx + 17, cy - 10);
            }

            using (var fnt = new Font("Segoe UI", 17f, FontStyle.Bold))
            using (var br = new SolidBrush(Color.White))
            {
                SizeF sz = g.MeasureString("CivicLens 2.0", fnt);
                g.DrawString("CivicLens 2.0", fnt, br, (w - sz.Width) / 2f, cy + 52);
            }

            using (var fnt = new Font("Segoe UI", 9f))
            using (var br = new SolidBrush(Color.FromArgb(190, 255, 255, 255)))
            {
                string t = "Multimedia Complaint Management";
                SizeF sz = g.MeasureString(t, fnt);
                g.DrawString(t, fnt, br, (w - sz.Width) / 2f, cy + 82);
            }

            string[] feats = {
                "  File civic complaints easily",
                "  Attach photos, videos & audio",
                "  Track your complaint in real-time",
                "  Connect with your community"
            };
            int fy = h - 175;
            using (var fnt = new Font("Segoe UI", 9f))
            using (var br = new SolidBrush(Color.FromArgb(205, 255, 255, 255)))
            {
                foreach (var feat in feats)
                {
                    g.FillEllipse(
                        new SolidBrush(Color.FromArgb(130, 255, 255, 255)),
                        w / 2 - 95, fy + 4, 6, 6);
                    g.DrawString(feat, fnt, br, w / 2 - 88f, fy);
                    fy += 26;
                }
            }

            using (var fnt = new Font("Segoe UI", 7.5f))
            using (var br = new SolidBrush(Color.FromArgb(90, 255, 255, 255)))
            {
                string ver = "v2.0  ·  Community Edition";
                SizeF sz = g.MeasureString(ver, fnt);
                g.DrawString(ver, fnt, br, (w - sz.Width) / 2f, h - 20);
            }
        }

        private void PanelCard_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            using (var br = new SolidBrush(Color.FromArgb(24, 95, 165)))
                g.FillRectangle(br, 0, 0, panelCard.Width, 4);

            using (var pen = new Pen(Color.FromArgb(181, 212, 244), 1f))
                g.DrawRectangle(pen, 0, 0, panelCard.Width - 1, panelCard.Height - 1);
        }

        private void PanelChatToast_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            using (var br = new LinearGradientBrush(
                new Point(0, 0), new Point(panelChatToast.Width, 0),
                Color.FromArgb(30, 118, 200),
                Color.FromArgb(12, 60, 130)))
                g.FillRectangle(br, 0, 0, panelChatToast.Width, panelChatToast.Height);

            using (var br = new SolidBrush(Color.FromArgb(80, 255, 255, 255)))
                g.FillEllipse(br, 13, 21, 14, 14);
            using (var br = new SolidBrush(Color.FromArgb(74, 255, 90)))
                g.FillEllipse(br, 16, 24, 8, 8);

            using (var fnt = new Font("Segoe UI Semibold", 10f, FontStyle.Bold))
            using (var br = new SolidBrush(Color.White))
                g.DrawString("NOW CHAT AND NEWSFEED IS AVAILABLE", fnt, br, 34, 10);

            using (var fnt = new Font("Segoe UI", 8f))
            using (var br = new SolidBrush(Color.FromArgb(181, 212, 244)))
                g.DrawString("Connect with CivicLens support instantly", fnt, br, 34, 34);

            g.FillRectangle(
                new SolidBrush(Color.FromArgb(40, 255, 255, 255)),
                6, panelChatToast.Height - 5, panelChatToast.Width - 12, 3);

            using (var pen = new Pen(Color.FromArgb(50, 255, 255, 255), 1f))
                g.DrawRectangle(pen, 0, 0, panelChatToast.Width - 1, panelChatToast.Height - 1);
        }

        private void PositionToast()
        {
            panelChatToast.Left = ClientSize.Width - panelChatToast.Width - 18;
            panelChatToast.Top = ClientSize.Height - panelChatToast.Height - 18;
        }

        private void DismissToast()
        {
            toastTimer.Stop();
            panelChatToast.Visible = false;
        }

        private void linkSignup_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (var f = new SignupForm())
            {
                f.StartPosition = FormStartPosition.CenterParent;
                f.ShowDialog(this);
            }
        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !chkShowPassword.Checked;
        }

        private void linkForgot_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                using (var f = new UpdatePasswordForm(UpdatePasswordMode.Forgot))
                {
                    f.StartPosition = FormStartPosition.CenterParent;
                    f.ShowDialog(this);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to open password reset: " + ex.Message, "Login");
            }
        }
    }
}