using OkalaCryptoCurrency.Application.DTOs.ApiClientsResponse;
using OkalaCryptoCurrency.Application.DTOs.CryptoCurrency;
using OkalaCryptoCurrency.Application.Interfaces.ApiClients.CryptoCurrency;
using OkalaCryptoCurrency.Application.Interfaces.Services.CryptoCurrency;
using OkalaCryptoCurrency.Domain.Base;
using OkalaCryptoCurrency.Domain.Interfaces.CacheService;
using OkalaCryptoCurrency.Domain.Interfaces.DomainService.CryptoCurrency;
using OkalaCryptoCurrency.Domain.Interfaces.Log;
using OkalaCryptoCurrency.Domain.Interfaces.Repositories.MongoDb;
using System.Collections;
using System.Collections.Concurrent;
using System.Linq;

namespace OkalaCryptoCurrency.Application.Services.Implementations.CryptoCurrency
{
    public class CryptoCurrencyService : ICryptoCurrencyService
    {
        private readonly ICryptoCurrencyRepository _cryptoCurrencyRepository;
        private readonly ICryptoCurrencyApiClient _cryptoCurrencyApi;
        private readonly ICacheService _cacheService;
        private readonly ICryptoCurrencyDomainService _cryptoCurrencyDomainService;
        private readonly ILoggerService _loggerService;
        private readonly IEnumerable<string> _validCurrencies = Environment.GetEnvironmentVariable("ValidCurrencies").Split(',');

        public CryptoCurrencyService(ICryptoCurrencyRepository cryptoCurrencyRepository, ICryptoCurrencyApiClient cryptoCurrencyApi,
            ICacheService cacheService, ICryptoCurrencyDomainService cryptoCurrencyDomainService, ILoggerService loggerService)
        {
            _cryptoCurrencyRepository = cryptoCurrencyRepository;
            _cryptoCurrencyApi = cryptoCurrencyApi;
            _cacheService = cacheService;
            _cryptoCurrencyDomainService = cryptoCurrencyDomainService;
            _loggerService = loggerService;

        }
        public async Task<RecentCryptoCurrencyDTO> GetRecentCryptoCurrency()
        {
            _loggerService.WriteDebug("Start GetRecentCryptoCurrency");
            var recentCryptoCurrencies = await _cryptoCurrencyRepository.GetAllAsync();
            var codes = recentCryptoCurrencies.Select(z => z.Code);
            var result = new RecentCryptoCurrencyDTO(codes);
            _loggerService.WriteDebug("Finish GetRecentCryptoCurrency");
            return result;
        }
        public async Task<CryptoCurrencyDTO> GetCryptoCurrencyPrice(string code)
        {
            _loggerService.WriteDebug("Start GetCryptoCurrencyPrice");
            List<string> errors = new();
            ConcurrentDictionary<string, decimal> currencyDict = new();
            var cryptoPriceByEachCurrencies = await GetEachCurrency(code, _validCurrencies, errors);
            await AddCryptoCurrencyToDataBase(code);
            var result = new CryptoCurrencyDTO(new CryptoCurrencyContentDTO(cryptoPriceByEachCurrencies.Select(z => new CurrencyDTO(z.Price, z.CuurencySymbol)), code), errors);
            _loggerService.WriteDebug("Finish GetCryptoCurrencyPrice");
            return result;
        }
        private async Task<IEnumerable<CryptoCurrencyDetailResponse>> GetEachCurrency(string cryptoCode, IEnumerable<string> validCurrencies, List<string> errors)
        {
            var cancellationTokenSource = new CancellationTokenSource();
            var tasks = validCurrencies.Select(async item =>
            {
                try
                {

                    return await _cryptoCurrencyApi.GetCryptoCurrencyDetail(cryptoCode, item, errors, cancellationTokenSource.Token);
                }
                catch (ManagedException ex)
                {
                    cancellationTokenSource.Cancel(); // Cancel all other tasks
                    throw; // Rethrow the ManagedException to be caught by the middleware
                }
                catch (Exception ex)
                {
                    var message = $"Errro in Fetching Currency {item}";
                    _loggerService.WriteError(message, ex);
                    errors.Add(message);
                    return null;
                }
            });


            var response = await Task.WhenAll(tasks);
            return response.Where(z => z != null);

        }
        private async Task AddCryptoCurrencyToDataBase(string code)
        {
            var entity = _cryptoCurrencyDomainService.CreateCryptoCurrency(code);
            await _cryptoCurrencyRepository.AddAsync(entity);

        }
        private async Task<CryptoCurrencyDTO> FetchCryptoCurrencyPriceFromCache(string code)
        {
            var result = await _cacheService.GetDataFromCacheAsync<CryptoCurrencyDTO>(code);
            return result;
        }
        private async Task SetCryptoCurrencyToCache(CryptoCurrencyDTO detailDTO)
        {
            await _cacheService.SetOrUpdateDataInCacheAsync(detailDTO, detailDTO.Data.Code);
        }
    }
}
