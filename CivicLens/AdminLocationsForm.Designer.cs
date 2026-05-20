using System;
using System.Windows.Forms;

namespace CivicLens
{
    partial class AdminLocationsForm
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblTitle;
        private Label lblSearch;
        private TextBox txtSearch;
        private Button btnSearch;
        private Button btnRefresh;

        private GroupBox grpEditor;
        private Label lblDistrict;
        private TextBox txtDistrict;
        private Label lblCity;
        private TextBox txtCity;
        private Label lblArea;
        private TextBox txtArea;
        private Button btnSave;
        private Button btnClear;

        private DataGridView dgvLocations;
        private DataGridViewTextBoxColumn colLocationId;
        private DataGridViewTextBoxColumn colDistrict;
        private DataGridViewTextBoxColumn colCity;
        private DataGridViewTextBoxColumn colArea;
        private DataGridViewTextBoxColumn colIsActive;
        private DataGridViewTextBoxColumn colCreatedAt;
        private DataGridViewButtonColumn colEdit;
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
            this.lblDistrict = new Label();
            this.txtDistrict = new TextBox();
            this.lblCity = new Label();
            this.txtCity = new TextBox();
            this.lblArea = new Label();
            this.txtArea = new TextBox();
            this.btnSave = new Button();
            this.btnClear = new Button();

            this.dgvLocations = new DataGridView();

            this.colLocationId = new DataGridViewTextBoxColumn();
            this.colDistrict = new DataGridViewTextBoxColumn();
            this.colCity = new DataGridViewTextBoxColumn();
            this.colArea = new DataGridViewTextBoxColumn();
            this.colIsActive = new DataGridViewTextBoxColumn();
            this.colCreatedAt = new DataGridViewTextBoxColumn();
            this.colEdit = new DataGridViewButtonColumn();
            this.colDelete = new DataGridViewButtonColumn();

            this.btnClose = new Button();

            // ===== Form =====
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(248, 250, 253);
            this.ClientSize = new System.Drawing.Size(1280, 720);
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.MaximizeBox = true;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Name = "AdminLocationsForm";
            this.Text = "Admin - Manage Locations";

            // ===== Title =====
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(28, 20);
            this.lblTitle.Text = "Manage Locations (District / City / Area)";

            // ===== Search =====
            this.lblSearch.Location = new System.Drawing.Point(32, 75);
            this.lblSearch.Size = new System.Drawing.Size(60, 24);
            this.lblSearch.Text = "Search:";

            this.txtSearch.Location = new System.Drawing.Point(95, 72);
            this.txtSearch.Size = new System.Drawing.Size(320, 27);

            this.btnSearch.Location = new System.Drawing.Point(425, 70);
            this.btnSearch.Size = new System.Drawing.Size(95, 30);
            this.btnSearch.Text = "Search";
            this.btnSearch.Click += new EventHandler(this.btnSearch_Click);

            this.btnRefresh.Location = new System.Drawing.Point(525, 70);
            this.btnRefresh.Size = new System.Drawing.Size(95, 30);
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.Click += new EventHandler(this.btnRefresh_Click);

            // ===== Editor =====
            // eLeft=18, eText=120, eWidth=220, eTop=45, eGap=38
            // eTop values: District=45, City=83, Area=121, Buttons=159
            // eTop-3 values: District=42, City=80, Area=118
            // eTop+8 (buttons): 159+8=167

            this.grpEditor.Text = "Location Editor";
            this.grpEditor.Location = new System.Drawing.Point(32, 120);
            this.grpEditor.Size = new System.Drawing.Size(380, 280);
            this.grpEditor.BackColor = System.Drawing.Color.White;

            // District row (eTop=45)
            this.lblDistrict.Location = new System.Drawing.Point(18, 45);
            this.lblDistrict.Size = new System.Drawing.Size(80, 25);
            this.lblDistrict.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblDistrict.Text = "District *";
            this.txtDistrict.Location = new System.Drawing.Point(120, 42);
            this.txtDistrict.Size = new System.Drawing.Size(220, 27);

            // City row (eTop=83)
            this.lblCity.Location = new System.Drawing.Point(18, 83);
            this.lblCity.Size = new System.Drawing.Size(80, 25);
            this.lblCity.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblCity.Text = "City *";
            this.txtCity.Location = new System.Drawing.Point(120, 80);
            this.txtCity.Size = new System.Drawing.Size(220, 27);

            // Area row (eTop=121)
            this.lblArea.Location = new System.Drawing.Point(18, 121);
            this.lblArea.Size = new System.Drawing.Size(80, 25);
            this.lblArea.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblArea.Text = "Area *";
            this.txtArea.Location = new System.Drawing.Point(120, 118);
            this.txtArea.Size = new System.Drawing.Size(220, 27);

            // Buttons row (eTop=159, eTop+8=167)
            this.btnSave.Location = new System.Drawing.Point(120, 167);
            this.btnSave.Size = new System.Drawing.Size(100, 35);
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.FlatStyle = FlatStyle.Flat;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new EventHandler(this.btnSave_Click);

            this.btnClear.Location = new System.Drawing.Point(230, 167);  // eText+110=230
            this.btnClear.Size = new System.Drawing.Size(100, 35);
            this.btnClear.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnClear.BackColor = System.Drawing.Color.FromArgb(230, 230, 230);
            this.btnClear.FlatStyle = FlatStyle.Flat;
            this.btnClear.Text = "Clear";
            this.btnClear.Click += new EventHandler(this.btnClear_Click);

            this.grpEditor.Controls.Add(this.lblDistrict);
            this.grpEditor.Controls.Add(this.txtDistrict);
            this.grpEditor.Controls.Add(this.lblCity);
            this.grpEditor.Controls.Add(this.txtCity);
            this.grpEditor.Controls.Add(this.lblArea);
            this.grpEditor.Controls.Add(this.txtArea);
            this.grpEditor.Controls.Add(this.btnSave);
            this.grpEditor.Controls.Add(this.btnClear);

            // ===== DataGrid =====
            this.dgvLocations.Location = new System.Drawing.Point(440, 120);
            this.dgvLocations.Size = new System.Drawing.Size(800, 520);
            this.dgvLocations.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.dgvLocations.BackgroundColor = System.Drawing.Color.White;
            this.dgvLocations.GridColor = System.Drawing.Color.LightGray;
            this.dgvLocations.AllowUserToAddRows = false;
            this.dgvLocations.AllowUserToDeleteRows = false;
            this.dgvLocations.ReadOnly = true;
            this.dgvLocations.RowHeadersVisible = false;
            this.dgvLocations.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvLocations.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLocations.RowTemplate.Height = 28;
            this.dgvLocations.CellContentClick += new DataGridViewCellEventHandler(this.dgvLocations_CellContentClick);

            // ===== Columns =====
            this.colLocationId.HeaderText = "LocationId";
            this.colLocationId.Name = "colLocationId";
            this.colLocationId.Visible = false;

            this.colDistrict.HeaderText = "District";
            this.colDistrict.Name = "colDistrict";
            this.colDistrict.FillWeight = 25;

            this.colCity.HeaderText = "City";
            this.colCity.Name = "colCity";
            this.colCity.FillWeight = 25;

            this.colArea.HeaderText = "Area";
            this.colArea.Name = "colArea";
            this.colArea.FillWeight = 25;

            // Hidden but kept for backend compatibility
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
            this.colEdit.FillWeight = 10;

            this.colDelete.HeaderText = "Delete";
            this.colDelete.Name = "colDelete";
            this.colDelete.Text = "Delete";
            this.colDelete.UseColumnTextForButtonValue = true;
            this.colDelete.FillWeight = 10;

            this.dgvLocations.Columns.AddRange(new DataGridViewColumn[] {
                this.colLocationId, this.colDistrict, this.colCity, this.colArea,
                this.colIsActive, this.colCreatedAt, this.colEdit, this.colDelete
            });

            // ===== Close Button =====
            this.btnClose.Text = "Close";
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnClose.Size = new System.Drawing.Size(90, 32);
            this.btnClose.Location = new System.Drawing.Point(1145, 660);
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
            this.Controls.Add(this.dgvLocations);
            this.Controls.Add(this.btnClose);
        }
        #endregion
    }
}