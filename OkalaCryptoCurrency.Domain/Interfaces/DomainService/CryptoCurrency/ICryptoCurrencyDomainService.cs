using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OkalaCryptoCurrency.Domain.Interfaces.DomainService.CryptoCurrency
{
    public interface ICryptoCurrencyDomainService
    {
        public Entites.CryptoCurrency CreateCryptoCurrency(string code);
    }
}
