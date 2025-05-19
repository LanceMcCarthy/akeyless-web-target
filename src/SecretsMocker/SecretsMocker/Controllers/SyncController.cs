using Microsoft.AspNetCore.Mvc;
using SecretsMocker.Authorization;
using SecretsMocker.Helpers;
using SecretsMocker.Models.AKeyless;
using SecretsMocker.Models.Terraform;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Nodes;
using SecretsMocker.Models;

namespace SecretsMocker.Controllers;

[Authorize]
[Route("api/sync")]
[ApiController]
public class SyncController(IHttpClientFactory httpClientFactory) : ControllerBase
{
    [HttpGet]
    public string Get()
    {
        return "Service is running! For normal operation, use AKeyless custom Dynamic Secrets provider.";
    }

    [AllowAnonymous]
    [Route("create")]
    [HttpPost]
    public async Task<AkeylessCreateOutput> Create([FromBody] AkeylessCreateInput input)
    {
        // Payload schema from Akeyless
        // API token - for rights to create team tokens
        // team id - The team name to generate the Team Token for
        // { api_token: "", team_id: "" }
        var payload = JsonSerializer.Deserialize<ProducerPayload>(input.Payload);
        var token = payload.api_token;
        var teamId = payload.team_id;

        // The request to terraform to generate a new team token
        //curl \
        //--header "Authorization: Bearer $TOKEN" \
        //--header "Content-Type: application/vnd.api+json" \
        //--request POST \
        //--data @payload.json \
        //https://app.terraform.io/api/v2/teams/owners/authentication-tokens

        var client = httpClientFactory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue($"Bearer ${token}");
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.api+json"));
        var url = $"https://app.terraform.io/api/v2/teams/{teamId}/authentication-tokens";

        var requestJson = JsonSerializer.Serialize(new TerraformRequest
        {
            data = new TerraformData
            {
                type = "authentication-tokens",
                attributes = new Attributes
                {
                    description = $"[CustomProducer] Team API token for team: {teamId}.",
                    expiredat = DateTime.UtcNow.AddDays(1)
                }
            }
        });

        // Get result from terraform, should contain a new Team Token

        var httpPost = await client.PostAsync(url, new StringContent(requestJson));

        if (httpPost.IsSuccessStatusCode)
        {
            throw new Exception($"Failed to create token: {httpPost.StatusCode} - {httpPost.ReasonPhrase}");
        }

        var jsonResponse = await httpPost.Content.ReadAsStringAsync();
        var response = JsonSerializer.Deserialize<TerraformResponse>(jsonResponse);

        return new AkeylessCreateOutput
        {
            Id = response.data.id,
            Response = new JsonObject
            {
                { "team_token", response.data.attributes.token },
                { "createdat", response.data.attributes.createdat },
                { "expiredat", response.data.attributes.expiredat },
                { "description", response.data.attributes.description }
            }
        };

        //// Demo ID, e.g. 'tmp.user_6a0WAeFkMVAi8kNwU2XsB0TM7zQ'
        //var generatedId = MockHelpers.GenerateId();

        //// Changing the response type depending on what the caller wants
        //var payload = MockHelpers.GeneratePasswordPayload();

        //return new AkeylessCreateOutput
        //{
        //    Id = generatedId,
        //    Response = payload
        //};
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