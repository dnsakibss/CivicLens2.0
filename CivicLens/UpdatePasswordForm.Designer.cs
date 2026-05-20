using System;
using System.Windows.Forms;

namespace CivicLens
{
    partial class UpdatePasswordForm
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblTitle;

        private GroupBox grpIdentity;
        private Label lblFullName;
        private TextBox txtFullName;
        private Label lblEmail;
        private TextBox txtEmail;
        private Label lblPhone;
        private TextBox txtPhone;
        private Label lblRole;
        private ComboBox cmbRole;
        private Label lblUsername;
        private TextBox txtUsername;

        private Label lblCurrentPassword;
        private TextBox txtCurrentPassword;

        private GroupBox grpNewPassword;
        private Label lblNewPassword;
        private TextBox txtNewPassword;
        private Label lblConfirmPassword;
        private TextBox txtConfirmPassword;
        private CheckBox chkShowNew;

        private Button btnUpdate;
        private Button btnCancel;

        private Panel panelHeader;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();

            this.panelHeader = new Panel();
            this.lblTitle = new Label();

            this.grpIdentity = new GroupBox();
            this.lblFullName = new Label();
            this.txtFullName = new TextBox();
            this.lblEmail = new Label();
            this.txtEmail = new TextBox();
            this.lblPhone = new Label();
            this.txtPhone = new TextBox();
            this.lblRole = new Label();
            this.cmbRole = new ComboBox();
            this.lblUsername = new Label();
            this.txtUsername = new TextBox();
            this.lblCurrentPassword = new Label();
            this.txtCurrentPassword = new TextBox();

            this.grpNewPassword = new GroupBox();
            this.lblNewPassword = new Label();
            this.txtNewPassword = new TextBox();
            this.lblConfirmPassword = new Label();
            this.txtConfirmPassword = new TextBox();
            this.chkShowNew = new CheckBox();

            this.btnUpdate = new Button();
            this.btnCancel = new Button();

            // ===== Form =====
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(248, 250, 253);
            this.ClientSize = new System.Drawing.Size(780, 580);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Name = "UpdatePasswordForm";
            this.Text = "Update Password";

            // ===== Header =====
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(235, 241, 250);
            this.panelHeader.Dock = DockStyle.Top;
            this.panelHeader.Height = 70;

            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(32, 56, 100);
            this.lblTitle.Location = new System.Drawing.Point(22, 20);
            this.lblTitle.Text = "Update Password";

            this.panelHeader.Controls.Add(this.lblTitle);

            // Metrics: leftL=18, leftI=180, w=520, h=28, gap=38
            // Identity top values: Row1=36, Row2=74, Row3=112, Row4=150, Row5=188, Row6=226
            // top-2:                       34,      72,       110,       148,       186,       224
            // h+2=30, leftI+392=572
            // New password: top2=34, top2-2=32, top2+gap=72, top2+gap-2=70

            // ===== Identity Group =====
            this.grpIdentity.Text = "Verify your identity";
            this.grpIdentity.Location = new System.Drawing.Point(24, 86);
            this.grpIdentity.Size = new System.Drawing.Size(732, 280);
            this.grpIdentity.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            // Row 1: Full Name (top=36)
            this.lblFullName.Location = new System.Drawing.Point(18, 36);
            this.lblFullName.Size = new System.Drawing.Size(150, 22);
            this.lblFullName.Text = "Full Name *";

            this.txtFullName.Location = new System.Drawing.Point(180, 34);   // top-2=34
            this.txtFullName.Size = new System.Drawing.Size(520, 28);
            this.txtFullName.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtFullName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            // Row 2: Email (top=74)
            this.lblEmail.Location = new System.Drawing.Point(18, 74);
            this.lblEmail.Size = new System.Drawing.Size(150, 22);
            this.lblEmail.Text = "Email *";

            this.txtEmail.Location = new System.Drawing.Point(180, 72);      // top-2=72
            this.txtEmail.Size = new System.Drawing.Size(520, 28);
            this.txtEmail.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtEmail.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            // Row 3: Phone (top=112)
            this.lblPhone.Location = new System.Drawing.Point(18, 112);
            this.lblPhone.Size = new System.Drawing.Size(150, 22);
            this.lblPhone.Text = "Phone *";

            this.txtPhone.Location = new System.Drawing.Point(180, 110);     // top-2=110
            this.txtPhone.Size = new System.Drawing.Size(520, 28);
            this.txtPhone.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtPhone.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            // Row 4: Role (top=150)
            this.lblRole.Location = new System.Drawing.Point(18, 150);
            this.lblRole.Size = new System.Drawing.Size(150, 22);
            this.lblRole.Text = "Role *";

            this.cmbRole.Location = new System.Drawing.Point(180, 148);      // top-2=148
            this.cmbRole.Size = new System.Drawing.Size(280, 30);            // h+2=30
            this.cmbRole.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbRole.Font = new System.Drawing.Font("Segoe UI", 10F);

            // Row 5: Username (top=188)
            this.lblUsername.Location = new System.Drawing.Point(18, 188);
            this.lblUsername.Size = new System.Drawing.Size(150, 22);
            this.lblUsername.Text = "Username *";

            this.txtUsername.Location = new System.Drawing.Point(180, 186);  // top-2=186
            this.txtUsername.Size = new System.Drawing.Size(380, 28);
            this.txtUsername.Font = new System.Drawing.Font("Segoe UI", 10F);

            // Row 6: Current Password (top=226) — hidden
            this.lblCurrentPassword.Location = new System.Drawing.Point(18, 226);
            this.lblCurrentPassword.Size = new System.Drawing.Size(150, 22);
            this.lblCurrentPassword.Text = "Current Password *";
            this.lblCurrentPassword.Visible = false;

            this.txtCurrentPassword.Location = new System.Drawing.Point(180, 224); // top-2=224
            this.txtCurrentPassword.Size = new System.Drawing.Size(420, 28);
            this.txtCurrentPassword.UseSystemPasswordChar = true;
            this.txtCurrentPassword.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtCurrentPassword.Visible = false;

            this.grpIdentity.Controls.AddRange(new Control[]
            {
                this.lblFullName,        this.txtFullName,
                this.lblEmail,           this.txtEmail,
                this.lblPhone,           this.txtPhone,
                this.lblRole,            this.cmbRole,
                this.lblUsername,        this.txtUsername,
                this.lblCurrentPassword, this.txtCurrentPassword
            });

            // ===== New Password Group =====
            // top2=34, top2-2=32, top2+gap=72, top2+gap-2=70, leftI+392=572
            this.grpNewPassword.Text = "Set new password";
            this.grpNewPassword.Location = new System.Drawing.Point(24, 378);
            this.grpNewPassword.Size = new System.Drawing.Size(732, 140);
            this.grpNewPassword.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            // New Password (top2=34)
            this.lblNewPassword.Location = new System.Drawing.Point(18, 34);
            this.lblNewPassword.Size = new System.Drawing.Size(150, 22);
            this.lblNewPassword.Text = "New Password *";

            this.txtNewPassword.Location = new System.Drawing.Point(180, 32);   // top2-2=32
            this.txtNewPassword.Size = new System.Drawing.Size(380, 28);
            this.txtNewPassword.UseSystemPasswordChar = true;
            this.txtNewPassword.Font = new System.Drawing.Font("Segoe UI", 10F);

            // Show passwords checkbox (inline with New Password)
            this.chkShowNew.Location = new System.Drawing.Point(572, 34);        // leftI+392=572, top2=34
            this.chkShowNew.Size = new System.Drawing.Size(150, 24);
            this.chkShowNew.Text = "Show passwords";
            this.chkShowNew.CheckedChanged += new EventHandler(this.chkShowNew_CheckedChanged);

            // Confirm Password (top2+gap=72)
            this.lblConfirmPassword.Location = new System.Drawing.Point(18, 72);
            this.lblConfirmPassword.Size = new System.Drawing.Size(160, 22);
            this.lblConfirmPassword.Text = "Confirm Password *";

            this.txtConfirmPassword.Location = new System.Drawing.Point(180, 70); // top2+gap-2=70
            this.txtConfirmPassword.Size = new System.Drawing.Size(380, 28);
            this.txtConfirmPassword.UseSystemPasswordChar = true;
            this.txtConfirmPassword.Font = new System.Drawing.Font("Segoe UI", 10F);

            this.grpNewPassword.Controls.AddRange(new Control[]
            {
                this.lblNewPassword,     this.txtNewPassword,
                this.chkShowNew,
                this.lblConfirmPassword, this.txtConfirmPassword
            });

            // ===== Buttons =====
            this.btnUpdate.Location = new System.Drawing.Point(474, 528);
            this.btnUpdate.Size = new System.Drawing.Size(120, 34);
            this.btnUpdate.Text = "Update";
            this.btnUpdate.BackColor = System.Drawing.Color.FromArgb(33, 150, 243);
            this.btnUpdate.ForeColor = System.Drawing.Color.White;
            this.btnUpdate.FlatStyle = FlatStyle.Flat;
            this.btnUpdate.FlatAppearance.BorderSize = 0;
            this.btnUpdate.Click += new EventHandler(this.btnUpdate_Click);

            this.btnCancel.Location = new System.Drawing.Point(604, 528);
            this.btnCancel.Size = new System.Drawing.Size(120, 34);
            this.btnCancel.Text = "Cancel";
            this.btnCancel.BackColor = System.Drawing.Color.White;
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(33, 37, 41);
            this.btnCancel.FlatStyle = FlatStyle.Flat;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);

            // ===== Add Controls =====
            this.Controls.Add(this.panelHeader);
            this.Controls.Add(this.grpIdentity);
            this.Controls.Add(this.grpNewPassword);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnCancel);
        }
        #endregion
    }
}