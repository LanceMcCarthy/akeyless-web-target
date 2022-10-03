using Newtonsoft.Json;

namespace SecretsMocker.Models.AKeyless;

public class ClientInfo
{
    [JsonProperty("access_id")]
    public string AccessId { get; set; }

    [JsonProperty("sub_claims")]
    public SubClaims SubClaims { get; set; }
}