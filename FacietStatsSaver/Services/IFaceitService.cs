using System;
using System.Collections.Generic;
using System.Text;
using FacietStatsSaver.Model;

namespace FacietStatsSaver.Services
{
    public interface IFaceitService
    {
        Task<List<Stats>> GetMatchesAsync(DateTime from, DateTime to, decimal countMatches, decimal startPosition, CancellationToken cancellationToken);
        Task<getPlayerByNameResponse> getAccountAsync(string accountName, CancellationToken cancellationToken);
    }
}
