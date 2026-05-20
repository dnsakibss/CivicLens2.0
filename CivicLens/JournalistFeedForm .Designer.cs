using System;
using System.Windows.Forms;

namespace CivicLens
{
    partial class JournalistFeedForm
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblTitle;

        private Label lblSearch;
        private TextBox txtSearch;
        private Label lblStatus;
        private ComboBox cmbStatus;
        private Button btnSearch;
        private Button btnRefresh;
        private Button btnClose;

        private DataGridView dgvAssigned;

        private DataGridViewTextBoxColumn colId;
        private DataGridViewTextBoxColumn colTitle;
        private DataGridViewTextBoxColumn colCategory;
        private DataGridViewTextBoxColumn colPriority;
        private DataGridViewTextBoxColumn colStatus;
        private DataGridViewTextBoxColumn colCreatedAt;
        private DataGridViewTextBoxColumn colReporter;
        private DataGridViewTextBoxColumn colLocation;
        private DataGridViewButtonColumn colView;
        private DataGridViewButtonColumn colCovered;
        private DataGridViewButtonColumn colChat;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();

            this.lblTitle = new Label();

            this.lblSearch = new Label();
            this.txtSearch = new TextBox();
            this.lblStatus = new Label();
            this.cmbStatus = new ComboBox();
            this.btnSearch = new Button();
            this.btnRefresh = new Button();
            this.btnClose = new Button();

            this.dgvAssigned = new DataGridView();

            this.colId = new DataGridViewTextBoxColumn();
            this.colTitle = new DataGridViewTextBoxColumn();
            this.colCategory = new DataGridViewTextBoxColumn();
            this.colPriority = new DataGridViewTextBoxColumn();
            this.colStatus = new DataGridViewTextBoxColumn();
            this.colCreatedAt = new DataGridViewTextBoxColumn();
            this.colReporter = new DataGridViewTextBoxColumn();
            this.colLocation = new DataGridViewTextBoxColumn();
            this.colView = new DataGridViewButtonColumn();
            this.colCovered = new DataGridViewButtonColumn();
            this.colChat = new DataGridViewButtonColumn(); // moved here from body

            // ===== Form =====
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(248, 250, 253);
            this.ClientSize = new System.Drawing.Size(1120, 640);
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.MaximizeBox = true;
            this.MinimizeBox = true;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Name = "JournalistFeedForm";
            this.Text = "Journalist Feed - CivicLens";

            // ===== Title =====
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(32, 56, 100);
            this.lblTitle.Location = new System.Drawing.Point(20, 16);
            this.lblTitle.Text = "Assigned to Me (Journalist)";

            // ===== Filters Row =====
            // top=66, left=20
            // left+66=86,  top-3=63
            // left+440=460, left+496=516
            // left+690=710, left+792=812

            this.lblSearch.Location = new System.Drawing.Point(20, 66);
            this.lblSearch.Size = new System.Drawing.Size(60, 24);
            this.lblSearch.Text = "Search:";

            this.txtSearch.Location = new System.Drawing.Point(86, 63);     // left+66=86, top-3=63
            this.txtSearch.Size = new System.Drawing.Size(360, 27);
            this.txtSearch.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            this.lblStatus.Location = new System.Drawing.Point(460, 66);    // left+440=460
            this.lblStatus.Size = new System.Drawing.Size(56, 24);
            this.lblStatus.Text = "Status:";

            this.cmbStatus.Location = new System.Drawing.Point(516, 63);    // left+496=516, top-3=63
            this.cmbStatus.Size = new System.Drawing.Size(180, 27);
            this.cmbStatus.DropDownStyle = ComboBoxStyle.DropDownList;

            this.btnSearch.Location = new System.Drawing.Point(710, 63);    // left+690=710, top-3=63
            this.btnSearch.Size = new System.Drawing.Size(96, 30);
            this.btnSearch.Text = "Search";
            this.btnSearch.FlatStyle = FlatStyle.Flat;
            this.btnSearch.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnSearch.Click += new EventHandler(this.btnSearch_Click);

            this.btnRefresh.Location = new System.Drawing.Point(812, 63);   // left+792=812, top-3=63
            this.btnRefresh.Size = new System.Drawing.Size(96, 30);
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.FlatStyle = FlatStyle.Flat;
            this.btnRefresh.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnRefresh.Click += new EventHandler(this.btnRefresh_Click);

            this.btnClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            this.btnClose.Location = new System.Drawing.Point(1018, 592);
            this.btnClose.Size = new System.Drawing.Size(80, 30);
            this.btnClose.Text = "Close";
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(231, 76, 60);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.FlatStyle = FlatStyle.Flat;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnClose.Click += new EventHandler(this.btnClose_Click);

            // ===== Grid =====
            this.dgvAssigned.Location = new System.Drawing.Point(20, 110);
            this.dgvAssigned.Size = new System.Drawing.Size(1078, 468);
            this.dgvAssigned.ReadOnly = true;
            this.dgvAssigned.AllowUserToAddRows = false;
            this.dgvAssigned.AllowUserToDeleteRows = false;
            this.dgvAssigned.MultiSelect = false;
            this.dgvAssigned.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvAssigned.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAssigned.RowHeadersVisible = false;
            this.dgvAssigned.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.dgvAssigned.CellContentClick += new DataGridViewCellEventHandler(this.dgvAssigned_CellContentClick);

            // ===== Columns =====
            this.colId.HeaderText = "ID";
            this.colId.Name = "colId";
            this.colId.Visible = false;
            this.colId.FillWeight = 6;

            this.colTitle.HeaderText = "Title";
            this.colTitle.Name = "colTitle";
            this.colTitle.FillWeight = 22;

            this.colCategory.HeaderText = "Category";
            this.colCategory.Name = "colCategory";
            this.colCategory.FillWeight = 12;

            this.colPriority.HeaderText = "Priority";
            this.colPriority.Name = "colPriority";
            this.colPriority.FillWeight = 9;

            this.colStatus.HeaderText = "Status";
            this.colStatus.Name = "colStatus";
            this.colStatus.FillWeight = 10;

            this.colCreatedAt.HeaderText = "Created At";
            this.colCreatedAt.Name = "colCreatedAt";
            this.colCreatedAt.FillWeight = 13;

            this.colReporter.HeaderText = "Reporter";
            this.colReporter.Name = "colReporter";
            this.colReporter.FillWeight = 12;

            this.colLocation.HeaderText = "Location";
            this.colLocation.Name = "colLocation";
            this.colLocation.FillWeight = 18;

            this.colView.HeaderText = "View";
            this.colView.Name = "colView";
            this.colView.Text = "View";
            this.colView.UseColumnTextForButtonValue = true;
            this.colView.FillWeight = 8;

            this.colCovered.HeaderText = "Mark";
            this.colCovered.Name = "colCovered";
            this.colCovered.Text = "Covered";
            this.colCovered.UseColumnTextForButtonValue = true;
            this.colCovered.FillWeight = 10;

            this.colChat.HeaderText = "Chat";
            this.colChat.Name = "colChat";
            this.colChat.Text = "💬 Chat";
            this.colChat.UseColumnTextForButtonValue = true;
            this.colChat.FillWeight = 8;

            this.dgvAssigned.Columns.AddRange(new DataGridViewColumn[] {
                this.colId, this.colTitle, this.colCategory, this.colPriority, this.colStatus,
                this.colCreatedAt, this.colReporter, this.colLocation,
                this.colView, this.colCovered, this.colChat
            });

            // ===== Add controls =====
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblSearch);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.cmbStatus);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.dgvAssigned);
        }
        #endregion
    }
}