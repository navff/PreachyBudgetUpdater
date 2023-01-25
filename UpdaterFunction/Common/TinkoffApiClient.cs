using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UpdaterCloudFunction.Common.Models;

namespace UpdaterCloudFunction.Common
{
    public class TinkoffApiClient: IDisposable
    {
        private readonly HttpClient _httpClient;
        private readonly TinkoffApiSettings _tinkoffApiSettings;

        public TinkoffApiClient(TinkoffApiSettings tinkoffApiSettings, HttpClient? httpClient = null)
        {
            httpClient ??= new HttpClient();

            _httpClient = httpClient;
            _tinkoffApiSettings = tinkoffApiSettings;

            _httpClient.BaseAddress = new Uri(tinkoffApiSettings.ApiUrl);
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", _tinkoffApiSettings.AuthToken);
        }

        public async Task<List<Account>> GetAccountsAsync()
        {
            var response = await _httpClient.GetAsync("https://business.tinkoff.ru/openapi/api/v3/bank-accounts");
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<Account>>(content ?? "");
            return result ?? new List<Account>();
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}