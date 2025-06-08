using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WorldCupWpfApp.Services;

namespace WorldCupWpfApp.Views
{
    public partial class InitialSettingsWindow : Window
    {
        public InitialSettingsWindow()
        {
            InitializeComponent();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Confirm_Click(sender, e);
                e.Handled = true;
            }
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            string gender = ((ComboBoxItem)ChampionshipComboBox.SelectedItem)?.Content.ToString().ToLower();
            string language = ((ComboBoxItem)LanguageComboBox.SelectedItem)?.Content.ToString().ToLower();
            string display = ((ComboBoxItem)DisplayComboBox.SelectedItem)?.Content.ToString().ToLower();

            if (gender == null || language == null || display == null)
            {
                MessageBox.Show(LocalizationService.Translate("please_select_all_fields"));
                return;
            }

            // Show confirmation dialog
            var result = MessageBox.Show(
                LocalizationService.Translate("confirm_settings_change"),
                LocalizationService.Translate("confirm_settings"),
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result != MessageBoxResult.Yes)
            {
                return;
            }

            Directory.CreateDirectory("Resources");
            File.WriteAllText("Resources/config.txt", $"gender={gender}\nsource=api");
            File.WriteAllText("Resources/language.txt", language);
            File.WriteAllText("Resources/display.txt", display);

            // Change the language immediately
            LocalizationService.ChangeLanguage(language);

            // Just close this window - App.xaml.cs will handle showing the main window
            this.DialogResult = true;
            this.Close();
        }
    }
}

