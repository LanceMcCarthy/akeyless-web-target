using Newtonsoft.Json;

namespace SecretsMocker.Models.AKeyless;

public class Input
{
    [JsonProperty("user")]
    public string User { get; set; }
}