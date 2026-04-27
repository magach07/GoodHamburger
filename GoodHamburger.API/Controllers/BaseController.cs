using GoodHamburger.Application.ResultPattern;
using Microsoft.AspNetCore.Mvc;

namespace GoodHamburger.API.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        protected ActionResult HandleResult<T>(AppResult<T> result)
        {
            return result.Success
                ? Ok(result.Data)
                : BadRequest(result.Error);
        }
    }
}