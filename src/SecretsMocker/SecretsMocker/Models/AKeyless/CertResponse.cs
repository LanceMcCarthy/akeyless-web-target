using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace SecretsMocker.Models.AKeyless;

public class CertResponse
{
    [JsonPropertyName("cert"), AllowNull]
    public string Cert { get; set; }

    [JsonPropertyName("private_key"), AllowNull]
    public JsonObject PrivateKey { get; set; }
}