using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace SecretsMocker.Models.AKeyless;

public class Input
{
    [JsonPropertyName("user"), AllowNull]
    public string User { get; set; }
}