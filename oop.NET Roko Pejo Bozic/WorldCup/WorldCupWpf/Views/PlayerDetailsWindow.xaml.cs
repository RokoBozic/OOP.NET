using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.IO;
using WorldCupDataLayer.Models;
using WorldCupWpfApp.Services;

namespace WorldCupWpfApp.Views
{
    public partial class PlayerDetailsWindow : Window
    {
        public PlayerDetailsWindow(Player player, int goals, int yellowCards, string teamCode)
        {
            InitializeComponent();

            PlayerName.Text = player.Name;
            ShirtNumber.Text = $"{LocalizationService.Translate("shirt_number")}: {player.ShirtNumber}";
            Position.Text = $"{LocalizationService.Translate("position")}: {player.Position}";
            IsCaptain.Text = $"{LocalizationService.Translate("captain")}: {(player.Captain ? LocalizationService.Translate("yes") : LocalizationService.Translate("no"))}";
            Goals.Text = $"{LocalizationService.Translate("goals")}: {goals}";
            YellowCards.Text = $"{LocalizationService.Translate("yellow_cards")}: {yellowCards}";

            try
            {
                // Get the base directory of the WPF app (i.e., bin\Debug\net8.0-windows)
                string wpfBinPath = AppContext.BaseDirectory;

                // Navigate to WorldCup\ folder (solution root)
                string solutionRoot = Path.GetFullPath(Path.Combine(wpfBinPath, @"..\..\..\.."));

                // Navigate to the WinForms build output path
                string winFormsImagePath = Path.Combine(solutionRoot, @"WorldCupWinForms\bin\Debug\net8.0-windows\Resources\PlayerImages");
                string winFormsDefaultImagePath = Path.Combine(solutionRoot, @"WorldCupWinForms\bin\Debug\net8.0-windows\Resources\default_image.png");

                // Form the final image path
                string imagePath = Path.Combine(winFormsImagePath, $"{player.Name}.jpg");
                string defaultImagePath = winFormsDefaultImagePath;

                // Try to load the player image
                if (File.Exists(imagePath))
                {
                    PlayerImage.Source = new BitmapImage(new Uri(imagePath));
                }
                else if (File.Exists(defaultImagePath))
                {
                    PlayerImage.Source = new BitmapImage(new Uri(defaultImagePath));
                }
                else
                {
                    PlayerImage.Source = null;
                }
            }
            catch
            {
                PlayerImage.Source = null;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var fadeIn = new DoubleAnimation(0, 1, new Duration(System.TimeSpan.FromSeconds(0.3)));
            BeginAnimation(OpacityProperty, fadeIn);
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

