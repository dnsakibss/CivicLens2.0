using System;
using System.Windows.Forms;

namespace CivicLens
{
    partial class AdminCategoriesForm
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblTitle;
        private Label lblSearch;
        private TextBox txtSearch;
        private Button btnSearch;
        private Button btnRefresh;

        private GroupBox grpEditor;
        private Label lblName;
        private TextBox txtName;

        // Hidden but kept for backend compatibility
        private Label lblDescription;
        private TextBox txtDescription;
        private CheckBox chkActive;

        private Button btnSave;
        private Button btnClear;

        private DataGridView dgvCategories;
        private DataGridViewTextBoxColumn colCategoryId;
        private DataGridViewTextBoxColumn colCategoryName;
        private DataGridViewTextBoxColumn colDescription;
        private DataGridViewTextBoxColumn colIsActive;
        private DataGridViewTextBoxColumn colCreatedAt;
        private DataGridViewButtonColumn colEdit;
        private DataGridViewButtonColumn colToggleActive;
        private DataGridViewButtonColumn colDelete;

        private Button btnClose;

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
            this.lblSearch = new Label();
            this.txtSearch = new TextBox();
            this.btnSearch = new Button();
            this.btnRefresh = new Button();

            this.grpEditor = new GroupBox();
            this.lblName = new Label();
            this.txtName = new TextBox();

            // Hidden fields for backend
            this.lblDescription = new Label();
            this.txtDescription = new TextBox();
            this.chkActive = new CheckBox();

            this.btnSave = new Button();
            this.btnClear = new Button();

            this.dgvCategories = new DataGridView();

            this.colCategoryId = new DataGridViewTextBoxColumn();
            this.colCategoryName = new DataGridViewTextBoxColumn();
            this.colDescription = new DataGridViewTextBoxColumn();
            this.colIsActive = new DataGridViewTextBoxColumn();
            this.colCreatedAt = new DataGridViewTextBoxColumn();
            this.colEdit = new DataGridViewButtonColumn();
            this.colToggleActive = new DataGridViewButtonColumn();
            this.colDelete = new DataGridViewButtonColumn();

            this.btnClose = new Button();

            // ===== Form =====
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(248, 250, 253);
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.MaximizeBox = true;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Name = "AdminCategoriesForm";
            this.Text = "Admin - Manage Categories";

            // ===== Title =====
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(24, 18);
            this.lblTitle.Text = "Manage Complaint Categories";

            // ===== Search Bar =====
            this.lblSearch.Location = new System.Drawing.Point(26, 70);
            this.lblSearch.Size = new System.Drawing.Size(60, 24);
            this.lblSearch.Text = "Search:";

            this.txtSearch.Location = new System.Drawing.Point(90, 68);
            this.txtSearch.Size = new System.Drawing.Size(320, 27);

            this.btnSearch.Location = new System.Drawing.Point(420, 67);
            this.btnSearch.Size = new System.Drawing.Size(95, 30);
            this.btnSearch.Text = "Search";
            this.btnSearch.Click += new EventHandler(this.btnSearch_Click);

            this.btnRefresh.Location = new System.Drawing.Point(520, 67);
            this.btnRefresh.Size = new System.Drawing.Size(95, 30);
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.Click += new EventHandler(this.btnRefresh_Click);

            // ===== Editor Box =====
            this.grpEditor.Text = "Category Editor";
            this.grpEditor.Location = new System.Drawing.Point(28, 120);
            this.grpEditor.Size = new System.Drawing.Size(380, 240);
            this.grpEditor.BackColor = System.Drawing.Color.White;
            this.grpEditor.ForeColor = System.Drawing.Color.Black;

            // eLeft=18, eText=120, eWidth=220, eTop=40, eGap=38
            // All variable expressions replaced with computed literals

            this.lblName.Location = new System.Drawing.Point(18, 40);
            this.lblName.Size = new System.Drawing.Size(90, 25);
            this.lblName.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblName.Text = "Name *";

            this.txtName.Location = new System.Drawing.Point(120, 37);  // eText=120, eTop-3=37
            this.txtName.Size = new System.Drawing.Size(220, 27);

            // Hidden description
            this.lblDescription.Visible = false;
            this.txtDescription.Visible = false;
            this.chkActive.Visible = false;

            // eTop after += eGap => 40+38=78, eTop+5=83
            this.btnSave.Location = new System.Drawing.Point(120, 83);   // eText=120, eTop+5=83
            this.btnSave.Size = new System.Drawing.Size(100, 35);
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.FlatStyle = FlatStyle.Flat;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new EventHandler(this.btnSave_Click);

            this.btnClear.Location = new System.Drawing.Point(230, 83);  // eText+110=230, eTop+5=83
            this.btnClear.Size = new System.Drawing.Size(100, 35);
            this.btnClear.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnClear.BackColor = System.Drawing.Color.FromArgb(230, 230, 230);
            this.btnClear.FlatStyle = FlatStyle.Flat;
            this.btnClear.Text = "Clear";
            this.btnClear.Click += new EventHandler(this.btnClear_Click);

            this.grpEditor.Controls.Add(this.lblName);
            this.grpEditor.Controls.Add(this.txtName);
            this.grpEditor.Controls.Add(this.lblDescription);
            this.grpEditor.Controls.Add(this.txtDescription);
            this.grpEditor.Controls.Add(this.chkActive);
            this.grpEditor.Controls.Add(this.btnSave);
            this.grpEditor.Controls.Add(this.btnClear);

            // ===== Categories Grid =====
            this.dgvCategories.Location = new System.Drawing.Point(430, 120);
            this.dgvCategories.Size = new System.Drawing.Size(740, 500);
            this.dgvCategories.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.dgvCategories.BackgroundColor = System.Drawing.Color.White;
            this.dgvCategories.GridColor = System.Drawing.Color.LightGray;
            this.dgvCategories.AllowUserToAddRows = false;
            this.dgvCategories.AllowUserToDeleteRows = false;
            this.dgvCategories.ReadOnly = true;
            this.dgvCategories.RowHeadersVisible = false;
            this.dgvCategories.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvCategories.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCategories.RowTemplate.Height = 28;
            this.dgvCategories.CellContentClick += new DataGridViewCellEventHandler(this.dgvCategories_CellContentClick);

            // ===== Columns =====
            this.colCategoryId.HeaderText = "CategoryId";
            this.colCategoryId.Name = "colCategoryId";
            this.colCategoryId.Visible = false;
            this.colCategoryId.FillWeight = 5;

            this.colCategoryName.HeaderText = "Name";
            this.colCategoryName.Name = "colCategoryName";
            this.colCategoryName.FillWeight = 40;

            // Hidden but kept for backend
            this.colDescription.HeaderText = "Description";
            this.colDescription.Name = "colDescription";
            this.colDescription.Visible = false;

            this.colIsActive.HeaderText = "Active";
            this.colIsActive.Name = "colIsActive";
            this.colIsActive.Visible = false;

            this.colCreatedAt.HeaderText = "Created At";
            this.colCreatedAt.Name = "colCreatedAt";
            this.colCreatedAt.FillWeight = 25;

            this.colEdit.HeaderText = "Edit";
            this.colEdit.Name = "colEdit";
            this.colEdit.Text = "Edit";
            this.colEdit.UseColumnTextForButtonValue = true;
            this.colEdit.FillWeight = 15;

            this.colToggleActive.HeaderText = "Toggle";
            this.colToggleActive.Name = "colToggleActive";
            this.colToggleActive.Text = "Toggle";
            this.colToggleActive.UseColumnTextForButtonValue = true;
            this.colToggleActive.Visible = false;

            this.colDelete.HeaderText = "Delete";
            this.colDelete.Name = "colDelete";
            this.colDelete.Text = "Delete";
            this.colDelete.UseColumnTextForButtonValue = true;
            this.colDelete.FillWeight = 15;

            this.dgvCategories.Columns.AddRange(new DataGridViewColumn[] {
                this.colCategoryId, this.colCategoryName,
                this.colDescription, this.colIsActive,
                this.colCreatedAt, this.colEdit,
                this.colToggleActive, this.colDelete
            });

            // ===== Close Button =====
            this.btnClose.Text = "Close";
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnClose.Size = new System.Drawing.Size(90, 32);
            this.btnClose.Location = new System.Drawing.Point(1080, 640);
            this.btnClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            this.btnClose.FlatStyle = FlatStyle.Flat;
            this.btnClose.Click += new EventHandler(this.btnClose_Click);

            // ===== Add Controls =====
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblSearch);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.grpEditor);
            this.Controls.Add(this.dgvCategories);
            this.Controls.Add(this.btnClose);
        }
        #endregion
    }
}