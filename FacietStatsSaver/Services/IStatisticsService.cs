using System;
using System.Collections.Generic;
using System.Text;

namespace FacietStatsSaver.Services
{
    public interface IStatisticsService
    {
        double CalculateKD(IEnumerable<Domain.Stats> matches);
        double CalculateAVG(IEnumerable<Domain.Stats> matches);

        double CalculateHS(IEnumerable<Domain.Stats> matches);

        double CalcutateWins(IEnumerable<Domain.Stats> matches);

        double CalcutateLoses(IEnumerable<Domain.Stats> matches);
    }
}
