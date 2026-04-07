using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace FacietStatsSaver.Domain
{
    public class ApiStats
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
