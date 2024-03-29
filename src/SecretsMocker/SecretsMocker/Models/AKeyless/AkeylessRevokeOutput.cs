using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace SecretsMocker.Models.AKeyless;

public class AkeylessRevokeOutput
{
    [JsonPropertyName("revoked"), AllowNull]
    public List<string> Revoked { get; set; }
    
    [JsonPropertyName("message"), AllowNull]
    public string Message { get; set; }
}

//revoked
//A list of revoked IDs.
//Example
// ["foo", "bar"]

//message
//An optional message in case any of the specified IDs were not properly revoked.This field should only be used for error handling.
//Example:
// "id foo was not removed: user does not exist"