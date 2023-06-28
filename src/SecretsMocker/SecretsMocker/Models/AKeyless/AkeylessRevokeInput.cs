using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace SecretsMocker.Models.AKeyless;

public class AkeylessRevokeInput
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("payload")]
    public string Payload { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("ids")]
    public List<string> Ids { get; set; }
}

//payload(Optional)
//Secret credentials stored by Akeyless.You define these credentials when you set up the custom dynamic secret in the Akeyless Gateway.
//Examples:
// mongodb://user:password@host
// {"user":"foo","pass":"bar"}

//ids
//A list of IDs to revoke. These IDs were previously received in response to POST /sync/create operations.
//Examples:
// ["foo", "bar"]