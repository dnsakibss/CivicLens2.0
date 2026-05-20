using System;
using System.Windows.Forms;

namespace CivicLens
{
    partial class AssignComplaintForm
    {
        private System.ComponentModel.IContainer components = null;

        // Complaint summary
        private GroupBox grpComplaint;
        private Label lblIdLabel;
        private Label lblIdValue;
        private Label lblTitleLabel;
        private TextBox txtTitle;
        private Label lblCategoryLabel;
        private TextBox txtCategory;
        private Label lblPriorityLabel;
        private TextBox txtPriority;
        private Label lblStatusLabel;
        private TextBox txtStatus;
        private Label lblCreatedAtLabel;
        private TextBox txtCreatedAt;
        private Label lblReporterLabel;
        private TextBox txtReporter;
        private Label lblLocationLabel;
        private TextBox txtLocation;

        // Assignment
        private GroupBox grpAssign;
        private Label lblRole;
        private ComboBox cmbRole;
        private Label lblAssignee;
        private ComboBox cmbAssignee;
        private Label lblNote;
        private TextBox txtNote;
        private Button btnRefreshCandidates;
        private Button btnAssign;
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

            // ---------- Form ----------
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(248, 250, 253);
            this.ClientSize = new System.Drawing.Size(900, 560);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Name = "AssignComplaintForm";
            this.Text = "Assign Complaint - CivicLens";

            // L=20, LT=130, W=320, G=34, H=28
            // T starts at 34, increments by G=34 each row
            // R=460, TR=R+110=570, T2 starts at 34, increments by G=34
            // T after first +=G : T=68
            // T+G=102, T+2*G=136, T+3*G=170
            // T2: 34 -> 68 -> 102 -> 136
            // T2-2: 32 -> 66 -> 100
            // A=24, AT=140, AW=340, ATOP=40
            // ATOP-2=38, AT+210=350, ATOP+G=74, ATOP+G-2=72
            // btnTop = ATOP+24+100+12 = 176

            // ================== Complaint Summary ==================
            this.grpComplaint = new GroupBox();
            this.grpComplaint.Text = "Complaint Summary";
            this.grpComplaint.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpComplaint.Location = new System.Drawing.Point(16, 16);
            this.grpComplaint.Size = new System.Drawing.Size(868, 238);

            // Row 1: T=34
            this.lblIdLabel = new Label();
            this.lblIdLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblIdLabel.Location = new System.Drawing.Point(20, 34);
            this.lblIdLabel.Size = new System.Drawing.Size(110, 22);
            this.lblIdLabel.Text = "Complaint ID:";

            this.lblIdValue = new Label();
            this.lblIdValue.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblIdValue.Location = new System.Drawing.Point(130, 34);
            this.lblIdValue.Size = new System.Drawing.Size(160, 22);
            this.lblIdValue.Text = "#0";

            // Row 2: T=68 (after T+=G)
            this.lblTitleLabel = new Label();
            this.lblTitleLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTitleLabel.Location = new System.Drawing.Point(20, 68);
            this.lblTitleLabel.Size = new System.Drawing.Size(110, 22);
            this.lblTitleLabel.Text = "Title:";

            this.txtTitle = new TextBox();
            this.txtTitle.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtTitle.Location = new System.Drawing.Point(130, 66);    // T-2=66
            this.txtTitle.Size = new System.Drawing.Size(320, 28);
            this.txtTitle.ReadOnly = true;

            // Right column: T2=34 (Category)
            this.lblCategoryLabel = new Label();
            this.lblCategoryLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblCategoryLabel.Location = new System.Drawing.Point(460, 34);
            this.lblCategoryLabel.Size = new System.Drawing.Size(100, 22);
            this.lblCategoryLabel.Text = "Category:";

            this.txtCategory = new TextBox();
            this.txtCategory.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtCategory.Location = new System.Drawing.Point(570, 32);  // TR=570, T2-2=32
            this.txtCategory.Size = new System.Drawing.Size(260, 28);
            this.txtCategory.ReadOnly = true;

            // Right column: T2=68 (Priority)
            this.lblPriorityLabel = new Label();
            this.lblPriorityLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblPriorityLabel.Location = new System.Drawing.Point(460, 68);
            this.lblPriorityLabel.Size = new System.Drawing.Size(100, 22);
            this.lblPriorityLabel.Text = "Priority:";

            this.txtPriority = new TextBox();
            this.txtPriority.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtPriority.Location = new System.Drawing.Point(570, 66);  // TR=570, T2-2=66
            this.txtPriority.Size = new System.Drawing.Size(260, 28);
            this.txtPriority.ReadOnly = true;

            // Right column: T2=102 (Status)
            this.lblStatusLabel = new Label();
            this.lblStatusLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblStatusLabel.Location = new System.Drawing.Point(460, 102);
            this.lblStatusLabel.Size = new System.Drawing.Size(100, 22);
            this.lblStatusLabel.Text = "Status:";

            this.txtStatus = new TextBox();
            this.txtStatus.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtStatus.Location = new System.Drawing.Point(570, 100);   // TR=570, T2-2=100
            this.txtStatus.Size = new System.Drawing.Size(260, 28);
            this.txtStatus.ReadOnly = true;

            // Left column: T+G=102 (CreatedAt)
            this.lblCreatedAtLabel = new Label();
            this.lblCreatedAtLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblCreatedAtLabel.Location = new System.Drawing.Point(20, 102);
            this.lblCreatedAtLabel.Size = new System.Drawing.Size(110, 22);
            this.lblCreatedAtLabel.Text = "Created At:";

            this.txtCreatedAt = new TextBox();
            this.txtCreatedAt.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtCreatedAt.Location = new System.Drawing.Point(130, 100); // T+G-2=100
            this.txtCreatedAt.Size = new System.Drawing.Size(320, 28);
            this.txtCreatedAt.ReadOnly = true;

            // Left column: T+2*G=136 (Reporter)
            this.lblReporterLabel = new Label();
            this.lblReporterLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblReporterLabel.Location = new System.Drawing.Point(20, 136);
            this.lblReporterLabel.Size = new System.Drawing.Size(110, 22);
            this.lblReporterLabel.Text = "Reporter:";

            this.txtReporter = new TextBox();
            this.txtReporter.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtReporter.Location = new System.Drawing.Point(130, 134); // T+2*G-2=134
            this.txtReporter.Size = new System.Drawing.Size(320, 28);
            this.txtReporter.ReadOnly = true;

            // Left column: T+3*G=170 (Location)
            this.lblLocationLabel = new Label();
            this.lblLocationLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblLocationLabel.Location = new System.Drawing.Point(20, 170);
            this.lblLocationLabel.Size = new System.Drawing.Size(110, 22);
            this.lblLocationLabel.Text = "Location:";

            this.txtLocation = new TextBox();
            this.txtLocation.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtLocation.Location = new System.Drawing.Point(130, 168); // T+3*G-2=168
            this.txtLocation.Size = new System.Drawing.Size(320, 28);
            this.txtLocation.ReadOnly = true;

            this.grpComplaint.Controls.Add(this.lblIdLabel);
            this.grpComplaint.Controls.Add(this.lblIdValue);
            this.grpComplaint.Controls.Add(this.lblTitleLabel);
            this.grpComplaint.Controls.Add(this.txtTitle);
            this.grpComplaint.Controls.Add(this.lblCategoryLabel);
            this.grpComplaint.Controls.Add(this.txtCategory);
            this.grpComplaint.Controls.Add(this.lblPriorityLabel);
            this.grpComplaint.Controls.Add(this.txtPriority);
            this.grpComplaint.Controls.Add(this.lblStatusLabel);
            this.grpComplaint.Controls.Add(this.txtStatus);
            this.grpComplaint.Controls.Add(this.lblCreatedAtLabel);
            this.grpComplaint.Controls.Add(this.txtCreatedAt);
            this.grpComplaint.Controls.Add(this.lblReporterLabel);
            this.grpComplaint.Controls.Add(this.txtReporter);
            this.grpComplaint.Controls.Add(this.lblLocationLabel);
            this.grpComplaint.Controls.Add(this.txtLocation);

            // ================== Assign Group ==================
            // A=24, AT=140, AW=340, ATOP=40, G=34
            // ATOP-2=38, AT+210=350
            // ATOP+G=74, ATOP+G-2=72
            // btnTop = ATOP+24+100+12 = 176

            this.grpAssign = new GroupBox();
            this.grpAssign.Text = "Assign To";
            this.grpAssign.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpAssign.Location = new System.Drawing.Point(16, 264);
            this.grpAssign.Size = new System.Drawing.Size(868, 230);

            this.lblRole = new Label();
            this.lblRole.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblRole.Location = new System.Drawing.Point(24, 40);       // A=24, ATOP=40
            this.lblRole.Size = new System.Drawing.Size(110, 22);
            this.lblRole.Text = "Role:";

            this.cmbRole = new ComboBox();
            this.cmbRole.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbRole.Location = new System.Drawing.Point(140, 38);      // AT=140, ATOP-2=38
            this.cmbRole.Size = new System.Drawing.Size(200, 28);
            this.cmbRole.DropDownStyle = ComboBoxStyle.DropDownList;

            this.btnRefreshCandidates = new Button();
            this.btnRefreshCandidates.Location = new System.Drawing.Point(350, 38); // AT+210=350, ATOP-2=38
            this.btnRefreshCandidates.Size = new System.Drawing.Size(160, 28);
            this.btnRefreshCandidates.Text = "Refresh Candidates";
            this.btnRefreshCandidates.Click += new EventHandler(this.btnRefreshCandidates_Click);

            this.lblAssignee = new Label();
            this.lblAssignee.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblAssignee.Location = new System.Drawing.Point(24, 74);   // A=24, ATOP+G=74
            this.lblAssignee.Size = new System.Drawing.Size(110, 22);
            this.lblAssignee.Text = "Assignee:";

            this.cmbAssignee = new ComboBox();
            this.cmbAssignee.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbAssignee.Location = new System.Drawing.Point(140, 72);  // AT=140, ATOP+G-2=72
            this.cmbAssignee.Size = new System.Drawing.Size(340, 28);
            this.cmbAssignee.DropDownStyle = ComboBoxStyle.DropDownList;

            this.lblNote = new Label();
            this.lblNote.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblNote.Location = new System.Drawing.Point(520, 40);      // ATOP=40
            this.lblNote.Size = new System.Drawing.Size(110, 22);
            this.lblNote.Text = "Note:";

            this.txtNote = new TextBox();
            this.txtNote.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtNote.Location = new System.Drawing.Point(520, 64);      // ATOP+24=64
            this.txtNote.Size = new System.Drawing.Size(330, 100);
            this.txtNote.Multiline = true;
            this.txtNote.ScrollBars = ScrollBars.Vertical;

            // btnTop = ATOP+24+100+12 = 40+24+100+12 = 176
            this.btnAssign = new Button();
            this.btnAssign.Location = new System.Drawing.Point(670, 176);
            this.btnAssign.Size = new System.Drawing.Size(90, 30);
            this.btnAssign.Text = "Assign";
            this.btnAssign.Click += new EventHandler(this.btnAssign_Click);

            this.btnCancel = new Button();
            this.btnCancel.Location = new System.Drawing.Point(760, 176);
            this.btnCancel.Size = new System.Drawing.Size(90, 30);
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);

            this.grpAssign.Controls.Add(this.lblRole);
            this.grpAssign.Controls.Add(this.cmbRole);
            this.grpAssign.Controls.Add(this.lblAssignee);
            this.grpAssign.Controls.Add(this.cmbAssignee);
            this.grpAssign.Controls.Add(this.lblNote);
            this.grpAssign.Controls.Add(this.txtNote);
            this.grpAssign.Controls.Add(this.btnRefreshCandidates);
            this.grpAssign.Controls.Add(this.btnAssign);
            this.grpAssign.Controls.Add(this.btnCancel);

            // ---------- Add groups ----------
            this.Controls.Add(this.grpComplaint);
            this.Controls.Add(this.grpAssign);
        }
        #endregion
    }
}