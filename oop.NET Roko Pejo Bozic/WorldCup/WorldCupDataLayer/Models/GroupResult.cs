using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WorldCupDataLayer.Models
{
    public class GroupResult
    {
        public int Id { get; set; }
        public string Letter { get; set; }

        [JsonPropertyName("ordered_teams")]
        public List<Team> OrderedTeams { get; set; }
    }
}
