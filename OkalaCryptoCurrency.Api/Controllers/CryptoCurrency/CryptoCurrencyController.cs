using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OkalaCryptoCurrency.Api.Utility;
using OkalaCryptoCurrency.Application.Interfaces.Services.CryptoCurrency;
using OkalaCryptoCurrency.Domain.Interfaces.Log;

namespace OkalaCryptoCurrency.Api.Controllers.CryptoCurrency
{
    [Route("api/[controller]")]
    [ApiController]
    public class CryptoCurrencyController : ApiControllerBase
    {
        private readonly ICryptoCurrencyService _cryptoCurrencyService;
        private readonly ILoggerService _loggerService;

        public CryptoCurrencyController(ICryptoCurrencyService cryptoCurrencyService, ILoggerService loggerService)
        {
            _cryptoCurrencyService = cryptoCurrencyService;
            _loggerService = loggerService;
        }
        [HttpGet("GetRecentCryptoCurrency")]
        public async Task<IActionResult> GetRecentCryptoCurrency()
        {
            var result = await _cryptoCurrencyService.GetRecentCryptoCurrency();
            return OkResult(result);
        }
        [HttpGet("GetCryptoCurrencyPrice/{code}")]
        public async Task<IActionResult> GetCryptoCurrencyPrice([FromRoute] string code)
        {
            var result = await _cryptoCurrencyService.GetCryptoCurrencyPrice(code);
            return OkResult(result.Data, result.Errors);
        }
    }
}
