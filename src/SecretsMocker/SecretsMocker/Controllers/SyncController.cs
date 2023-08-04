using Microsoft.AspNetCore.Mvc;
using SecretsMocker.Authorization;
using SecretsMocker.Helpers;
using SecretsMocker.Models.AKeyless;

namespace SecretsMocker.Controllers;

[Authorize]
[Route("api/sync")]
[ApiController]
public class SyncController : ControllerBase
{
    [HttpGet]
    public string Get()
    {
        return "Service is running! For normal operation, use AKeyless custom Dynamic Secrets provider.";
    }

    [AllowAnonymous]
    [Route("create")]
    [HttpPost]
    public AkeylessCreateOutput Create([FromBody] AkeylessCreateInput input)
    {
        // Demo ID, e.g. 'tmp.user_6a0WAeFkMVAi8kNwU2XsB0TM7zQ'
        var generatedId = MockHelpers.GenerateId();

        // Changing the response type depending on what the caller wants
        var payload = MockHelpers.GeneratePasswordPayload();

        return new AkeylessCreateOutput
        {
            Id = generatedId,
            Response = payload
        };
    }

    [AllowAnonymous]
    [Route("revoke")]
    [HttpPost]
    public AkeylessRevokeOutput Revoke([FromBody] AkeylessRevokeInput input)
    {
        // This is just an example, automatically saying all of the credentials were revoked.
        // In a real-world application, you would take the process of revoking the ids and then returning back the results.
        return new AkeylessRevokeOutput
        {
            Revoked = input.Ids,
            Message = $"{string.Join(",", input.Ids)} were revoked"
        };
    }

    [AllowAnonymous]
    [Route("rotate")]
    [HttpPost]
    public AkeylessRotateOutput Rotate([FromBody] AkeylessRotateInput input)
    {
        // Mimic rotating a secret;
        return new AkeylessRotateOutput
        {
            Payload = MockHelpers.GeneratePasswordPayload()
        };
    }
}