using System;
using System.Windows.Forms;

namespace CivicLens
{
    partial class AdminUsersForm
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblTitle;

        private Label lblSearch;
        private TextBox txtSearch;

        private Label lblRole;
        private ComboBox cmbRole;

        private Label lblStatus;
        private ComboBox cmbStatus;

        private Button btnSearch;
        private Button btnRefresh;

        private Button btnBulkActivate;
        private Button btnBulkDeactivate;
        private Button btnBulkDelete;
        private Button btnClose;

        private DataGridView dgvUsers;

        private DataGridViewTextBoxColumn colUserId;
        private DataGridViewTextBoxColumn colFullName;
        private DataGridViewTextBoxColumn colEmail;
        private DataGridViewTextBoxColumn colPhone;
        private DataGridViewTextBoxColumn colRole;
        private DataGridViewTextBoxColumn colStatusCol;
        private DataGridViewTextBoxColumn colCreatedAt;
        private DataGridViewButtonColumn colView;
        private DataGridViewButtonColumn colChangeRole;
        private DataGridViewButtonColumn colToggleActive;
        private DataGridViewButtonColumn colDelete;

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
            this.lblStatus = new Label();
            this.cmbStatus = new ComboBox();

            this.btnSearch = new Button();
            this.btnRefresh = new Button();

            this.btnBulkActivate = new Button();
            this.btnBulkDeactivate = new Button();
            this.btnBulkDelete = new Button();
            this.btnClose = new Button();

            this.dgvUsers = new DataGridView();

            this.colUserId = new DataGridViewTextBoxColumn();
            this.colFullName = new DataGridViewTextBoxColumn();
            this.colEmail = new DataGridViewTextBoxColumn();
            this.colPhone = new DataGridViewTextBoxColumn();
            this.colRole = new DataGridViewTextBoxColumn();
            this.colStatusCol = new DataGridViewTextBoxColumn();
            this.colCreatedAt = new DataGridViewTextBoxColumn();
            this.colView = new DataGridViewButtonColumn();
            this.colChangeRole = new DataGridViewButtonColumn();
            this.colToggleActive = new DataGridViewButtonColumn();
            this.colDelete = new DataGridViewButtonColumn();

            // Form
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(245, 247, 250);
            this.ClientSize = new System.Drawing.Size(1180, 640);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Name = "AdminUsersForm";
            this.Text = "Admin - Manage Users";

            // Title
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(20, 16);
            this.lblTitle.Text = "Manage Users";

            // Filters row
            // top=56, left=20
            // left+64=84, top-3=53, top-1=55

            this.lblSearch.Location = new System.Drawing.Point(20, 56);
            this.lblSearch.Size = new System.Drawing.Size(60, 22);
            this.lblSearch.Text = "Search:";

            this.txtSearch.Location = new System.Drawing.Point(84, 53);    // left+64=84, top-3=53
            this.txtSearch.Size = new System.Drawing.Size(300, 25);

            this.lblRole.Location = new System.Drawing.Point(380, 56);
            this.lblRole.Size = new System.Drawing.Size(36, 22);
            this.lblRole.Text = "Role:";

            this.cmbRole.Location = new System.Drawing.Point(420, 53);     // top-3=53
            this.cmbRole.Size = new System.Drawing.Size(160, 25);
            this.cmbRole.DropDownStyle = ComboBoxStyle.DropDownList;

            this.lblStatus.Location = new System.Drawing.Point(600, 56);
            this.lblStatus.Size = new System.Drawing.Size(48, 22);
            this.lblStatus.Text = "Status:";

            this.cmbStatus.Location = new System.Drawing.Point(652, 53);   // top-3=53
            this.cmbStatus.Size = new System.Drawing.Size(140, 25);
            this.cmbStatus.DropDownStyle = ComboBoxStyle.DropDownList;

            this.btnSearch.Location = new System.Drawing.Point(810, 55);   // top-1=55
            this.btnSearch.Size = new System.Drawing.Size(90, 27);
            this.btnSearch.Text = "Search";

            this.btnRefresh.Location = new System.Drawing.Point(906, 55);  // top-1=55
            this.btnRefresh.Size = new System.Drawing.Size(90, 27);
            this.btnRefresh.Text = "Refresh";

            // Bulk
            this.btnBulkActivate.Location = new System.Drawing.Point(20, 94);
            this.btnBulkActivate.Size = new System.Drawing.Size(140, 28);
            this.btnBulkActivate.Text = "Activate Selected";

            this.btnBulkDeactivate.Location = new System.Drawing.Point(166, 94);
            this.btnBulkDeactivate.Size = new System.Drawing.Size(160, 28);
            this.btnBulkDeactivate.Text = "Deactivate Selected";

            this.btnBulkDelete.Location = new System.Drawing.Point(332, 94);
            this.btnBulkDelete.Size = new System.Drawing.Size(130, 28);
            this.btnBulkDelete.Text = "Delete Selected";

            this.btnClose.Location = new System.Drawing.Point(1080, 592);
            this.btnClose.Size = new System.Drawing.Size(80, 28);
            this.btnClose.Text = "Close";

            // Grid
            this.dgvUsers.Location = new System.Drawing.Point(20, 130);
            this.dgvUsers.Size = new System.Drawing.Size(1140, 450);
            this.dgvUsers.ReadOnly = true;
            this.dgvUsers.AllowUserToAddRows = false;
            this.dgvUsers.AllowUserToDeleteRows = false;
            this.dgvUsers.MultiSelect = true;
            this.dgvUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvUsers.RowHeadersVisible = false;
            this.dgvUsers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Columns
            this.colUserId.HeaderText = "UserId";
            this.colUserId.Name = "colUserId";
            this.colUserId.Visible = false;

            this.colFullName.HeaderText = "Full Name";
            this.colFullName.Name = "colFullName";

            this.colEmail.HeaderText = "Email";
            this.colEmail.Name = "colEmail";

            this.colPhone.HeaderText = "Phone";
            this.colPhone.Name = "colPhone";

            this.colRole.HeaderText = "Role";
            this.colRole.Name = "colRole";

            this.colStatusCol.HeaderText = "Status";
            this.colStatusCol.Name = "colStatusCol";

            this.colCreatedAt.HeaderText = "Created At";
            this.colCreatedAt.Name = "colCreatedAt";

            this.colView.HeaderText = "View";
            this.colView.Name = "colView";
            this.colView.Text = "View";
            this.colView.UseColumnTextForButtonValue = true;

            this.colChangeRole.HeaderText = "Role";
            this.colChangeRole.Name = "colChangeRole";
            this.colChangeRole.Text = "Change Role";
            this.colChangeRole.UseColumnTextForButtonValue = true;

            this.colToggleActive.HeaderText = "Active?";
            this.colToggleActive.Name = "colToggleActive";
            this.colToggleActive.Text = "Toggle";
            this.colToggleActive.UseColumnTextForButtonValue = true;

            this.colDelete.HeaderText = "Delete";
            this.colDelete.Name = "colDelete";
            this.colDelete.Text = "Delete";
            this.colDelete.UseColumnTextForButtonValue = true;

            this.dgvUsers.Columns.AddRange(new DataGridViewColumn[]
            {
                this.colUserId, this.colFullName, this.colEmail, this.colPhone,
                this.colRole, this.colStatusCol, this.colCreatedAt,
                this.colView, this.colChangeRole, this.colToggleActive, this.colDelete
            });

            // Add controls
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblSearch);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.lblRole);
            this.Controls.Add(this.cmbRole);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.cmbStatus);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.btnRefresh);

            this.Controls.Add(this.btnBulkActivate);
            this.Controls.Add(this.btnBulkDeactivate);
            this.Controls.Add(this.btnBulkDelete);
            this.Controls.Add(this.btnClose);

            this.Controls.Add(this.dgvUsers);
        }
        #endregion
    }
}