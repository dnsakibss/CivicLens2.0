// ============================================================
// NewsfeedForm.Designer.cs  –  CivicLens 2.0
// ============================================================
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace CivicLens
{
    partial class NewsfeedForm
    {
        private System.ComponentModel.IContainer components = null;

        // ── Header ─────────────────────────────────────────────────────
        private Panel panelHeader;
        private Label lblAppTitle;
        private Label lblUserInfo;

        // ── Filter bar ─────────────────────────────────────────────────
        private Panel panelFilter;
        private Label lblStatusFilter;
        private ComboBox cmbStatus;
        private Label lblCategoryFilter;
        private ComboBox cmbCategory;
        private Button btnFilter;
        private Button btnReset;

        // ── Feed area ──────────────────────────────────────────────────
        private Panel panelBody;
        private FlowLayoutPanel flowFeed;
        private Button btnLoadMore;
        private Button btnClose;

        // ── Status bar ─────────────────────────────────────────────────
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

            // clrNavy  = Color.FromArgb(18,78,148)
            // clrPageBg= Color.FromArgb(245,247,252)
            // clrWhite = Color.White
            // clrBlue  = Color.FromArgb(37,99,235)
            // clrBorder= Color.FromArgb(203,213,225)
            // clrTextDk= Color.FromArgb(32,56,100)
            // clrMuted = Color.FromArgb(120,150,185)

            // ── instantiate ────────────────────────────────────────────
            this.panelHeader = new Panel();
            this.lblAppTitle = new Label();
            this.lblUserInfo = new Label();
            this.panelFilter = new Panel();
            this.lblStatusFilter = new Label();
            this.cmbStatus = new ComboBox();
            this.lblCategoryFilter = new Label();
            this.cmbCategory = new ComboBox();
            this.btnFilter = new Button();
            this.btnReset = new Button();
            this.panelBody = new Panel();
            this.flowFeed = new FlowLayoutPanel();
            this.btnLoadMore = new Button();
            this.btnClose = new Button();
            this.panelStatusBar = new Panel();

            // ── SuspendLayout ──────────────────────────────────────────
            this.panelHeader.SuspendLayout();
            this.panelFilter.SuspendLayout();
            this.panelBody.SuspendLayout();
            this.SuspendLayout();

            // ══════════════════════════════════════════════════════════
            // FORM
            // ══════════════════════════════════════════════════════════
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(245, 247, 252);
            this.ClientSize = new System.Drawing.Size(860, 680);
            this.MinimumSize = new System.Drawing.Size(620, 500);
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Name = "NewsfeedForm";
            this.Text = "CivicLens 2.0 – Newsfeed";

            // ══════════════════════════════════════════════════════════
            // panelHeader
            // ══════════════════════════════════════════════════════════
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(18, 78, 148);
            this.panelHeader.Dock = DockStyle.Top;
            this.panelHeader.Height = 60;
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.TabIndex = 0;
            this.panelHeader.Paint += new PaintEventHandler(this.PanelHeader_Paint);

            // lblAppTitle
            this.lblAppTitle.AutoSize = true;
            this.lblAppTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblAppTitle.Font = new System.Drawing.Font("Segoe UI", 15f, System.Drawing.FontStyle.Bold);
            this.lblAppTitle.ForeColor = System.Drawing.Color.White;
            this.lblAppTitle.Location = new System.Drawing.Point(56, 14);
            this.lblAppTitle.Name = "lblAppTitle";
            this.lblAppTitle.Text = "📰  Newsfeed";

            // lblUserInfo
            this.lblUserInfo.AutoSize = true;
            this.lblUserInfo.BackColor = System.Drawing.Color.Transparent;
            this.lblUserInfo.Font = new System.Drawing.Font("Segoe UI", 8.5f, System.Drawing.FontStyle.Italic);
            this.lblUserInfo.ForeColor = System.Drawing.Color.FromArgb(190, 220, 255);
            this.lblUserInfo.Location = new System.Drawing.Point(58, 40);
            this.lblUserInfo.Name = "lblUserInfo";
            this.lblUserInfo.Text = "Signed in as …";

            this.panelHeader.Controls.Add(this.lblAppTitle);
            this.panelHeader.Controls.Add(this.lblUserInfo);

            // ══════════════════════════════════════════════════════════
            // panelFilter
            // ══════════════════════════════════════════════════════════
            this.panelFilter.BackColor = System.Drawing.Color.White;
            this.panelFilter.Dock = DockStyle.Top;
            this.panelFilter.Height = 54;
            this.panelFilter.Name = "panelFilter";
            this.panelFilter.TabIndex = 1;
            this.panelFilter.Paint += new PaintEventHandler(this.PanelFilter_Paint);

            // lblStatusFilter
            this.lblStatusFilter.AutoSize = true;
            this.lblStatusFilter.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
            this.lblStatusFilter.ForeColor = System.Drawing.Color.FromArgb(32, 56, 100);
            this.lblStatusFilter.Location = new System.Drawing.Point(14, 18);
            this.lblStatusFilter.Name = "lblStatusFilter";
            this.lblStatusFilter.Text = "Status:";

            // cmbStatus
            this.cmbStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbStatus.Font = new System.Drawing.Font("Segoe UI", 9f);
            this.cmbStatus.Location = new System.Drawing.Point(68, 14);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(140, 24);
            this.cmbStatus.TabIndex = 0;
            this.cmbStatus.FlatStyle = FlatStyle.Flat;

            // lblCategoryFilter
            this.lblCategoryFilter.AutoSize = true;
            this.lblCategoryFilter.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
            this.lblCategoryFilter.ForeColor = System.Drawing.Color.FromArgb(32, 56, 100);
            this.lblCategoryFilter.Location = new System.Drawing.Point(220, 18);
            this.lblCategoryFilter.Name = "lblCategoryFilter";
            this.lblCategoryFilter.Text = "Category:";

            // cmbCategory
            this.cmbCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbCategory.Font = new System.Drawing.Font("Segoe UI", 9f);
            this.cmbCategory.Location = new System.Drawing.Point(286, 14);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Size = new System.Drawing.Size(160, 24);
            this.cmbCategory.TabIndex = 1;
            this.cmbCategory.FlatStyle = FlatStyle.Flat;

            // btnFilter
            this.btnFilter.BackColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.btnFilter.FlatStyle = FlatStyle.Flat;
            this.btnFilter.FlatAppearance.BorderSize = 0;
            this.btnFilter.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(29, 78, 216);
            this.btnFilter.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
            this.btnFilter.ForeColor = System.Drawing.Color.White;
            this.btnFilter.Location = new System.Drawing.Point(458, 13);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(90, 28);
            this.btnFilter.TabIndex = 2;
            this.btnFilter.Text = "🔍  Filter";
            this.btnFilter.Cursor = Cursors.Hand;
            this.btnFilter.Click += new EventHandler(this.btnFilter_Click);

            // btnReset
            this.btnReset.BackColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.btnReset.FlatStyle = FlatStyle.Flat;
            this.btnReset.FlatAppearance.BorderSize = 0;
            this.btnReset.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(75, 85, 99);
            this.btnReset.Font = new System.Drawing.Font("Segoe UI", 9f);
            this.btnReset.ForeColor = System.Drawing.Color.White;
            this.btnReset.Location = new System.Drawing.Point(556, 13);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(80, 28);
            this.btnReset.TabIndex = 3;
            this.btnReset.Text = "↺  Reset";
            this.btnReset.Cursor = Cursors.Hand;
            this.btnReset.Click += new EventHandler(this.btnReset_Click);

            this.panelFilter.Controls.Add(this.lblStatusFilter);
            this.panelFilter.Controls.Add(this.cmbStatus);
            this.panelFilter.Controls.Add(this.lblCategoryFilter);
            this.panelFilter.Controls.Add(this.cmbCategory);
            this.panelFilter.Controls.Add(this.btnFilter);
            this.panelFilter.Controls.Add(this.btnReset);

            // ══════════════════════════════════════════════════════════
            // panelStatusBar
            // ══════════════════════════════════════════════════════════
            this.panelStatusBar.BackColor = System.Drawing.Color.FromArgb(232, 239, 250);
            this.panelStatusBar.Dock = DockStyle.Bottom;
            this.panelStatusBar.Height = 22;
            this.panelStatusBar.Name = "panelStatusBar";
            this.panelStatusBar.TabIndex = 4;
            this.panelStatusBar.Paint += new PaintEventHandler(this.PanelStatusBar_Paint);

            // ══════════════════════════════════════════════════════════
            // panelBody
            // ══════════════════════════════════════════════════════════
            this.panelBody.BackColor = System.Drawing.Color.FromArgb(245, 247, 252);
            this.panelBody.Dock = DockStyle.Fill;
            this.panelBody.Name = "panelBody";
            this.panelBody.TabIndex = 2;
            this.panelBody.Padding = new Padding(0, 0, 0, 48);

            // flowFeed
            this.flowFeed.AutoScroll = true;
            this.flowFeed.BackColor = System.Drawing.Color.FromArgb(245, 247, 252);
            this.flowFeed.Dock = DockStyle.Fill;
            this.flowFeed.FlowDirection = FlowDirection.TopDown;
            this.flowFeed.Name = "flowFeed";
            this.flowFeed.Padding = new Padding(6, 8, 6, 60);
            this.flowFeed.WrapContents = false;
            this.flowFeed.TabIndex = 0;

            // btnLoadMore
            this.btnLoadMore.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.btnLoadMore.BackColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.btnLoadMore.FlatStyle = FlatStyle.Flat;
            this.btnLoadMore.FlatAppearance.BorderSize = 0;
            this.btnLoadMore.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(29, 78, 216);
            this.btnLoadMore.Font = new System.Drawing.Font("Segoe UI", 9.5f, System.Drawing.FontStyle.Bold);
            this.btnLoadMore.ForeColor = System.Drawing.Color.White;
            this.btnLoadMore.Location = new System.Drawing.Point(10, 0);
            this.btnLoadMore.Name = "btnLoadMore";
            this.btnLoadMore.Size = new System.Drawing.Size(200, 34);
            this.btnLoadMore.TabIndex = 1;
            this.btnLoadMore.Text = "⬇  Load More";
            this.btnLoadMore.Visible = false;
            this.btnLoadMore.Cursor = Cursors.Hand;
            this.btnLoadMore.Click += new EventHandler(this.btnLoadMore_Click);
            this.btnLoadMore.Dock = DockStyle.Bottom;

            // btnClose
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(220, 38, 38);
            this.btnClose.FlatStyle = FlatStyle.Flat;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(185, 28, 28);
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 9.5f, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(130, 34);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "✕  Close";
            this.btnClose.Cursor = Cursors.Hand;
            this.btnClose.Dock = DockStyle.Bottom;
            this.btnClose.Click += new EventHandler(this.btnClose_Click);

            this.panelBody.Controls.Add(this.flowFeed);
            this.panelBody.Controls.Add(this.btnLoadMore);
            this.panelBody.Controls.Add(this.btnClose);

            // ══════════════════════════════════════════════════════════
            // Add to Form
            // ══════════════════════════════════════════════════════════
            this.Controls.Add(this.panelBody);
            this.Controls.Add(this.panelFilter);
            this.Controls.Add(this.panelHeader);
            this.Controls.Add(this.panelStatusBar);

            // ── ResumeLayout ──────────────────────────────────────────
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelFilter.ResumeLayout(false);
            this.panelFilter.PerformLayout();
            this.panelBody.ResumeLayout(false);
            this.ResumeLayout(false);
        }
        #endregion

        // ── Paint handlers ─────────────────────────────────────────────
        private void PanelHeader_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            using (var p = new Pen(System.Drawing.Color.FromArgb(14, 0, 0, 0), 2f))
                g.DrawLine(p, 0, panelHeader.Height - 1, panelHeader.Width, panelHeader.Height - 1);
            int w = panelHeader.Width, h = panelHeader.Height;
            using (var br = new LinearGradientBrush(
                new System.Drawing.Point(w - 120, 0), new System.Drawing.Point(w, 0),
                System.Drawing.Color.Transparent, System.Drawing.Color.FromArgb(25, 255, 255, 255)))
                g.FillRectangle(br, w - 120, 0, 120, h);
        }

        private void PanelFilter_Paint(object sender, PaintEventArgs e)
        {
            using (var p = new Pen(System.Drawing.Color.FromArgb(203, 213, 225), 1f))
            {
                e.Graphics.DrawLine(p, 0, 0, panelFilter.Width, 0);
                e.Graphics.DrawLine(p, 0, panelFilter.Height - 1, panelFilter.Width, panelFilter.Height - 1);
            }
        }

        private void PanelStatusBar_Paint(object sender, PaintEventArgs e)
        {
            using (var p = new Pen(System.Drawing.Color.FromArgb(200, 218, 238), 1f))
                e.Graphics.DrawLine(p, 0, 0, panelStatusBar.Width, 0);
            using (var fnt = new System.Drawing.Font("Segoe UI", 8f))
            using (var br = new SolidBrush(System.Drawing.Color.FromArgb(140, 162, 192)))
                e.Graphics.DrawString(
                    "\u00a9 2025 CivicLens  \u00b7  Community Edition  v2.0",
                    fnt, br, 12, 4);
        }
    }
}