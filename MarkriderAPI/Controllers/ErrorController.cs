using MarkriderAPI.Controllers.errors;
using Microsoft.AspNetCore.Mvc;

namespace MarkriderAPI.Controllers
{
    [Route("errors/{code}")]
    [ApiExplorerSettings(IgnoreApi =true)]
    public class ErrorController : BaseAPiController
    {
        public IActionResult Error(int code)
        {
            return new ObjectResult(new ApiResponse(code));            
        }
    }
}