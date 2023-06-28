using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace SecretsMocker.Models.AKeyless;

public class AkeylessRotateOutput
{
    [JsonPropertyName("payload"), AllowNull]
    public string Payload { get; set; }
}

//payload
//New secret credentials to replace the existing credentials stored by Akeyless.
//Examples:
// mongodb://user:password@host
// { "user":"mun","pass":"goh"}