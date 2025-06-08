using System.IO;

namespace WorldCupDataLayer.Config
{
    public static class ConfigService
    {
        private const string ConfigPath = "Resources/config.txt";

        public static string GetDataSource()
        {
            if (!File.Exists(ConfigPath))
                return "api"; // default fallback

            var line = File.ReadAllText(ConfigPath).Trim();
            var parts = line.Split('=');
            return parts.Length == 2 ? parts[1].ToLower() : "api";
        }
    }
}
