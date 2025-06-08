using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using WorldCupDataLayer.Models;
using WorldCupWpfApp.Views;
using System.Windows.Media;
using WorldCupWpfApp.Services;

namespace WorldCupWpfApp.Controls
{
    // Custom control for displaying a player's information and image
    public partial class PlayerControl : UserControl
    {
        // Player data
        private Player _player;
        // Match events involving this player
        private List<TeamEvent> _matchEvents;
        // Team code for this player
        private string _teamCode;

        // Dependency property for flipping the player image horizontally
        public static readonly DependencyProperty IsFlippedProperty = DependencyProperty.Register(
            "IsFlipped", typeof(bool), typeof(PlayerControl), new PropertyMetadata(false, OnIsFlippedChanged));

        // Property to get/set whether the player image is flipped
        public bool IsFlipped
        {
            get => (bool)GetValue(IsFlippedProperty);
            set => SetValue(IsFlippedProperty, value);
        }

        // Handle changes to the IsFlipped property
        private static void OnIsFlippedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is PlayerControl control)
            {
                var flip = (bool)e.NewValue ? -1 : 1;
                if (control.PlayerImage.RenderTransform is ScaleTransform st)
                    st.ScaleX = flip;
            }
        }

        // Initialize the player control with player data and match events
        public PlayerControl(Player player, string teamCode, List<TeamEvent> matchEvents, bool isFlipped = false)
        {
            InitializeComponent();
            _player = player;
            _matchEvents = matchEvents;
            _teamCode = teamCode;
            IsFlipped = isFlipped;

            PlayerName.Text = player.Name;
            ShirtNumber.Text = $"#{player.ShirtNumber}";

            try
            {
                // Get the base directory of the WPF app (i.e., bin\Debug\net8.0-windows)
                string wpfBinPath = AppContext.BaseDirectory;

                // Navigate to WorldCup\ folder (solution root)
                string solutionRoot = Path.GetFullPath(Path.Combine(wpfBinPath, @"..\..\..\.."));

                // Navigate to the WinForms build output path
                string winFormsImagePath = Path.Combine(solutionRoot, @"WorldCupWinForms\bin\Debug\net8.0-windows\Resources\PlayerImages");
                string winFormsDefaultImagePath = Path.Combine(solutionRoot, @"WorldCupWinForms\bin\Debug\net8.0-windows\Resources\default_image.png");

                // Then form the final image path
                string imagePath = Path.Combine(winFormsImagePath, $"{_player.Name}.jpg");
                string defaultImagePath = winFormsDefaultImagePath;





                // Try to load the player image
                if (File.Exists(imagePath))
                {
                    PlayerImage.Source = new BitmapImage(new Uri(imagePath));
                }
                // If player image doesn't exist, try to load default image
                else if (File.Exists(defaultImagePath))
                {
                    PlayerImage.Source = new BitmapImage(new Uri(defaultImagePath));
                }
                // If no images are available, just leave the image empty
                else
                {
                    PlayerImage.Source = null;
                }
            }
            catch (Exception)
            {
                // If there's any error loading the image, just leave it empty
                PlayerImage.Source = null;
            }

            this.MouseLeftButtonUp += PlayerControl_Clicked;
        }

        // Handle click on the player control to show details
        private void PlayerControl_Clicked(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var goals = _matchEvents?.Count(ev =>
                    ev.TypeOfEvent == "goal" && ev.Player == _player.Name) ?? 0;

                var yellowCards = _matchEvents?.Count(ev =>
                    ev.TypeOfEvent == "yellow-card" && ev.Player == _player.Name) ?? 0;

                var detailWindow = new PlayerDetailsWindow(_player, goals, yellowCards, _teamCode);
                detailWindow.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(LocalizationService.Translate("error_loading_players") + ": " + ex.Message, 
                    LocalizationService.Translate("error"), 
                    MessageBoxButton.OK, 
                    MessageBoxImage.Error);
            }
        }
    }
}



