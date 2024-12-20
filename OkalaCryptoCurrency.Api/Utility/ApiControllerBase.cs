using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OkalaCryptoCurrency.Api.Utility
{
    [ApiController]
    public class ApiControllerBase : ControllerBase
    {
        [NonAction]
        protected virtual IActionResult OkResult(object Data, Status status)
        {
            return Ok(new BaseResponse(status, Data));
        }
        [NonAction]
        protected virtual IActionResult OkResult(object Data, IEnumerable<string> Errors)
        {
            return Ok(new BaseResponse(new Status(Errors), Data));
        }
        [NonAction]
        protected virtual IActionResult OkResult(Status status)
        {
            return Ok(new BaseResponse(status));
        }
        [NonAction]
        protected virtual IActionResult OkResult(object Data)
        {
            return Ok(new BaseResponse(new Status(), Data));
        }
    }
}
