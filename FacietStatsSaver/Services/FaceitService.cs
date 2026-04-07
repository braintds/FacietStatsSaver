using System;
using System.Collections.Generic;
using System.Text;
using FacietStatsSaver.Model;

namespace FacietStatsSaver.Services
{
    class FaceitService : IFaceitService
    {
        private readonly faceIt_api _api;
        public FaceitService(string accountName)
        {
            _api = new faceIt_api(accountName);
        }

        public async Task<List<Stats>> GetMatchesAsync(DateTime from, DateTime to, decimal countMatches, decimal startPosition)
        {
            return await _api.getPlayerMatchesAsync(from,to,countMatches,startPosition,CancellationToken.None);
        }
    }
}
