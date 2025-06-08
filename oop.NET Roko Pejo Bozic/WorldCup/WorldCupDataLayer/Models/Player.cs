using System.Text.Json.Serialization;

namespace WorldCupDataLayer.Models
{
    /// <summary>
    /// Represents a football player in the World Cup tournament.
    /// This class contains the basic information about a player including their name,
    /// shirt number, position, and captain status.
    /// </summary>
    public class Player
    {
        /// <summary>
        /// Player's full name
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// Player's jersey number
        /// </summary>
        [JsonPropertyName("shirt_number")]
        public int ShirtNumber { get; set; }

        /// <summary>
        /// Player's position (Forward, Midfielder, Defender, Goalkeeper)
        /// </summary>
        [JsonPropertyName("position")]
        public string Position { get; set; }

        /// <summary>
        /// Whether the player is the team captain
        /// </summary>
        [JsonPropertyName("captain")]
        public bool Captain { get; set; }
    }
}
