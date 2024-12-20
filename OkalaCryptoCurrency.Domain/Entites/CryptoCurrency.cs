using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OkalaCryptoCurrency.Domain.Entites
{
    public class CryptoCurrency : MongoBaseEntity
    {
        private CryptoCurrency(string code) : base()
        {
            Code = code;
        }
        public string Code { get; private set; }
        public static CryptoCurrency Creat(string code)
        {
            return new CryptoCurrency(code);
        }


    }
}
