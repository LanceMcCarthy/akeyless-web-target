using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace SecretsMocker.Models.AKeyless;

public class CertResponse
{
    [JsonPropertyName("updated_at"), AllowNull]
    public DateTime UpdatedAt { get; set; }

    [JsonPropertyName("cert"), AllowNull]
    public string Cert { get; set; }

    [JsonPropertyName("private_key"), AllowNull]
    public string PrivateKey { get; set; }
}