using OkalaCryptoCurrency.Application.DTOs.CryptoCurrency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OkalaCryptoCurrency.Application.Interfaces.Services.CryptoCurrency
{
    public interface ICryptoCurrencyService
    {
        public Task<CryptoCurrencyDTO> GetCryptoCurrencyPrice(string Code);
        public Task<RecentCryptoCurrencyDTO> GetRecentCryptoCurrency();
    }
}
