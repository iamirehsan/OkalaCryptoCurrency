using OkalaCryptoCurrency.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OkalaCryptoCurrency.Domain.Interfaces.Repositories.MongoDb
{
    public interface ICryptoCurrencyRepository : IRepository<CryptoCurrency>
    {
    }
}
