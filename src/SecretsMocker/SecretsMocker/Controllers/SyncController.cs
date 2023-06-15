using Microsoft.AspNetCore.Mvc;
using SecretsMocker.Authorization;
using SecretsMocker.Models.AKeyless;

namespace SecretsMocker.Controllers
{
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
            return new AkeylessCreateOutput()
            {
                Id = "1234",
                Response = @"{ ""cert"":""abcdefghijklmnop"", ""private_key"":""qrstuvwyznowiknowmyabcs"" }"
            };
        }

        [AllowAnonymous]
        [Route("revoke")]
        [HttpPost]
        public AkeylessRevokeOutput Revoke([FromBody] AkeylessRevokeInput input) // AkeylessRevokeInput has special empty/null payload handler for dry-run test from Akeyless gateway
        {
            return new AkeylessRevokeOutput()
            {
                Revoked = input.Ids,
                Message = "All items have been removed"
            };
        }

        [AllowAnonymous]
        [Route("rotate")]
        [HttpPost]
        public AkeylessRotateOutput Rotate([FromBody] AkeylessRotateInput input)
        {
            var inputPayloadToFindAndUpdate = input.Payload;
            var newPayload = $"NEW-{inputPayloadToFindAndUpdate}-NEW";

            return new AkeylessRotateOutput()
            {
                Payload = newPayload
            };
        }
    }
}
