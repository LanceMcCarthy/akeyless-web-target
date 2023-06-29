using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace SecretsMocker.Models.AKeyless;

public class AkeylessCreateInput
{
    [JsonPropertyName("payload"), AllowNull]
    public string Payload { get; set; }
    
    [JsonPropertyName("input"), AllowNull]
    public string Input { get; set; }
    
    [JsonPropertyName("client_info"), AllowNull]
    public ClientInfo ClientInfo { get; set; }
}

//payload
//(Optional) Secret credentials stored by Akeyless.You define these credentials when you set up the custom dynamic secret in the Akeyless Gateway.
//Examples:
// mongodb://user:password@host
// {"user":"foo","pass":"bar"}

//input
//(Optional) User input provided with the current
//get-dynamic-secret-value operation. This is a JSON object, and its format depends on the information provided by the user.
//Examples:
// { "domain":"foo.example.com", "use_staging":true }
// { "project_id":42}

//client_info
//Information about the user requesting the credentials. It includes the user's Akeyless access ID, as well as any sub-claims.
//Examples:
// { "access_id": "p-1234", "sub_claims": { "claim1": ["value1"] } }
