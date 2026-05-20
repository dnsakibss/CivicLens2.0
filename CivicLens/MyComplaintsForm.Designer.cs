using System;
using System.Windows.Forms;

namespace CivicLens
{
    partial class MyComplaintsForm
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblTitle;
        private TextBox txtSearch;
        private Button btnSearch;
        private DataGridView dgvComplaints;
        private Button btnRefresh;
        private Button btnClose;

        private DataGridViewTextBoxColumn colId;
        private DataGridViewTextBoxColumn colTitle;
        private DataGridViewTextBoxColumn colCategory;
        private DataGridViewTextBoxColumn colStatus;
        private DataGridViewTextBoxColumn colCreatedAt;
        private DataGridViewButtonColumn colView;
        private DataGridViewButtonColumn colEdit;

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
            this.txtSearch = new TextBox();
            this.btnSearch = new Button();
            this.dgvComplaints = new DataGridView();
            this.btnRefresh = new Button();
            this.btnClose = new Button();

            this.colId = new DataGridViewTextBoxColumn();
            this.colTitle = new DataGridViewTextBoxColumn();
            this.colCategory = new DataGridViewTextBoxColumn();
            this.colStatus = new DataGridViewTextBoxColumn();
            this.colCreatedAt = new DataGridViewTextBoxColumn();
            this.colView = new DataGridViewButtonColumn();
            this.colEdit = new DataGridViewButtonColumn();

            // ===== Form =====
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(248, 250, 253);
            this.ClientSize = new System.Drawing.Size(940, 560);
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.MaximizeBox = true;
            this.MinimizeBox = true;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Name = "MyComplaintsForm";
            this.Text = "My Complaints - CivicLens";

            // ===== Title =====
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(32, 56, 100);
            this.lblTitle.Location = new System.Drawing.Point(20, 16);
            this.lblTitle.Text = "My Complaints";

            // ===== Search =====
            this.txtSearch.Location = new System.Drawing.Point(24, 62);
            this.txtSearch.Size = new System.Drawing.Size(560, 27);
            this.txtSearch.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSearch.Text = "Search by title or status...";
            this.txtSearch.GotFocus += new EventHandler(this.txtSearch_GotFocus);
            this.txtSearch.LostFocus += new EventHandler(this.txtSearch_LostFocus);

            this.btnSearch.Location = new System.Drawing.Point(594, 60);
            this.btnSearch.Size = new System.Drawing.Size(96, 30);
            this.btnSearch.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.btnSearch.Text = "Search";
            this.btnSearch.BackColor = System.Drawing.Color.White;
            this.btnSearch.FlatStyle = FlatStyle.Flat;
            this.btnSearch.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnSearch.Click += new EventHandler(this.btnSearch_Click);

            // ===== Grid =====
            this.dgvComplaints.Location = new System.Drawing.Point(24, 104);
            this.dgvComplaints.Size = new System.Drawing.Size(892, 388);
            this.dgvComplaints.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.dgvComplaints.ReadOnly = true;
            this.dgvComplaints.AllowUserToAddRows = false;
            this.dgvComplaints.AllowUserToDeleteRows = false;
            this.dgvComplaints.MultiSelect = false;
            this.dgvComplaints.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvComplaints.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvComplaints.RowHeadersVisible = false;
            this.dgvComplaints.CellContentClick += new DataGridViewCellEventHandler(this.dgvComplaints_CellContentClick);

            // Table styling
            this.dgvComplaints.BackgroundColor = System.Drawing.Color.White;
            this.dgvComplaints.BorderStyle = BorderStyle.None;
            this.dgvComplaints.EnableHeadersVisualStyles = false;
            this.dgvComplaints.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(235, 241, 250);
            this.dgvComplaints.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(33, 37, 41);
            this.dgvComplaints.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.dgvComplaints.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.dgvComplaints.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(249, 252, 255);
            this.dgvComplaints.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(209, 230, 255);
            this.dgvComplaints.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvComplaints.GridColor = System.Drawing.Color.FromArgb(230, 236, 245);

            // ===== Columns =====
            this.colId.HeaderText = "ID";
            this.colId.Name = "colId";
            this.colId.FillWeight = 8;
            this.colId.Visible = false;

            this.colTitle.HeaderText = "Title";
            this.colTitle.Name = "colTitle";
            this.colTitle.FillWeight = 28;

            this.colCategory.HeaderText = "Category";
            this.colCategory.Name = "colCategory";
            this.colCategory.FillWeight = 18;

            this.colStatus.HeaderText = "Status";
            this.colStatus.Name = "colStatus";
            this.colStatus.FillWeight = 14;

            this.colCreatedAt.HeaderText = "Created At";
            this.colCreatedAt.Name = "colCreatedAt";
            this.colCreatedAt.FillWeight = 18;

            this.colView.HeaderText = "View";
            this.colView.Name = "colView";
            this.colView.Text = "View";
            this.colView.UseColumnTextForButtonValue = true;
            this.colView.FillWeight = 7;

            this.colEdit.HeaderText = "Edit";
            this.colEdit.Name = "colEdit";
            this.colEdit.Text = "Edit";
            this.colEdit.UseColumnTextForButtonValue = true;
            this.colEdit.FillWeight = 7;

            this.dgvComplaints.Columns.AddRange(new DataGridViewColumn[]
            {
                this.colId, this.colTitle, this.colCategory,
                this.colStatus, this.colCreatedAt, this.colView, this.colEdit
            });

            // ===== Bottom Buttons =====
            this.btnRefresh.Location = new System.Drawing.Point(712, 506);
            this.btnRefresh.Size = new System.Drawing.Size(96, 32);
            this.btnRefresh.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.BackColor = System.Drawing.Color.White;
            this.btnRefresh.FlatStyle = FlatStyle.Flat;
            this.btnRefresh.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnRefresh.Click += new EventHandler(this.btnRefresh_Click);

            this.btnClose.Location = new System.Drawing.Point(820, 506);
            this.btnClose.Size = new System.Drawing.Size(96, 32);
            this.btnClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            this.btnClose.Text = "Close";
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(231, 76, 60);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.FlatStyle = FlatStyle.Flat;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.Click += new EventHandler(this.btnClose_Click);

            // ===== Add Controls =====
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.dgvComplaints);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnClose);
        }
        #endregion
    }
}