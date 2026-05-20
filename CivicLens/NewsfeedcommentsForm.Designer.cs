// ============================================================
// NewsfeedCommentsForm.Designer.cs  –  CivicLens 2.0
// ============================================================
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace CivicLens
{
    partial class NewsfeedCommentsForm
    {
        private System.ComponentModel.IContainer components = null;

        // ── Header ─────────────────────────────────────────────────────
        private Panel panelHeader;
        private Label lblHeaderIcon;
        private Label lblHeaderTitle;
        private Label lblHeaderSub;

        // ── Comment scroll area ───────────────────────────────────────
        private Panel panelComments;
        private FlowLayoutPanel flowComments;

        // ── Input area ────────────────────────────────────────────────
        private Panel panelInput;
        private Label lblInputHint;
        private TextBox txtComment;
        private Button btnPost;
        private Button btnClose;

        // ── Status bar ────────────────────────────────────────────────
        private Panel panelStatusBar;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();

            // ── colour aliases ─────────────────────────────────────────
            Color clrNavy = Color.FromArgb(18, 78, 148);
            Color clrPageBg = Color.FromArgb(245, 247, 252);
            Color clrWhite = Color.White;
            Color clrBlue = Color.FromArgb(37, 99, 235);
            Color clrBorder = Color.FromArgb(203, 213, 225);
            Color clrTextDk = Color.FromArgb(32, 56, 100);
            Color clrMuted = Color.FromArgb(120, 150, 185);
            Color clrInputBg = Color.FromArgb(248, 250, 253);

            // ── instantiate ────────────────────────────────────────────
            this.panelHeader = new Panel();
            this.lblHeaderIcon = new Label();
            this.lblHeaderTitle = new Label();
            this.lblHeaderSub = new Label();

            this.panelComments = new Panel();
            this.flowComments = new FlowLayoutPanel();

            this.panelInput = new Panel();
            this.lblInputHint = new Label();
            this.txtComment = new TextBox();
            this.btnPost = new Button();
            this.btnClose = new Button();

            this.panelStatusBar = new Panel();

            // ── SuspendLayout ──────────────────────────────────────────
            this.panelHeader.SuspendLayout();
            this.panelComments.SuspendLayout();
            this.panelInput.SuspendLayout();
            this.SuspendLayout();

            // ══════════════════════════════════════════════════════════
            // FORM
            // ══════════════════════════════════════════════════════════
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = clrPageBg;
            this.ClientSize = new Size(620, 620);
            this.MinimumSize = new Size(480, 440);
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Name = "NewsfeedCommentsForm";
            this.Text = "Comments — CivicLens";

            // ══════════════════════════════════════════════════════════
            // panelHeader
            // ══════════════════════════════════════════════════════════
            this.panelHeader.BackColor = clrNavy;
            this.panelHeader.Dock = DockStyle.Top;
            this.panelHeader.Height = 68;
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.TabIndex = 0;
            this.panelHeader.Paint += new PaintEventHandler(this.PanelHeader_Paint);

            // icon badge
            this.lblHeaderIcon.AutoSize = false;
            this.lblHeaderIcon.BackColor = Color.FromArgb(55, 255, 255, 255);
            this.lblHeaderIcon.Font = new Font("Segoe UI", 18f);
            this.lblHeaderIcon.ForeColor = Color.White;
            this.lblHeaderIcon.Location = new Point(12, 11);
            this.lblHeaderIcon.Name = "lblHeaderIcon";
            this.lblHeaderIcon.Size = new Size(46, 46);
            this.lblHeaderIcon.TabIndex = 0;
            this.lblHeaderIcon.Text = "💬";
            this.lblHeaderIcon.TextAlign = ContentAlignment.MiddleCenter;

            // title
            this.lblHeaderTitle.AutoSize = true;
            this.lblHeaderTitle.BackColor = Color.Transparent;
            this.lblHeaderTitle.Font = new Font("Segoe UI", 12f, FontStyle.Bold);
            this.lblHeaderTitle.ForeColor = Color.White;
            this.lblHeaderTitle.Location = new Point(66, 10);
            this.lblHeaderTitle.Name = "lblHeaderTitle";
            this.lblHeaderTitle.TabIndex = 1;
            this.lblHeaderTitle.Text = "Complaint Comments";

            // sub
            this.lblHeaderSub.AutoSize = true;
            this.lblHeaderSub.BackColor = Color.Transparent;
            this.lblHeaderSub.Font = new Font("Segoe UI", 9f, FontStyle.Italic);
            this.lblHeaderSub.ForeColor = Color.FromArgb(190, 220, 255);
            this.lblHeaderSub.Location = new Point(68, 38);
            this.lblHeaderSub.Name = "lblHeaderSub";
            this.lblHeaderSub.TabIndex = 2;
            this.lblHeaderSub.Text = "Public discussion thread";

            this.panelHeader.Controls.Add(this.lblHeaderIcon);
            this.panelHeader.Controls.Add(this.lblHeaderTitle);
            this.panelHeader.Controls.Add(this.lblHeaderSub);

            // ══════════════════════════════════════════════════════════
            // panelStatusBar
            // ══════════════════════════════════════════════════════════
            this.panelStatusBar.BackColor = Color.FromArgb(232, 239, 250);
            this.panelStatusBar.Dock = DockStyle.Bottom;
            this.panelStatusBar.Height = 22;
            this.panelStatusBar.Name = "panelStatusBar";
            this.panelStatusBar.TabIndex = 4;
            this.panelStatusBar.Paint += new PaintEventHandler(this.PanelStatusBar_Paint);

            // ══════════════════════════════════════════════════════════
            // panelInput  (fixed bottom compose bar)
            // ══════════════════════════════════════════════════════════
            this.panelInput.BackColor = clrWhite;
            this.panelInput.Dock = DockStyle.Bottom;
            this.panelInput.Height = 108;
            this.panelInput.Name = "panelInput";
            this.panelInput.TabIndex = 3;
            this.panelInput.Paint += new PaintEventHandler(this.PanelInput_Paint);

            // hint
            this.lblInputHint.AutoSize = true;
            this.lblInputHint.Font = new Font("Segoe UI", 8.5f, FontStyle.Bold);
            this.lblInputHint.ForeColor = clrTextDk;
            this.lblInputHint.Location = new Point(14, 8);
            this.lblInputHint.Name = "lblInputHint";
            this.lblInputHint.Text = "Add a comment  (Enter = post,  Shift+Enter = new line)";

            // txtComment
            this.txtComment.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.txtComment.BackColor = clrInputBg;
            this.txtComment.BorderStyle = BorderStyle.FixedSingle;
            this.txtComment.Font = new Font("Segoe UI", 10f);
            this.txtComment.Location = new Point(14, 28);
            this.txtComment.Multiline = true;
            this.txtComment.Name = "txtComment";
            this.txtComment.ScrollBars = ScrollBars.Vertical;
            this.txtComment.Size = new Size(476, 66);
            this.txtComment.TabIndex = 0;
            this.txtComment.KeyDown += new KeyEventHandler(this.txtComment_KeyDown);

            // btnPost
            this.btnPost.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.btnPost.BackColor = clrBlue;
            this.btnPost.FlatStyle = FlatStyle.Flat;
            this.btnPost.FlatAppearance.BorderSize = 0;
            this.btnPost.FlatAppearance.MouseOverBackColor = Color.FromArgb(29, 78, 216);
            this.btnPost.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            this.btnPost.ForeColor = Color.White;
            this.btnPost.Location = new Point(498, 28);
            this.btnPost.Name = "btnPost";
            this.btnPost.Size = new Size(106, 30);
            this.btnPost.TabIndex = 1;
            this.btnPost.Text = "↑  Post";
            this.btnPost.Cursor = Cursors.Hand;
            this.btnPost.Click += new EventHandler(this.btnPost_Click);

            // btnClose
            this.btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.btnClose.BackColor = Color.FromArgb(220, 38, 38);
            this.btnClose.FlatStyle = FlatStyle.Flat;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatAppearance.MouseOverBackColor = Color.FromArgb(185, 28, 28);
            this.btnClose.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            this.btnClose.ForeColor = Color.White;
            this.btnClose.Location = new Point(498, 64);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new Size(106, 30);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "✕  Close";
            this.btnClose.Cursor = Cursors.Hand;
            this.btnClose.Click += new EventHandler(this.btnClose_Click);

            this.panelInput.Controls.Add(this.lblInputHint);
            this.panelInput.Controls.Add(this.txtComment);
            this.panelInput.Controls.Add(this.btnPost);
            this.panelInput.Controls.Add(this.btnClose);

            // ══════════════════════════════════════════════════════════
            // panelComments + flowComments
            // ══════════════════════════════════════════════════════════
            this.panelComments.AutoScroll = true;
            this.panelComments.BackColor = clrPageBg;
            this.panelComments.Dock = DockStyle.Fill;
            this.panelComments.Name = "panelComments";
            this.panelComments.TabIndex = 1;

            this.flowComments.AutoScroll = true;
            this.flowComments.BackColor = clrPageBg;
            this.flowComments.Dock = DockStyle.Fill;
            this.flowComments.FlowDirection = FlowDirection.TopDown;
            this.flowComments.Name = "flowComments";
            this.flowComments.Padding = new Padding(8, 10, 8, 10);
            this.flowComments.WrapContents = false;
            this.flowComments.TabIndex = 0;

            this.panelComments.Controls.Add(this.flowComments);

            // ══════════════════════════════════════════════════════════
            // Add to Form
            // ══════════════════════════════════════════════════════════
            this.Controls.Add(this.panelComments);    // Fill
            this.Controls.Add(this.panelInput);       // Bottom
            this.Controls.Add(this.panelStatusBar);   // Bottom (below input)
            this.Controls.Add(this.panelHeader);      // Top

            // ── ResumeLayout ──────────────────────────────────────────
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelComments.ResumeLayout(false);
            this.panelInput.ResumeLayout(false);
            this.panelInput.PerformLayout();
            this.ResumeLayout(false);
        }
        #endregion

        // ── Paint handlers ─────────────────────────────────────────────
        private void PanelHeader_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            using (var p = new Pen(Color.FromArgb(14, 0, 0, 0), 2f))
                g.DrawLine(p, 0, panelHeader.Height - 1, panelHeader.Width, panelHeader.Height - 1);
            int w = panelHeader.Width, h = panelHeader.Height;
            using (var br = new LinearGradientBrush(
                new Point(w - 80, 0), new Point(w, 0),
                Color.Transparent, Color.FromArgb(30, 255, 255, 255)))
                g.FillRectangle(br, w - 80, 0, 80, h);
        }

        private void PanelInput_Paint(object sender, PaintEventArgs e)
        {
            using (var p = new Pen(Color.FromArgb(203, 213, 225), 1f))
                e.Graphics.DrawLine(p, 0, 0, panelInput.Width, 0);
        }

        private void PanelStatusBar_Paint(object sender, PaintEventArgs e)
        {
            using (var p = new Pen(Color.FromArgb(200, 218, 238), 1f))
                e.Graphics.DrawLine(p, 0, 0, panelStatusBar.Width, 0);
            using (var fnt = new Font("Segoe UI", 8f))
            using (var br = new SolidBrush(Color.FromArgb(140, 162, 192)))
                e.Graphics.DrawString(
                    "\u00a9 2025 CivicLens  \u00b7  Community Edition  v2.0",
                    fnt, br, 12, 4);
        }
    }
}