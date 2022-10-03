using Newtonsoft.Json;

namespace SecretsMocker.Models.AKeyless;

public class SubClaims
{
    [JsonProperty("claim1")]
    public List<string> Claim1 { get; set; }
}