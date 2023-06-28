using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace SecretsMocker.Models.AKeyless;

public class AkeylessRotateInput
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("payload")]
    public string Payload { get; set; }
}

//payload
//Secret credentials stored by Akeyless.You define these credentials when you set up the custom dynamic secret in the Akeyless Gateway.
//Examples:
// mongodb://user:password@host
// { "user":"foo","pass":"bar"}