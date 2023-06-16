using Microsoft.AspNetCore.Mvc;
using SecretsMocker.Authorization;
using SecretsMocker.Models.AKeyless;
using System.Text;
using SecretsMocker.Helpers;

namespace SecretsMocker.Controllers
{
    [Authorize]
    [Route("api/sync")]
    [ApiController]
    public class SyncController : ControllerBase
    {
        private readonly List<string> knownIds = new(){ "p-1234", "dry-run", "p-custom" };

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
            var generateId = MockHelpers.GenerateId();
            knownIds.Add(generateId);

            return new AkeylessCreateOutput
            {
                Id = generateId,
                Response = @"{ ""cert"":""abcdefghijklmnopqrstuvwyz"", ""private_key"":""abcdefghijklmnopqrstuvwyz-now-i-know-my-abcs"" }"
            };
        }
        
        [AllowAnonymous]
        [Route("revoke")]
        [HttpPost]
        public AkeylessRevokeOutput Revoke([FromBody] AkeylessRevokeInput input)
        {
            // PREP //

            // In case of empty request, for example a dry-run from the Akeyless gateway.
            if (input?.Ids == null)
            {
                return new AkeylessRevokeOutput
                {
                    Revoked = new List<string> { "p-custom" },
                    Message = "Empty request, no credentials were revoked."
                };
            }
            
            // prepare input data
            var matchedIds = new List<string>();
            var missingIds = new List<string>();

            input.Ids.ForEach((id =>
            {
                if (knownIds.Contains(id))
                    matchedIds.Add(id);
                else
                    missingIds.Add(id);
            }));
            

            // ACTIONS //

            var sb = new StringBuilder(string.Empty);
            
            // Handle removals
            matchedIds.ForEach(id => knownIds.Remove(id));

            // Handle "unremovables"
            if (missingIds.Count > 0)
            {
                sb.Append("Warning. ID(s): ");
                sb.AppendJoin(",", missingIds);
                sb.Append(" were not removed. User(s) not found.");
            }
            

            // RESPOND //
            
            // Return result
            return new AkeylessRevokeOutput
            {
                Revoked = matchedIds,
                Message = sb.ToString() 
            };
        }

        [AllowAnonymous]
        [Route("rotate")]
        [HttpPost]
        public AkeylessRotateOutput Rotate([FromBody] AkeylessRotateInput input)
        {
            var newPayload = $"UPDATED at {DateTime.UtcNow.Ticks} - {input.Payload}";

            return new AkeylessRotateOutput
            {
                Payload = newPayload
            };
        }
    }
}
