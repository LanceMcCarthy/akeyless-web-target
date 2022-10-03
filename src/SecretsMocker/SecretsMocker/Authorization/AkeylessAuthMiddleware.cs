using SecretsMocker.Models.AKeyless;

namespace SecretsMocker.Authorization;

public class AkeylessAuthMiddleware
{
    private readonly RequestDelegate next;

    public AkeylessAuthMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            string authHeader = context.Request.Headers["Authorization"];
            
            var akeylessProducerCredentials = AkeylessCreds.FromJson(authHeader);

            // "p-1234"
            var accessId = akeylessProducerCredentials.ExpectedAccessId;

            context.Items["expectedId"] = accessId;

        }
        catch
        {
            // do nothing if invalid auth header
            // user is not attached to context so request won't have access to secure routes
        }

        await next(context);
    }
}