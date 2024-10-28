using Microsoft.AspNetCore.Mvc.Filters;
using QRMenu.Areas.Client.Controllers;
using System.Security.Claims;

namespace QRMenu.Areas.Client.ActionFilter
{
    public class ValidationCurrentUserAttribute
    {
        private IHttpContextAccessor _httpContextAccessor;

        public ValidationCurrentUserAttribute(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            ClaimsPrincipal claimsPrincipal =_httpContextAccessor.HttpContext.User;
            if (!claimsPrincipal.Identity.IsAuthenticated)
            {
                var controller = (AuthController)context.Controller;
                context.Result = controller.RedirectToRoute("client-auth-login");
            }
        }
    }
}
