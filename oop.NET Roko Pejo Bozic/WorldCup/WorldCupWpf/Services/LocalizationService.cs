using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Windows;

namespace WorldCupWpfApp.Services
{
    // Service for handling application localization and translations
    public static class LocalizationService
    {
        // Current language setting
        private static string _language = "english";
        // Dictionary of translation strings
        private static Dictionary<string, string> _strings = new();
        // Event triggered when language is changed
        public static event EventHandler LanguageChanged;

        // Load language settings from config file
        public static void LoadLanguage()
        {
            string configPath = Path.Combine("Resources", "language.txt");

            if (File.Exists(configPath))
            {
                _language = File.ReadAllText(configPath).Trim().ToLower();
            }

            LoadLanguageFile();
        }

        // Load translation strings from language file
        private static void LoadLanguageFile()
        {
            string langFile = Path.Combine("Resources", $"lang.{_language}.json");

            if (File.Exists(langFile))
            {
                try
                {
                    string json = File.ReadAllText(langFile);
                    _strings = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading language file: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show($"Language file not found: {langFile}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Change the application language
        public static void ChangeLanguage(string newLanguage)
        {
            if (_language != newLanguage.ToLower())
            {
                _language = newLanguage.ToLower();
                LoadLanguageFile();
                LanguageChanged?.Invoke(null, EventArgs.Empty);
            }
        }

        // Get translation for a given key
        public static string Translate(string key)
        {
            if (_strings.Count == 0)
            {
                LoadLanguage();
            }

            return _strings.TryGetValue(key, out var value) ? value : key;
        }
    }
} 