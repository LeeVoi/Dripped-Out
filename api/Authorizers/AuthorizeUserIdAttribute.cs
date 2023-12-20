using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using service.Helpers;

namespace api.Authorizers;

public class AuthorizeUserIdAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var token = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (token == null)
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        var authentication = (AuthenticationHelper)context.HttpContext.RequestServices.GetService(typeof(AuthenticationHelper));
        if (!authentication.ValidateCurrentToken(token))
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        var payload = authentication.ExtractPayloadFromToken(token);
        var userId = payload.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;

        if (userId == null)
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        // Add the user id to the HttpContext items, so you can access it in your controller actions.
        context.HttpContext.Items["userId"] = userId;
    }   
}