using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace SecretsMocker.Models.AKeyless;

public class SubClaims
{
    [JsonPropertyName("claim1"), AllowNull]
    public List<string> Claim1 { get; set; }
}