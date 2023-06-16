using System.Text.Json.Serialization;

namespace SecretsMocker.Models.AKeyless;

public class SubClaims
{
    [JsonPropertyName("claim1")]
    public List<string> Claim1 { get; set; }
}