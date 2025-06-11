using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.VisualBasic;
using Supportly.Application.Services;

namespace Supportly.Application.Middleware
{
    public class JwtAuthenticationAttribute : ActionFilterAttribute
    {
        private readonly string[] _roles;

        public JwtAuthenticationAttribute(params string[] roles)
        {
            _roles = roles;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var request = filterContext.HttpContext.Request;
            var token = request.Cookies["jwt"];

            try
            {
                if (token != null)
                {
                    var userInfo = AuthenticationService.ValidateToken(token);
                    if (userInfo == null)
                    {
                        filterContext.Result = new RedirectToActionResult("Unauthorized", "Unauthorized", null);
                        return;
                    }

                    if (_roles.Contains(userInfo[0]))
                    {
                        return; //Authorized user
                    }
                    else
                    {
                        filterContext.Result = new RedirectToActionResult("Unauthorized", "Unauthorized", null);
                        return;
                    }
                }
                else
                {
                    filterContext.Result = new RedirectToActionResult("Unauthorized", "Unauthorized", null);
                    return;
                }
            }
            catch (Exception ex)
            {
                filterContext.Result = new RedirectToActionResult("Unauthorized", "Unauthorized", null);
            }
        }
    }
}