using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace FacietStatsSaver.Model
{
    public class getPlayerByNameResponse{
        public Account account;
        public getPlayerByNameResponse(Account Account) => account = Account;
    }

    public class getPlayerMatchesResponse {
        public MatchesStats? matches{ get; set; }
    }

    public class Account
    {
        public DateTime? activated_at { get; set; }
        public string? avatar { get; set; }
        public string? country { get; set; }
        public string? cover_featured_image { get; set; }
        public string? cover_image { get; set; }
        public string? faceit_url { get; set; }
        public List<string>? friends_ids { get; set; }
        public Games? games { get; set; }
        public object? infractions { get; set; }
        public string? membership_type { get; set; }
        public List<string>? memberships { get; set; }
        public string? new_steam_id { get; set; }
        public string? nickname { get; set; }
        public Platforms? platforms { get; set; }
        public string? player_id { get; set; }
        public Settings? settings { get; set; }
        public string? steam_id_64 { get; set; }
        public string? steam_nickname { get; set; }
        public bool? verified { get; set; }
    }

    public class Cs2
    {
        public string region { get; set; }
        public string game_player_id { get; set; }
        public int skill_level { get; set; }
        public int faceit_elo { get; set; }
        public string game_player_name { get; set; }
        public string skill_level_label { get; set; }
        public Regions regions { get; set; }
        public string game_profile_id { get; set; }
    }

    public class Games
    {
        public Cs2 cs2 { get; set; }
    }

    public class Infractions
    {
    }

    public class Platforms
    {
        public string steam { get; set; }
    }

    public class Regions
    {
    }
    public class Settings
    {
        public string language { get; set; }
    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Item
    {
        public Stats stats { get; set; }
    }

    public class MatchesStats
    {
        public List<Item> items { get; set; }
        public int start { get; set; }
        public int end { get; set; }
    }

    public class Stats
    {
        public string Kills { get; set; }

        [JsonProperty("Best Of")]
        public string BestOf { get; set; }

        [JsonProperty("Game Mode")]
        public string GameMode { get; set; }

        [JsonProperty("Player Id")]
        public string PlayerId { get; set; }
        public string Result { get; set; }
        public string Game { get; set; }
        public string Headshots { get; set; }
        public string Winner { get; set; }
        public string Map { get; set; }

        [JsonProperty("Quadro Kills")]
        public string QuadroKills { get; set; }

        [JsonProperty("Match Id")]
        public string MatchId { get; set; }
        public string Team { get; set; }

        [JsonProperty("Triple Kills")]
        public string TripleKills { get; set; }
        public string ADR { get; set; }

        [JsonProperty("K/D Ratio")]
        public string KDRatio { get; set; }

        [JsonProperty("Match Round")]
        public string MatchRound { get; set; }

        [JsonProperty("Match Finished At")]
        public object MatchFinishedAt { get; set; }

        [JsonProperty("Competition Id")]
        public string CompetitionId { get; set; }

        [JsonProperty("Second Half Score")]
        public string SecondHalfScore { get; set; }
        public string Nickname { get; set; }
        public string Region { get; set; }
        public string Score { get; set; }

        [JsonProperty("Headshots %")]
        public string HeadshotsPercentage { get; set; }
        public string Deaths { get; set; }

        [JsonProperty("Final Score")]
        public string FinalScore { get; set; }
        public string Rounds { get; set; }

        [JsonProperty("First Half Score")]
        public string FirstHalfScore { get; set; }

        [JsonProperty("Double Kills")]
        public string DoubleKills { get; set; }
        public string Damage { get; set; }

        [JsonProperty("Overtime score")]
        public string Overtimescore { get; set; }
        public string MVPs { get; set; }
        public string Assists { get; set; }

        [JsonProperty("Created At")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("Updated At")]
        public DateTime UpdatedAt { get; set; }

        [JsonProperty("Penta Kills")]
        public string PentaKills { get; set; }

        [JsonProperty("K/R Ratio")]
        public string KRRatio { get; set; }
    }
}