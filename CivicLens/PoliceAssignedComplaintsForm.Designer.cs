using System;
using System.Windows.Forms;

namespace CivicLens
{
    partial class PoliceAssignedComplaintsForm
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
        private DataGridViewButtonColumn colUpdate;
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
            this.colUpdate = new DataGridViewButtonColumn();
            this.colChat = new DataGridViewButtonColumn();

            // ===== Form =====
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(248, 250, 253);
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.MaximizeBox = true;
            this.MinimizeBox = true;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Name = "PoliceAssignedComplaintsForm";
            this.Text = "My Assigned Complaints - CivicLens";

            // ===== Title =====
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(32, 56, 100);
            this.lblTitle.Location = new System.Drawing.Point(20, 16);
            this.lblTitle.Text = "My Assigned Complaints";

            // ===== Filters row =====
            // top=60, left=20
            // left+66=86,          top-3=57
            // left+66+388=474,     top=60
            // left+66+388+56=530,  top-3=57
            // top-1=59

            this.lblSearch.Location = new System.Drawing.Point(20, 60);
            this.lblSearch.Size = new System.Drawing.Size(60, 22);
            this.lblSearch.Text = "Search:";

            this.txtSearch.Location = new System.Drawing.Point(86, 57);        // left+66=86, top-3=57
            this.txtSearch.Size = new System.Drawing.Size(380, 27);
            this.txtSearch.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 10F);

            this.lblStatus.Location = new System.Drawing.Point(474, 60);       // left+66+388=474
            this.lblStatus.Size = new System.Drawing.Size(52, 22);
            this.lblStatus.Text = "Status:";
            this.lblStatus.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            this.cmbStatus.Location = new System.Drawing.Point(530, 57);       // left+66+388+56=530, top-3=57
            this.cmbStatus.Size = new System.Drawing.Size(170, 27);
            this.cmbStatus.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.cmbStatus.DropDownStyle = ComboBoxStyle.DropDownList;

            this.btnSearch.Location = new System.Drawing.Point(820, 59);       // top-1=59
            this.btnSearch.Size = new System.Drawing.Size(88, 30);
            this.btnSearch.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.btnSearch.Text = "Search";
            this.btnSearch.BackColor = System.Drawing.Color.White;
            this.btnSearch.FlatStyle = FlatStyle.Flat;
            this.btnSearch.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnSearch.Click += new EventHandler(this.btnSearch_Click);

            this.btnRefresh.Location = new System.Drawing.Point(916, 59);      // top-1=59
            this.btnRefresh.Size = new System.Drawing.Size(88, 30);
            this.btnRefresh.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.BackColor = System.Drawing.Color.White;
            this.btnRefresh.FlatStyle = FlatStyle.Flat;
            this.btnRefresh.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnRefresh.Click += new EventHandler(this.btnRefresh_Click);

            // ===== Close button =====
            this.btnClose.Location = new System.Drawing.Point(910, 552);
            this.btnClose.Size = new System.Drawing.Size(80, 32);
            this.btnClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            this.btnClose.Text = "Close";
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(231, 76, 60);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.FlatStyle = FlatStyle.Flat;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.Click += new EventHandler(this.btnClose_Click);

            // ===== Grid =====
            this.dgvAssigned.Location = new System.Drawing.Point(20, 106);
            this.dgvAssigned.Size = new System.Drawing.Size(960, 430);
            this.dgvAssigned.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.dgvAssigned.ReadOnly = true;
            this.dgvAssigned.AllowUserToAddRows = false;
            this.dgvAssigned.AllowUserToDeleteRows = false;
            this.dgvAssigned.MultiSelect = false;
            this.dgvAssigned.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvAssigned.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAssigned.RowHeadersVisible = false;
            this.dgvAssigned.CellContentClick += new DataGridViewCellEventHandler(this.dgvAssigned_CellContentClick);

            // Table styling
            this.dgvAssigned.BackgroundColor = System.Drawing.Color.White;
            this.dgvAssigned.BorderStyle = BorderStyle.None;
            this.dgvAssigned.EnableHeadersVisualStyles = false;
            this.dgvAssigned.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(235, 241, 250);
            this.dgvAssigned.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(33, 37, 41);
            this.dgvAssigned.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.dgvAssigned.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.dgvAssigned.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(249, 252, 255);
            this.dgvAssigned.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(209, 230, 255);
            this.dgvAssigned.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvAssigned.GridColor = System.Drawing.Color.FromArgb(230, 236, 245);

            // ===== Columns =====
            this.colId.HeaderText = "ID";
            this.colId.Name = "colId";
            this.colId.Visible = false;
            this.colId.FillWeight = 8;

            this.colTitle.HeaderText = "Title";
            this.colTitle.Name = "colTitle";
            this.colTitle.FillWeight = 26;

            this.colCategory.HeaderText = "Category";
            this.colCategory.Name = "colCategory";
            this.colCategory.FillWeight = 14;

            this.colPriority.HeaderText = "Priority";
            this.colPriority.Name = "colPriority";
            this.colPriority.FillWeight = 10;

            this.colStatus.HeaderText = "Status";
            this.colStatus.Name = "colStatus";
            this.colStatus.FillWeight = 12;

            this.colCreatedAt.HeaderText = "Created At";
            this.colCreatedAt.Name = "colCreatedAt";
            this.colCreatedAt.FillWeight = 14;

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

            this.colUpdate.HeaderText = "Update";
            this.colUpdate.Name = "colUpdate";
            this.colUpdate.Text = "Update";
            this.colUpdate.UseColumnTextForButtonValue = true;
            this.colUpdate.FillWeight = 8;

            this.colChat.HeaderText = "Chat";
            this.colChat.Name = "colChat";
            this.colChat.Text = "💬 Chat";
            this.colChat.UseColumnTextForButtonValue = true;
            this.colChat.FillWeight = 8;

            this.dgvAssigned.Columns.AddRange(new DataGridViewColumn[] {
                this.colId, this.colTitle, this.colCategory, this.colPriority, this.colStatus,
                this.colCreatedAt, this.colReporter, this.colLocation,
                this.colView, this.colUpdate, this.colChat
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