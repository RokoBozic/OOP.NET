using System;
using System.IO;
using System.Windows.Forms;

namespace WorldCupWinForms
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            string genderPath = "Resources/config.txt";
            string langPath = "Resources/language.txt";

            if (!File.Exists(genderPath) || !File.Exists(langPath))
            {
                var settingsForm = new FormSettings();
                if (settingsForm.ShowDialog() != DialogResult.OK)
                    return; // exit app if canceled
            }

            Application.Run(new MainForm()); // will create this next
        }
    }
}
