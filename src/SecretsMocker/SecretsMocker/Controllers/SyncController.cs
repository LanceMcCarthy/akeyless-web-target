using Microsoft.AspNetCore.Mvc;
using SecretsMocker.Authorization;
using SecretsMocker.Helpers;
using SecretsMocker.Models.AKeyless;
using System.Text.Json;

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
        public JsonResult Create([FromBody] AkeylessCreateInput input)
        {
            // This is a demo that doesn't really create credentials, so I am returning a fingerprint of a successful generation
            var generatedId = MockHelpers.GenerateId();

            return new JsonResult(new AkeylessCreateOutput
            {
                Id = generatedId,
                Response = GenerateSamplePayload()
            });
        }
        
        [AllowAnonymous]
        [Route("revoke")]
        [HttpPost]
        public JsonResult Revoke([FromBody] AkeylessRevokeInput input)
        {
            // This is just an example, automatically saying all of the credentials were revoked.
            // In a real-world application, you would take the process of revoking the ids and then returning back the results.

            return new JsonResult(new AkeylessRevokeOutput
            {
                Revoked = input.Ids,
                Message = GenerateSamplePayload()
            });
        }

        [AllowAnonymous]
        [Route("rotate")]
        [HttpPost]
        public JsonResult Rotate([FromBody] AkeylessRotateInput input)
        {
            // This is just a demo. To mimic rotating a secret;
            return new JsonResult(new AkeylessRotateOutput
            {
                Payload = GenerateSamplePayload()
            });
        }

        private string GenerateSamplePayload()
        {
            //var certRes = new CertResponse
            //{
            //    UpdatedAt = DateTime.UtcNow,
            //    Cert = "now-i-know-my-abcs",
            //    PrivateKey = "abcdefghijklmnopqrstuvwyz"
            //};

            //return JsonSerializer.Serialize(certRes);

            var response = "{'updated_at': '";

            response += $"{DateTime.UtcNow.Ticks}";

            response += "', 'cert': 'now-i-know-my-abcs', 'secret_key': 'abcdefghijklmnopqrstuvwyz'}";

            return response;

            //return "{ 'cert':'redacted', 'private_key':'redacted' }";
        }
    }
}
