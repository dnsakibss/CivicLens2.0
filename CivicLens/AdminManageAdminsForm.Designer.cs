using System;
using System.Windows.Forms;

namespace CivicLens
{
    partial class AdminManageAdminsForm
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblTitle;

        // Left: Current Admins
        private GroupBox grpAdmins;
        private Label lblSearchAdmin;
        private TextBox txtSearchAdmin;
        private Button btnSearchAdmin;
        private Button btnRefreshAdmins;
        private Button btnDemoteSelected;
        private DataGridView dgvAdmins;

        private DataGridViewTextBoxColumn colAdminId;
        private DataGridViewTextBoxColumn colAdminName;
        private DataGridViewTextBoxColumn colAdminEmail;
        private DataGridViewTextBoxColumn colAdminPhone;
        private DataGridViewTextBoxColumn colAdminCreatedAt;
        private DataGridViewButtonColumn colAdminView;
        private DataGridViewButtonColumn colAdminDemote;

        // Right: Promote Users to Admin
        private GroupBox grpPromote;
        private Label lblUserSearch;
        private TextBox txtUserSearch;
        private Button btnSearchUsers;
        private Label lblRoleFilter;
        private ComboBox cmbRoleFilter;
        private Button btnPromoteSelected;
        private DataGridView dgvUsers;

        private DataGridViewTextBoxColumn colUserId;
        private DataGridViewTextBoxColumn colUserName;
        private DataGridViewTextBoxColumn colUserEmail;
        private DataGridViewTextBoxColumn colUserPhone;
        private DataGridViewTextBoxColumn colUserRole;
        private DataGridViewTextBoxColumn colUserStatus;
        private DataGridViewButtonColumn colUserView;
        private DataGridViewButtonColumn colUserPromote;

        private Button btnClose;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();

            this.grpAdmins = new System.Windows.Forms.GroupBox();
            this.lblSearchAdmin = new System.Windows.Forms.Label();
            this.txtSearchAdmin = new System.Windows.Forms.TextBox();
            this.btnSearchAdmin = new System.Windows.Forms.Button();
            this.btnRefreshAdmins = new System.Windows.Forms.Button();
            this.btnDemoteSelected = new System.Windows.Forms.Button();
            this.dgvAdmins = new System.Windows.Forms.DataGridView();

            this.colAdminId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAdminName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAdminEmail = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAdminPhone = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAdminCreatedAt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAdminView = new System.Windows.Forms.DataGridViewButtonColumn();
            this.colAdminDemote = new System.Windows.Forms.DataGridViewButtonColumn();

            this.grpPromote = new System.Windows.Forms.GroupBox();
            this.lblUserSearch = new System.Windows.Forms.Label();
            this.txtUserSearch = new System.Windows.Forms.TextBox();
            this.btnSearchUsers = new System.Windows.Forms.Button();
            this.lblRoleFilter = new System.Windows.Forms.Label();
            this.cmbRoleFilter = new System.Windows.Forms.ComboBox();
            this.btnPromoteSelected = new System.Windows.Forms.Button();
            this.dgvUsers = new System.Windows.Forms.DataGridView();

            this.colUserId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUserName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUserEmail = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUserPhone = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUserRole = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUserStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUserView = new System.Windows.Forms.DataGridViewButtonColumn();
            this.colUserPromote = new System.Windows.Forms.DataGridViewButtonColumn();

            this.btnClose = new System.Windows.Forms.Button();

            // ===== Form =====
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(245, 247, 250);
            this.ClientSize = new System.Drawing.Size(1360, 780);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.MaximizeBox = true;
            this.MinimizeBox = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Name = "AdminManageAdminsForm";
            this.Text = "Admin - Manage Admins";

            // Title
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(22, 16);
            this.lblTitle.Text = "Manage Admins";

            // ===== Left Group: Current Admins =====
            this.grpAdmins.Text = "Current Admins";
            this.grpAdmins.Location = new System.Drawing.Point(24, 64);
            this.grpAdmins.Size = new System.Drawing.Size(640, 650);
            this.grpAdmins.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;

            // Admins search row
            this.lblSearchAdmin.Location = new System.Drawing.Point(16, 32);
            this.lblSearchAdmin.Size = new System.Drawing.Size(60, 24);
            this.lblSearchAdmin.Text = "Search:";

            this.txtSearchAdmin.Location = new System.Drawing.Point(78, 30);
            this.txtSearchAdmin.Size = new System.Drawing.Size(300, 27);

            this.btnSearchAdmin.Location = new System.Drawing.Point(384, 28);
            this.btnSearchAdmin.Size = new System.Drawing.Size(90, 30);
            this.btnSearchAdmin.Text = "Search";

            this.btnRefreshAdmins.Location = new System.Drawing.Point(480, 28);
            this.btnRefreshAdmins.Size = new System.Drawing.Size(90, 30);
            this.btnRefreshAdmins.Text = "Refresh";
            this.btnRefreshAdmins.Click += new System.EventHandler(this.btnRefreshAdmins_Click);

            this.btnDemoteSelected.Location = new System.Drawing.Point(16, 70);
            this.btnDemoteSelected.Size = new System.Drawing.Size(190, 32);
            this.btnDemoteSelected.Text = "Demote Selected";
            this.btnDemoteSelected.Click += new System.EventHandler(this.btnDemoteSelected_Click);

            // Admins grid
            this.dgvAdmins.Location = new System.Drawing.Point(16, 112);
            this.dgvAdmins.Size = new System.Drawing.Size(608, 520);
            this.dgvAdmins.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.dgvAdmins.AllowUserToAddRows = false;
            this.dgvAdmins.AllowUserToDeleteRows = false;
            this.dgvAdmins.ReadOnly = true;
            this.dgvAdmins.RowHeadersVisible = false;
            this.dgvAdmins.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvAdmins.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAdmins.RowTemplate.Height = 26;
            this.dgvAdmins.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAdmins.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAdmins_CellContentClick);

            // Admins columns
            this.colAdminId.HeaderText = "UserId";
            this.colAdminId.Name = "colAdminId";
            this.colAdminId.Visible = false;
            this.colAdminId.FillWeight = 6F;

            this.colAdminName.HeaderText = "Full Name";
            this.colAdminName.Name = "colAdminName";
            this.colAdminName.FillWeight = 28F;

            this.colAdminEmail.HeaderText = "Email";
            this.colAdminEmail.Name = "colAdminEmail";
            this.colAdminEmail.FillWeight = 26F;

            this.colAdminPhone.HeaderText = "Phone";
            this.colAdminPhone.Name = "colAdminPhone";
            this.colAdminPhone.FillWeight = 18F;

            this.colAdminCreatedAt.HeaderText = "Admin Since";
            this.colAdminCreatedAt.Name = "colAdminCreatedAt";
            this.colAdminCreatedAt.FillWeight = 18F;

            this.colAdminView.HeaderText = "View";
            this.colAdminView.Name = "colAdminView";
            this.colAdminView.Text = "View";
            this.colAdminView.UseColumnTextForButtonValue = true;
            this.colAdminView.FillWeight = 7F;

            this.colAdminDemote.HeaderText = "Demote";
            this.colAdminDemote.Name = "colAdminDemote";
            this.colAdminDemote.Text = "Demote";
            this.colAdminDemote.UseColumnTextForButtonValue = true;
            this.colAdminDemote.FillWeight = 7F;

            this.dgvAdmins.Columns.AddRange(new DataGridViewColumn[]
            {
                this.colAdminId, this.colAdminName, this.colAdminEmail,
                this.colAdminPhone, this.colAdminCreatedAt,
                this.colAdminView, this.colAdminDemote
            });

            // Add controls to left group
            this.grpAdmins.Controls.Add(this.lblSearchAdmin);
            this.grpAdmins.Controls.Add(this.txtSearchAdmin);
            this.grpAdmins.Controls.Add(this.btnSearchAdmin);
            this.grpAdmins.Controls.Add(this.btnRefreshAdmins);
            this.grpAdmins.Controls.Add(this.btnDemoteSelected);
            this.grpAdmins.Controls.Add(this.dgvAdmins);

            // ===== Right Group: Promote Users =====
            this.grpPromote.Text = "Promote Users to Admin";
            this.grpPromote.Location = new System.Drawing.Point(688, 64);
            this.grpPromote.Size = new System.Drawing.Size(640, 650);
            this.grpPromote.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            // Users search row
            this.lblUserSearch.Location = new System.Drawing.Point(16, 32);
            this.lblUserSearch.Size = new System.Drawing.Size(60, 24);
            this.lblUserSearch.Text = "Search:";

            this.txtUserSearch.Location = new System.Drawing.Point(78, 30);
            this.txtUserSearch.Size = new System.Drawing.Size(260, 27);

            this.lblRoleFilter.Location = new System.Drawing.Point(346, 32);
            this.lblRoleFilter.Size = new System.Drawing.Size(40, 24);
            this.lblRoleFilter.Text = "Role:";

            this.cmbRoleFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbRoleFilter.Location = new System.Drawing.Point(388, 30);
            this.cmbRoleFilter.Size = new System.Drawing.Size(130, 28);

            this.btnSearchUsers.Location = new System.Drawing.Point(526, 28);
            this.btnSearchUsers.Size = new System.Drawing.Size(90, 30);
            this.btnSearchUsers.Text = "Search";

            this.btnPromoteSelected.Location = new System.Drawing.Point(16, 70);
            this.btnPromoteSelected.Size = new System.Drawing.Size(190, 32);
            this.btnPromoteSelected.Text = "Promote Selected";
            this.btnPromoteSelected.Click += new System.EventHandler(this.btnPromoteSelected_Click);

            // Users grid
            this.dgvUsers.Location = new System.Drawing.Point(16, 112);
            this.dgvUsers.Size = new System.Drawing.Size(608, 520);
            this.dgvUsers.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.dgvUsers.AllowUserToAddRows = false;
            this.dgvUsers.AllowUserToDeleteRows = false;
            this.dgvUsers.ReadOnly = true;
            this.dgvUsers.RowHeadersVisible = false;
            this.dgvUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvUsers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvUsers.RowTemplate.Height = 26;
            this.dgvUsers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUsers.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvUsers_CellContentClick);

            // Users columns
            this.colUserId.HeaderText = "UserId";
            this.colUserId.Name = "colUserId";
            this.colUserId.Visible = false;
            this.colUserId.FillWeight = 6F;

            this.colUserName.HeaderText = "Full Name";
            this.colUserName.Name = "colUserName";
            this.colUserName.FillWeight = 22F;

            this.colUserEmail.HeaderText = "Email";
            this.colUserEmail.Name = "colUserEmail";
            this.colUserEmail.FillWeight = 22F;

            this.colUserPhone.HeaderText = "Phone";
            this.colUserPhone.Name = "colUserPhone";
            this.colUserPhone.FillWeight = 14F;

            this.colUserRole.HeaderText = "Role";
            this.colUserRole.Name = "colUserRole";
            this.colUserRole.FillWeight = 12F;

            this.colUserStatus.HeaderText = "Status";
            this.colUserStatus.Name = "colUserStatus";
            this.colUserStatus.FillWeight = 12F;

            this.colUserView.HeaderText = "View";
            this.colUserView.Name = "colUserView";
            this.colUserView.Text = "View";
            this.colUserView.UseColumnTextForButtonValue = true;
            this.colUserView.FillWeight = 8F;

            this.colUserPromote.HeaderText = "Promote";
            this.colUserPromote.Name = "colUserPromote";
            this.colUserPromote.Text = "Promote";
            this.colUserPromote.UseColumnTextForButtonValue = true;
            this.colUserPromote.FillWeight = 8F;

            this.dgvUsers.Columns.AddRange(new DataGridViewColumn[]
            {
                this.colUserId, this.colUserName, this.colUserEmail, this.colUserPhone,
                this.colUserRole, this.colUserStatus, this.colUserView, this.colUserPromote
            });

            // Add controls to right group
            this.grpPromote.Controls.Add(this.lblUserSearch);
            this.grpPromote.Controls.Add(this.txtUserSearch);
            this.grpPromote.Controls.Add(this.btnSearchUsers);
            this.grpPromote.Controls.Add(this.lblRoleFilter);
            this.grpPromote.Controls.Add(this.cmbRoleFilter);
            this.grpPromote.Controls.Add(this.btnPromoteSelected);
            this.grpPromote.Controls.Add(this.dgvUsers);

            // Close button
            // ClientSize = (1360, 780) => 1360-110=1250, 780-48=732
            this.btnClose.Text = "Close";
            this.btnClose.Size = new System.Drawing.Size(90, 32);
            this.btnClose.Location = new System.Drawing.Point(1250, 732);
            this.btnClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);

            // Add to Form
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.grpAdmins);
            this.Controls.Add(this.grpPromote);
            this.Controls.Add(this.btnClose);

            // End init
            ((System.ComponentModel.ISupportInitialize)(this.dgvAdmins)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).EndInit();
        }
        #endregion
    }
}