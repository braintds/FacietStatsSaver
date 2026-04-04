using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace FacietStatsSaver.ViewModel
{
    class ApplicationViewModel
    {
        FacietStatsSaver.Model.faceIt_api _client;

        public ApplicationViewModel(string name)
        {
            _client = new(name);
        }

        public async Task<string?> checkAccAsync(string name) => (await _client.getAccountAsync(name, CancellationToken.None)).account.player_id;
        
        public async Task<string> LastMatchesAsync(DateTime from, DateTime to, decimal countMatches, decimal startPosition) => JsonConvert.SerializeObject((await _client.GetPlayerMatchesAsync(from, to, 5, 0, CancellationToken.None)));
    }
}
