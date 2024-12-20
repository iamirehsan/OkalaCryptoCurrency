using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OkalaCryptoCurrency.Application.DTOs.CryptoCurrency
{
    public record CryptoCurrencyDTO(CryptoCurrencyContentDTO Data, IEnumerable<string> Errors)
    {
    }
    public record CryptoCurrencyContentDTO(IEnumerable<CurrencyDTO> Currencies , string Code)
    {
    }
    public record CurrencyDTO(decimal Price, string Symbol)
    {
    }
    public record RecentCryptoCurrencyDTO(IEnumerable<string> Code)
    {
    }
}
