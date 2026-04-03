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
        
        public async Task<string> LastMatchesAsync() => JsonConvert.SerializeObject((await _client.GetPlayerMatchesAsync(new DateTime(2026, 3, 18, 17, 0, 0), DateTime.UtcNow, 5, 0, CancellationToken.None)));
    }
}
