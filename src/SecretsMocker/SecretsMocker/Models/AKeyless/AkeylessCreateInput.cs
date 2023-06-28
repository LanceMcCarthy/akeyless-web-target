using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SecretsMocker.Models.AKeyless;

public class AkeylessCreateInput
{
    [JsonPropertyName("payload"), AllowNull]
    public string Payload { get; set; }
    
    [JsonPropertyName("input"), AllowNull]
    public Input Input { get; set; }
    
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

// IMPORTANT
// When first adding a custom producer to the Akeyless gateway, it will do a dry run test.
// You will need to be able to handle this request, the empty 'input' will cause an exception with the json deserializer, so we use a custom converter to handle null
// {"payload":"","input":"","client_info":{"access_id":"p-custom","sub_claims":null}}