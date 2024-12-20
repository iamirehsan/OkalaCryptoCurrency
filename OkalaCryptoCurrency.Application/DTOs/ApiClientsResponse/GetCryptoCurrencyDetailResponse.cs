using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace OkalaCryptoCurrency.Application.DTOs.ApiClientsResponse
{
    public record CryptoCurrencyDetailResponse
    {
        public decimal Price { get; set; }
        public string CuurencySymbol { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
     
}
