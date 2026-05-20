using System;
using System.Windows.Forms;
using System.Drawing;

namespace CivicLens
{
    partial class ChatForm
    {
        private System.ComponentModel.IContainer components = null;

        // Header panel
        private Panel panelHeader;
        private Label lblAppName;
        private Label lblChatTitle;
        private Label lblComplaintBadge;

        // Sub-panels inside header (declared as fields so Designer is happy)
        private Panel accentStrip;
        private Panel headerBorder;

        // Messages panel (scrollable)
        private Panel panelMessages;
        private FlowLayoutPanel flowMessages;

        // Status bar
        private Panel panelStatus;
        private Label lblOnlineStatus;
        private Label lblParticipants;
        private Panel statusTopBorder;

        // Input area
        private Panel panelInput;
        private Panel panelInputBox;
        private Panel inputTopBorder;
        private RichTextBox rtxInput;
        private Button btnSend;
        private Button btnRefresh;

        // Right sidebar
        private Panel panelSidebar;
        private Panel sidebarLeftBorder;
        private Panel sidebarDivider;
        private Panel panelComplaintCard;
        private Panel cardAccent;
        private Label lblSidebarTitle;
        private ListBox lstParticipants;
        private Label lblComplaintInfo;
        private Label lblCardId;
        private Label lblCardStatus;
        private Label lblCardCategory;
        private Label lblCardHeading;

        // Header buttons
        private Button btnClose;
        private Button btnBack;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();

            // ── FORM ──────────────────────────────────────────────
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(245, 248, 252);
            this.ClientSize = new System.Drawing.Size(1060, 680);
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Name = "ChatForm";
            this.Text = "CivicLens Chat";

            // ── HEADER ────────────────────────────────────────────
            this.panelHeader = new Panel();
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
            this.panelHeader.Dock = DockStyle.Top;
            this.panelHeader.Height = 64;

            this.accentStrip = new Panel();
            this.accentStrip.BackColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.accentStrip.Dock = DockStyle.Top;
            this.accentStrip.Height = 3;

            this.headerBorder = new Panel();
            this.headerBorder.BackColor = System.Drawing.Color.FromArgb(203, 213, 225);
            this.headerBorder.Dock = DockStyle.Bottom;
            this.headerBorder.Height = 1;

            this.panelHeader.Controls.Add(this.accentStrip);
            this.panelHeader.Controls.Add(this.headerBorder);

            this.lblAppName = new Label();
            this.lblAppName.Text = "CIVICLENS";
            this.lblAppName.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
            this.lblAppName.ForeColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.lblAppName.Location = new System.Drawing.Point(120, 14);
            this.lblAppName.AutoSize = true;

            this.lblChatTitle = new Label();
            this.lblChatTitle.Text = "Complaint Chat";
            this.lblChatTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 14f, System.Drawing.FontStyle.Bold);
            this.lblChatTitle.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.lblChatTitle.Location = new System.Drawing.Point(119, 30);
            this.lblChatTitle.AutoSize = true;

            this.lblComplaintBadge = new Label();
            this.lblComplaintBadge.Text = "#0000";
            this.lblComplaintBadge.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
            this.lblComplaintBadge.ForeColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.lblComplaintBadge.BackColor = System.Drawing.Color.FromArgb(219, 234, 254);
            this.lblComplaintBadge.Location = new System.Drawing.Point(278, 35);
            this.lblComplaintBadge.AutoSize = true;
            this.lblComplaintBadge.Padding = new Padding(6, 2, 6, 2);

            // ── BACK BUTTON ──────────────────────────────────────
            this.btnBack = new Button();
            this.btnBack.Text = "◀  Back";
            this.btnBack.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
            this.btnBack.ForeColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.btnBack.BackColor = System.Drawing.Color.FromArgb(219, 234, 254);
            this.btnBack.FlatStyle = FlatStyle.Flat;
            this.btnBack.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.btnBack.FlatAppearance.BorderSize = 1;
            this.btnBack.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(191, 219, 254);
            this.btnBack.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(147, 197, 253);
            this.btnBack.Size = new System.Drawing.Size(90, 32);
            this.btnBack.Location = new System.Drawing.Point(16, 16);
            this.btnBack.Cursor = Cursors.Hand;
            this.btnBack.Click += new EventHandler(this.btnBack_Click);

            // ── CLOSE BUTTON ─────────────────────────────────────
            // panelHeader.Width=1060 at design time; 1060-50=1010
            this.btnClose = new Button();
            this.btnClose.Text = "✕";
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 11f);
            this.btnClose.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.FlatStyle = FlatStyle.Flat;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(254, 226, 226);
            this.btnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(252, 165, 165);
            this.btnClose.Size = new System.Drawing.Size(40, 40);
            this.btnClose.Location = new System.Drawing.Point(1010, 12);
            this.btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.btnClose.Cursor = Cursors.Hand;
            this.btnClose.Click += new EventHandler(this.btnClose_Click);

            this.panelHeader.Controls.Add(this.lblAppName);
            this.panelHeader.Controls.Add(this.lblChatTitle);
            this.panelHeader.Controls.Add(this.lblComplaintBadge);
            this.panelHeader.Controls.Add(this.btnBack);
            this.panelHeader.Controls.Add(this.btnClose);

            // ── RIGHT SIDEBAR ─────────────────────────────────────
            this.panelSidebar = new Panel();
            this.panelSidebar.BackColor = System.Drawing.Color.FromArgb(250, 252, 255);
            this.panelSidebar.Dock = DockStyle.Right;
            this.panelSidebar.Width = 240;
            this.panelSidebar.Padding = new Padding(14);

            this.sidebarLeftBorder = new Panel();
            this.sidebarLeftBorder.BackColor = System.Drawing.Color.FromArgb(203, 213, 225);
            this.sidebarLeftBorder.Dock = DockStyle.Left;
            this.sidebarLeftBorder.Width = 1;

            this.panelSidebar.Controls.Add(this.sidebarLeftBorder);

            this.lblSidebarTitle = new Label();
            this.lblSidebarTitle.Text = "PARTICIPANTS";
            this.lblSidebarTitle.Font = new System.Drawing.Font("Segoe UI", 8f, System.Drawing.FontStyle.Bold);
            this.lblSidebarTitle.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblSidebarTitle.Location = new System.Drawing.Point(15, 14);
            this.lblSidebarTitle.AutoSize = true;

            this.lstParticipants = new ListBox();
            this.lstParticipants.Location = new System.Drawing.Point(15, 38);
            this.lstParticipants.Size = new System.Drawing.Size(211, 130);
            this.lstParticipants.BackColor = System.Drawing.Color.FromArgb(250, 252, 255);
            this.lstParticipants.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.lstParticipants.BorderStyle = BorderStyle.None;
            this.lstParticipants.Font = new System.Drawing.Font("Segoe UI", 9.5f);
            this.lstParticipants.SelectionMode = SelectionMode.None;

            this.sidebarDivider = new Panel();
            this.sidebarDivider.BackColor = System.Drawing.Color.FromArgb(203, 213, 225);
            this.sidebarDivider.Location = new System.Drawing.Point(15, 178);
            this.sidebarDivider.Size = new System.Drawing.Size(211, 1);

            this.lblCardHeading = new Label();
            this.lblCardHeading.Text = "COMPLAINT INFO";
            this.lblCardHeading.Font = new System.Drawing.Font("Segoe UI", 8f, System.Drawing.FontStyle.Bold);
            this.lblCardHeading.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblCardHeading.Location = new System.Drawing.Point(15, 190);
            this.lblCardHeading.AutoSize = true;

            this.panelComplaintCard = new Panel();
            this.panelComplaintCard.BackColor = System.Drawing.Color.White;
            this.panelComplaintCard.Location = new System.Drawing.Point(15, 214);
            this.panelComplaintCard.Size = new System.Drawing.Size(211, 120);
            this.panelComplaintCard.BorderStyle = BorderStyle.FixedSingle;

            this.cardAccent = new Panel();
            this.cardAccent.BackColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.cardAccent.Dock = DockStyle.Left;
            this.cardAccent.Width = 4;

            this.panelComplaintCard.Controls.Add(this.cardAccent);

            this.lblCardId = new Label();
            this.lblCardId.Text = "ID: #0000";
            this.lblCardId.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
            this.lblCardId.ForeColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.lblCardId.Location = new System.Drawing.Point(12, 10);
            this.lblCardId.AutoSize = true;

            this.lblCardStatus = new Label();
            this.lblCardStatus.Text = "Status: New";
            this.lblCardStatus.Font = new System.Drawing.Font("Segoe UI", 9f);
            this.lblCardStatus.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.lblCardStatus.Location = new System.Drawing.Point(12, 32);
            this.lblCardStatus.AutoSize = true;

            this.lblCardCategory = new Label();
            this.lblCardCategory.Text = "Category: —";
            this.lblCardCategory.Font = new System.Drawing.Font("Segoe UI", 9f);
            this.lblCardCategory.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.lblCardCategory.Location = new System.Drawing.Point(12, 54);
            this.lblCardCategory.AutoSize = true;

            this.lblComplaintInfo = new Label();
            this.lblComplaintInfo.Text = "Participants are notified\nof all messages in this\ncomplaint thread.";
            this.lblComplaintInfo.Font = new System.Drawing.Font("Segoe UI", 8.5f);
            this.lblComplaintInfo.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblComplaintInfo.Location = new System.Drawing.Point(12, 76);
            this.lblComplaintInfo.Size = new System.Drawing.Size(188, 50);

            this.panelComplaintCard.Controls.Add(this.lblCardId);
            this.panelComplaintCard.Controls.Add(this.lblCardStatus);
            this.panelComplaintCard.Controls.Add(this.lblCardCategory);
            this.panelComplaintCard.Controls.Add(this.lblComplaintInfo);

            this.panelSidebar.Controls.Add(this.lblSidebarTitle);
            this.panelSidebar.Controls.Add(this.lstParticipants);
            this.panelSidebar.Controls.Add(this.sidebarDivider);
            this.panelSidebar.Controls.Add(this.lblCardHeading);
            this.panelSidebar.Controls.Add(this.panelComplaintCard);

            // ── STATUS BAR ────────────────────────────────────────
            this.panelStatus = new Panel();
            this.panelStatus.BackColor = System.Drawing.Color.FromArgb(240, 245, 252);
            this.panelStatus.Dock = DockStyle.Bottom;
            this.panelStatus.Height = 32;

            this.statusTopBorder = new Panel();
            this.statusTopBorder.BackColor = System.Drawing.Color.FromArgb(203, 213, 225);
            this.statusTopBorder.Dock = DockStyle.Top;
            this.statusTopBorder.Height = 1;

            this.panelStatus.Controls.Add(this.statusTopBorder);

            this.lblOnlineStatus = new Label();
            this.lblOnlineStatus.Text = "● Connected";
            this.lblOnlineStatus.Font = new System.Drawing.Font("Segoe UI", 8.5f);
            this.lblOnlineStatus.ForeColor = System.Drawing.Color.FromArgb(22, 163, 74);
            this.lblOnlineStatus.Location = new System.Drawing.Point(16, 8);
            this.lblOnlineStatus.AutoSize = true;

            this.lblParticipants = new Label();
            this.lblParticipants.Text = "Auto-refresh every 10s";
            this.lblParticipants.Font = new System.Drawing.Font("Segoe UI", 8.5f);
            this.lblParticipants.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblParticipants.Location = new System.Drawing.Point(130, 8);
            this.lblParticipants.AutoSize = true;

            this.panelStatus.Controls.Add(this.lblOnlineStatus);
            this.panelStatus.Controls.Add(this.lblParticipants);

            // ── INPUT AREA ────────────────────────────────────────
            this.panelInput = new Panel();
            this.panelInput.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
            this.panelInput.Dock = DockStyle.Bottom;
            this.panelInput.Height = 90;
            this.panelInput.Padding = new Padding(16, 12, 16, 12);

            this.inputTopBorder = new Panel();
            this.inputTopBorder.BackColor = System.Drawing.Color.FromArgb(203, 213, 225);
            this.inputTopBorder.Dock = DockStyle.Top;
            this.inputTopBorder.Height = 1;

            this.panelInput.Controls.Add(this.inputTopBorder);

            this.panelInputBox = new Panel();
            this.panelInputBox.BackColor = System.Drawing.Color.White;
            this.panelInputBox.Location = new System.Drawing.Point(16, 12);
            this.panelInputBox.Size = new System.Drawing.Size(590, 66);
            this.panelInputBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.panelInputBox.BorderStyle = BorderStyle.FixedSingle;

            this.rtxInput = new RichTextBox();
            this.rtxInput.BackColor = System.Drawing.Color.White;
            this.rtxInput.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.rtxInput.BorderStyle = BorderStyle.None;
            this.rtxInput.Font = new System.Drawing.Font("Segoe UI", 10.5f);
            this.rtxInput.Dock = DockStyle.Fill;
            this.rtxInput.Multiline = true;
            this.rtxInput.ScrollBars = RichTextBoxScrollBars.None;
            this.rtxInput.Padding = new Padding(10, 8, 10, 8);
            this.rtxInput.KeyDown += new KeyEventHandler(this.rtxInput_KeyDown);

            this.panelInputBox.Controls.Add(this.rtxInput);

            this.btnSend = new Button();
            this.btnSend.Text = "SEND  ➤";
            this.btnSend.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
            this.btnSend.ForeColor = System.Drawing.Color.White;
            this.btnSend.BackColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.btnSend.FlatStyle = FlatStyle.Flat;
            this.btnSend.FlatAppearance.BorderSize = 0;
            this.btnSend.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(29, 78, 216);
            this.btnSend.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(30, 64, 175);
            this.btnSend.Size = new System.Drawing.Size(110, 66);
            this.btnSend.Cursor = Cursors.Hand;
            this.btnSend.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.btnSend.Click += new EventHandler(this.btnSend_Click);

            this.btnRefresh = new Button();
            this.btnRefresh.Text = "↻";
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 13f);
            this.btnRefresh.ForeColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.btnRefresh.BackColor = System.Drawing.Color.Transparent;
            this.btnRefresh.FlatStyle = FlatStyle.Flat;
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(219, 234, 254);
            this.btnRefresh.Size = new System.Drawing.Size(40, 40);
            this.btnRefresh.Cursor = Cursors.Hand;
            this.btnRefresh.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.btnRefresh.Click += new EventHandler(this.btnRefresh_Click);

            this.panelInput.Controls.Add(this.panelInputBox);
            this.panelInput.Controls.Add(this.btnSend);
            this.panelInput.Controls.Add(this.btnRefresh);

            // ── MESSAGES AREA ─────────────────────────────────────
            this.panelMessages = new Panel();
            this.panelMessages.BackColor = System.Drawing.Color.FromArgb(245, 248, 252);
            this.panelMessages.Dock = DockStyle.Fill;
            this.panelMessages.Padding = new Padding(16, 16, 16, 8);
            this.panelMessages.AutoScroll = true;

            this.flowMessages = new FlowLayoutPanel();
            this.flowMessages.Dock = DockStyle.Fill;
            this.flowMessages.FlowDirection = FlowDirection.TopDown;
            this.flowMessages.WrapContents = false;
            this.flowMessages.AutoScroll = true;
            this.flowMessages.BackColor = System.Drawing.Color.FromArgb(245, 248, 252);
            this.flowMessages.Padding = new Padding(8, 8, 8, 8);

            this.panelMessages.Controls.Add(this.flowMessages);

            // ── ADD TO FORM ───────────────────────────────────────
            this.Controls.Add(this.panelMessages);
            this.Controls.Add(this.panelSidebar);
            this.Controls.Add(this.panelInput);
            this.Controls.Add(this.panelStatus);
            this.Controls.Add(this.panelHeader);

            // NOTE: The Resize lambdas and MouseDown drag handler are moved
            // to the backend (.cs) file's constructor or Load event,
            // as lambda event handlers are not supported by the Designer.
        }
        #endregion
    }

    // Helper for borderless drag
    internal static class NativeMethods
    {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
    }
}