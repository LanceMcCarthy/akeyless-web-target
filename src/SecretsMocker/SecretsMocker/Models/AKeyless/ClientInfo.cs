using SecretsMocker.Models.Converters;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace SecretsMocker.Models.AKeyless;

//[JsonConverter(typeof(ClientInfoConverter))]
public class ClientInfo
{
    [JsonPropertyName("access_id"), AllowNull]
    public string AccessId { get; set; }
    
    [JsonPropertyName("sub_claims"), AllowNull]
    public SubClaims SubClaims { get; set; }
}