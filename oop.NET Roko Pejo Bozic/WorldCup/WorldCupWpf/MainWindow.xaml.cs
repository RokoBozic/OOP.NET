using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WorldCupDataLayer.Models;
using WorldCupDataLayer.Services;
using WorldCupWpfApp.Services;
using WorldCupWpfApp.Views;
using System.Windows.Input;

namespace WorldCupWpfApp
{
    // Main window of the World Cup WPF application
    public partial class MainWindow : Window
    {
        // List of all matches
        private List<Match> matches;
        // Currently selected match
        private Match selectedMatch;
        // Selected gender (men/women)
        private string gender;
        // List of team results
        private List<TeamResult> teams;

        // Initialize the main window
        public MainWindow()
        {
            InitializeComponent();
            ShowLoading();
            LoadData();
            UpdateLocalizedText();
            
            // Subscribe to language changes
            LocalizationService.LanguageChanged += (s, e) => UpdateLocalizedText();
        }

        // Update all text elements with current language translations
        private void UpdateLocalizedText()
        {
            SelectTeamLabel.Text = LocalizationService.Translate("select_your_team");
            SelectOpponentLabel.Text = LocalizationService.Translate("select_opponent");
            HomeInfoButton.Content = LocalizationService.Translate("your_team_info");
            AwayInfoButton.Content = LocalizationService.Translate("opponent_info");
            ShowLineupButton.Content = LocalizationService.Translate("view_starting_xi");
            BackToSettingsButton.Content = LocalizationService.Translate("settings");
            LoadingText.Text = LocalizationService.Translate("loading");
        }

        // Handle escape key press to exit application
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                var result = MessageBox.Show(
                    LocalizationService.Translate("confirm_exit"),
                    LocalizationService.Translate("confirmation"),
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    Close();
                }
                e.Handled = true;
            }
        }

        // Prevent buttons from getting focus when window is active
        private void Window_GotFocus(object sender, RoutedEventArgs e)
        {
            MainGrid.Focus();
        }

        // Load initial data from API and config files
        private async void LoadData()
        {
            try
            {
                gender = File.ReadAllLines("Resources/config.txt").FirstOrDefault(l => l.StartsWith("gender"))?.Split('=')[1] ?? "men";
                string favoriteTeam = File.Exists($"Resources/favorite_team_{gender}.txt")
                    ? File.ReadAllText($"Resources/favorite_team_{gender}.txt").Trim()
                    : "ENG";

                var provider = new DataProvider();
                matches = await provider.GetMatchesAsync(gender);
                teams = await provider.GetTeamResultsAsync(gender);

                TeamComboBox.ItemsSource = teams.Select(t => $"{t.Country} ({t.FifaCode})").ToList();
                TeamComboBox.SelectedIndex = teams.FindIndex(t => t.FifaCode == favoriteTeam);

                var played = matches.Where(m => m.HomeTeam.Code == favoriteTeam || m.AwayTeam.Code == favoriteTeam).ToList();
                var opponentCodes = played.Select(m =>
                    m.HomeTeam.Code == favoriteTeam ? m.AwayTeam.Code : m.HomeTeam.Code).Distinct().ToList();

                OpponentComboBox.ItemsSource = opponentCodes.Select(code =>
                    $"{teams.FirstOrDefault(t => t.FifaCode == code)?.Country} ({code})").ToList();
            }
            finally
            {
                HideLoading();
            }
        }

        // Show loading overlay
        private void ShowLoading()
        {
            LoadingOverlay.Visibility = Visibility.Visible;
        }

        // Hide loading overlay
        private void HideLoading()
        {
            LoadingOverlay.Visibility = Visibility.Collapsed;
        }

        // Handle opponent selection change
        private void OpponentComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (TeamComboBox.SelectedItem == null || OpponentComboBox.SelectedItem == null) return;

            string selectedTeamCode = TeamComboBox.SelectedItem.ToString().Split('(')[1].Trim(')', ' ');
            string opponentCode = OpponentComboBox.SelectedItem.ToString().Split('(')[1].Trim(')', ' ');

            selectedMatch = matches.FirstOrDefault(m => 
                (m.HomeTeam.Code == selectedTeamCode && m.AwayTeam.Code == opponentCode) ||
                (m.HomeTeam.Code == opponentCode && m.AwayTeam.Code == selectedTeamCode));

            if (selectedMatch != null)
            {
                ResultText.Text = $"{selectedMatch.HomeTeam.Country} {selectedMatch.HomeTeam.Goals} - {selectedMatch.AwayTeam.Goals} {selectedMatch.AwayTeam.Country}";
            }
            else
            {
                ResultText.Text = LocalizationService.Translate("no_matches_found");
            }
        }

        // Show home team information window
        private void HomeInfoButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedMatch == null) return;

            int yellowCards = selectedMatch.HomeTeamEvents
                .Count(ev => ev.TypeOfEvent == "yellow-card");

            var infoWindow = new Views.TeamInfoWindow(
                selectedMatch.HomeTeamCountry,
                selectedMatch.HomeTeam.Code,
                selectedMatch.HomeTeam.Goals,
                yellowCards
            );

            infoWindow.ShowDialog();
        }

        // Show away team information window
        private void AwayInfoButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedMatch == null) return;

            int yellowCards = selectedMatch.AwayTeamEvents
                .Count(ev => ev.TypeOfEvent == "yellow-card");

            var infoWindow = new Views.TeamInfoWindow(
                selectedMatch.AwayTeamCountry,
                selectedMatch.AwayTeam.Code,
                selectedMatch.AwayTeam.Goals,
                yellowCards
            );

            infoWindow.ShowDialog();
        }

        // Handle team selection change
        private void TeamComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (TeamComboBox.SelectedItem == null) return;

            string selectedTeamCode = TeamComboBox.SelectedItem.ToString().Split('(')[1].Trim(')', ' ');

            var played = matches.Where(m => m.HomeTeam.Code == selectedTeamCode || m.AwayTeam.Code == selectedTeamCode).ToList();
            var opponentCodes = played.Select(m =>
                m.HomeTeam.Code == selectedTeamCode ? m.AwayTeam.Code : m.HomeTeam.Code).Distinct().ToList();

            OpponentComboBox.ItemsSource = opponentCodes.Select(code =>
                $"{teams.FirstOrDefault(t => t.FifaCode == code)?.Country} ({code})").ToList();
            OpponentComboBox.SelectedIndex = 0;
        }

        // Show starting lineup window
        private void ShowLineup_Click(object sender, RoutedEventArgs e)
        {
            if (selectedMatch == null)
            {
                MessageBox.Show(
                    LocalizationService.Translate("please_select_teams"),
                    LocalizationService.Translate("no_match_selected"),
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
                return;
            }

            if (selectedMatch.HomeTeamStatistics?.StartingEleven == null || selectedMatch.AwayTeamStatistics?.StartingEleven == null)
            {
                MessageBox.Show(
                    LocalizationService.Translate("starting_lineup_unavailable"),
                    LocalizationService.Translate("data_unavailable"),
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
                return;
            }

            // Ensure only 11 players are shown
            var homeXI = selectedMatch.HomeTeamStatistics.StartingEleven?.Take(11).ToList() ?? new List<Player>();
            var awayXI = selectedMatch.AwayTeamStatistics.StartingEleven?.Take(11).ToList() ?? new List<Player>();

            if (homeXI.Count != 11 || awayXI.Count != 11)
            {
                MessageBox.Show(
                    LocalizationService.Translate("lineup_warning"),
                    LocalizationService.Translate("incomplete_lineup"),
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
            }

            var lineupWindow = new Views.StartingLineupWindow(
                homeXI,
                awayXI,
                selectedMatch.HomeTeam.Code,
                selectedMatch.AwayTeam.Code,
                selectedMatch.HomeTeamEvents,
                selectedMatch.AwayTeamEvents
            );

            lineupWindow.ShowDialog();
        }

        private void BackToSettingsButton_Click(object sender, RoutedEventArgs e)
        {
            var settingsWindow = new Views.InitialSettingsWindow();
            this.Hide(); // Hide the main window instead of closing
            var result = settingsWindow.ShowDialog();
            if (result == true)
            {
                var mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close(); // Close the old (hidden) main window
            }
            else
            {
                this.Show(); // If settings were not changed, show the main window again
            }
        }
    }
}

