using System;
using System.Windows.Forms;

namespace CivicLens
{
    partial class UpdateStatusForm
    {
        private System.ComponentModel.IContainer components = null;

        private Panel panelHeader;
        private Label lblHeaderTitle;

        private GroupBox grpSummary;
        private Label lblIdLabel;
        private Label lblIdValue;
        private Label lblTitleLabel;
        private TextBox txtTitle;
        private Label lblCategoryLabel;
        private TextBox txtCategory;
        private Label lblPriorityLabel;
        private TextBox txtPriority;
        private Label lblReporterLabel;
        private TextBox txtReporter;
        private Label lblCreatedAtLabel;
        private TextBox txtCreatedAt;

        private GroupBox grpStatus;
        private Label lblCurrentStatus;
        private TextBox txtCurrentStatus;
        private Label lblNewStatus;
        private ComboBox cmbNewStatus;
        private Label lblNote;
        private TextBox txtNote;
        private Label lblWhen;
        private DateTimePicker dtWhen;

        private Button btnSave;
        private Button btnCancel;

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
            this.lblHeaderTitle = new Label();

            this.grpSummary = new GroupBox();
            this.lblIdLabel = new Label();
            this.lblIdValue = new Label();
            this.lblTitleLabel = new Label();
            this.txtTitle = new TextBox();
            this.lblCategoryLabel = new Label();
            this.txtCategory = new TextBox();
            this.lblPriorityLabel = new Label();
            this.txtPriority = new TextBox();
            this.lblReporterLabel = new Label();
            this.txtReporter = new TextBox();
            this.lblCreatedAtLabel = new Label();
            this.txtCreatedAt = new TextBox();

            this.grpStatus = new GroupBox();
            this.lblCurrentStatus = new Label();
            this.txtCurrentStatus = new TextBox();
            this.lblNewStatus = new Label();
            this.cmbNewStatus = new ComboBox();
            this.lblNote = new Label();
            this.txtNote = new TextBox();
            this.lblWhen = new Label();
            this.dtWhen = new DateTimePicker();

            this.btnSave = new Button();
            this.btnCancel = new Button();

            // ===== Form =====
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(248, 250, 253);
            this.ClientSize = new System.Drawing.Size(820, 560);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Name = "UpdateStatusForm";
            this.Text = "Update Status - CivicLens";

            // ===== Header =====
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(235, 241, 250);
            this.panelHeader.Dock = DockStyle.Top;
            this.panelHeader.Height = 70;

            this.lblHeaderTitle.AutoSize = true;
            this.lblHeaderTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold);
            this.lblHeaderTitle.ForeColor = System.Drawing.Color.FromArgb(32, 56, 100);
            this.lblHeaderTitle.Location = new System.Drawing.Point(18, 18);
            this.lblHeaderTitle.Text = "Update Status";

            this.panelHeader.Controls.Add(this.lblHeaderTitle);

            // ===== Summary Group =====
            // L=20, LT=140, W=290, G=34, T=32, H=28
            // R=420, TR=R+110=530, T2=32
            // Left:  T=32, T+G=66, T+G-2=64, T+2*G=100, T+2*G-2=98
            // Right: T2=32, T2-2=30, T2+G=66, T2+G-2=64, T2+2*G=100, T2+2*G-2=98

            this.grpSummary.Text = "Complaint Summary";
            this.grpSummary.Location = new System.Drawing.Point(16, 86);
            this.grpSummary.Size = new System.Drawing.Size(788, 180);
            this.grpSummary.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            // Row 1 left: T=32
            this.lblIdLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblIdLabel.Location = new System.Drawing.Point(20, 32);
            this.lblIdLabel.Size = new System.Drawing.Size(110, 22);
            this.lblIdLabel.Text = "Complaint ID:";

            this.lblIdValue.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblIdValue.Location = new System.Drawing.Point(140, 32);
            this.lblIdValue.Size = new System.Drawing.Size(150, 22);
            this.lblIdValue.Text = "#?";

            // Row 2 left: T+G=66, T+G-2=64
            this.lblTitleLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTitleLabel.Location = new System.Drawing.Point(20, 66);
            this.lblTitleLabel.Size = new System.Drawing.Size(110, 22);
            this.lblTitleLabel.Text = "Title:";

            this.txtTitle.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtTitle.Location = new System.Drawing.Point(140, 64);        // T+G-2=64
            this.txtTitle.Size = new System.Drawing.Size(290, 28);
            this.txtTitle.ReadOnly = true;

            // Row 1 right: T2=32, T2-2=30, TR=530
            this.lblCategoryLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblCategoryLabel.Location = new System.Drawing.Point(420, 32); // R=420
            this.lblCategoryLabel.Size = new System.Drawing.Size(100, 22);
            this.lblCategoryLabel.Text = "Category:";

            this.txtCategory.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtCategory.Location = new System.Drawing.Point(530, 30);      // TR=530, T2-2=30
            this.txtCategory.Size = new System.Drawing.Size(240, 28);
            this.txtCategory.ReadOnly = true;

            // Row 2 right: T2+G=66, T2+G-2=64
            this.lblPriorityLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblPriorityLabel.Location = new System.Drawing.Point(420, 66); // R, T2+G=66
            this.lblPriorityLabel.Size = new System.Drawing.Size(100, 22);
            this.lblPriorityLabel.Text = "Priority:";

            this.txtPriority.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtPriority.Location = new System.Drawing.Point(530, 64);      // TR=530, T2+G-2=64
            this.txtPriority.Size = new System.Drawing.Size(240, 28);
            this.txtPriority.ReadOnly = true;

            // Row 3 left: T+2*G=100, T+2*G-2=98
            this.lblReporterLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblReporterLabel.Location = new System.Drawing.Point(20, 100); // T+2*G=100
            this.lblReporterLabel.Size = new System.Drawing.Size(110, 22);
            this.lblReporterLabel.Text = "Reporter:";

            this.txtReporter.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtReporter.Location = new System.Drawing.Point(140, 98);      // T+2*G-2=98
            this.txtReporter.Size = new System.Drawing.Size(290, 28);
            this.txtReporter.ReadOnly = true;

            // Row 3 right: T2+2*G=100, T2+2*G-2=98
            this.lblCreatedAtLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblCreatedAtLabel.Location = new System.Drawing.Point(420, 100); // R, T2+2*G=100
            this.lblCreatedAtLabel.Size = new System.Drawing.Size(100, 22);
            this.lblCreatedAtLabel.Text = "Created At:";

            this.txtCreatedAt.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtCreatedAt.Location = new System.Drawing.Point(530, 98);       // TR=530, T2+2*G-2=98
            this.txtCreatedAt.Size = new System.Drawing.Size(240, 28);
            this.txtCreatedAt.ReadOnly = true;

            this.grpSummary.Controls.Add(this.lblIdLabel);
            this.grpSummary.Controls.Add(this.lblIdValue);
            this.grpSummary.Controls.Add(this.lblTitleLabel);
            this.grpSummary.Controls.Add(this.txtTitle);
            this.grpSummary.Controls.Add(this.lblCategoryLabel);
            this.grpSummary.Controls.Add(this.txtCategory);
            this.grpSummary.Controls.Add(this.lblPriorityLabel);
            this.grpSummary.Controls.Add(this.txtPriority);
            this.grpSummary.Controls.Add(this.lblReporterLabel);
            this.grpSummary.Controls.Add(this.txtReporter);
            this.grpSummary.Controls.Add(this.lblCreatedAtLabel);
            this.grpSummary.Controls.Add(this.txtCreatedAt);

            // ===== Status Group =====
            // S=24, ST=170, SW=360, STOP=34, H=28
            // STOP-2=32, STOP+34=68, STOP+36=70
            // STOP+70=104, STOP+72=106
            // STOP+108=142, STOP+110=144

            this.grpStatus.Text = "Change Status";
            this.grpStatus.Location = new System.Drawing.Point(16, 276);
            this.grpStatus.Size = new System.Drawing.Size(788, 200);
            this.grpStatus.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            // Current Status (STOP=34)
            this.lblCurrentStatus.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblCurrentStatus.Location = new System.Drawing.Point(24, 34);
            this.lblCurrentStatus.Size = new System.Drawing.Size(140, 22);
            this.lblCurrentStatus.Text = "Current Status:";

            this.txtCurrentStatus.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtCurrentStatus.Location = new System.Drawing.Point(170, 32);  // ST=170, STOP-2=32
            this.txtCurrentStatus.Size = new System.Drawing.Size(180, 28);
            this.txtCurrentStatus.ReadOnly = true;

            // New Status (STOP+36=70, STOP+34=68)
            this.lblNewStatus.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblNewStatus.Location = new System.Drawing.Point(24, 70);       // STOP+36=70
            this.lblNewStatus.Size = new System.Drawing.Size(140, 22);
            this.lblNewStatus.Text = "New Status:";

            this.cmbNewStatus.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbNewStatus.Location = new System.Drawing.Point(170, 68);      // ST=170, STOP+34=68
            this.cmbNewStatus.Size = new System.Drawing.Size(180, 28);
            this.cmbNewStatus.DropDownStyle = ComboBoxStyle.DropDownList;

            // When (STOP+72=106, STOP+70=104)
            this.lblWhen.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblWhen.Location = new System.Drawing.Point(24, 106);           // STOP+72=106
            this.lblWhen.Size = new System.Drawing.Size(140, 22);
            this.lblWhen.Text = "When:";

            this.dtWhen.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtWhen.Location = new System.Drawing.Point(170, 104);           // ST=170, STOP+70=104
            this.dtWhen.Size = new System.Drawing.Size(220, 28);
            this.dtWhen.Format = DateTimePickerFormat.Custom;
            this.dtWhen.CustomFormat = "yyyy-MM-dd HH:mm";
            this.dtWhen.ShowUpDown = true;

            // Action Note (STOP+110=144, STOP+108=142)
            this.lblNote.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblNote.Location = new System.Drawing.Point(24, 144);           // STOP+110=144
            this.lblNote.Size = new System.Drawing.Size(140, 22);
            this.lblNote.Text = "Action Note:";

            this.txtNote.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtNote.Location = new System.Drawing.Point(170, 142);          // ST=170, STOP+108=142
            this.txtNote.Size = new System.Drawing.Size(360, 70);
            this.txtNote.Multiline = true;
            this.txtNote.ScrollBars = ScrollBars.Vertical;

            this.grpStatus.Controls.Add(this.lblCurrentStatus);
            this.grpStatus.Controls.Add(this.txtCurrentStatus);
            this.grpStatus.Controls.Add(this.lblNewStatus);
            this.grpStatus.Controls.Add(this.cmbNewStatus);
            this.grpStatus.Controls.Add(this.lblWhen);
            this.grpStatus.Controls.Add(this.dtWhen);
            this.grpStatus.Controls.Add(this.lblNote);
            this.grpStatus.Controls.Add(this.txtNote);

            // ===== Buttons =====
            this.btnSave.Location = new System.Drawing.Point(608, 490);
            this.btnSave.Size = new System.Drawing.Size(100, 34);
            this.btnSave.Text = "Save";
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(33, 150, 243);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.FlatStyle = FlatStyle.Flat;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.Click += new EventHandler(this.btnSave_Click);

            this.btnCancel.Location = new System.Drawing.Point(724, 490);
            this.btnCancel.Size = new System.Drawing.Size(80, 34);
            this.btnCancel.Text = "Cancel";
            this.btnCancel.BackColor = System.Drawing.Color.White;
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(33, 37, 41);
            this.btnCancel.FlatStyle = FlatStyle.Flat;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);

            // ===== Add to form =====
            this.Controls.Add(this.panelHeader);
            this.Controls.Add(this.grpSummary);
            this.Controls.Add(this.grpStatus);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
        }
        #endregion
    }
}