using System.Text.Json.Serialization;

namespace WorldCupDataLayer.Models
{
    public class Team
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("country")]
        public string Country { get; set; }

        [JsonPropertyName("alternate_name")]
        public string AlternateName { get; set; }

        [JsonPropertyName("fifa_code")]
        public string FifaCode { get; set; }

        [JsonPropertyName("group_id")]
        public int GroupId { get; set; }

        [JsonPropertyName("group_letter")]
        public string GroupLetter { get; set; }
    }
}
