using PdfSharp.Drawing;
using PdfSharp.Fonts;
using PdfSharp.Pdf;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WorldCupDataLayer.Models;
using WorldCupDataLayer.Services;

namespace WorldCupWinForms
{
    // Main form for the World Cup application
    public partial class MainForm : Form
    {
        // Data provider for fetching World Cup data
        private readonly DataProvider _dataProvider = new();
        // Selected gender (men/women)
        private string _gender = "men";  // default
        // List of all teams
        private List<Team> _teams = new();
        // List of group results
        private List<GroupResult> _groupResults = new();
        // Flag to confirm form closing
        private bool _closeConfirmed = false;
        // Drag and drop variables
        private bool isDragging = false;
        private Point dragStartPoint;
        // List of current matches
        private List<Match> _currentMatches = new List<Match>();

        // Initialize the main form
        public MainForm()
        {
            InitializeComponent();
            LocalizationService.LoadLanguage(); // Load lang first
            LoadGenderFromConfig();
            LoadTeamsAsync();
            ApplyTranslations();
            LoadGroupsAsync();
            this.KeyPreview = true;
            this.KeyDown += MainForm_KeyDown;
            GlobalFontSettings.FontResolver = CustomFontResolver.Instance;

            // Enable drag and drop for list boxes
            lstOthers.AllowDrop = true;
            lstFavorites.AllowDrop = true;

            // Add drag and drop event handlers
            lstOthers.MouseDown += ListBox_MouseDown;
            lstFavorites.MouseDown += ListBox_MouseDown;
            lstOthers.MouseMove += ListBox_MouseMove;
            lstFavorites.MouseMove += ListBox_MouseMove;
            lstOthers.DragEnter += ListBox_DragEnter;
            lstFavorites.DragEnter += ListBox_DragEnter;
            lstOthers.DragDrop += ListBox_DragDrop;
            lstFavorites.DragDrop += ListBox_DragDrop;

            // Disable double-click behavior
            lstOthers.DoubleClick += lstOthers_DoubleClick;
            lstFavorites.DoubleClick += lstFavorites_DoubleClick;
        }

        // Handle escape key press
        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close(); // Will trigger FormClosing
            }
        }

        // Load gender preference from config file
        private void LoadGenderFromConfig()
        {
            try
            {
                string path = Path.Combine("Resources", "config.txt");
                if (File.Exists(path))
                {
                    var lines = File.ReadAllLines(path);
                    foreach (var line in lines)
                    {
                        if (line.StartsWith("gender="))
                        {
                            _gender = line.Split("=")[1].Trim().ToLower();
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(LocalizationService.Translate("error_loading_config"),
                    LocalizationService.Translate("error"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // Load teams from the API
        private async void LoadTeamsAsync()
        {
            try
            {
                _teams = await _dataProvider.GetTeamsAsync(_gender);

                cmbTeams.Items.Clear();
                foreach (var team in _teams)
                    cmbTeams.Items.Add($"{team.Country} ({team.FifaCode})");

                LoadFavoriteTeam();
            }
            catch (Exception ex)
            {
                MessageBox.Show(LocalizationService.Translate("error_loading_teams"),
                    LocalizationService.Translate("error"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // Load user's favorite team from file
        private void LoadFavoriteTeam()
        {
            try
            {
                string favPath = Path.Combine("Resources", "favorite_team.txt");
                if (File.Exists(favPath))
                {
                    var savedCode = File.ReadAllText(favPath).Trim();
                    var index = _teams.FindIndex(t => t.FifaCode == savedCode);
                    if (index >= 0)
                        cmbTeams.SelectedIndex = index;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(LocalizationService.Translate("error_loading_favorite"),
                    LocalizationService.Translate("error"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // Save selected team as favorite
        private void btnSaveTeam_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbTeams.SelectedIndex >= 0)
                {
                    var selectedTeam = _teams[cmbTeams.SelectedIndex];
                    string path = Path.Combine("Resources", "favorite_team.txt");
                    File.WriteAllText(path, selectedTeam.FifaCode);

                    MessageBox.Show(
                        string.Format(LocalizationService.Translate("favorite_saved"), $"{selectedTeam.Country} ({selectedTeam.FifaCode})"),
                        LocalizationService.Translate("success")
                    );
                }
                else
                {
                    MessageBox.Show(LocalizationService.Translate("please_select_team"));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(LocalizationService.Translate("error_saving_team"),
                    LocalizationService.Translate("error"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // Apply translations to all UI elements
        private void ApplyTranslations()
        {
            lblTeam.Text = LocalizationService.Translate("select_team");
            btnSaveTeam.Text = LocalizationService.Translate("save_selection");
            cmbGroups.Text = LocalizationService.Translate("select_group");
            lblFav.Text = LocalizationService.Translate("favorite_players");
            lblOthers.Text = LocalizationService.Translate("other_players");

            btnAddToFavorites.Text = LocalizationService.Translate("add_to_favorites");
            btnRemoveFromFavorites.Text = LocalizationService.Translate("remove_from_favorites");

            btnShowPlayers.Text = LocalizationService.Translate("show_players");
            btnShowMatches.Text = LocalizationService.Translate("show_matches");
            btnShowMatchRankings.Text = LocalizationService.Translate("show_match_rankings");
            btnSetImage.Text = LocalizationService.Translate("set_image");
            btnShowPR.Text = LocalizationService.Translate("show_player_rankings");
            btnExportPdf.Text = LocalizationService.Translate("export_pdf");
        }

        // Show players for selected team
        private async void btnShowPlayers_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbTeams.SelectedIndex < 0)
                {
                    MessageBox.Show(LocalizationService.Translate("please_select_team"));
                    return;
                }

                var selectedTeam = _teams[cmbTeams.SelectedIndex].Country;
                _currentMatches = await _dataProvider.GetMatchesAsync(_gender);
                var players = new List<Player>();

                foreach (var match in _currentMatches)
                {
                    // Collect all players for the selected team, whether home or away
                    if (match.HomeTeamCountry == selectedTeam && match.HomeTeamStatistics != null)
                    {
                        players.AddRange(match.HomeTeamStatistics.StartingEleven ?? new List<Player>());
                        players.AddRange(match.HomeTeamStatistics.Substitutes ?? new List<Player>());
                    }
                    if (match.AwayTeamCountry == selectedTeam && match.AwayTeamStatistics != null)
                    {
                        players.AddRange(match.AwayTeamStatistics.StartingEleven ?? new List<Player>());
                        players.AddRange(match.AwayTeamStatistics.Substitutes ?? new List<Player>());
                    }
                }

                // Group by player name (case-insensitive, trimmed) and combine captain status
                var uniquePlayers = players
                    .GroupBy(p => p.Name.Trim(), StringComparer.OrdinalIgnoreCase)
                    .Select(g =>
                    {
                        var first = g.First();
                        first.Captain = g.Any(p => p.Captain); // Set Captain true if ever a captain
                        return first;
                    })
                    .OrderBy(p => p.ShirtNumber)
                    .ToList();

                var allPlayers = uniquePlayers.Select(p =>
                    $"{p.Name} #{p.ShirtNumber} - {p.Position}" + (p.Captain ? " (Captain)" : "")
                ).ToList();

                var savedFavorites = File.Exists("Resources/favorite_players.txt")
                    ? File.ReadAllLines("Resources/favorite_players.txt").ToList()
                    : new List<string>();

                lstFavorites.Items.Clear();
                lstOthers.Items.Clear();

                foreach (var player in allPlayers)
                {
                    if (savedFavorites.Contains(player))
                        lstFavorites.Items.Add(player);
                    else
                        lstOthers.Items.Add(player);
                }

                // Clear panels when showing new players
                ClearPlayerPanels();
            }
            catch (Exception ex)
            {
                MessageBox.Show(LocalizationService.Translate("error_loading_players"),
                    LocalizationService.Translate("error"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private async void btnShowMatches_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbTeams.SelectedIndex < 0)
                {
                    MessageBox.Show(LocalizationService.Translate("please_select_team"));
                    return;
                }

                var selectedTeam = _teams[cmbTeams.SelectedIndex].Country;
                var matches = await _dataProvider.GetMatchesAsync(_gender);

                var teamMatches = matches
                    .Where(m => m.HomeTeamCountry == selectedTeam || m.AwayTeamCountry == selectedTeam)
                    .OrderBy(m => m.Datetime)
                    .ToList();

                lstMatches.Items.Clear();

                if (teamMatches.Count == 0)
                {
                    lstMatches.Items.Add(LocalizationService.Translate("no_matches_found"));
                    return;
                }

                foreach (var match in teamMatches)
                {
                    string opponent = match.HomeTeamCountry == selectedTeam
                        ? match.AwayTeamCountry
                        : match.HomeTeamCountry;

                    int teamGoals = match.HomeTeamCountry == selectedTeam
                        ? match.HomeTeam.Goals
                        : match.AwayTeam.Goals;

                    int opponentGoals = match.HomeTeamCountry == selectedTeam
                        ? match.AwayTeam.Goals
                        : match.HomeTeam.Goals;

                    string stage = TranslateStageName(match.StageName);

                    string line = string.Format(
                        LocalizationService.Translate("match_format"),
                        match.Datetime.ToString("dd MMM yyyy", CultureInfo.CurrentCulture),
                        selectedTeam,
                        opponent,
                        teamGoals,
                        opponentGoals,
                        stage
                    );

                    lstMatches.Items.Add(line);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(LocalizationService.Translate("error_loading_matches"),
                    LocalizationService.Translate("error"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private string TranslateStageName(string stage)
        {
            return stage.ToLower() switch
            {
                "first stage" => LocalizationService.Translate("first_stage"),
                "quarter-final" => LocalizationService.Translate("quarter_final"),
                "semi-final" => LocalizationService.Translate("semi_final"),
                "final" => LocalizationService.Translate("final"),
                _ => stage
            };
        }

        private void cmbGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbGroups.SelectedIndex < 0 || _groupResults.Count == 0)
                    return;

                var selectedLetter = cmbGroups.SelectedItem.ToString().Split(' ').Last();
                var group = _groupResults.FirstOrDefault(g => g.Letter == selectedLetter);

                lstGroupStandings.Items.Clear();
                if (group != null)
                {
                    foreach (var team in group.OrderedTeams)
                        lstGroupStandings.Items.Add($"{team.Country} ({team.FifaCode})");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(LocalizationService.Translate("error_loading_group_standings"),
                    LocalizationService.Translate("error"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private async void LoadGroupsAsync()
        {
            try
            {
                _groupResults = await _dataProvider.GetGroupResultsAsync(_gender);
                cmbGroups.Items.Clear();

                foreach (var group in _groupResults.OrderBy(g => g.Letter))
                    cmbGroups.Items.Add($"Group {group.Letter}");

                if (cmbGroups.Items.Count > 0)
                    cmbGroups.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(LocalizationService.Translate("error_loading_groups"),
                    LocalizationService.Translate("error"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void ListBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ListBox listBox = (ListBox)sender;
                int index = listBox.IndexFromPoint(e.X, e.Y);
                if (index != ListBox.NoMatches)
                {
                    // Store the starting point for drag detection
                    dragStartPoint = new Point(e.X, e.Y);
                    isDragging = false;
                }
            }
        }

        private void ListBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && !isDragging)
            {
                // Check if mouse has moved enough to start a drag
                if (Math.Abs(e.X - dragStartPoint.X) > 5 || Math.Abs(e.Y - dragStartPoint.Y) > 5)
                {
                    ListBox listBox = (ListBox)sender;
                    int index = listBox.IndexFromPoint(dragStartPoint.X, dragStartPoint.Y);
                    if (index != ListBox.NoMatches)
                    {
                        isDragging = true;
                        string item = listBox.Items[index].ToString();
                        var dragData = new DataObject();
                        dragData.SetData("Item", item);
                        dragData.SetData("SourceList", listBox);
                        listBox.DoDragDrop(dragData, DragDropEffects.Move);
                    }
                }
            }
        }

        private void ListBox_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("Item"))
            {
                e.Effect = DragDropEffects.Move;
            }
        }

        private void ListBox_DragDrop(object sender, DragEventArgs e)
        {
            ListBox targetListBox = (ListBox)sender;

            if (e.Data.GetDataPresent("Item"))
            {
                string draggedItem = (string)e.Data.GetData("Item");
                ListBox sourceListBox = (ListBox)e.Data.GetData("SourceList");

                // Only check the limit when moving from others to favorites
                if (targetListBox == lstFavorites && sourceListBox == lstOthers)
                {
                    if (lstFavorites.Items.Count >= 3)
                    {
                        MessageBox.Show(LocalizationService.Translate("max_favorites_warning"));
                        return;
                    }
                }

                // Remove from source and add to target
                sourceListBox.Items.Remove(draggedItem);
                targetListBox.Items.Add(draggedItem);
            }
        }

        private void btnAddToFavorites_Click(object sender, EventArgs e)
        {
            try
            {
                int currentCount = lstFavorites.Items.Count;
                int toAddCount = lstOthers.SelectedItems.Count;

                if (currentCount + toAddCount > 3)
                {
                    MessageBox.Show(LocalizationService.Translate("max_favorites_warning"));
                    return;
                }

                foreach (var item in lstOthers.SelectedItems.Cast<string>().ToList())
                {
                    lstFavorites.Items.Add(item);
                    lstOthers.Items.Remove(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(LocalizationService.Translate("error_adding_favorites"),
                    LocalizationService.Translate("error"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnRemoveFromFavorites_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (var item in lstFavorites.SelectedItems.Cast<string>().ToList())
                {
                    lstOthers.Items.Add(item);
                    lstFavorites.Items.Remove(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(LocalizationService.Translate("error_removing_favorites"),
                    LocalizationService.Translate("error"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnSaveFavorites_Click(object sender, EventArgs e)
        {
            try
            {
                File.WriteAllLines("Resources/favorite_players.txt", lstFavorites.Items.Cast<string>());
                MessageBox.Show(LocalizationService.Translate("favorites_saved"));
            }
            catch (Exception ex)
            {
                MessageBox.Show(LocalizationService.Translate("error_saving_favorites"),
                    LocalizationService.Translate("error"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private async void btnShowMatchRankings_Click(object sender, EventArgs e)
        {
            if (cmbTeams.SelectedIndex < 0)
            {
                MessageBox.Show(LocalizationService.Translate("please_select_team"));
                return;
            }

            var selectedTeam = _teams[cmbTeams.SelectedIndex].Country;
            var matches = await _dataProvider.GetMatchesAsync(_gender);

            var teamMatches = matches
                .Where(m => m.HomeTeamCountry == selectedTeam || m.AwayTeamCountry == selectedTeam)
                .Where(m => int.TryParse(m.Attendance, out _))
                .Select(m => new
                {
                    m.HomeTeamCountry,
                    m.AwayTeamCountry,
                    m.Location,
                    Attendance = int.Parse(m.Attendance)
                })
                .OrderByDescending(m => m.Attendance)
                .ToList();

            lstMatchRankings.Items.Clear();

            if (teamMatches.Count == 0)
            {
                lstMatchRankings.Items.Add(LocalizationService.Translate("no_match_rankings"));
                return;
            }

            foreach (var match in teamMatches)
            {
                lstMatchRankings.Items.Add(
                    string.Format(LocalizationService.Translate("match_ranking_format"),
                        match.HomeTeamCountry,
                        match.AwayTeamCountry,
                        match.Location,
                        match.Attendance)
                );
            }
        }

        private void lstFavorites_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstFavorites.SelectedIndex >= 0)
            {
                UpdatePlayerPanel(lstFavorites.SelectedItem.ToString(), true);
            }
        }

        private void lstOthers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstOthers.SelectedIndex >= 0)
            {
                UpdatePlayerPanel(lstOthers.SelectedItem.ToString(), false);
            }
        }

        private void UpdatePlayerPanel(string playerInfo, bool isFavorite)
        {
            // Parse player info string (format: "Name #Number - Position (Captain)")
            var parts = playerInfo.Split(new[] { " #", " - " }, StringSplitOptions.None);
            if (parts.Length < 3) return;

            string name = parts[0];
            string number = parts[1];
            string position = parts[2].Replace(" (Captain)", "");
            bool isCaptain = parts[2].Contains("(Captain)");

            // Get the current team
            string team = cmbTeams.SelectedItem?.ToString() ?? "";

            // Use already loaded matches
            var playerStats = GetPlayerStats(name, _currentMatches);

            // Build stats text
            var statsText = new List<string>();
            statsText.Add($"{LocalizationService.Translate("captain")}: {playerStats.IsCaptain}");
            statsText.Add($"{LocalizationService.Translate("appearances")}: {playerStats.Appearances}");
            statsText.Add($"{LocalizationService.Translate("goals")}: {playerStats.Goals}");
            statsText.Add($"{LocalizationService.Translate("yellow_cards")}: {playerStats.YellowCards}");

            // Image loading logic
            string sanitizedName = SanitizeFileName(name);
            string customImagePath = Path.Combine("Resources", "PlayerImages", $"{sanitizedName}.jpg");
            string defaultPlayerImagePath = Path.Combine("Resources", "Players", $"{name.ToLower().Replace(" ", "_")}.jpg");
            string genericDefaultImagePath = Path.Combine("Resources", "default_image.png");

            Image playerImage = null;
            if (File.Exists(customImagePath))
            {
                playerImage = Image.FromFile(customImagePath);
            }
            else if (File.Exists(defaultPlayerImagePath))
            {
                playerImage = Image.FromFile(defaultPlayerImagePath);
            }
            else if (File.Exists(genericDefaultImagePath))
            {
                playerImage = Image.FromFile(genericDefaultImagePath);
            }
            // Always set the image, even if null (to clear previous image)
            if (isFavorite)
            {
                lblPlayerName.Text = name;
                lblPlayerNumber.Text = $"#{number}";
                lblPlayerPosition.Text = position;
                lblPlayerTeam.Text = team;
                lblPlayerStats.Text = string.Join("\n", statsText);
                picPlayerImage.Image = playerImage;
            }
            else
            {
                lblOtherPlayerName.Text = name;
                lblOtherPlayerNumber.Text = $"#{number}";
                lblOtherPlayerPosition.Text = position;
                lblOtherPlayerTeam.Text = team;
                lblOtherPlayerStats.Text = string.Join("\n", statsText);
                picOtherPlayerImage.Image = playerImage;
            }
        }

        private class PlayerStats
        {
            public int Goals { get; set; }
            public int YellowCards { get; set; }
            public int Appearances { get; set; }
            public bool IsCaptain { get; set; }
        }

        private PlayerStats GetPlayerStats(string playerName, List<Match> matches)
        {
            var stats = new PlayerStats();
            try
            {
                foreach (var match in matches)
                {
                    // Check home team events
                    if (match.HomeTeamEvents != null)
                    {
                        foreach (var evt in match.HomeTeamEvents)
                        {
                            if (evt.Player == playerName)
                            {
                                if (evt.TypeOfEvent == "goal" || evt.TypeOfEvent == "goal-penalty")
                                    stats.Goals++;
                                else if (evt.TypeOfEvent == "yellow-card")
                                    stats.YellowCards++;
                            }
                        }
                    }

                    // Check away team events
                    if (match.AwayTeamEvents != null)
                    {
                        foreach (var evt in match.AwayTeamEvents)
                        {
                            if (evt.Player == playerName)
                            {
                                if (evt.TypeOfEvent == "goal" || evt.TypeOfEvent == "goal-penalty")
                                    stats.Goals++;
                                else if (evt.TypeOfEvent == "yellow-card")
                                    stats.YellowCards++;
                            }
                        }
                    }

                    // Count appearances and check captain status
                    if (match.HomeTeamStatistics != null)
                    {
                        var player = (match.HomeTeamStatistics.StartingEleven?.FirstOrDefault(p => p.Name == playerName) ??
                                      match.HomeTeamStatistics.Substitutes?.FirstOrDefault(p => p.Name == playerName));
                        if (player != null)
                        {
                            stats.Appearances++;
                            if (player.Captain)
                                stats.IsCaptain = true;
                        }
                    }
                    if (match.AwayTeamStatistics != null)
                    {
                        var player = (match.AwayTeamStatistics.StartingEleven?.FirstOrDefault(p => p.Name == playerName) ??
                                      match.AwayTeamStatistics.Substitutes?.FirstOrDefault(p => p.Name == playerName));
                        if (player != null)
                        {
                            stats.Appearances++;
                            if (player.Captain)
                                stats.IsCaptain = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Optionally log the error or show a message
                // MessageBox.Show($"Error calculating player stats: {ex.Message}");
            }
            return stats;
        }

        private string SanitizeFileName(string fileName)
        {
            return string.Join("_", fileName.Split(Path.GetInvalidFileNameChars()));
        }

        private void ClearPlayerPanels()
        {
            // Clear favorite player panel
            lblPlayerName.Text = "";
            lblPlayerNumber.Text = "";
            lblPlayerPosition.Text = "";
            lblPlayerTeam.Text = "";
            lblPlayerStats.Text = "";
            picPlayerImage.Image = null;

            // Clear other player panel
            lblOtherPlayerName.Text = "";
            lblOtherPlayerNumber.Text = "";
            lblOtherPlayerPosition.Text = "";
            lblOtherPlayerTeam.Text = "";
            lblOtherPlayerStats.Text = "";
            picOtherPlayerImage.Image = null;
        }

        private void btnSetImage_Click(object sender, EventArgs e)
        {
            try
            {
                string playerKey = null;
                bool isFavorite = false;

                // Check both listboxes for a selected item
                if (lstFavorites.SelectedItem != null)
                {
                    playerKey = lstFavorites.SelectedItem.ToString();
                    isFavorite = true;
                }
                else if (lstOthers.SelectedItem != null)
                {
                    playerKey = lstOthers.SelectedItem.ToString();
                    isFavorite = false;
                }
                else
                {
                    MessageBox.Show(LocalizationService.Translate("select_player_first"));
                    return;
                }

                using OpenFileDialog ofd = new OpenFileDialog();
                ofd.Title = LocalizationService.Translate("select_player_image");
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string selectedPath = ofd.FileName;
                    // Extract only the player name from the listbox item
                    string name = playerKey.Split(new[] { " #" }, StringSplitOptions.None)[0];
                    string sanitizedName = SanitizeFileName(name);

                    string destinationFolder = Path.Combine("Resources", "PlayerImages");
                    Directory.CreateDirectory(destinationFolder);

                    string destinationPath = Path.Combine(destinationFolder, $"{sanitizedName}.jpg");

                    File.Copy(selectedPath, destinationPath, true);

                    // Refresh the panel to show the new image
                    UpdatePlayerPanel(playerKey, isFavorite);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(LocalizationService.Translate("error_setting_image"),
                    LocalizationService.Translate("error"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Only prompt if the user is closing the form (not system shutdown, etc.)
            if (e.CloseReason != CloseReason.UserClosing) return;

            // Cancel the current close attempt
            e.Cancel = true;

            var result = MessageBox.Show(
                LocalizationService.Translate("confirm_exit"),
                LocalizationService.Translate("confirmation"),
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button1
            );

            if (result == DialogResult.Yes)
            {
                e.Cancel = false;  // Allow the form to close
            }
        }

        private string NormalizeName(string name)
        {
            if (string.IsNullOrEmpty(name)) return "";
            var normalized = name.Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder();
            foreach (var c in normalized)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                    sb.Append(c);
            }
            return sb.ToString().ToLowerInvariant().Trim();
        }


        private async void btnShowPR_Click(object sender, EventArgs e)
        {
            if (cmbTeams.SelectedIndex < 0)
            {
                MessageBox.Show(LocalizationService.Translate("please_select_team"));
                return;
            }

            try
            {
                var selectedTeam = _teams[cmbTeams.SelectedIndex].Country;
                var matches = await _dataProvider.GetMatchesAsync(_gender);

                // Use normalized player name as key
                var playersStats = new Dictionary<string, (string Name, int Goals, int YellowCards, int Appearances)>();

                foreach (var match in matches)
                {
                    // Get all players for the selected team in this match
                    List<Player> allPlayers = new List<Player>();
                    if (match.HomeTeamCountry == selectedTeam && match.HomeTeamStatistics != null)
                    {
                        allPlayers.AddRange(match.HomeTeamStatistics.StartingEleven ?? new List<Player>());
                        allPlayers.AddRange(match.HomeTeamStatistics.Substitutes ?? new List<Player>());
                    }
                    if (match.AwayTeamCountry == selectedTeam && match.AwayTeamStatistics != null)
                    {
                        allPlayers.AddRange(match.AwayTeamStatistics.StartingEleven ?? new List<Player>());
                        allPlayers.AddRange(match.AwayTeamStatistics.Substitutes ?? new List<Player>());
                    }

                    // Build a set of normalized player names for the selected team
                    var playerNameSet = new HashSet<string>(allPlayers.Select(p => NormalizeName(p.Name)));

                    // Build a mapping from normalized player name to player object
                    var playerMap = allPlayers
                        .GroupBy(p => NormalizeName(p.Name))
                        .ToDictionary(g => g.Key, g => g.First());

                    // Count appearances for players in the match
                    foreach (var player in allPlayers)
                    {
                        var key = NormalizeName(player.Name);
                        if (!playersStats.ContainsKey(key))
                            playersStats[key] = (player.Name, 0, 0, 0);

                        var stats = playersStats[key];
                        stats.Appearances++;
                        playersStats[key] = stats;
                    }

                    // Process both HomeTeamEvents and AwayTeamEvents
                    var allEvents = new List<TeamEvent>();
                    if (match.HomeTeamEvents != null) allEvents.AddRange(match.HomeTeamEvents);
                    if (match.AwayTeamEvents != null) allEvents.AddRange(match.AwayTeamEvents);

                    foreach (var evt in allEvents)
                    {
                        if (string.IsNullOrEmpty(evt.Player)) continue;
                        var evtKey = NormalizeName(evt.Player);

                        // Only count events for players in the selected team
                        if (!playerNameSet.Contains(evtKey)) continue;

                        var playerName = playerMap[evtKey].Name;
                        var key = NormalizeName(playerName);
                        if (!playersStats.ContainsKey(key))
                            playersStats[key] = (playerName, 0, 0, 0);

                        var stats = playersStats[key];

                        if (evt.TypeOfEvent == "goal" || evt.TypeOfEvent == "goal-penalty")
                            stats.Goals++;
                        else if (evt.TypeOfEvent == "yellow-card")
                            stats.YellowCards++;

                        playersStats[key] = stats;
                    }
                }

                // Sort by goals, then yellow cards, then appearances
                var ranked = playersStats.Values
                    .OrderByDescending(p => p.Goals)
                    .ThenByDescending(p => p.YellowCards)
                    .ThenByDescending(p => p.Appearances)
                    .ToList();

                lstPlayerRankings.Items.Clear();

                if (ranked.Count == 0)
                {
                    lstPlayerRankings.Items.Add(LocalizationService.Translate("no_player_rankings"));
                    return;
                }

                foreach (var player in ranked)
                {
                    lstPlayerRankings.Items.Add($"{player.Name} - ⚽ {player.Goals}, 🟨 {player.YellowCards}, 👕 {player.Appearances}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading player rankings: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExportPdf_Click(object sender, EventArgs e)
        {
            try
            {
                var doc = new PdfSharp.Pdf.PdfDocument();
                doc.Info.Title = "Rankings";

                var page = doc.AddPage();
                var gfx = PdfSharp.Drawing.XGraphics.FromPdfPage(page);
                var font = new PdfSharp.Drawing.XFont("Helvetica", 12);

                double y = 40;

                gfx.DrawString("Player Rankings", font, PdfSharp.Drawing.XBrushes.Black, new PdfSharp.Drawing.XPoint(40, y));
                y += 20;

                foreach (var item in lstPlayerRankings.Items)
                {
                    gfx.DrawString(item.ToString(), font, PdfSharp.Drawing.XBrushes.Black, new PdfSharp.Drawing.XPoint(40, y));
                    y += 15;
                }

                y += 20;
                gfx.DrawString("Match Rankings", font, PdfSharp.Drawing.XBrushes.Black, new PdfSharp.Drawing.XPoint(40, y));
                y += 20;

                foreach (var item in lstMatches.Items)
                {
                    gfx.DrawString(item.ToString(), font, PdfSharp.Drawing.XBrushes.Black, new PdfSharp.Drawing.XPoint(40, y));
                    y += 15;
                }

                string file = Path.Combine("Resources", "rankings.pdf");
                Directory.CreateDirectory("Resources");
                doc.Save(file);

                MessageBox.Show($"{LocalizationService.Translate("pdf_saved")}\n{file}", LocalizationService.Translate("success"));

            }
            catch (Exception ex)
            {
                MessageBox.Show($"{LocalizationService.Translate("pdf_export_failed")}\n{ex.Message}", LocalizationService.Translate("error"));

            }
        }

        private void lstFavorites_DoubleClick(object sender, EventArgs e)
        {
            // Prevent double-click from doing anything
            return;
        }

        private void lstOthers_DoubleClick(object sender, EventArgs e)
        {
            // Prevent double-click from doing anything
            return;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }
}
