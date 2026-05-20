using System;
using System.Windows.Forms;

namespace CivicLens
{
    public partial class DashboardForm : Form
    {
        private int currentUserId;
        private string currentFullName;
        private string currentRole;

        public DashboardForm(int userId, string fullName, string role)
        {
            InitializeComponent();
            currentUserId = userId;
            currentFullName = fullName;
            currentRole = role;

            this.Load += DashboardForm_Load;
        }

        private void DashboardForm_Load(object sender, EventArgs e)
        {
            lblWelcome.Text = $"Welcome, {currentFullName}";
            lblRole.Text = $"Role: {currentRole}";

            panelSidebar.Invalidate();

          
            grpAdmin.Visible = false;
            grpModerator.Visible = false;
            grpPolice.Visible = false;
            grpJournalist.Visible = false;
            grpCitizen.Visible = false;

            switch (currentRole)
            {
                case "Admin": grpAdmin.Visible = true; break;
                case "Moderator": grpModerator.Visible = true; break;
                case "Police": grpPolice.Visible = true; break;
                case "Journalist": grpJournalist.Visible = true; break;
                case "Citizen": grpCitizen.Visible = true; break;
                default:
                    MessageBox.Show("Unknown role type. Some actions may be hidden.");
                    break;
            }
        }

        private void btnViewProfile_Click(object sender, EventArgs e)
        {
            using (var f = new ViewProfileForm(currentUserId, currentFullName))
                f.ShowDialog(this);
        }

        private void btnEditProfile_Click(object sender, EventArgs e)
        {
            using (var f = new EditProfileForm(currentUserId))
                f.ShowDialog(this);
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to log out?", "Confirm",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            this.Close();
        }

        //Newsfeed
        private void btnNewsfeed_Click(object sender, EventArgs e)
        {
            using (var f = new NewsfeedForm(currentUserId, currentFullName, currentRole))
                f.ShowDialog(this);
        }

        //Admin 
        private void btnAdminApprovals_Click(object sender, EventArgs e)
        {
            using (var f = new AdminUserApprovalsForm()) f.ShowDialog(this);
        }

        private void btnAdminUsers_Click(object sender, EventArgs e)
        {
            using (var f = new AdminUsersForm()) f.ShowDialog(this);
        }

        private void btnAdminManageAdmins_Click(object sender, EventArgs e)
        {
            using (var f = new AdminManageAdminsForm()) f.ShowDialog(this);
        }

        private void btnAdminCategories_Click(object sender, EventArgs e)
        {
            using (var f = new AdminCategoriesForm()) f.ShowDialog(this);
        }

        private void btnAdminLocations_Click(object sender, EventArgs e)
        {
            using (var f = new AdminLocationsForm()) f.ShowDialog(this);
        }

        //Moderator 
        private void btnModeratorQueue_Click(object sender, EventArgs e)
        {
            using (var f = new ModeratorQueueForm(currentUserId)) f.ShowDialog(this);
        }

        private void btnAssignComplaint_Click(object sender, EventArgs e)
        {
            using (var f = new ModeratorQueueForm(currentUserId)) f.ShowDialog(this);
        }

        //Police 
        private void btnPoliceAssigned_Click(object sender, EventArgs e)
        {
            using (var f = new PoliceAssignedComplaintsForm(currentUserId)) f.ShowDialog(this);
        }

        private void btnUpdateStatus_Click(object sender, EventArgs e)
        {
            using (var f = new UpdateStatusForm()) f.ShowDialog(this);
        }

        //Journalist
        private void btnJournalistFeed_Click(object sender, EventArgs e)
        {
            using (var f = new JournalistFeedForm(currentUserId)) f.ShowDialog(this);
        }

        private void btnJournalistNotes_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Opening journalist public notes...");
        }

        //Citizen 
        private void btnSubmitComplaint_Click(object sender, EventArgs e)
        {
            using (var f = new SubmitComplaintForm(currentUserId)) f.ShowDialog(this);
        }

        private void btnMyComplaints_Click(object sender, EventArgs e)
        {
            using (var f = new MyComplaintsForm(currentUserId)) f.ShowDialog(this);
        }

        //delegates
        private void btnUserApprovals_Click(object sender, EventArgs e) => btnAdminApprovals_Click(sender, e);
        private void btnManageUsers_Click(object sender, EventArgs e) => btnAdminUsers_Click(sender, e);
        private void btnManageAdmins_Click(object sender, EventArgs e) => btnAdminManageAdmins_Click(sender, e);
        private void btnCategories_Click(object sender, EventArgs e) => btnAdminCategories_Click(sender, e);
        private void btnLocations_Click(object sender, EventArgs e) => btnAdminLocations_Click(sender, e);
    }
}