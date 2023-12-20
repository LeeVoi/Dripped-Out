using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using service.Helpers;

namespace api.Authorizers;

public class AuthorizeAdminAttribute : Attribute, IAuthorizationFilter
{

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var authentication = (AuthenticationHelper)context.HttpContext.RequestServices.GetService(typeof(AuthenticationHelper));

        string token = null;

        if (context.HttpContext.Request.Headers.ContainsKey("Authorization"))
            token = context.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

        if (token == null)
        {
            context.Result = new UnauthorizedObjectResult("Please log in to be authorized");
            return;
        }

        if (!authentication.ValidateCurrentToken(token))
        {
            context.Result = new UnauthorizedObjectResult("This is fishy");
            return;
        }

        var payload = authentication.ExtractPayloadFromToken(token);
        var isAdmin = bool.Parse(payload["isadmin"].ToString());

        if (!isAdmin)
        {
            context.Result = new ForbidResult("You do not have access to these controls");
        }
    }
}