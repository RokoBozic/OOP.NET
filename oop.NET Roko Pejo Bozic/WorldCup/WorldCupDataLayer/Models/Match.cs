using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WorldCupDataLayer.Models
{
    // Represents a World Cup match with all its details
    public class Match
    {
        // Stadium where the match is played
        [JsonPropertyName("venue")]
        public string Venue { get; set; }

        // City where the match is played
        [JsonPropertyName("location")]
        public string Location { get; set; }

        // Current status of the match (e.g., "completed", "in progress")
        [JsonPropertyName("status")]
        public string Status { get; set; }

        // Match time
        [JsonPropertyName("time")]
        public string Time { get; set; }

        // FIFA's unique identifier for the match
        [JsonPropertyName("fifa_id")]
        public string FifaId { get; set; }

        // Number of spectators at the match
        [JsonPropertyName("attendance")]
        public string Attendance { get; set; }

        // Stage of the tournament (e.g., "Group Stage", "Round of 16")
        [JsonPropertyName("stage_name")]
        public string StageName { get; set; }

        // Home team's country name
        [JsonPropertyName("home_team_country")]
        public string HomeTeamCountry { get; set; }

        // Away team's country name
        [JsonPropertyName("away_team_country")]
        public string AwayTeamCountry { get; set; }

        // Date and time of the match
        [JsonPropertyName("datetime")]
        public DateTime Datetime { get; set; }

        // Home team's score and details
        [JsonPropertyName("home_team")]
        public TeamScore HomeTeam { get; set; }

        // Away team's score and details
        [JsonPropertyName("away_team")]
        public TeamScore AwayTeam { get; set; }

        // List of events (goals, cards, etc.) for home team
        [JsonPropertyName("home_team_events")]
        public List<TeamEvent> HomeTeamEvents { get; set; }

        // List of events (goals, cards, etc.) for away team
        [JsonPropertyName("away_team_events")]
        public List<TeamEvent> AwayTeamEvents { get; set; }

        // Statistics and lineup for home team
        [JsonPropertyName("home_team_statistics")]
        public TeamStatistics HomeTeamStatistics { get; set; }

        // Statistics and lineup for away team
        [JsonPropertyName("away_team_statistics")]
        public TeamStatistics AwayTeamStatistics { get; set; }
    }

    // Contains score information for a team
    public class TeamScore
    {
        // Team's country name
        [JsonPropertyName("country")]
        public string Country { get; set; }
        // Team's FIFA code
        [JsonPropertyName("code")]
        public string Code { get; set; }
        // Number of goals scored
        [JsonPropertyName("goals")]
        public int Goals { get; set; }
        // Number of penalty goals
        [JsonPropertyName("penalties")]
        public int Penalties { get; set; }
    }

    // Represents an event during the match (goal, card, substitution)
    public class TeamEvent
    {
        // Unique identifier for the event
        [JsonPropertyName("id")]
        public int Id { get; set; }
        // Type of event (goal, yellow card, red card, etc.)
        [JsonPropertyName("type_of_event")]
        public string TypeOfEvent { get; set; }
        // Player involved in the event
        [JsonPropertyName("player")]
        public string Player { get; set; }
        // Time when the event occurred
        [JsonPropertyName("time")]
        public string Time { get; set; }
    }

    // Contains team statistics and lineup information
    public class TeamStatistics
    {
        // Team's country name
        [JsonPropertyName("country")]
        public string Country { get; set; }
        // Team's formation/tactics
        [JsonPropertyName("tactics")]
        public string Tactics { get; set; }
        // List of starting players
        [JsonPropertyName("starting_eleven")]
        public List<Player> StartingEleven { get; set; }
        // List of substitute players
        [JsonPropertyName("substitutes")]
        public List<Player> Substitutes { get; set; }
    }
}
