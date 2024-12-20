using Newtonsoft.Json.Linq;
using OkalaCryptoCurrency.Application.DTOs.ApiClientsResponse;
using OkalaCryptoCurrency.Application.Interfaces.ApiClients.CryptoCurrency;
using OkalaCryptoCurrency.Domain.Base;
using OkalaCryptoCurrency.Domain.Interfaces.Log;

namespace OkalaCryptoCurrency.Infrastructure.ApiClients.CryptoCurrency
{
    public class CryptoCurrencyApiClient : ICryptoCurrencyApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILoggerService _loggerService;
        private readonly string _baseAdress = Environment.GetEnvironmentVariable("CoinMarketCapBaseAdress");

        public CryptoCurrencyApiClient(HttpClient httpClient, ILoggerService loggerService)
        {
            _httpClient = httpClient;
            _loggerService = loggerService;

        }

        public async Task<CryptoCurrencyDetailResponse> GetCryptoCurrencyDetail(string cryptoCode, string currency, List<string> errors, CancellationToken token)
        {
            _loggerService.WriteDebug($"Start GetCryptoCurrencyDetail for crypto{cryptoCode} and currrency{currency}");
            var url = $"{_baseAdress}?symbol={cryptoCode}&convert={currency}";
            var response = await _httpClient.GetAsync(url, token);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var json = JObject.Parse(content);
                if (!json["data"].Any())
                    throw new ManagedException("This CryptoCurrency Does Not Exist");
                var price = json["data"][cryptoCode]["quote"][currency]["price"];
                var result = new CryptoCurrencyDetailResponse() { CuurencySymbol = currency, Price = (decimal)price };
                _loggerService.WriteDebug($"Finish GetCryptoCurrencyDetail for crypto{cryptoCode} and currrency{currency}");
                return result;

            }
            else
            {
                throw new HttpRequestException($"Error fetching Currency data: {response.StatusCode}");
            }

        }
    }

}
