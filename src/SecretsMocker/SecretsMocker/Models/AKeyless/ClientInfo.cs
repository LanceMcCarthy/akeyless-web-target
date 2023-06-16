using System.Text.Json.Serialization;

namespace SecretsMocker.Models.AKeyless;

public class ClientInfo
{
    [JsonPropertyName("access_id")]
    public string AccessId { get; set; }

    [JsonPropertyName("sub_claims")]
    public SubClaims SubClaims { get; set; }
}