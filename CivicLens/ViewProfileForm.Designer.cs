using System;
using System.Windows.Forms;

namespace CivicLens
{
    partial class ViewProfileForm
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

        private Label lblRole;
        private TextBox txtRole;

        private Label lblApproval;
        private TextBox txtApproval;

        private Label lblCreatedAt;
        private TextBox txtCreatedAt;

        private Label lblApprovedAt;
        private TextBox txtApprovedAt;

        private Button btnRefresh;
        private Button btnEdit;
        private Button btnClose;

        // promoted from local variable so Designer can see it
        private GroupBox grpDetails;

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

            this.lblRole = new Label();
            this.txtRole = new TextBox();

            this.lblApproval = new Label();
            this.txtApproval = new TextBox();

            this.lblCreatedAt = new Label();
            this.txtCreatedAt = new TextBox();

            this.lblApprovedAt = new Label();
            this.txtApprovedAt = new TextBox();

            this.btnRefresh = new Button();
            this.btnEdit = new Button();
            this.btnClose = new Button();

            this.grpDetails = new GroupBox();

            // ===== Form =====
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(248, 250, 253);
            this.ClientSize = new System.Drawing.Size(720, 460);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ViewProfileForm";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Profile - CivicLens";
            this.CancelButton = this.btnClose;

            // ===== Title =====
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(24, 18);
            this.lblTitle.Text = "My Profile";

            // ===== Group Box =====
            // leftLabel=24, leftText=150, widthText=480, h=28, gap=40
            // top values: FullName=36, Email=76, Phone=116, Address=156, Role=196, CreatedAt=236
            // top-4:                   32,       72,        112,          152,       192,          232
            // leftText+200=350, leftText+280=430, leftText+300=450

            this.grpDetails.Text = "Details";
            this.grpDetails.BackColor = System.Drawing.Color.White;
            this.grpDetails.Location = new System.Drawing.Point(20, 58);
            this.grpDetails.Size = new System.Drawing.Size(680, 320);
            this.grpDetails.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.grpDetails.Padding = new Padding(14);

            // ── Full Name (top=36) ──
            this.lblFullName.Location = new System.Drawing.Point(24, 36);
            this.lblFullName.Size = new System.Drawing.Size(120, 22);
            this.lblFullName.Text = "Full Name:";
            this.lblFullName.Font = new System.Drawing.Font("Segoe UI", 10F);

            this.txtFullName.Location = new System.Drawing.Point(150, 32);   // top-4=32
            this.txtFullName.Size = new System.Drawing.Size(480, 28);
            this.txtFullName.ReadOnly = true;
            this.txtFullName.BorderStyle = BorderStyle.FixedSingle;
            this.txtFullName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            // ── Email (top=76) ──
            this.lblEmail.Location = new System.Drawing.Point(24, 76);
            this.lblEmail.Size = new System.Drawing.Size(120, 22);
            this.lblEmail.Text = "Email:";
            this.lblEmail.Font = new System.Drawing.Font("Segoe UI", 10F);

            this.txtEmail.Location = new System.Drawing.Point(150, 72);      // top-4=72
            this.txtEmail.Size = new System.Drawing.Size(480, 28);
            this.txtEmail.ReadOnly = true;
            this.txtEmail.BorderStyle = BorderStyle.FixedSingle;
            this.txtEmail.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            // ── Phone (top=116) ──
            this.lblPhone.Location = new System.Drawing.Point(24, 116);
            this.lblPhone.Size = new System.Drawing.Size(120, 22);
            this.lblPhone.Text = "Phone:";
            this.lblPhone.Font = new System.Drawing.Font("Segoe UI", 10F);

            this.txtPhone.Location = new System.Drawing.Point(150, 112);     // top-4=112
            this.txtPhone.Size = new System.Drawing.Size(480, 28);
            this.txtPhone.ReadOnly = true;
            this.txtPhone.BorderStyle = BorderStyle.FixedSingle;
            this.txtPhone.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            // ── Address (top=156) ──
            this.lblAddress.Location = new System.Drawing.Point(24, 156);
            this.lblAddress.Size = new System.Drawing.Size(120, 22);
            this.lblAddress.Text = "Address:";
            this.lblAddress.Font = new System.Drawing.Font("Segoe UI", 10F);

            this.txtAddress.Location = new System.Drawing.Point(150, 152);   // top-4=152
            this.txtAddress.Size = new System.Drawing.Size(480, 28);
            this.txtAddress.ReadOnly = true;
            this.txtAddress.BorderStyle = BorderStyle.FixedSingle;
            this.txtAddress.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            // ── Role + Approval inline (top=196) ──
            this.lblRole.Location = new System.Drawing.Point(24, 196);
            this.lblRole.Size = new System.Drawing.Size(120, 22);
            this.lblRole.Text = "Role:";
            this.lblRole.Font = new System.Drawing.Font("Segoe UI", 10F);

            this.txtRole.Location = new System.Drawing.Point(150, 192);      // top-4=192
            this.txtRole.Size = new System.Drawing.Size(190, 28);
            this.txtRole.ReadOnly = true;
            this.txtRole.BorderStyle = BorderStyle.FixedSingle;

            this.lblApproval.Location = new System.Drawing.Point(350, 196);  // leftText+200=350
            this.lblApproval.Size = new System.Drawing.Size(80, 22);
            this.lblApproval.Text = "Approval:";
            this.lblApproval.Font = new System.Drawing.Font("Segoe UI", 10F);

            this.txtApproval.Location = new System.Drawing.Point(430, 192);  // leftText+280=430, top-4=192
            this.txtApproval.Size = new System.Drawing.Size(190, 28);
            this.txtApproval.ReadOnly = true;
            this.txtApproval.BorderStyle = BorderStyle.FixedSingle;

            // ── Created At + Approved At inline (top=236) ──
            this.lblCreatedAt.Location = new System.Drawing.Point(24, 236);
            this.lblCreatedAt.Size = new System.Drawing.Size(120, 22);
            this.lblCreatedAt.Text = "Created At:";
            this.lblCreatedAt.Font = new System.Drawing.Font("Segoe UI", 10F);

            this.txtCreatedAt.Location = new System.Drawing.Point(150, 232); // top-4=232
            this.txtCreatedAt.Size = new System.Drawing.Size(190, 28);
            this.txtCreatedAt.ReadOnly = true;
            this.txtCreatedAt.BorderStyle = BorderStyle.FixedSingle;

            this.lblApprovedAt.Location = new System.Drawing.Point(350, 236); // leftText+200=350
            this.lblApprovedAt.Size = new System.Drawing.Size(100, 22);
            this.lblApprovedAt.Text = "Approved At:";
            this.lblApprovedAt.Font = new System.Drawing.Font("Segoe UI", 10F);

            this.txtApprovedAt.Location = new System.Drawing.Point(450, 232); // leftText+300=450, top-4=232
            this.txtApprovedAt.Size = new System.Drawing.Size(170, 28);
            this.txtApprovedAt.ReadOnly = true;
            this.txtApprovedAt.BorderStyle = BorderStyle.FixedSingle;

            // Add to group
            this.grpDetails.Controls.Add(this.lblFullName);
            this.grpDetails.Controls.Add(this.txtFullName);
            this.grpDetails.Controls.Add(this.lblEmail);
            this.grpDetails.Controls.Add(this.txtEmail);
            this.grpDetails.Controls.Add(this.lblPhone);
            this.grpDetails.Controls.Add(this.txtPhone);
            this.grpDetails.Controls.Add(this.lblAddress);
            this.grpDetails.Controls.Add(this.txtAddress);
            this.grpDetails.Controls.Add(this.lblRole);
            this.grpDetails.Controls.Add(this.txtRole);
            this.grpDetails.Controls.Add(this.lblApproval);
            this.grpDetails.Controls.Add(this.txtApproval);
            this.grpDetails.Controls.Add(this.lblCreatedAt);
            this.grpDetails.Controls.Add(this.txtCreatedAt);
            this.grpDetails.Controls.Add(this.lblApprovedAt);
            this.grpDetails.Controls.Add(this.txtApprovedAt);

            // ===== Bottom Buttons =====
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.Size = new System.Drawing.Size(110, 36);
            this.btnRefresh.Location = new System.Drawing.Point(360, 390);
            this.btnRefresh.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            this.btnRefresh.FlatStyle = FlatStyle.Flat;
            this.btnRefresh.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnRefresh.Click += new EventHandler(this.btnRefresh_Click);

            this.btnEdit.Text = "Edit Profile";
            this.btnEdit.Size = new System.Drawing.Size(120, 36);
            this.btnEdit.Location = new System.Drawing.Point(476, 390);
            this.btnEdit.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            this.btnEdit.BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            this.btnEdit.ForeColor = System.Drawing.Color.White;
            this.btnEdit.FlatStyle = FlatStyle.Flat;
            this.btnEdit.FlatAppearance.BorderSize = 0;
            this.btnEdit.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnEdit.Click += new EventHandler(this.btnEdit_Click);

            this.btnClose.Text = "Close";
            this.btnClose.Size = new System.Drawing.Size(110, 36);
            this.btnClose.Location = new System.Drawing.Point(598, 390);
            this.btnClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            this.btnClose.FlatStyle = FlatStyle.Flat;
            this.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnClose.Click += new EventHandler(this.btnClose_Click);

            // ===== Add to Form =====
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.grpDetails);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnClose);
        }
        #endregion
    }
}