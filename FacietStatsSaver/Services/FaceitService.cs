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

        public FaceitService(faceIt_api api)
        {
            _api = api;
        }

        public async Task<getPlayerByNameResponse> getAccountAsync(string accountName, CancellationToken cancellationToken)
        {
            return await _api.getAccountAsync(accountName, cancellationToken);
        }

        public async Task<List<Stats>> GetMatchesAsync(DateTime from, DateTime to, decimal countMatches, decimal startPosition)
        {
            return await _api.getPlayerMatchesAsync(from,to,countMatches,startPosition,CancellationToken.None);
        }
    }
}
