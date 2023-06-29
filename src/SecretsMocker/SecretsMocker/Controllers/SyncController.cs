using Microsoft.AspNetCore.Mvc;
using SecretsMocker.Authorization;
using SecretsMocker.Helpers;
using SecretsMocker.Models.AKeyless;
using System.Text.Json.Nodes;

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
            // Demo ID, e.g. 'tmp.cxfzw_6a0WAeFkMVAi8kNwU2XsB0TM7zQ'
            var generatedId = MockHelpers.GenerateId();

            return new AkeylessCreateOutput
            {
                Id = generatedId,
                Response = GenerateSamplePayload()
            };

            //var json = new JsonObject();
            //json.Add("id", generatedId);
            //json.Add("response", GenerateSamplePayload());
            //return new JsonResult(json);

            //return new JsonResult(new AkeylessCreateOutput
            //{
            //    Id = generatedId,
            //    Response = GenerateSamplePayload()
            //});
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
                Message = GenerateSamplePayload()
            };

            //return new JsonResult(new AkeylessRevokeOutput
            //{
            //    Revoked = input.Ids,
            //    Message = GenerateSamplePayload()
            //});
        }

        [AllowAnonymous]
        [Route("rotate")]
        [HttpPost]
        public AkeylessRotateOutput Rotate([FromBody] AkeylessRotateInput input)
        {
            // This is just a demo. To mimic rotating a secret;
            return new AkeylessRotateOutput
            {
                Payload = GenerateSamplePayload()
            };

            //return new JsonResult(new AkeylessRotateOutput
            //{
            //    Payload = GenerateSamplePayload()
            //});
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
            
            var json = new JsonObject
            {
                { "updated_at", DateTime.UtcNow.Ticks },
                { "cert", "now-i-know-my-abcs" },
                { "secret_key", "abcdefghijklmnopqrstuvwyz" }
            };
            return json.ToJsonString();
        }
    }
}
