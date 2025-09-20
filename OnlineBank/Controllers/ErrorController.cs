using Microsoft.AspNetCore.Mvc;
using OnlineBank.Source.Interfaces;

namespace OnlineBank.Controllers
{
    public class ErrorController : BaseController
    {
        public ErrorController(IUserService userService)
            : base(userService)
        {
        }

        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            if (statusCode == 404)
            {
                ViewBag.ErrorMessage = "Sorry, the resource you requested could not be found.";
                return View("NotFound");
            }
            return View("NotFound");
        }
    }
}
