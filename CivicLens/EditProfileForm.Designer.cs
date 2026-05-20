using System;
using System.Windows.Forms;

namespace CivicLens
{
    partial class EditProfileForm
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblTitle;

        private Label lblFullName;
        private TextBox txtFullName;

        private Label lblEmail;
        private TextBox txtEmail;

        private Label lblPhone;
        private TextBox txtPhone;

        private Label lblAddress;
        private TextBox txtAddress;

        private Label lblPassword;
        private TextBox txtPassword;
        private Button btnChangePassword;

        private Button btnSave;
        private Button btnCancel;

        // kept for backend compatibility, but hidden
        private Button btnUploadPhoto;
        private PictureBox pbAvatar;

        // promoted from local variable to field so Designer can see it
        private GroupBox grpProfile;

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

            this.lblFullName = new Label();
            this.txtFullName = new TextBox();

            this.lblEmail = new Label();
            this.txtEmail = new TextBox();

            this.lblPhone = new Label();
            this.txtPhone = new TextBox();

            this.lblAddress = new Label();
            this.txtAddress = new TextBox();

            this.lblPassword = new Label();
            this.txtPassword = new TextBox();
            this.btnChangePassword = new Button();

            this.btnSave = new Button();
            this.btnCancel = new Button();

            this.btnUploadPhoto = new Button();
            this.pbAvatar = new PictureBox();

            this.grpProfile = new GroupBox();

            // ===== Form =====
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(248, 250, 253);
            this.ClientSize = new System.Drawing.Size(720, 420);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Edit Profile - CivicLens";

            this.AcceptButton = this.btnSave;
            this.CancelButton = this.btnCancel;

            // ===== Title =====
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(24, 18);
            this.lblTitle.Text = "Edit My Profile";

            // ===== Group Box =====
            // leftLabel=24, leftText=150, widthText=480, h=28, gap=40
            // top values: FullName=36, Email=76, Phone=116, Address=156, Password=196
            // top-4 values:            32,      72,       112,         152,          192
            // widthText-168 = 312
            // leftText+widthText-160 = 150+480-160 = 470

            this.grpProfile.Text = "Profile";
            this.grpProfile.BackColor = System.Drawing.Color.White;
            this.grpProfile.Location = new System.Drawing.Point(20, 58);
            this.grpProfile.Size = new System.Drawing.Size(680, 280);
            this.grpProfile.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.grpProfile.Padding = new Padding(14);

            // ── Full Name (top=36) ──
            this.lblFullName.Location = new System.Drawing.Point(24, 36);
            this.lblFullName.Size = new System.Drawing.Size(120, 22);
            this.lblFullName.Text = "Full Name:";
            this.lblFullName.Font = new System.Drawing.Font("Segoe UI", 10F);

            this.txtFullName.Location = new System.Drawing.Point(150, 32);  // top-4=32
            this.txtFullName.Size = new System.Drawing.Size(480, 28);
            this.txtFullName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.txtFullName.BorderStyle = BorderStyle.FixedSingle;
            this.txtFullName.TabIndex = 0;

            // ── Email (top=76) ──
            this.lblEmail.Location = new System.Drawing.Point(24, 76);
            this.lblEmail.Size = new System.Drawing.Size(120, 22);
            this.lblEmail.Text = "Email:";
            this.lblEmail.Font = new System.Drawing.Font("Segoe UI", 10F);

            this.txtEmail.Location = new System.Drawing.Point(150, 72);     // top-4=72
            this.txtEmail.Size = new System.Drawing.Size(480, 28);
            this.txtEmail.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.txtEmail.BorderStyle = BorderStyle.FixedSingle;
            this.txtEmail.TabIndex = 1;

            // ── Phone (top=116) ──
            this.lblPhone.Location = new System.Drawing.Point(24, 116);
            this.lblPhone.Size = new System.Drawing.Size(120, 22);
            this.lblPhone.Text = "Phone:";
            this.lblPhone.Font = new System.Drawing.Font("Segoe UI", 10F);

            this.txtPhone.Location = new System.Drawing.Point(150, 112);    // top-4=112
            this.txtPhone.Size = new System.Drawing.Size(480, 28);
            this.txtPhone.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.txtPhone.BorderStyle = BorderStyle.FixedSingle;
            this.txtPhone.TabIndex = 2;

            // ── Address (top=156) ──
            this.lblAddress.Location = new System.Drawing.Point(24, 156);
            this.lblAddress.Size = new System.Drawing.Size(120, 22);
            this.lblAddress.Text = "Address:";
            this.lblAddress.Font = new System.Drawing.Font("Segoe UI", 10F);

            this.txtAddress.Location = new System.Drawing.Point(150, 152);  // top-4=152
            this.txtAddress.Size = new System.Drawing.Size(480, 28);
            this.txtAddress.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.txtAddress.BorderStyle = BorderStyle.FixedSingle;
            this.txtAddress.TabIndex = 3;

            // ── Password (top=196) ──
            this.lblPassword.Location = new System.Drawing.Point(24, 196);
            this.lblPassword.Size = new System.Drawing.Size(120, 22);
            this.lblPassword.Text = "Password:";
            this.lblPassword.Font = new System.Drawing.Font("Segoe UI", 10F);

            this.txtPassword.Location = new System.Drawing.Point(150, 192); // top-4=192
            this.txtPassword.Size = new System.Drawing.Size(312, 28);       // widthText-168=312
            this.txtPassword.PasswordChar = '•';
            this.txtPassword.BorderStyle = BorderStyle.FixedSingle;
            this.txtPassword.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.txtPassword.TabIndex = 4;

            this.btnChangePassword.Text = "Change Password…";
            this.btnChangePassword.Size = new System.Drawing.Size(160, 28);
            this.btnChangePassword.Location = new System.Drawing.Point(470, 192); // leftText+widthText-160=470, top-4=192
            this.btnChangePassword.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.btnChangePassword.FlatStyle = FlatStyle.Flat;
            this.btnChangePassword.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnChangePassword.TabIndex = 5;
            this.btnChangePassword.Click += new EventHandler(this.btnChangePassword_Click);

            // ── Add inputs to group ──
            this.grpProfile.Controls.Add(this.lblFullName);
            this.grpProfile.Controls.Add(this.txtFullName);
            this.grpProfile.Controls.Add(this.lblEmail);
            this.grpProfile.Controls.Add(this.txtEmail);
            this.grpProfile.Controls.Add(this.lblPhone);
            this.grpProfile.Controls.Add(this.txtPhone);
            this.grpProfile.Controls.Add(this.lblAddress);
            this.grpProfile.Controls.Add(this.txtAddress);
            this.grpProfile.Controls.Add(this.lblPassword);
            this.grpProfile.Controls.Add(this.txtPassword);
            this.grpProfile.Controls.Add(this.btnChangePassword);

            // ===== Bottom Buttons =====
            this.btnSave.Text = "Save";
            this.btnSave.Size = new System.Drawing.Size(120, 36);
            this.btnSave.Location = new System.Drawing.Point(320, 352);
            this.btnSave.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.FlatStyle = FlatStyle.Flat;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnSave.TabIndex = 6;
            this.btnSave.Click += new EventHandler(this.btnSave_Click);

            this.btnCancel.Text = "Cancel";
            this.btnCancel.Size = new System.Drawing.Size(120, 36);
            this.btnCancel.Location = new System.Drawing.Point(450, 352);
            this.btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(235, 237, 240);
            this.btnCancel.FlatStyle = FlatStyle.Flat;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);

            // ===== Hidden Photo controls =====
            this.pbAvatar.Location = new System.Drawing.Point(12, 12);
            this.pbAvatar.Size = new System.Drawing.Size(1, 1);
            this.pbAvatar.Visible = false;
            this.pbAvatar.SizeMode = PictureBoxSizeMode.Zoom;

            this.btnUploadPhoto.Text = "Upload Photo";
            this.btnUploadPhoto.Size = new System.Drawing.Size(1, 1);
            this.btnUploadPhoto.Location = new System.Drawing.Point(12, 12);
            this.btnUploadPhoto.Visible = false;
            this.btnUploadPhoto.Click += new EventHandler(this.btnUploadPhoto_Click);

            // ===== Add Controls to Form =====
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.grpProfile);
            this.Controls.Add(this.pbAvatar);
            this.Controls.Add(this.btnUploadPhoto);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
        }
        #endregion
    }
}