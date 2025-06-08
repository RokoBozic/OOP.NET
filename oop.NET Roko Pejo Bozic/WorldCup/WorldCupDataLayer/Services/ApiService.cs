using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using WorldCupDataLayer.Models;

namespace WorldCupDataLayer.Services
{
    // Service for making HTTP requests to the World Cup API
    public class ApiService
    {
        // HTTP client for making API requests
        private readonly HttpClient _httpClient = new();

        // JSON serialization options (case-insensitive property names)
        private readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        // Get team results from the API
        public async Task<List<TeamResult>> GetTeamResultsAsync(string gender)
        {
            try
            {
                var response = await _httpClient.GetStringAsync($"http://worldcup-vua.nullbit.hr/{gender}/teams/results");
                return JsonSerializer.Deserialize<List<TeamResult>>(response, _jsonOptions);
            }
            catch { return new List<TeamResult>(); }
        }

        // Get match data from the API
        public async Task<List<Match>> GetMatchesAsync(string gender)
        {
            try
            {
                var response = await _httpClient.GetStringAsync($"http://worldcup-vua.nullbit.hr/{gender}/matches");
                return JsonSerializer.Deserialize<List<Match>>(response, _jsonOptions);
            }
            catch { return new List<Match>(); }
        }

        // Get team information from the API
        public async Task<List<Team>> GetTeamsAsync(string gender)
        {
            try
            {
                var response = await _httpClient.GetStringAsync($"http://worldcup-vua.nullbit.hr/{gender}/teams");
                return JsonSerializer.Deserialize<List<Team>>(response, _jsonOptions);
            }
            catch { return new List<Team>(); }
        }

        // Get group results from the API
        public async Task<List<GroupResult>> GetGroupResultsAsync(string gender)
        {
            try
            {
                var response = await _httpClient.GetStringAsync($"http://worldcup-vua.nullbit.hr/{gender}/teams/group_results");
                return JsonSerializer.Deserialize<List<GroupResult>>(response, _jsonOptions);
            }
            catch { return new List<GroupResult>(); }
        }
    }
}
