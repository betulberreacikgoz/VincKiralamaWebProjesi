using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace VincKiralamaProjesi.Filters
{
    public class AdminOnlyAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var isAdmin = context.HttpContext.Session.GetString("IsAdmin");

            if (isAdmin != "true")
            {
                context.Result = new RedirectToActionResult("Login", "AdminAuth", null);
                return;
            }

            base.OnActionExecuting(context);
        }
    }
}
