using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;

namespace FacietStatsSaver.Model
{
    class faceIt_api
    {
        HttpClient _client = new HttpClient();
        string _accountName;
        private Account _account;

        string _api_key = "7022beff-d269-4ebd-9431-4036de604150";
        public faceIt_api(string accountName)
        {
            _accountName = accountName;
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _api_key);
        }

        public faceIt_api(Account account) => _account = account;

        public async Task<getPlayerByNameResponse> getAccountAsync(string accountName, CancellationToken cancellationToken)
        {
            _client.BaseAddress = new System.Uri("https://open.faceit.com/data/v4/");

            int maxAttempts = 3;
            int delayMs = 3000;

            for (int attempt = 1; attempt <= maxAttempts; attempt++)
            {
                var response = await _client.GetAsync($"players?nickname={_accountName}&game=CS2", cancellationToken);
                var deserializeResponse = JsonConvert.DeserializeObject<Account>(
                        await response.Content.ReadAsStringAsync(cancellationToken));

                if (response.IsSuccessStatusCode && !string.IsNullOrEmpty(deserializeResponse.player_id))
                {
                    _account = deserializeResponse;
                    return new getPlayerByNameResponse(_account);
                }
                if (response.StatusCode == HttpStatusCode.ServiceUnavailable &&
                    attempt < maxAttempts)
                {
                    await Task.Delay(delayMs, cancellationToken);
                    continue;
                }
                response.EnsureSuccessStatusCode();
            }
            throw new HttpRequestException("request attemps is end", null, HttpStatusCode.ServiceUnavailable);
        }

        public async Task<getPlayerMatchesResponse> GetPlayerMatchesAsync(DateTime from, DateTime to, decimal countMatches, decimal startPosition, CancellationToken cancellationToken)
        {
            
            int maxAttempts = 3;
            int delayMs = 3000;

            for (int attempt = 1; attempt <= maxAttempts; attempt++)
            {
                var response = await _client.GetAsync($"players/{_account.player_id}/games/cs2/stats?to={new DateTimeOffset(to.ToUniversalTime()).ToUnixTimeMilliseconds()}" +
                $"&from={new DateTimeOffset(from.ToUniversalTime()).ToUnixTimeMilliseconds()}&limit={countMatches}&offset={startPosition}", cancellationToken);
                if (response.IsSuccessStatusCode)
                {
                    return new getPlayerMatchesResponse()
                    {
                        matches = JsonConvert.DeserializeObject<MatchesStats>(
                            await response.Content.ReadAsStringAsync(cancellationToken))
                    };
                }
                if (response.StatusCode == HttpStatusCode.ServiceUnavailable &&
                    attempt < maxAttempts)
                {
                    await Task.Delay(delayMs, cancellationToken);
                    continue;
                }
                response.EnsureSuccessStatusCode();
            }
            throw new HttpRequestException("request attemps is end", null, HttpStatusCode.ServiceUnavailable);
        }
    }
}
