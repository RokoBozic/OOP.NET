using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using WorldCupDataLayer.Models;
using WorldCupWpfApp.Controls;
using WorldCupWpfApp.Services;
using System.Windows.Input;

namespace WorldCupWpfApp.Views
{
    // Window that displays the starting lineups for both teams in a match
    public partial class StartingLineupWindow : Window
    {
        // Order of positions from back to front (goalkeeper to forward)
        private readonly Dictionary<string, int> _positionOrder = new Dictionary<string, int>
        {
            { "Goalkeeper", 0 },
            { "Defender", 1 },
            { "Midfielder", 2 },
            { "Forward", 3 }
        };

        // Initialize the starting lineup window with player data
        public StartingLineupWindow(List<Player> homePlayers, List<Player> awayPlayers,
                                    string homeCode, string awayCode,
                                    List<TeamEvent> homeEvents, List<TeamEvent> awayEvents)
        {
            try
            {
                InitializeComponent();

                // Set window size based on display settings
                if (File.Exists("Resources/display.txt"))
                {
                    string displayMode = File.ReadAllText("Resources/display.txt").Trim().ToLower();
                    switch (displayMode)
                    {
                        case "fullscreen":
                            this.WindowState = WindowState.Maximized;
                            break;
                        case "1366x768":
                            this.Width = 1366;
                            this.Height = 768;
                            break;
                        case "1440x900":
                            this.Width = 1440;
                            this.Height = 900;
                            break;
                        case "1920x1080":
                            this.Width = 1920;
                            this.Height = 1080;
                            break;
                    }

                    // Center the window on screen
                    if (this.WindowState != WindowState.Maximized)
                    {
                        var screenWidth = SystemParameters.PrimaryScreenWidth;
                        var screenHeight = SystemParameters.PrimaryScreenHeight;
                        this.Left = (screenWidth - this.Width) / 2;
                        this.Top = (screenHeight - this.Height) / 2;
                    }
                }

                // Check if field background image exists
                try
                {
                    var test = System.Windows.Application.GetResourceStream(new Uri("pack://application:,,,/Resources/field.png"));
                    if (test == null)
                    {
                        this.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.White); // fallback
                        MessageBox.Show("field.png not found as a resource. Check its location and Build Action.", "Missing Image", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                catch
                {
                    this.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.White); // fallback
                    MessageBox.Show("Error loading field.png. Check its location and Build Action.", "Missing Image", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

                // Display players in formation
                if (homePlayers != null)
                {
                    ArrangePlayersInFormationGrid(homePlayers, homeCode, homeEvents, HomePanel, true);
                }
                if (awayPlayers != null)
                {
                    ArrangePlayersInFormationGrid(awayPlayers, awayCode, awayEvents, AwayPanel, false);
                }

                // Set window title
                this.Title = $"Starting Lineups - {homeCode} vs {awayCode}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing lineup window: {ex.Message}", 
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close();
            }
        }

        // Order of positions for displaying players
        private readonly string[] _formationOrder = new[] { "Goalkeeper", "Defender", "Midfielder", "Forward" };

        // Normalize position names to standard format
        private string NormalizePosition(string pos)
        {
            return pos switch
            {
                "Goalie" => "Goalkeeper",
                "Midfield" => "Midfielder",
                _ => pos
            };
        }

        // Arrange players in a grid formation based on their positions
        private void ArrangePlayersInFormationGrid(List<Player> players, string teamCode, List<TeamEvent> events, Grid panel, bool isHomeTeam = true)
        {
            panel.Children.Clear();
            panel.RowDefinitions.Clear();
            panel.ColumnDefinitions.Clear();

            // Group players by position in the desired order
            var grouped = _formationOrder
                .Select(pos => new { Position = pos, Players = players.Where(p => NormalizePosition(p.Position) == pos).OrderBy(p => p.ShirtNumber).ToList() })
                .Where(g => g.Players.Count > 0)
                .ToList();

            int numCols = grouped.Count;
            int maxPlayersInCol = grouped.Max(g => g.Players.Count);

            // Create columns for each position
            for (int i = 0; i < numCols; i++)
                panel.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            // Create enough rows to fit the tallest group
            for (int i = 0; i < maxPlayersInCol; i++)
                panel.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

            for (int col = 0; col < grouped.Count; col++)
            {
                // For home: GK left, A right; for away: GK right, A left
                int actualCol = isHomeTeam ? col : (grouped.Count - 1 - col);
                var group = grouped[col];
                int playerCount = group.Players.Count;
                // Vertically center the players in the column
                int startRow = (maxPlayersInCol - playerCount) / 2;
                for (int row = 0; row < playerCount; row++)
                {
                    var player = group.Players[row];
                    try
                    {
                        var control = new PlayerControl(player, teamCode, events ?? new List<TeamEvent>());
                        Grid.SetColumn(control, actualCol);
                        Grid.SetRow(control, startRow + row);
                        panel.Children.Add(control);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error creating player control for {player.Name}: {ex.Message}", 
                            "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            }
        }

        // Handle escape key press to close window
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
    }
}


