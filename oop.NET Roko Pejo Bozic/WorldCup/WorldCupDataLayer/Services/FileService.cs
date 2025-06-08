using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using WorldCupDataLayer.Models;

namespace WorldCupDataLayer.Services
{
    // Service for loading World Cup data from local JSON files
    public class FileService
    {
        // JSON serialization options (case-insensitive property names)
        private readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        // Load team results from local JSON file
        public List<TeamResult> LoadTeamResults(string gender)
        {
            try
            {
                string path = Path.Combine("Resources", gender, "results.json");
                return JsonSerializer.Deserialize<List<TeamResult>>(File.ReadAllText(path), _jsonOptions);
            }
            catch { return new List<TeamResult>(); }
        }

        // Load match data from local JSON file
        public List<Match> LoadMatches(string gender)
        {
            try
            {
                string path = Path.Combine("Resources", gender, "matches.json");
                return JsonSerializer.Deserialize<List<Match>>(File.ReadAllText(path), _jsonOptions);
            }
            catch { return new List<Match>(); }
        }

        // Load team information from local JSON file
        public List<Team> LoadTeams(string gender)
        {
            try
            {
                string path = Path.Combine("Resources", gender, "teams.json");
                return JsonSerializer.Deserialize<List<Team>>(File.ReadAllText(path), _jsonOptions);
            }
            catch { return new List<Team>(); }
        }

        // Load group results from local JSON file
        public List<GroupResult> LoadGroupResults(string gender)
        {
            try
            {
                string path = Path.Combine("Resources", gender, "group_results.json");
                return JsonSerializer.Deserialize<List<GroupResult>>(File.ReadAllText(path), _jsonOptions);
            }
            catch { return new List<GroupResult>(); }
        }
    }
}
