using System.Windows;
using System.Windows.Media.Animation;
using WorldCupWpfApp.Services;

namespace WorldCupWpfApp.Views
{
    public partial class TeamInfoWindow : Window
    {
        private string _teamName;
        private string _fifaCode;
        private int _goals;
        private int _yellowCards;

        public TeamInfoWindow(string teamName, string fifaCode, int goals, int yellowCards)
        {
            InitializeComponent();

            _teamName = teamName;
            _fifaCode = fifaCode;
            _goals = goals;
            _yellowCards = yellowCards;

            UpdateLocalizedText();
            
            // Subscribe to language changes
            LocalizationService.LanguageChanged += (s, e) => UpdateLocalizedText();
        }

        private void UpdateLocalizedText()
        {
            Title = LocalizationService.Translate("team_info");
            TeamName.Text = $"{LocalizationService.Translate("team")}: {_teamName}";
            FifaCode.Text = $"{LocalizationService.Translate("fifa_code")}: {_fifaCode}";
            Goals.Text = $"{LocalizationService.Translate("goals")}: {_goals}";
            YellowCards.Text = $"{LocalizationService.Translate("yellow_cards")}: {_yellowCards}";
            CloseButton.Content = LocalizationService.Translate("close");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var fadeIn = new DoubleAnimation(0, 1, new Duration(System.TimeSpan.FromSeconds(0.5)));
            BeginAnimation(OpacityProperty, fadeIn);
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}


