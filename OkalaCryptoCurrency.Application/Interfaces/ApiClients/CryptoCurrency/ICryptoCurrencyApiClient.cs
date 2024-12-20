using OkalaCryptoCurrency.Application.DTOs.ApiClientsResponse;
using OkalaCryptoCurrency.Application.DTOs.CryptoCurrency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OkalaCryptoCurrency.Application.Interfaces.ApiClients.CryptoCurrency
{
    public interface ICryptoCurrencyApiClient
    {
        public Task<CryptoCurrencyDetailResponse> GetCryptoCurrencyDetail(string cryptoCode, string currency, List<string> errors, CancellationToken token);
    }
}
