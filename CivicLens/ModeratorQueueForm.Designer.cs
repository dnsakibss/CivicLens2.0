using System;
using System.Windows.Forms;

namespace CivicLens
{
    partial class ModeratorQueueForm
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblTitle;

        private Label lblSearch;
        private TextBox txtSearch;
        private Label lblCategory;
        private ComboBox cmbCategory;
        private Label lblStatus;
        private ComboBox cmbStatus;
        private CheckBox chkOnlyUnassigned;

        private Button btnSearch;
        private Button btnRefresh;
        private Button btnAssignSelected;
        private Button btnClose;

        private DataGridView dgvQueue;

        private DataGridViewTextBoxColumn colId;
        private DataGridViewTextBoxColumn colTitle;
        private DataGridViewTextBoxColumn colCategory;
        private DataGridViewTextBoxColumn colPriority;
        private DataGridViewTextBoxColumn colStatus;
        private DataGridViewTextBoxColumn colCreatedAt;
        private DataGridViewTextBoxColumn colReporter;
        private DataGridViewTextBoxColumn colLocation;
        private DataGridViewButtonColumn colView;
        private DataGridViewButtonColumn colAssign;

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
            this.lblCategory = new Label();
            this.cmbCategory = new ComboBox();
            this.lblStatus = new Label();
            this.cmbStatus = new ComboBox();
            this.chkOnlyUnassigned = new CheckBox();

            this.btnSearch = new Button();
            this.btnRefresh = new Button();
            this.btnAssignSelected = new Button();
            this.btnClose = new Button();

            this.dgvQueue = new DataGridView();

            this.colId = new DataGridViewTextBoxColumn();
            this.colTitle = new DataGridViewTextBoxColumn();
            this.colCategory = new DataGridViewTextBoxColumn();
            this.colPriority = new DataGridViewTextBoxColumn();
            this.colStatus = new DataGridViewTextBoxColumn();
            this.colCreatedAt = new DataGridViewTextBoxColumn();
            this.colReporter = new DataGridViewTextBoxColumn();
            this.colLocation = new DataGridViewTextBoxColumn();
            this.colView = new DataGridViewButtonColumn();
            this.colAssign = new DataGridViewButtonColumn();

            // ===== FORM =====
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(248, 250, 253);
            this.ClientSize = new System.Drawing.Size(1120, 640);
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.MaximizeBox = true;
            this.MinimizeBox = true;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Name = "ModeratorQueueForm";
            this.Text = "Moderator Queue - CivicLens";

            // ===== TITLE =====
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(32, 56, 100);
            this.lblTitle.Location = new System.Drawing.Point(20, 16);
            this.lblTitle.Text = "New / Pending Complaints";

            // ===== FILTER ROW =====
            // top=70, left=20
            // left+70=90,  top-3=67
            // left+400=420
            // left+470=490, top-3=67
            // left+670=690
            // left+730=750, top-3=67
            // left+880=900

            this.lblSearch.Location = new System.Drawing.Point(20, 70);
            this.lblSearch.Size = new System.Drawing.Size(64, 24);
            this.lblSearch.Text = "Search:";

            this.txtSearch.Location = new System.Drawing.Point(90, 67);     // left+70=90, top-3=67
            this.txtSearch.Size = new System.Drawing.Size(320, 27);

            this.lblCategory.Location = new System.Drawing.Point(420, 70);  // left+400=420
            this.lblCategory.Size = new System.Drawing.Size(72, 24);
            this.lblCategory.Text = "Category:";

            this.cmbCategory.Location = new System.Drawing.Point(490, 67);  // left+470=490, top-3=67
            this.cmbCategory.Size = new System.Drawing.Size(180, 27);
            this.cmbCategory.DropDownStyle = ComboBoxStyle.DropDownList;

            this.lblStatus.Location = new System.Drawing.Point(690, 70);    // left+670=690
            this.lblStatus.Size = new System.Drawing.Size(56, 24);
            this.lblStatus.Text = "Status:";

            this.cmbStatus.Location = new System.Drawing.Point(750, 67);    // left+730=750, top-3=67
            this.cmbStatus.Size = new System.Drawing.Size(140, 27);
            this.cmbStatus.DropDownStyle = ComboBoxStyle.DropDownList;

            this.chkOnlyUnassigned.Location = new System.Drawing.Point(900, 70); // left+880=900
            this.chkOnlyUnassigned.Size = new System.Drawing.Size(150, 24);
            this.chkOnlyUnassigned.Text = "Only unassigned";

            // ===== BUTTON ROW =====
            // top after +=44 => 70+44=114

            this.btnSearch.Location = new System.Drawing.Point(20, 114);
            this.btnSearch.Size = new System.Drawing.Size(100, 32);
            this.btnSearch.Text = "Search";
            this.btnSearch.FlatStyle = FlatStyle.Flat;
            this.btnSearch.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnSearch.Click += new EventHandler(this.btnSearch_Click);

            this.btnRefresh.Location = new System.Drawing.Point(130, 114);
            this.btnRefresh.Size = new System.Drawing.Size(100, 32);
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.FlatStyle = FlatStyle.Flat;
            this.btnRefresh.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnRefresh.Click += new EventHandler(this.btnRefresh_Click);

            this.btnAssignSelected.Location = new System.Drawing.Point(240, 114);
            this.btnAssignSelected.Size = new System.Drawing.Size(160, 32);
            this.btnAssignSelected.Text = "Assign Selected";
            this.btnAssignSelected.BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            this.btnAssignSelected.ForeColor = System.Drawing.Color.White;
            this.btnAssignSelected.FlatStyle = FlatStyle.Flat;
            this.btnAssignSelected.FlatAppearance.BorderSize = 0;
            this.btnAssignSelected.Click += new EventHandler(this.btnAssignSelected_Click);

            // ===== GRID =====
            // top+50 = 114+50 = 164
            this.dgvQueue.Location = new System.Drawing.Point(20, 164);
            this.dgvQueue.Size = new System.Drawing.Size(1080, 420);
            this.dgvQueue.ReadOnly = true;
            this.dgvQueue.AllowUserToAddRows = false;
            this.dgvQueue.AllowUserToDeleteRows = false;
            this.dgvQueue.MultiSelect = false;
            this.dgvQueue.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvQueue.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvQueue.RowHeadersVisible = false;
            this.dgvQueue.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.dgvQueue.CellContentClick += new DataGridViewCellEventHandler(this.dgvQueue_CellContentClick);

            // ===== COLUMNS =====
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

            this.colAssign.HeaderText = "Assign";
            this.colAssign.Name = "colAssign";
            this.colAssign.Text = "Assign";
            this.colAssign.UseColumnTextForButtonValue = true;
            this.colAssign.FillWeight = 8;

            this.dgvQueue.Columns.AddRange(new DataGridViewColumn[] {
                this.colId, this.colTitle, this.colCategory, this.colPriority, this.colStatus,
                this.colCreatedAt, this.colReporter, this.colLocation, this.colView, this.colAssign
            });

            // ===== CLOSE BUTTON =====
            this.btnClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            this.btnClose.Location = new System.Drawing.Point(1018, 592);
            this.btnClose.Size = new System.Drawing.Size(80, 32);
            this.btnClose.Text = "Close";
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(231, 76, 60);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.FlatStyle = FlatStyle.Flat;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnClose.Click += new EventHandler(this.btnClose_Click);

            // ===== ADD CONTROLS =====
            this.Controls.Add(this.lblTitle);

            this.Controls.Add(this.lblSearch);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.lblCategory);
            this.Controls.Add(this.cmbCategory);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.cmbStatus);
            this.Controls.Add(this.chkOnlyUnassigned);

            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnAssignSelected);

            this.Controls.Add(this.dgvQueue);
            this.Controls.Add(this.btnClose);
        }
        #endregion
    }
}