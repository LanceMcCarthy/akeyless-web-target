using System.Text.Json;
using SecretsMocker.Models.AKeyless;

namespace SecretsMocker.Authorization;

public class AkeylessAuthMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            string authHeader = context.Request.Headers["Authorization"];

            var bytes = Convert.FromBase64String(authHeader);

            using var stream = new MemoryStream(bytes);

            var akeylessProducerCredentials = await JsonSerializer.DeserializeAsync<AkeylessCreds>(stream);
            
            // "p-1234" or "p-custom" for dry-run
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