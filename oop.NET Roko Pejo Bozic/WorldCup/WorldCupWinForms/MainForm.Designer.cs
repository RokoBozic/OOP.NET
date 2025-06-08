namespace WorldCupWinForms
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblTeam = new Label();
            cmbTeams = new ComboBox();
            btnSaveTeam = new Button();
            btnShowPlayers = new Button();
            lstMatchRankings = new ListBox();
            lstMatches = new ListBox();
            btnShowMatches = new Button();
            cmbGroups = new ComboBox();
            lstGroupStandings = new ListBox();
            lblFav = new Label();
            lblOthers = new Label();
            lstFavorites = new ListBox();
            lstOthers = new ListBox();
            btnAddToFavorites = new Button();
            btnSaveFavorites = new Button();
            btnRemoveFromFavorites = new Button();
            btnShowMatchRankings = new Button();
            picPlayerImage = new PictureBox();
            btnSetImage = new Button();
            btnShowPR = new Button();
            lstPlayerRankings = new ListBox();
            btnExportPdf = new Button();
            panel1 = new Panel();
            lblPlayerName = new Label();
            lblPlayerPosition = new Label();
            lblPlayerNumber = new Label();
            lblPlayerTeam = new Label();
            lblPlayerStats = new Label();
            panel2 = new Panel();
            picOtherPlayerImage = new PictureBox();
            lblOtherPlayerName = new Label();
            lblOtherPlayerPosition = new Label();
            lblOtherPlayerNumber = new Label();
            lblOtherPlayerTeam = new Label();
            lblOtherPlayerStats = new Label();
            ((System.ComponentModel.ISupportInitialize)picPlayerImage).BeginInit();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picOtherPlayerImage).BeginInit();
            SuspendLayout();
            // 
            // lblTeam
            // 
            lblTeam.AutoSize = true;
            lblTeam.Location = new Point(27, 32);
            lblTeam.Name = "lblTeam";
            lblTeam.Size = new Size(171, 25);
            lblTeam.TabIndex = 0;
            lblTeam.Text = "Select Favorite Team";
            // 
            // cmbTeams
            // 
            cmbTeams.FormattingEnabled = true;
            cmbTeams.Location = new Point(214, 29);
            cmbTeams.Name = "cmbTeams";
            cmbTeams.Size = new Size(182, 33);
            cmbTeams.TabIndex = 1;
            // 
            // btnSaveTeam
            // 
            btnSaveTeam.Location = new Point(419, 26);
            btnSaveTeam.Name = "btnSaveTeam";
            btnSaveTeam.Size = new Size(145, 36);
            btnSaveTeam.TabIndex = 2;
            btnSaveTeam.Text = "Save selection";
            btnSaveTeam.UseVisualStyleBackColor = true;
            btnSaveTeam.Click += btnSaveTeam_Click;
            // 
            // btnShowPlayers
            // 
            btnShowPlayers.Location = new Point(132, 346);
            btnShowPlayers.Name = "btnShowPlayers";
            btnShowPlayers.Size = new Size(145, 36);
            btnShowPlayers.TabIndex = 3;
            btnShowPlayers.Text = "Show Players";
            btnShowPlayers.UseVisualStyleBackColor = true;
            btnShowPlayers.Click += btnShowPlayers_Click;
            // 
            // lstMatchRankings
            // 
            lstMatchRankings.FormattingEnabled = true;
            lstMatchRankings.ItemHeight = 25;
            lstMatchRankings.Location = new Point(21, 81);
            lstMatchRankings.Name = "lstMatchRankings";
            lstMatchRankings.Size = new Size(569, 79);
            lstMatchRankings.TabIndex = 4;
            // 
            // lstMatches
            // 
            lstMatches.FormattingEnabled = true;
            lstMatches.ItemHeight = 25;
            lstMatches.Location = new Point(21, 178);
            lstMatches.Name = "lstMatches";
            lstMatches.Size = new Size(543, 79);
            lstMatches.TabIndex = 5;
            // 
            // btnShowMatches
            // 
            btnShowMatches.Location = new Point(580, 178);
            btnShowMatches.Name = "btnShowMatches";
            btnShowMatches.Size = new Size(168, 41);
            btnShowMatches.TabIndex = 6;
            btnShowMatches.Text = "Show Matches";
            btnShowMatches.UseVisualStyleBackColor = true;
            btnShowMatches.Click += btnShowMatches_Click;
            // 
            // cmbGroups
            // 
            cmbGroups.FormattingEnabled = true;
            cmbGroups.Location = new Point(22, 280);
            cmbGroups.Name = "cmbGroups";
            cmbGroups.Size = new Size(206, 33);
            cmbGroups.TabIndex = 7;
            cmbGroups.SelectedIndexChanged += cmbGroups_SelectedIndexChanged;
            // 
            // lstGroupStandings
            // 
            lstGroupStandings.FormattingEnabled = true;
            lstGroupStandings.ItemHeight = 25;
            lstGroupStandings.Location = new Point(318, 280);
            lstGroupStandings.Name = "lstGroupStandings";
            lstGroupStandings.Size = new Size(264, 54);
            lstGroupStandings.TabIndex = 8;
            // 
            // lblFav
            // 
            lblFav.AutoSize = true;
            lblFav.Location = new Point(402, 386);
            lblFav.Name = "lblFav";
            lblFav.Size = new Size(92, 25);
            lblFav.TabIndex = 9;
            lblFav.Text = "Favourites";
            // 
            // lblOthers
            // 
            lblOthers.AutoSize = true;
            lblOthers.Location = new Point(27, 386);
            lblOthers.Name = "lblOthers";
            lblOthers.Size = new Size(65, 25);
            lblOthers.TabIndex = 10;
            lblOthers.Text = "Others";
            // 
            // lstFavorites
            // 
            lstFavorites.FormattingEnabled = true;
            lstFavorites.ItemHeight = 25;
            lstFavorites.Location = new Point(402, 414);
            lstFavorites.Name = "lstFavorites";
            lstFavorites.Size = new Size(350, 129);
            lstFavorites.TabIndex = 11;
            lstFavorites.SelectedIndexChanged += lstFavorites_SelectedIndexChanged;
            // 
            // lstOthers
            // 
            lstOthers.FormattingEnabled = true;
            lstOthers.ItemHeight = 25;
            lstOthers.Location = new Point(27, 414);
            lstOthers.Name = "lstOthers";
            lstOthers.Size = new Size(350, 129);
            lstOthers.TabIndex = 12;
            lstOthers.SelectedIndexChanged += lstOthers_SelectedIndexChanged;
            // 
            // btnAddToFavorites
            // 
            btnAddToFavorites.Location = new Point(86, 578);
            btnAddToFavorites.Name = "btnAddToFavorites";
            btnAddToFavorites.Size = new Size(112, 34);
            btnAddToFavorites.TabIndex = 13;
            btnAddToFavorites.Text = "Add →";
            btnAddToFavorites.UseVisualStyleBackColor = true;
            btnAddToFavorites.Click += btnAddToFavorites_Click;
            // 
            // btnSaveFavorites
            // 
            btnSaveFavorites.Location = new Point(242, 578);
            btnSaveFavorites.Name = "btnSaveFavorites";
            btnSaveFavorites.Size = new Size(112, 34);
            btnSaveFavorites.TabIndex = 14;
            btnSaveFavorites.Text = "Save Favorites";
            btnSaveFavorites.UseVisualStyleBackColor = true;
            btnSaveFavorites.Click += btnSaveFavorites_Click;
            // 
            // btnRemoveFromFavorites
            // 
            btnRemoveFromFavorites.Location = new Point(402, 578);
            btnRemoveFromFavorites.Name = "btnRemoveFromFavorites";
            btnRemoveFromFavorites.Size = new Size(112, 34);
            btnRemoveFromFavorites.TabIndex = 15;
            btnRemoveFromFavorites.Text = "← Remove";
            btnRemoveFromFavorites.UseVisualStyleBackColor = true;
            btnRemoveFromFavorites.Click += btnRemoveFromFavorites_Click;
            // 
            // btnShowMatchRankings
            // 
            btnShowMatchRankings.Location = new Point(609, 81);
            btnShowMatchRankings.Name = "btnShowMatchRankings";
            btnShowMatchRankings.Size = new Size(139, 79);
            btnShowMatchRankings.TabIndex = 16;
            btnShowMatchRankings.Text = "Show Match Rankings";
            btnShowMatchRankings.UseVisualStyleBackColor = true;
            btnShowMatchRankings.Click += btnShowMatchRankings_Click;
            // 
            // picPlayerImage
            // 
            picPlayerImage.BorderStyle = BorderStyle.FixedSingle;
            picPlayerImage.Location = new Point(20, 20);
            picPlayerImage.Name = "picPlayerImage";
            picPlayerImage.Size = new Size(150, 150);
            picPlayerImage.SizeMode = PictureBoxSizeMode.Zoom;
            picPlayerImage.TabIndex = 17;
            picPlayerImage.TabStop = false;
            // 
            // btnSetImage
            // 
            btnSetImage.Location = new Point(609, 562);
            btnSetImage.Name = "btnSetImage";
            btnSetImage.Size = new Size(191, 50);
            btnSetImage.TabIndex = 18;
            btnSetImage.Text = "Set Image";
            btnSetImage.UseVisualStyleBackColor = true;
            btnSetImage.Click += btnSetImage_Click;
            // 
            // btnShowPR
            // 
            btnShowPR.Location = new Point(483, 665);
            btnShowPR.Name = "btnShowPR";
            btnShowPR.Size = new Size(287, 37);
            btnShowPR.TabIndex = 19;
            btnShowPR.Text = "Show Player Rankings";
            btnShowPR.UseVisualStyleBackColor = true;
            btnShowPR.Click += btnShowPR_Click;
            // 
            // lstPlayerRankings
            // 
            lstPlayerRankings.FormattingEnabled = true;
            lstPlayerRankings.ItemHeight = 25;
            lstPlayerRankings.Location = new Point(27, 665);
            lstPlayerRankings.Name = "lstPlayerRankings";
            lstPlayerRankings.Size = new Size(428, 104);
            lstPlayerRankings.TabIndex = 20;
            // 
            // btnExportPdf
            // 
            btnExportPdf.Location = new Point(31, 802);
            btnExportPdf.Name = "btnExportPdf";
            btnExportPdf.Size = new Size(424, 105);
            btnExportPdf.TabIndex = 21;
            btnExportPdf.Text = "Export Rankings to PDF";
            btnExportPdf.UseVisualStyleBackColor = true;
            btnExportPdf.Click += btnExportPdf_Click;
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(lblPlayerName);
            panel1.Controls.Add(lblPlayerPosition);
            panel1.Controls.Add(lblPlayerNumber);
            panel1.Controls.Add(lblPlayerTeam);
            panel1.Controls.Add(lblPlayerStats);
            panel1.Controls.Add(picPlayerImage);
            panel1.Location = new Point(883, 103);
            panel1.Name = "panel1";
            panel1.Size = new Size(471, 354);
            panel1.TabIndex = 22;
            // 
            // lblPlayerName
            // 
            lblPlayerName.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblPlayerName.Location = new Point(190, 20);
            lblPlayerName.Name = "lblPlayerName";
            lblPlayerName.Size = new Size(250, 25);
            lblPlayerName.TabIndex = 18;
            // 
            // lblPlayerPosition
            // 
            lblPlayerPosition.Location = new Point(190, 55);
            lblPlayerPosition.Name = "lblPlayerPosition";
            lblPlayerPosition.Size = new Size(250, 25);
            lblPlayerPosition.TabIndex = 19;
            // 
            // lblPlayerNumber
            // 
            lblPlayerNumber.Location = new Point(190, 90);
            lblPlayerNumber.Name = "lblPlayerNumber";
            lblPlayerNumber.Size = new Size(250, 25);
            lblPlayerNumber.TabIndex = 20;
            // 
            // lblPlayerTeam
            // 
            lblPlayerTeam.Location = new Point(190, 125);
            lblPlayerTeam.Name = "lblPlayerTeam";
            lblPlayerTeam.Size = new Size(250, 25);
            lblPlayerTeam.TabIndex = 21;
            // 
            // lblPlayerStats
            // 
            lblPlayerStats.Location = new Point(20, 190);
            lblPlayerStats.Name = "lblPlayerStats";
            lblPlayerStats.Size = new Size(420, 140);
            lblPlayerStats.TabIndex = 22;
            // 
            // panel2
            // 
            panel2.BackColor = Color.White;
            panel2.BorderStyle = BorderStyle.FixedSingle;
            panel2.Controls.Add(picOtherPlayerImage);
            panel2.Controls.Add(lblOtherPlayerName);
            panel2.Controls.Add(lblOtherPlayerPosition);
            panel2.Controls.Add(lblOtherPlayerNumber);
            panel2.Controls.Add(lblOtherPlayerTeam);
            panel2.Controls.Add(lblOtherPlayerStats);
            panel2.Location = new Point(883, 522);
            panel2.Name = "panel2";
            panel2.Size = new Size(471, 354);
            panel2.TabIndex = 23;
            // 
            // picOtherPlayerImage
            // 
            picOtherPlayerImage.BorderStyle = BorderStyle.FixedSingle;
            picOtherPlayerImage.Location = new Point(20, 20);
            picOtherPlayerImage.Name = "picOtherPlayerImage";
            picOtherPlayerImage.Size = new Size(150, 150);
            picOtherPlayerImage.SizeMode = PictureBoxSizeMode.Zoom;
            picOtherPlayerImage.TabIndex = 0;
            picOtherPlayerImage.TabStop = false;
            // 
            // lblOtherPlayerName
            // 
            lblOtherPlayerName.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblOtherPlayerName.Location = new Point(190, 20);
            lblOtherPlayerName.Name = "lblOtherPlayerName";
            lblOtherPlayerName.Size = new Size(250, 25);
            lblOtherPlayerName.TabIndex = 1;
            // 
            // lblOtherPlayerPosition
            // 
            lblOtherPlayerPosition.Location = new Point(190, 55);
            lblOtherPlayerPosition.Name = "lblOtherPlayerPosition";
            lblOtherPlayerPosition.Size = new Size(250, 25);
            lblOtherPlayerPosition.TabIndex = 2;
            // 
            // lblOtherPlayerNumber
            // 
            lblOtherPlayerNumber.Location = new Point(190, 90);
            lblOtherPlayerNumber.Name = "lblOtherPlayerNumber";
            lblOtherPlayerNumber.Size = new Size(250, 25);
            lblOtherPlayerNumber.TabIndex = 3;
            // 
            // lblOtherPlayerTeam
            // 
            lblOtherPlayerTeam.Location = new Point(190, 125);
            lblOtherPlayerTeam.Name = "lblOtherPlayerTeam";
            lblOtherPlayerTeam.Size = new Size(250, 25);
            lblOtherPlayerTeam.TabIndex = 4;
            // 
            // lblOtherPlayerStats
            // 
            lblOtherPlayerStats.Location = new Point(20, 190);
            lblOtherPlayerStats.Name = "lblOtherPlayerStats";
            lblOtherPlayerStats.Size = new Size(420, 140);
            lblOtherPlayerStats.TabIndex = 5;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1378, 944);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(btnExportPdf);
            Controls.Add(lstPlayerRankings);
            Controls.Add(btnShowPR);
            Controls.Add(btnSetImage);
            Controls.Add(btnShowMatchRankings);
            Controls.Add(btnRemoveFromFavorites);
            Controls.Add(btnSaveFavorites);
            Controls.Add(btnAddToFavorites);
            Controls.Add(lstOthers);
            Controls.Add(lstFavorites);
            Controls.Add(lblOthers);
            Controls.Add(lblFav);
            Controls.Add(lstGroupStandings);
            Controls.Add(cmbGroups);
            Controls.Add(btnShowMatches);
            Controls.Add(lstMatches);
            Controls.Add(lstMatchRankings);
            Controls.Add(btnShowPlayers);
            Controls.Add(btnSaveTeam);
            Controls.Add(cmbTeams);
            Controls.Add(lblTeam);
            Name = "MainForm";
            Text = "MainForm";
            FormClosing += MainForm_FormClosing;
            Load += MainForm_Load;
            ((System.ComponentModel.ISupportInitialize)picPlayerImage).EndInit();
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picOtherPlayerImage).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTeam;
        private ComboBox cmbTeams;
        private Button btnSaveTeam;
        private Button btnShowPlayers;
        private ListBox lstMatchRankings;
        private ListBox lstMatches;
        private Button btnShowMatches;
        private ComboBox cmbGroups;
        private ListBox lstGroupStandings;
        private Label lblFav;
        private Label lblOthers;
        private ListBox lstFavorites;
        private ListBox lstOthers;
        private Button btnAddToFavorites;
        private Button btnSaveFavorites;
        private Button btnRemoveFromFavorites;
        private Button btnShowMatchRankings;
        private PictureBox picPlayerImage;
        private Button btnSetImage;
        private Button btnShowPR;
        private ListBox lstPlayerRankings;
        private Button btnExportPdf;
        private Panel panel1;
        private Panel panel2;
        private PictureBox picOtherPlayerImage;
        private Label lblPlayerName;
        private Label lblPlayerPosition;
        private Label lblPlayerNumber;
        private Label lblPlayerTeam;
        private Label lblPlayerStats;
        private Label lblOtherPlayerName;
        private Label lblOtherPlayerPosition;
        private Label lblOtherPlayerNumber;
        private Label lblOtherPlayerTeam;
        private Label lblOtherPlayerStats;
    }
}