using System.Text.Json.Serialization;

namespace SecretsMocker.Models.AKeyless;

public class Input
{
    [JsonPropertyName("user")]
    public string User { get; set; }
}