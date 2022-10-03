using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SecretsMocker.Authorization;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        // skip authorization if action is decorated with [AllowAnonymous] attribute
        var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
        if (allowAnonymous)
            return;

        try
        {
            var accessId = (string)context.HttpContext.Items["expectedId"];

            Console.WriteLine($"Awesome, we got a value in the auth header: {accessId}");

            //if (string.IsNullOrEmpty(accessId))
            //{
            //    context.Result = new JsonResult(new { message = "Unauthorized. Unknown access-id" }) { StatusCode = StatusCodes.Status401Unauthorized };
            //}
            //else
            //{
            //    context.Result = new OkResult();
            //}
        }
        catch
        {
            Console.WriteLine("Couldn't read the auth header");
        }


    }
}