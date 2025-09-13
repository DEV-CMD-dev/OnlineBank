using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OnlineBank.Data.Interfaces;

namespace OnlineBank.Controllers
{
    public class BaseController : Controller
    {
        protected readonly IUserService _userService;
        public BaseController(IUserService userService)
        {
            _userService = userService;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            ViewBag.CurrentUser = userId.HasValue ? _userService.GetUser(userId.Value) : null;

            base.OnActionExecuting(context);
        }
    }
}
