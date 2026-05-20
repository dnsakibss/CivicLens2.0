using System;
using System.Windows.Forms;

namespace CivicLens
{
    partial class ComplaintDetailForm
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblHeader;

        private Label lblTitle;
        private TextBox txtTitle;

        private Label lblCategory;
        private TextBox txtCategory;

        private Label lblPriority;
        private TextBox txtPriority;

        private Label lblStatus;
        private TextBox txtStatus;

        private Label lblCreatedAt;
        private TextBox txtCreatedAt;

        private Label lblLocation;
        private TextBox txtDistrict;
        private TextBox txtCity;
        private TextBox txtArea;

        private Label lblDescription;
        private TextBox txtDescription;

        private GroupBox grpMedia;
        private ListView lvMedia;
        private ColumnHeader colPath;
        private ColumnHeader colType;
        private ColumnHeader colPrimary;
        private PictureBox pbPreview;
        private Label lblPreviewNote;

        private GroupBox grpTimeline;
        private ListBox lstTimeline;

        private Button btnRefresh;
        private Button btnSave;
        private Button btnClose;
        private Button btnChat;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();

            this.lblHeader = new Label();
            this.lblTitle = new Label();
            this.txtTitle = new TextBox();
            this.lblCategory = new Label();
            this.txtCategory = new TextBox();
            this.lblPriority = new Label();
            this.txtPriority = new TextBox();
            this.lblStatus = new Label();
            this.txtStatus = new TextBox();
            this.lblCreatedAt = new Label();
            this.txtCreatedAt = new TextBox();
            this.lblLocation = new Label();
            this.txtDistrict = new TextBox();
            this.txtCity = new TextBox();
            this.txtArea = new TextBox();
            this.lblDescription = new Label();
            this.txtDescription = new TextBox();
            this.grpMedia = new GroupBox();
            this.lvMedia = new ListView();
            this.colPath = new ColumnHeader();
            this.colType = new ColumnHeader();
            this.colPrimary = new ColumnHeader();
            this.pbPreview = new PictureBox();
            this.lblPreviewNote = new Label();
            this.grpTimeline = new GroupBox();
            this.lstTimeline = new ListBox();
            this.btnRefresh = new Button();
            this.btnSave = new Button();
            this.btnClose = new Button();
            this.btnChat = new Button();

            // ===== Form =====
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(248, 250, 253);
            this.ClientSize = new System.Drawing.Size(980, 650);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Complaint Detail - CivicLens";

            // ===== Header =====
            this.lblHeader.AutoSize = true;
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold);
            this.lblHeader.ForeColor = System.Drawing.Color.FromArgb(32, 56, 100);
            this.lblHeader.Location = new System.Drawing.Point(20, 16);
            this.lblHeader.Text = "Complaint Details";

            // leftLabel=22, leftText=140, wText=340, gap=34, H=28
            // top values per row: Title=86, Category/Priority=120, Status/CreatedAt=154, Location=188, Description=222
            // top-2 for textboxes: 84, 118, 152, 186, 220
            // leftText+180=320, leftText+245=385, leftText+265=405
            // leftText+125=265, leftText+250=390

            // ===== Title (top=86) =====
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTitle.Location = new System.Drawing.Point(22, 86);
            this.lblTitle.Size = new System.Drawing.Size(110, 22);
            this.lblTitle.Text = "Title:";

            this.txtTitle.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtTitle.Location = new System.Drawing.Point(140, 84);    // top-2=84
            this.txtTitle.Size = new System.Drawing.Size(340, 28);
            this.txtTitle.ReadOnly = true;

            // ===== Category + Priority (top=120) =====
            this.lblCategory.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblCategory.Location = new System.Drawing.Point(22, 120);
            this.lblCategory.Size = new System.Drawing.Size(110, 22);
            this.lblCategory.Text = "Category:";

            this.txtCategory.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtCategory.Location = new System.Drawing.Point(140, 118); // top-2=118
            this.txtCategory.Size = new System.Drawing.Size(155, 28);
            this.txtCategory.ReadOnly = true;

            this.lblPriority.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblPriority.Location = new System.Drawing.Point(320, 120); // leftText+180=320
            this.lblPriority.Size = new System.Drawing.Size(65, 22);
            this.lblPriority.Text = "Priority:";

            this.txtPriority.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtPriority.Location = new System.Drawing.Point(385, 118); // leftText+245=385, top-2=118
            this.txtPriority.Size = new System.Drawing.Size(110, 28);
            this.txtPriority.ReadOnly = true;

            // ===== Status + CreatedAt (top=154) =====
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblStatus.Location = new System.Drawing.Point(22, 154);
            this.lblStatus.Size = new System.Drawing.Size(110, 22);
            this.lblStatus.Text = "Status:";

            this.txtStatus.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtStatus.Location = new System.Drawing.Point(140, 152);   // top-2=152
            this.txtStatus.Size = new System.Drawing.Size(160, 28);
            this.txtStatus.ReadOnly = true;

            this.lblCreatedAt.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblCreatedAt.Location = new System.Drawing.Point(320, 154); // leftText+180=320
            this.lblCreatedAt.Size = new System.Drawing.Size(85, 22);
            this.lblCreatedAt.Text = "Created At:";

            this.txtCreatedAt.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtCreatedAt.Location = new System.Drawing.Point(405, 152); // leftText+265=405, top-2=152
            this.txtCreatedAt.Size = new System.Drawing.Size(140, 28);
            this.txtCreatedAt.ReadOnly = true;

            // ===== Location (top=188) =====
            this.lblLocation.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblLocation.Location = new System.Drawing.Point(22, 188);
            this.lblLocation.Size = new System.Drawing.Size(110, 22);
            this.lblLocation.Text = "Location:";

            this.txtDistrict.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtDistrict.Location = new System.Drawing.Point(140, 186);  // top-2=186
            this.txtDistrict.Size = new System.Drawing.Size(115, 28);
            this.txtDistrict.ReadOnly = true;

            this.txtCity.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtCity.Location = new System.Drawing.Point(265, 186);      // leftText+125=265, top-2=186
            this.txtCity.Size = new System.Drawing.Size(115, 28);
            this.txtCity.ReadOnly = true;

            this.txtArea.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtArea.Location = new System.Drawing.Point(390, 186);      // leftText+250=390, top-2=186
            this.txtArea.Size = new System.Drawing.Size(115, 28);
            this.txtArea.ReadOnly = true;

            // ===== Description (top=222) =====
            this.lblDescription.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblDescription.Location = new System.Drawing.Point(22, 222);
            this.lblDescription.Size = new System.Drawing.Size(110, 22);
            this.lblDescription.Text = "Description:";

            this.txtDescription.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtDescription.Location = new System.Drawing.Point(140, 220); // top-2=220
            this.txtDescription.Size = new System.Drawing.Size(340, 120);
            this.txtDescription.ReadOnly = true;
            this.txtDescription.Multiline = true;
            this.txtDescription.ScrollBars = ScrollBars.Vertical;

            // ===== Media =====
            this.grpMedia.Text = "Media";
            this.grpMedia.Location = new System.Drawing.Point(620, 86);
            this.grpMedia.Size = new System.Drawing.Size(340, 300);

            this.lvMedia.Location = new System.Drawing.Point(14, 26);
            this.lvMedia.Size = new System.Drawing.Size(312, 120);
            this.lvMedia.View = View.Details;
            this.lvMedia.FullRowSelect = true;
            this.lvMedia.GridLines = true;
            this.lvMedia.MultiSelect = false;
            this.lvMedia.HideSelection = false;
            this.lvMedia.SelectedIndexChanged += new EventHandler(this.lvMedia_SelectedIndexChanged);

            this.colPath.Text = "File Path";
            this.colPath.Width = 190;
            this.colType.Text = "Type";
            this.colType.Width = 60;
            this.colPrimary.Text = "Primary";
            this.colPrimary.Width = 60;
            this.lvMedia.Columns.AddRange(new ColumnHeader[] { colPath, colType, colPrimary });

            this.pbPreview.Location = new System.Drawing.Point(14, 154);
            this.pbPreview.Size = new System.Drawing.Size(312, 100);
            this.pbPreview.BorderStyle = BorderStyle.FixedSingle;
            this.pbPreview.SizeMode = PictureBoxSizeMode.Zoom;

            this.lblPreviewNote.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblPreviewNote.ForeColor = System.Drawing.Color.FromArgb(108, 117, 125);
            this.lblPreviewNote.Location = new System.Drawing.Point(14, 258);
            this.lblPreviewNote.Size = new System.Drawing.Size(312, 30);
            this.lblPreviewNote.AutoEllipsis = true;
            this.lblPreviewNote.Text = "If selected item is a video, preview is not shown. Open file externally.";

            this.grpMedia.Controls.Add(this.lvMedia);
            this.grpMedia.Controls.Add(this.pbPreview);
            this.grpMedia.Controls.Add(this.lblPreviewNote);

            // ===== Timeline =====
            this.grpTimeline.Text = "Timeline / Notes";
            this.grpTimeline.Location = new System.Drawing.Point(20, 410);
            this.grpTimeline.Size = new System.Drawing.Size(940, 180);

            this.lstTimeline.Location = new System.Drawing.Point(16, 26);
            this.lstTimeline.Size = new System.Drawing.Size(908, 140);
            this.grpTimeline.Controls.Add(this.lstTimeline);

            // ===== Buttons (btnY=605) =====
            this.btnChat.Text = "💬 Chat";
            this.btnChat.Location = new System.Drawing.Point(580, 605);
            this.btnChat.Size = new System.Drawing.Size(100, 32);
            this.btnChat.BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            this.btnChat.ForeColor = System.Drawing.Color.White;
            this.btnChat.FlatStyle = FlatStyle.Flat;
            this.btnChat.FlatAppearance.BorderSize = 0;
            this.btnChat.Click += new EventHandler(this.btnChat_Click);

            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.Location = new System.Drawing.Point(680, 605);
            this.btnRefresh.Size = new System.Drawing.Size(100, 32);
            this.btnRefresh.Click += new EventHandler(this.btnRefresh_Click);

            this.btnSave.Name = "btnSave";
            this.btnSave.Text = "Save";
            this.btnSave.Location = new System.Drawing.Point(785, 605);
            this.btnSave.Size = new System.Drawing.Size(90, 32);
            this.btnSave.Click += new EventHandler(this.btnSave_Click);
            this.btnSave.Visible = false;

            this.btnClose.Text = "Close";
            this.btnClose.Location = new System.Drawing.Point(880, 605);
            this.btnClose.Size = new System.Drawing.Size(80, 32);
            this.btnClose.Click += new EventHandler(this.btnClose_Click);

            // ===== Add controls =====
            this.Controls.Add(this.lblHeader);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.txtTitle);
            this.Controls.Add(this.lblCategory);
            this.Controls.Add(this.txtCategory);
            this.Controls.Add(this.lblPriority);
            this.Controls.Add(this.txtPriority);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.txtStatus);
            this.Controls.Add(this.lblCreatedAt);
            this.Controls.Add(this.txtCreatedAt);
            this.Controls.Add(this.lblLocation);
            this.Controls.Add(this.txtDistrict);
            this.Controls.Add(this.txtCity);
            this.Controls.Add(this.txtArea);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.grpMedia);
            this.Controls.Add(this.grpTimeline);
            this.Controls.Add(this.btnChat);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnClose);
        }
        #endregion
    }
}