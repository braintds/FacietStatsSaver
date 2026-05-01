using System;
using System.Collections.Generic;
using System.Text;
using FacietStatsSaver.Domain;

namespace FacietStatsSaver.Services
{
    public class StatisticsService : IStatisticsService
    {
        public double CalculateKD(IEnumerable<Domain.Stats> matches) => matches.Sum(x => x.KDRatio) / matches.Count();

        double IStatisticsService.CalculateHS(IEnumerable<Stats> matches) => matches.Sum(x => x.HeadshotsPercentage) / matches.Count();

        double IStatisticsService.CalculateAVG(IEnumerable<Stats> matches) => matches.Sum(x => x.Kills) / matches.Count();

        double IStatisticsService.CalcutateWins(IEnumerable<Stats> matches) => matches.Count(x => x.Result == "Win");

        double IStatisticsService.CalcutateLoses(IEnumerable<Stats> matches) => matches.Count(x=>x.Result == "Lose");
    }
}
