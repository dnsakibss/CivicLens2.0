// ============================================================
// DashboardForm.Designer.cs  –  CivicLens 2.0
// Updated: added btnNewsfeed to the sidebar (visible to all roles).
// ============================================================
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace CivicLens
{
    partial class DashboardForm
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblAppTitle;
        private Label lblWelcome;
        private Label lblRole;
        private GroupBox grpShared;
        private GroupBox grpAdmin;
        private GroupBox grpModerator;
        private GroupBox grpPolice;
        private GroupBox grpJournalist;
        private GroupBox grpCitizen;
        private Button btnViewProfile;
        private Button btnEditProfile;
        private Button btnLogout;
        private Button btnNewsfeed;         // ← NEW: Newsfeed for all roles
        private Button btnUserApprovals;
        private Button btnManageUsers;
        private Button btnManageAdmins;
        private Button btnCategories;
        private Button btnLocations;
        private Button btnModeratorQueue;
        private Button btnPoliceAssigned;
        private Button btnJournalistFeed;
        private Button btnSubmitComplaint;
        private Button btnMyComplaints;
        private Panel panelHeader;
        private Panel panelSidebar;
        private Panel panelContent;
        private Panel panelStatusBar;
        private Panel cardPanelAdmin;
        private Panel cardPanelModerator;
        private Panel cardPanelPolice;
        private Panel cardPanelJournalist;
        private Panel cardPanelCitizen;
        private Label lblPageTitle;
        private Label lblPageSub;
        private Panel sepLine;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblAppTitle = new System.Windows.Forms.Label();
            this.lblWelcome = new System.Windows.Forms.Label();
            this.lblRole = new System.Windows.Forms.Label();
            this.grpShared = new System.Windows.Forms.GroupBox();
            this.grpAdmin = new System.Windows.Forms.GroupBox();
            this.grpModerator = new System.Windows.Forms.GroupBox();
            this.grpPolice = new System.Windows.Forms.GroupBox();
            this.grpJournalist = new System.Windows.Forms.GroupBox();
            this.grpCitizen = new System.Windows.Forms.GroupBox();
            this.btnViewProfile = new System.Windows.Forms.Button();
            this.btnEditProfile = new System.Windows.Forms.Button();
            this.btnNewsfeed = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.btnUserApprovals = new System.Windows.Forms.Button();
            this.btnManageUsers = new System.Windows.Forms.Button();
            this.btnManageAdmins = new System.Windows.Forms.Button();
            this.btnCategories = new System.Windows.Forms.Button();
            this.btnLocations = new System.Windows.Forms.Button();
            this.btnModeratorQueue = new System.Windows.Forms.Button();
            this.btnPoliceAssigned = new System.Windows.Forms.Button();
            this.btnJournalistFeed = new System.Windows.Forms.Button();
            this.btnSubmitComplaint = new System.Windows.Forms.Button();
            this.btnMyComplaints = new System.Windows.Forms.Button();
            this.panelSidebar = new System.Windows.Forms.Panel();
            this.sepLine = new System.Windows.Forms.Panel();
            this.panelContent = new System.Windows.Forms.Panel();
            this.lblPageTitle = new System.Windows.Forms.Label();
            this.lblPageSub = new System.Windows.Forms.Label();
            this.cardPanelAdmin = new System.Windows.Forms.Panel();
            this.cardPanelModerator = new System.Windows.Forms.Panel();
            this.cardPanelPolice = new System.Windows.Forms.Panel();
            this.cardPanelJournalist = new System.Windows.Forms.Panel();
            this.cardPanelCitizen = new System.Windows.Forms.Panel();
            this.panelStatusBar = new System.Windows.Forms.Panel();
            this.panelHeader.SuspendLayout();
            this.panelSidebar.SuspendLayout();
            this.panelContent.SuspendLayout();
            this.cardPanelAdmin.SuspendLayout();
            this.cardPanelModerator.SuspendLayout();
            this.cardPanelPolice.SuspendLayout();
            this.cardPanelJournalist.SuspendLayout();
            this.cardPanelCitizen.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(78)))), ((int)(((byte)(148)))));
            this.panelHeader.Controls.Add(this.lblAppTitle);
            this.panelHeader.Controls.Add(this.lblWelcome);
            this.panelHeader.Controls.Add(this.lblRole);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(674, 70);
            this.panelHeader.TabIndex = 2;
            // 
            // lblAppTitle
            // 
            this.lblAppTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblAppTitle.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.lblAppTitle.ForeColor = System.Drawing.Color.White;
            this.lblAppTitle.Location = new System.Drawing.Point(3, 2);
            this.lblAppTitle.Name = "lblAppTitle";
            this.lblAppTitle.Size = new System.Drawing.Size(135, 26);
            this.lblAppTitle.TabIndex = 0;
            this.lblAppTitle.Text = "CivicLens 2.0";
            this.lblAppTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblWelcome
            // 
            this.lblWelcome.BackColor = System.Drawing.Color.Transparent;
            this.lblWelcome.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblWelcome.ForeColor = System.Drawing.Color.White;
            this.lblWelcome.Location = new System.Drawing.Point(156, 7);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(250, 35);
            this.lblWelcome.TabIndex = 1;
            this.lblWelcome.Text = "Welcome, User";
            // 
            // lblRole
            // 
            this.lblRole.BackColor = System.Drawing.Color.Transparent;
            this.lblRole.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.lblRole.ForeColor = System.Drawing.Color.White;
            this.lblRole.Location = new System.Drawing.Point(157, 42);
            this.lblRole.Name = "lblRole";
            this.lblRole.Size = new System.Drawing.Size(350, 25);
            this.lblRole.TabIndex = 2;
            this.lblRole.Text = "Role: Citizen";
            // 
            // grpShared
            // 
            this.grpShared.Location = new System.Drawing.Point(0, 0);
            this.grpShared.Name = "grpShared";
            this.grpShared.Size = new System.Drawing.Size(200, 100);
            this.grpShared.TabIndex = 4;
            this.grpShared.TabStop = false;
            this.grpShared.Visible = false;
            // 
            // grpAdmin
            // 
            this.grpAdmin.Location = new System.Drawing.Point(0, 0);
            this.grpAdmin.Name = "grpAdmin";
            this.grpAdmin.Size = new System.Drawing.Size(200, 100);
            this.grpAdmin.TabIndex = 5;
            this.grpAdmin.TabStop = false;
            this.grpAdmin.Visible = false;
            // 
            // grpModerator
            // 
            this.grpModerator.Location = new System.Drawing.Point(0, 0);
            this.grpModerator.Name = "grpModerator";
            this.grpModerator.Size = new System.Drawing.Size(200, 100);
            this.grpModerator.TabIndex = 6;
            this.grpModerator.TabStop = false;
            this.grpModerator.Visible = false;
            // 
            // grpPolice
            // 
            this.grpPolice.Location = new System.Drawing.Point(0, 0);
            this.grpPolice.Name = "grpPolice";
            this.grpPolice.Size = new System.Drawing.Size(200, 100);
            this.grpPolice.TabIndex = 7;
            this.grpPolice.TabStop = false;
            this.grpPolice.Visible = false;
            // 
            // grpJournalist
            // 
            this.grpJournalist.Location = new System.Drawing.Point(0, 0);
            this.grpJournalist.Name = "grpJournalist";
            this.grpJournalist.Size = new System.Drawing.Size(200, 100);
            this.grpJournalist.TabIndex = 8;
            this.grpJournalist.TabStop = false;
            this.grpJournalist.Visible = false;
            // 
            // grpCitizen
            // 
            this.grpCitizen.Location = new System.Drawing.Point(0, 0);
            this.grpCitizen.Name = "grpCitizen";
            this.grpCitizen.Size = new System.Drawing.Size(200, 100);
            this.grpCitizen.TabIndex = 9;
            this.grpCitizen.TabStop = false;
            this.grpCitizen.Visible = false;
            // 
            // btnViewProfile
            // 
            this.btnViewProfile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnViewProfile.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnViewProfile.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(230)))), ((int)(((byte)(255)))));
            this.btnViewProfile.Location = new System.Drawing.Point(2, 145);
            this.btnViewProfile.Name = "btnViewProfile";
            this.btnViewProfile.Size = new System.Drawing.Size(104, 36);
            this.btnViewProfile.TabIndex = 0;
            this.btnViewProfile.Text = "👤   View Profile";
            this.btnViewProfile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnViewProfile.Click += new System.EventHandler(this.btnViewProfile_Click);
            // 
            // btnEditProfile
            // 
            this.btnEditProfile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditProfile.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnEditProfile.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(230)))), ((int)(((byte)(255)))));
            this.btnEditProfile.Location = new System.Drawing.Point(2, 187);
            this.btnEditProfile.Name = "btnEditProfile";
            this.btnEditProfile.Size = new System.Drawing.Size(104, 37);
            this.btnEditProfile.TabIndex = 1;
            this.btnEditProfile.Text = "✏   Edit Profile";
            this.btnEditProfile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEditProfile.Click += new System.EventHandler(this.btnEditProfile_Click);
            // 
            // btnNewsfeed
            // 
            this.btnNewsfeed.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnNewsfeed.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNewsfeed.FlatAppearance.BorderSize = 0;
            this.btnNewsfeed.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNewsfeed.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnNewsfeed.ForeColor = System.Drawing.Color.White;
            this.btnNewsfeed.Location = new System.Drawing.Point(2, 230);
            this.btnNewsfeed.Name = "btnNewsfeed";
            this.btnNewsfeed.Size = new System.Drawing.Size(104, 36);
            this.btnNewsfeed.TabIndex = 2;
            this.btnNewsfeed.Text = "📰   Newsfeed";
            this.btnNewsfeed.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNewsfeed.UseVisualStyleBackColor = false;
            this.btnNewsfeed.Click += new System.EventHandler(this.btnNewsfeed_Click);
            // 
            // btnLogout
            // 
            this.btnLogout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(45)))), ((int)(((byte)(28)))));
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogout.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnLogout.ForeColor = System.Drawing.Color.White;
            this.btnLogout.Location = new System.Drawing.Point(2, 272);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(104, 31);
            this.btnLogout.TabIndex = 3;
            this.btnLogout.Text = "⏻   Logout";
            this.btnLogout.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // btnUserApprovals
            // 
            this.btnUserApprovals.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnUserApprovals.Location = new System.Drawing.Point(20, 30);
            this.btnUserApprovals.Name = "btnUserApprovals";
            this.btnUserApprovals.Size = new System.Drawing.Size(120, 35);
            this.btnUserApprovals.TabIndex = 0;
            this.btnUserApprovals.Text = "User Approvals";
            this.btnUserApprovals.Click += new System.EventHandler(this.btnUserApprovals_Click);
            // 
            // btnManageUsers
            // 
            this.btnManageUsers.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnManageUsers.Location = new System.Drawing.Point(150, 30);
            this.btnManageUsers.Name = "btnManageUsers";
            this.btnManageUsers.Size = new System.Drawing.Size(120, 35);
            this.btnManageUsers.TabIndex = 1;
            this.btnManageUsers.Text = "Manage Users";
            this.btnManageUsers.Click += new System.EventHandler(this.btnManageUsers_Click);
            // 
            // btnManageAdmins
            // 
            this.btnManageAdmins.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnManageAdmins.Location = new System.Drawing.Point(280, 30);
            this.btnManageAdmins.Name = "btnManageAdmins";
            this.btnManageAdmins.Size = new System.Drawing.Size(120, 35);
            this.btnManageAdmins.TabIndex = 2;
            this.btnManageAdmins.Text = "Manage Admins";
            this.btnManageAdmins.Click += new System.EventHandler(this.btnManageAdmins_Click);
            // 
            // btnCategories
            // 
            this.btnCategories.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnCategories.Location = new System.Drawing.Point(20, 75);
            this.btnCategories.Name = "btnCategories";
            this.btnCategories.Size = new System.Drawing.Size(120, 35);
            this.btnCategories.TabIndex = 3;
            this.btnCategories.Text = "Categories";
            this.btnCategories.Click += new System.EventHandler(this.btnCategories_Click);
            // 
            // btnLocations
            // 
            this.btnLocations.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnLocations.Location = new System.Drawing.Point(150, 75);
            this.btnLocations.Name = "btnLocations";
            this.btnLocations.Size = new System.Drawing.Size(120, 35);
            this.btnLocations.TabIndex = 4;
            this.btnLocations.Text = "Locations";
            this.btnLocations.Click += new System.EventHandler(this.btnLocations_Click);
            // 
            // btnModeratorQueue
            // 
            this.btnModeratorQueue.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnModeratorQueue.Location = new System.Drawing.Point(20, 30);
            this.btnModeratorQueue.Name = "btnModeratorQueue";
            this.btnModeratorQueue.Size = new System.Drawing.Size(150, 35);
            this.btnModeratorQueue.TabIndex = 0;
            this.btnModeratorQueue.Text = "Complaint Queue";
            this.btnModeratorQueue.Click += new System.EventHandler(this.btnModeratorQueue_Click);
            // 
            // btnPoliceAssigned
            // 
            this.btnPoliceAssigned.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnPoliceAssigned.Location = new System.Drawing.Point(20, 30);
            this.btnPoliceAssigned.Name = "btnPoliceAssigned";
            this.btnPoliceAssigned.Size = new System.Drawing.Size(150, 35);
            this.btnPoliceAssigned.TabIndex = 0;
            this.btnPoliceAssigned.Text = "Assigned Complaints";
            this.btnPoliceAssigned.Click += new System.EventHandler(this.btnPoliceAssigned_Click);
            // 
            // btnJournalistFeed
            // 
            this.btnJournalistFeed.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnJournalistFeed.Location = new System.Drawing.Point(20, 30);
            this.btnJournalistFeed.Name = "btnJournalistFeed";
            this.btnJournalistFeed.Size = new System.Drawing.Size(120, 35);
            this.btnJournalistFeed.TabIndex = 0;
            this.btnJournalistFeed.Text = "Journalist Feed";
            this.btnJournalistFeed.Click += new System.EventHandler(this.btnJournalistFeed_Click);
            // 
            // btnSubmitComplaint
            // 
            this.btnSubmitComplaint.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnSubmitComplaint.Location = new System.Drawing.Point(20, 30);
            this.btnSubmitComplaint.Name = "btnSubmitComplaint";
            this.btnSubmitComplaint.Size = new System.Drawing.Size(150, 35);
            this.btnSubmitComplaint.TabIndex = 0;
            this.btnSubmitComplaint.Text = "Submit Complaint";
            this.btnSubmitComplaint.Click += new System.EventHandler(this.btnSubmitComplaint_Click);
            // 
            // btnMyComplaints
            // 
            this.btnMyComplaints.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnMyComplaints.Location = new System.Drawing.Point(180, 30);
            this.btnMyComplaints.Name = "btnMyComplaints";
            this.btnMyComplaints.Size = new System.Drawing.Size(150, 35);
            this.btnMyComplaints.TabIndex = 1;
            this.btnMyComplaints.Text = "My Complaints";
            this.btnMyComplaints.Click += new System.EventHandler(this.btnMyComplaints_Click);
            // 
            // panelSidebar
            // 
            this.panelSidebar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(46)))), ((int)(((byte)(96)))));
            this.panelSidebar.Controls.Add(this.btnViewProfile);
            this.panelSidebar.Controls.Add(this.btnEditProfile);
            this.panelSidebar.Controls.Add(this.btnNewsfeed);
            this.panelSidebar.Controls.Add(this.sepLine);
            this.panelSidebar.Controls.Add(this.btnLogout);
            this.panelSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelSidebar.Location = new System.Drawing.Point(0, 70);
            this.panelSidebar.Name = "panelSidebar";
            this.panelSidebar.Size = new System.Drawing.Size(108, 374);
            this.panelSidebar.TabIndex = 1;
            this.panelSidebar.Paint += new System.Windows.Forms.PaintEventHandler(this.PanelSidebar_Paint);
            // 
            // sepLine
            // 
            this.sepLine.BackColor = System.Drawing.Color.White;
            this.sepLine.Location = new System.Drawing.Point(11, 135);
            this.sepLine.Name = "sepLine";
            this.sepLine.Size = new System.Drawing.Size(91, 1);
            this.sepLine.TabIndex = 2;
            // 
            // panelContent
            // 
            this.panelContent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(252)))));
            this.panelContent.Controls.Add(this.lblPageTitle);
            this.panelContent.Controls.Add(this.lblPageSub);
            this.panelContent.Controls.Add(this.cardPanelAdmin);
            this.panelContent.Controls.Add(this.cardPanelModerator);
            this.panelContent.Controls.Add(this.cardPanelPolice);
            this.panelContent.Controls.Add(this.cardPanelJournalist);
            this.panelContent.Controls.Add(this.cardPanelCitizen);
            this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContent.Location = new System.Drawing.Point(108, 70);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(566, 374);
            this.panelContent.TabIndex = 0;
            // 
            // lblPageTitle
            // 
            this.lblPageTitle.AutoSize = true;
            this.lblPageTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblPageTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(50)))), ((int)(((byte)(100)))));
            this.lblPageTitle.Location = new System.Drawing.Point(9, 8);
            this.lblPageTitle.Name = "lblPageTitle";
            this.lblPageTitle.Size = new System.Drawing.Size(245, 59);
            this.lblPageTitle.TabIndex = 0;
            this.lblPageTitle.Text = "Dashboard";
            // 
            // lblPageSub
            // 
            this.lblPageSub.AutoSize = true;
            this.lblPageSub.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblPageSub.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(150)))), ((int)(((byte)(185)))));
            this.lblPageSub.Location = new System.Drawing.Point(11, 38);
            this.lblPageSub.Name = "lblPageSub";
            this.lblPageSub.Size = new System.Drawing.Size(460, 36);
            this.lblPageSub.TabIndex = 1;
            this.lblPageSub.Text = "Your available actions are shown below";
            // 
            // cardPanelAdmin
            // 
            this.cardPanelAdmin.BackColor = System.Drawing.Color.White;
            this.cardPanelAdmin.Controls.Add(this.btnUserApprovals);
            this.cardPanelAdmin.Controls.Add(this.btnManageUsers);
            this.cardPanelAdmin.Controls.Add(this.btnManageAdmins);
            this.cardPanelAdmin.Controls.Add(this.btnCategories);
            this.cardPanelAdmin.Controls.Add(this.btnLocations);
            this.cardPanelAdmin.Location = new System.Drawing.Point(28, 80);
            this.cardPanelAdmin.Name = "cardPanelAdmin";
            this.cardPanelAdmin.Size = new System.Drawing.Size(510, 130);
            this.cardPanelAdmin.TabIndex = 2;
            this.cardPanelAdmin.Visible = false;
            // 
            // cardPanelModerator
            // 
            this.cardPanelModerator.BackColor = System.Drawing.Color.White;
            this.cardPanelModerator.Controls.Add(this.btnModeratorQueue);
            this.cardPanelModerator.Location = new System.Drawing.Point(28, 80);
            this.cardPanelModerator.Name = "cardPanelModerator";
            this.cardPanelModerator.Size = new System.Drawing.Size(510, 100);
            this.cardPanelModerator.TabIndex = 3;
            this.cardPanelModerator.Visible = false;
            // 
            // cardPanelPolice
            // 
            this.cardPanelPolice.BackColor = System.Drawing.Color.White;
            this.cardPanelPolice.Controls.Add(this.btnPoliceAssigned);
            this.cardPanelPolice.Location = new System.Drawing.Point(28, 80);
            this.cardPanelPolice.Name = "cardPanelPolice";
            this.cardPanelPolice.Size = new System.Drawing.Size(510, 100);
            this.cardPanelPolice.TabIndex = 4;
            this.cardPanelPolice.Visible = false;
            // 
            // cardPanelJournalist
            // 
            this.cardPanelJournalist.BackColor = System.Drawing.Color.White;
            this.cardPanelJournalist.Controls.Add(this.btnJournalistFeed);
            this.cardPanelJournalist.Location = new System.Drawing.Point(28, 80);
            this.cardPanelJournalist.Name = "cardPanelJournalist";
            this.cardPanelJournalist.Size = new System.Drawing.Size(510, 100);
            this.cardPanelJournalist.TabIndex = 5;
            this.cardPanelJournalist.Visible = false;
            // 
            // cardPanelCitizen
            // 
            this.cardPanelCitizen.BackColor = System.Drawing.Color.White;
            this.cardPanelCitizen.Controls.Add(this.btnSubmitComplaint);
            this.cardPanelCitizen.Controls.Add(this.btnMyComplaints);
            this.cardPanelCitizen.Location = new System.Drawing.Point(28, 80);
            this.cardPanelCitizen.Name = "cardPanelCitizen";
            this.cardPanelCitizen.Size = new System.Drawing.Size(510, 100);
            this.cardPanelCitizen.TabIndex = 6;
            this.cardPanelCitizen.Visible = false;
            // 
            // panelStatusBar
            // 
            this.panelStatusBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(239)))), ((int)(((byte)(250)))));
            this.panelStatusBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelStatusBar.Location = new System.Drawing.Point(0, 444);
            this.panelStatusBar.Name = "panelStatusBar";
            this.panelStatusBar.Size = new System.Drawing.Size(674, 14);
            this.panelStatusBar.TabIndex = 3;
            // 
            // DashboardForm
            // 
            this.ClientSize = new System.Drawing.Size(674, 458);
            this.Controls.Add(this.panelContent);
            this.Controls.Add(this.panelSidebar);
            this.Controls.Add(this.panelHeader);
            this.Controls.Add(this.panelStatusBar);
            this.Controls.Add(this.grpShared);
            this.Controls.Add(this.grpAdmin);
            this.Controls.Add(this.grpModerator);
            this.Controls.Add(this.grpPolice);
            this.Controls.Add(this.grpJournalist);
            this.Controls.Add(this.grpCitizen);
            this.Name = "DashboardForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CivicLens 2.0 – Dashboard";
            this.Load += new System.EventHandler(this.DashboardForm_SetupUI);
            this.panelHeader.ResumeLayout(false);
            this.panelSidebar.ResumeLayout(false);
            this.panelContent.ResumeLayout(false);
            this.panelContent.PerformLayout();
            this.cardPanelAdmin.ResumeLayout(false);
            this.cardPanelModerator.ResumeLayout(false);
            this.cardPanelPolice.ResumeLayout(false);
            this.cardPanelJournalist.ResumeLayout(false);
            this.cardPanelCitizen.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        // ── Setup paint & layout (preserved from original) ────────────
        private void DashboardForm_SetupUI(object sender, EventArgs e)
        {
            BindGroupToCard(grpAdmin, cardPanelAdmin);
            BindGroupToCard(grpModerator, cardPanelModerator);
            BindGroupToCard(grpPolice, cardPanelPolice);
            BindGroupToCard(grpJournalist, cardPanelJournalist);
            BindGroupToCard(grpCitizen, cardPanelCitizen);

            panelHeader.Paint += PanelHeader_Paint;
            panelStatusBar.Paint += PanelStatusBar_Paint;
            panelSidebar.Paint += PanelSidebar_Paint;

            SetupCardPaint(cardPanelAdmin, "Admin Tools", Color.FromArgb(24, 95, 165));
            SetupCardPaint(cardPanelModerator, "Moderator", Color.FromArgb(34, 148, 130));
            SetupCardPaint(cardPanelPolice, "Police", Color.FromArgb(40, 100, 180));
            SetupCardPaint(cardPanelJournalist, "Journalist", Color.FromArgb(130, 80, 180));
            SetupCardPaint(cardPanelCitizen, "Citizen", Color.FromArgb(34, 148, 68));

            panelSidebar.Resize += (s2, e2) =>
            {
                int bw = panelSidebar.Width - 12;
                foreach (Control c in panelSidebar.Controls)
                    if (c is Button b) b.Width = bw;
                sepLine.Width = panelSidebar.Width - 28;
                panelSidebar.Invalidate();
            };

            panelContent.Resize += (s2, e2) =>
            {
                int cw = Math.Max(300, panelContent.ClientSize.Width - 56);
                foreach (var card in new[] {
                    cardPanelAdmin, cardPanelModerator, cardPanelPolice,
                    cardPanelJournalist, cardPanelCitizen })
                {
                    card.Width = cw;
                    card.Invalidate();
                }
            };

            panelHeader.Resize += (s2, e2) => PositionHeaderLabels();
            PositionHeaderLabels();

            StyleSidebarButton(btnViewProfile);
            StyleSidebarButton(btnEditProfile);
            StyleNewsfeedButton(btnNewsfeed);

            panelSidebar.Invalidate();
            panelHeader.Invalidate();
            panelStatusBar.Invalidate();
        }

        private void BindGroupToCard(GroupBox grp, Panel card)
        {
            grp.VisibleChanged += (s, e) =>
            {
                card.Visible = grp.Visible;
                RelayoutCards();
            };
        }

        private void RelayoutCards()
        {
            if (panelContent == null) return;
            const int MARGIN = 28;
            const int START_Y = 80;
            const int CARD_GAP = 16;

            int y = START_Y;
            foreach (var card in new Panel[] {
                cardPanelAdmin, cardPanelModerator, cardPanelPolice,
                cardPanelJournalist, cardPanelCitizen })
            {
                if (card == null) continue;
                if (card.Visible)
                {
                    card.Location = new Point(MARGIN, y);
                    card.Width = Math.Max(300, panelContent.ClientSize.Width - MARGIN * 2);
                    y += card.Height + CARD_GAP;
                }
            }
        }

        private void PositionHeaderLabels()
        {
            int lx = panelSidebar.Width + 24;
            lblWelcome.Location = new Point(lx, 10);
            lblRole.Location = new Point(lx, 42);
        }

        private void StyleSidebarButton(Button btn)
        {
            btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(40, 255, 255, 255);
            btn.FlatAppearance.MouseDownBackColor = Color.FromArgb(65, 255, 255, 255);
            btn.Padding = new Padding(8, 0, 0, 0);
            btn.Cursor = Cursors.Hand;
        }

        private void StyleNewsfeedButton(Button btn)
        {
            btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(29, 78, 216);
            btn.FlatAppearance.MouseDownBackColor = Color.FromArgb(21, 63, 180);
            btn.Padding = new Padding(8, 0, 0, 0);
            btn.Cursor = Cursors.Hand;
        }

        private void SetupCardPaint(Panel card, string title, Color accent)
        {
            Color la = accent;
            string lt = title;
            card.Paint += (s, e) =>
            {
                var g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.FillRectangle(Brushes.White, 0, 0, card.Width, card.Height);
                using (var br = new SolidBrush(la))
                    g.FillRectangle(br, 0, 0, 5, card.Height);
                using (var pen = new Pen(Color.FromArgb(215, 225, 240), 1f))
                    g.DrawRectangle(pen, 0, 0, card.Width - 1, card.Height - 1);
                using (var fnt = new Font("Segoe UI Semibold", 10f, FontStyle.Bold))
                using (var br = new SolidBrush(la))
                    g.DrawString(lt, fnt, br, 14, 10);
            };
        }

        private void PanelHeader_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            int w = panelHeader.Width, h = panelHeader.Height;
            using (var p = new Pen(Color.FromArgb(14, 0, 0, 0), 2f))
                g.DrawLine(p, 0, h - 1, w, h - 1);

            int cx = 30, cy = h / 2;
            PointF[] shield = {
                new PointF(cx,    cy-14), new PointF(cx+11, cy-7),
                new PointF(cx+11, cy+4),  new PointF(cx,    cy+14),
                new PointF(cx-11, cy+4),  new PointF(cx-11, cy-7),
            };
            using (var br = new SolidBrush(Color.FromArgb(55, 255, 255, 255)))
                g.FillPolygon(br, shield);
            using (var pen = new Pen(Color.FromArgb(200, 255, 255, 255), 1.5f))
                g.DrawPolygon(pen, shield);
            using (var pen = new Pen(Color.White, 1.6f)
            { StartCap = LineCap.Round, EndCap = LineCap.Round })
            {
                g.DrawLine(pen, cx - 4, cy + 1, cx, cy + 5);
                g.DrawLine(pen, cx, cy + 5, cx + 6, cy - 3);
            }
        }

        private void PanelStatusBar_Paint(object sender, PaintEventArgs e)
        {
            using (var p = new Pen(Color.FromArgb(200, 218, 238), 1f))
                e.Graphics.DrawLine(p, 0, 0, panelStatusBar.Width, 0);
            using (var fnt = new Font("Segoe UI", 8f))
            using (var br = new SolidBrush(Color.FromArgb(140, 162, 192)))
                e.Graphics.DrawString(
                    "\u00a9 2025 CivicLens  \u00b7  Community Edition  v2.0",
                    fnt, br, 12, 6);
        }

        private void PanelSidebar_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            int w = panelSidebar.Width;
            using (var p = new Pen(Color.FromArgb(30, 0, 0, 0), 3f))
                g.DrawLine(p, w - 1, 0, w - 1, panelSidebar.Height);
            int ax = w / 2, ay = 56;
            using (var br = new SolidBrush(Color.FromArgb(45, 255, 255, 255)))
                g.FillEllipse(br, ax - 32, ay - 32, 64, 64);
            using (var pen = new Pen(Color.FromArgb(90, 255, 255, 255), 1.5f))
                g.DrawEllipse(pen, ax - 32, ay - 32, 64, 64);

            string txt = lblWelcome.Text;
            string initial = txt.Length > 9 ? txt[9].ToString().ToUpper() : "U";
            using (var fnt = new Font("Segoe UI", 20f, FontStyle.Bold))
            using (var br = new SolidBrush(Color.White))
            {
                SizeF sz = g.MeasureString(initial, fnt);
                g.DrawString(initial, fnt, br, ax - sz.Width / 2f, ay - sz.Height / 2f);
            }

            using (var fnt = new Font("Segoe UI", 7.5f, FontStyle.Bold))
            using (var br = new SolidBrush(Color.FromArgb(100, 175, 215)))
                g.DrawString("NAVIGATION", fnt, br, 16, 120);
        }
    }
}