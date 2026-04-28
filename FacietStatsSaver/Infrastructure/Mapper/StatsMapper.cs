using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Newtonsoft.Json;

namespace FacietStatsSaver.Infrastructure.Mapper
{
    public static class StatsMapper
    {
        public static Domain.Stats ToDomain(this Model.Stats model)
        {
            return new Domain.Stats
            {
                Team = model.Team,
                Map = model.Map,
                Result = int.TryParse(model.Result, out var res)? res==1 ? "Win": "Lose" : "n/a",
                MatchFinishedAt = DateTimeOffset.FromUnixTimeMilliseconds(Convert.ToInt64(model.MatchFinishedAt)).LocalDateTime,
                Score = model.Score,
                Kills = Convert.ToInt32(model.Kills),
                Deaths = Convert.ToInt32(model.Deaths),
                Assists = Convert.ToInt32(model.Assists),
                KDRatio = double.TryParse(model.KDRatio, NumberStyles.Any, CultureInfo.InvariantCulture, out var kd) ? kd : 0,
                KRRatio = double.TryParse(model.KRRatio, NumberStyles.Any, CultureInfo.InvariantCulture, out var kr) ? kr : 0,
                ADR = double.TryParse(model.ADR, NumberStyles.Any, CultureInfo.InvariantCulture, out var adr) ? adr : 0,
                MVPs = Convert.ToInt32(model.MVPs),
                QuadroKills = Convert.ToInt32(model.QuadroKills),
                TripleKills = Convert.ToInt32(model.TripleKills),
                DoubleKills = Convert.ToInt32(model.DoubleKills),
                HeadshotsPercentage = double.TryParse(model.HeadshotsPercentage, NumberStyles.Any, CultureInfo.InvariantCulture, out var hs) ? hs : 0,
            };
        }
    }
}
