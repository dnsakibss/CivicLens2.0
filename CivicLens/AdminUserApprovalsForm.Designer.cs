using System;
using System.Windows.Forms;

namespace CivicLens
{
    partial class AdminUserApprovalsForm
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblTitle;

        private Label lblSearch;
        private TextBox txtSearch;
        private Label lblRole;
        private ComboBox cmbRole;
        private Button btnSearch;
        private Button btnRefresh;
        private Button btnApproveSelected;
        private Button btnRejectSelected;
        private Button btnClose;

        private DataGridView dgvPending;

        private DataGridViewTextBoxColumn colUserId;
        private DataGridViewTextBoxColumn colFullName;
        private DataGridViewTextBoxColumn colEmail;
        private DataGridViewTextBoxColumn colPhone;
        private DataGridViewTextBoxColumn colRoleName;
        private DataGridViewTextBoxColumn colCreatedAt;
        private DataGridViewButtonColumn colApprove;
        private DataGridViewButtonColumn colReject;

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
            this.lblRole = new Label();
            this.cmbRole = new ComboBox();
            this.btnSearch = new Button();
            this.btnRefresh = new Button();
            this.btnApproveSelected = new Button();
            this.btnRejectSelected = new Button();
            this.btnClose = new Button();

            this.dgvPending = new DataGridView();

            this.colUserId = new DataGridViewTextBoxColumn();
            this.colFullName = new DataGridViewTextBoxColumn();
            this.colEmail = new DataGridViewTextBoxColumn();
            this.colPhone = new DataGridViewTextBoxColumn();
            this.colRoleName = new DataGridViewTextBoxColumn();
            this.colCreatedAt = new DataGridViewTextBoxColumn();
            this.colApprove = new DataGridViewButtonColumn();
            this.colReject = new DataGridViewButtonColumn();

            // Form
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(245, 247, 250);
            this.ClientSize = new System.Drawing.Size(980, 560);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Name = "AdminUserApprovalsForm";
            this.Text = "Admin - User Approvals (First Login Approval)";

            // Title
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(20, 16);
            this.lblTitle.Text = "Pending User Approvals";

            // Filter row
            // top=56, left=20
            // left+62=82, top-3=53, top-1=55

            this.lblSearch.Location = new System.Drawing.Point(20, 56);
            this.lblSearch.Size = new System.Drawing.Size(56, 22);
            this.lblSearch.Text = "Search:";

            this.txtSearch.Location = new System.Drawing.Point(82, 53);   // left+62=82, top-3=53
            this.txtSearch.Size = new System.Drawing.Size(280, 25);

            this.lblRole.Location = new System.Drawing.Point(360, 56);
            this.lblRole.Size = new System.Drawing.Size(36, 22);
            this.lblRole.Text = "Role:";

            this.cmbRole.Location = new System.Drawing.Point(400, 53);    // top-3=53
            this.cmbRole.Size = new System.Drawing.Size(160, 25);
            this.cmbRole.DropDownStyle = ComboBoxStyle.DropDownList;

            this.btnSearch.Location = new System.Drawing.Point(580, 55);   // top-1=55
            this.btnSearch.Size = new System.Drawing.Size(90, 27);
            this.btnSearch.Text = "Search";
            //this.btnSearch.Click += new EventHandler(this.btnSearch_Click);

            this.btnRefresh.Location = new System.Drawing.Point(676, 55);  // top-1=55
            this.btnRefresh.Size = new System.Drawing.Size(90, 27);
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.Click += new EventHandler(this.btnRefresh_Click);

            this.btnApproveSelected.Location = new System.Drawing.Point(772, 55);  // top-1=55
            this.btnApproveSelected.Size = new System.Drawing.Size(90, 27);
            this.btnApproveSelected.Text = "Approve ✓";
            //this.btnApproveSelected.Click += new EventHandler(this.btnApproveSelected_Click);

            this.btnRejectSelected.Location = new System.Drawing.Point(868, 55);   // top-1=55
            this.btnRejectSelected.Size = new System.Drawing.Size(90, 27);
            this.btnRejectSelected.Text = "Reject ✗";
            //this.btnRejectSelected.Click += new EventHandler(this.btnRejectSelected_Click);

            this.btnClose.Location = new System.Drawing.Point(888, 512);
            this.btnClose.Size = new System.Drawing.Size(90, 28);
            this.btnClose.Text = "Close";
            this.btnClose.Click += new EventHandler(this.btnClose_Click);

            // Grid
            this.dgvPending.Location = new System.Drawing.Point(20, 100);
            this.dgvPending.Size = new System.Drawing.Size(960, 400);
            this.dgvPending.ReadOnly = true;
            this.dgvPending.AllowUserToAddRows = false;
            this.dgvPending.AllowUserToDeleteRows = false;
            this.dgvPending.MultiSelect = true;
            this.dgvPending.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvPending.RowHeadersVisible = false;
            this.dgvPending.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPending.CellContentClick += new DataGridViewCellEventHandler(this.dgvPending_CellContentClick);

            // Columns
            this.colUserId.HeaderText = "UserId";
            this.colUserId.Name = "colUserId";
            this.colUserId.Visible = false;
            this.colUserId.FillWeight = 6;

            this.colFullName.HeaderText = "Full Name";
            this.colFullName.Name = "colFullName";
            this.colFullName.FillWeight = 22;

            this.colEmail.HeaderText = "Email";
            this.colEmail.Name = "colEmail";
            this.colEmail.FillWeight = 22;

            this.colPhone.HeaderText = "Phone";
            this.colPhone.Name = "colPhone";
            this.colPhone.FillWeight = 14;

            this.colRoleName.HeaderText = "Role";
            this.colRoleName.Name = "colRoleName";
            this.colRoleName.FillWeight = 10;

            this.colCreatedAt.HeaderText = "Created At";
            this.colCreatedAt.Name = "colCreatedAt";
            this.colCreatedAt.FillWeight = 16;

            this.colApprove.HeaderText = "Approve";
            this.colApprove.Name = "colApprove";
            this.colApprove.Text = "Approve";
            this.colApprove.UseColumnTextForButtonValue = true;
            this.colApprove.FillWeight = 8;

            this.colReject.HeaderText = "Reject";
            this.colReject.Name = "colReject";
            this.colReject.Text = "Reject";
            this.colReject.UseColumnTextForButtonValue = true;
            this.colReject.FillWeight = 8;

            this.dgvPending.Columns.AddRange(new DataGridViewColumn[] {
                this.colUserId, this.colFullName, this.colEmail, this.colPhone,
                this.colRoleName, this.colCreatedAt, this.colApprove, this.colReject
            });

            // Add controls
            this.Controls.Add(this.lblTitle);

            this.Controls.Add(this.lblSearch);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.lblRole);
            this.Controls.Add(this.cmbRole);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnApproveSelected);
            this.Controls.Add(this.btnRejectSelected);
            this.Controls.Add(this.btnClose);

            this.Controls.Add(this.dgvPending);
        }
        #endregion
    }
}