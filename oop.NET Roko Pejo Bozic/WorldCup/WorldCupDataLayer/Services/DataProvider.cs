using System.Collections.Generic;
using System.Threading.Tasks;
using WorldCupDataLayer.Config;
using WorldCupDataLayer.Models;

namespace WorldCupDataLayer.Services
{
    // Main service for retrieving World Cup data from either API or local files
    public class DataProvider
    {
        // Service for making API calls
        private readonly ApiService _apiService = new();
        // Service for reading local files
        private readonly FileService _fileService = new();
        // Data source type (api or file)
        private readonly string _source;

        // Initialize the data provider with the configured data source
        public DataProvider()
        {
            _source = ConfigService.GetDataSource();
        }

        // Get team results from either API or local files
        public async Task<List<TeamResult>> GetTeamResultsAsync(string gender)
        {
            if (_source == "api")
                return await _apiService.GetTeamResultsAsync(gender);
            else
                return _fileService.LoadTeamResults(gender);
        }

        // Get match data from either API or local files
        public async Task<List<Match>> GetMatchesAsync(string gender)
        {
            if (_source == "api")
                return await _apiService.GetMatchesAsync(gender);
            else
                return _fileService.LoadMatches(gender);
        }

        // Get team information from either API or local files
        public async Task<List<Team>> GetTeamsAsync(string gender)
        {
            if (_source == "api")
                return await _apiService.GetTeamsAsync(gender);
            else
                return _fileService.LoadTeams(gender);
        }

        // Get group results from either API or local files
        public async Task<List<GroupResult>> GetGroupResultsAsync(string gender)
        {
            if (_source == "api")
                return await _apiService.GetGroupResultsAsync(gender);
            else
                return _fileService.LoadGroupResults(gender);
        }
    }
}
