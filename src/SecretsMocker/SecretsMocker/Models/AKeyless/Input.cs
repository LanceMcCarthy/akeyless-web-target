using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using SecretsMocker.Models.Converters;

namespace SecretsMocker.Models.AKeyless;

[JsonConverter(typeof(InputConverter))]
public class Input
{
    [JsonPropertyName("user"), AllowNull]
    public string User { get; set; }
}