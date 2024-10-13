using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EMSFRONTEND.Filters
{
    public class RoleAuthorizationFilter : IAuthorizationFilter
    {
        private readonly string[] _allowedRoles;

        public RoleAuthorizationFilter(params string[] allowedRoles)
        {
            _allowedRoles = allowedRoles;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var userRole = context.HttpContext.Session.GetString("Role");

            if (string.IsNullOrEmpty(userRole) || !_allowedRoles.Contains(userRole))
            {
                // If the user is not logged in or doesn't have the required role, redirect to login page
                context.Result = new RedirectToActionResult("Login", "Auth", null);
            }
        }
    }

    public class AuthorizeRoleAttribute : TypeFilterAttribute
    {
        public AuthorizeRoleAttribute(params string[] roles) : base(typeof(RoleAuthorizationFilter))
        {
            Arguments = new object[] { roles };
        }
    }
}
