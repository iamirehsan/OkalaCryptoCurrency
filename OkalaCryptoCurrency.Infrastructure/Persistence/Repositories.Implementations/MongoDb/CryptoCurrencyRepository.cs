using OkalaCryptoCurrency.Domain.Entites;
using OkalaCryptoCurrency.Domain.Interfaces.Repositories.MongoDb;
using OkalaCryptoCurrency.Infrastructure.Persistence.DbContext.MongoDb;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OkalaCryptoCurrency.Infrastructure.Persistence.Repositories.Implementations.MongoDb
{
    public class CryptoCurrencyRepository : Repository<CryptoCurrency>, ICryptoCurrencyRepository
    {

        public CryptoCurrencyRepository(MongoDbContext context) : base(context, "CryptoCurrencies")
        {
        }
    }
}
