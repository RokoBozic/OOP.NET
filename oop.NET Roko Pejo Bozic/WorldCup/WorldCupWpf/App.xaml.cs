using System.Windows;
using WorldCupWpfApp.Views;

namespace WorldCupWpfApp
{
    public partial class App : Application
    {
        private MainWindow _mainWindow;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Check if settings exist
            if (!System.IO.File.Exists("Resources/config.txt") || !System.IO.File.Exists("Resources/language.txt"))
            {
                // Show initial settings window
                var initialSettings = new InitialSettingsWindow();
                initialSettings.ShowDialog();

                // If settings were not confirmed, shut down the application
                if (initialSettings.DialogResult != true)
                {
                    Shutdown();
                    return;
                }
            }

            // Create and show the main window
            _mainWindow = new MainWindow();
            _mainWindow.Show();
        }

        public void RestartMainWindow()
        {
            // Close existing main window if it exists
            _mainWindow?.Close();

            // Create and show new main window
            _mainWindow = new MainWindow();
            _mainWindow.Show();
        }
    }
}

