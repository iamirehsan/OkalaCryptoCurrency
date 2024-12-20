using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OkalaCryptoCurrency.Domain.Base
{
    [Serializable]
    public class ManagedException : Exception
    {

        public ManagedException(string errorMessage)
        {
            ErrorMessage = new List<string> { errorMessage };
        }

        public ManagedException(IEnumerable<string> errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        public IEnumerable<string> ErrorMessage { get; }
    }
}
