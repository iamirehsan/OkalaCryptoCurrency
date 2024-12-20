using OkalaCryptoCurrency.Domain.Interfaces.DomainService.CryptoCurrency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OkalaCryptoCurrency.Domain.DomainServices.Implementations.CryptoCurrency
{
    public class CryptoCurrencyDomainService : ICryptoCurrencyDomainService
    {
        public Entites.CryptoCurrency CreateCryptoCurrency(string code)
        {
            return Entites.CryptoCurrency.Creat(code);
        }
    }
}
