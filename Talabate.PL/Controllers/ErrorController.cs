using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabate.PL.ErrorsHandle;

namespace Talabate.PL.Controllers
{
    [Route("errors/{code}")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    
    public class ErrorController : ControllerBase
    {
        public ActionResult errors(int code)
        {
            return NotFound(new ErrorApiResponse(code));
        }
    }
}
