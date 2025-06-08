using System.Text.Json;

public static class LocalizationService
{
    private static string _language = "english";
    private static Dictionary<string, string> _strings = new();

    public static void LoadLanguage()
    {
        string configPath = Path.Combine("Resources", "config.txt");

        if (File.Exists(configPath))
        {
            var lines = File.ReadAllLines(configPath);
            foreach (var line in lines)
            {
                if (line.StartsWith("language="))
                {
                    _language = line.Split('=')[1].Trim().ToLower();
                    break;
                }
            }
        }

        string langFile = Path.Combine("Resources", $"lang.{_language}.json");

        if (File.Exists(langFile))
        {
            string json = File.ReadAllText(langFile);
            _strings = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
        }
        else
        {
            MessageBox.Show($"Language file not found: {langFile}");
        }
    }

    public static string Translate(string key)
    {
        return _strings.TryGetValue(key, out var value) ? value : key;
    }
}


