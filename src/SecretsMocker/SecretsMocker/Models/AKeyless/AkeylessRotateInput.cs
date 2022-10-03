using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Globalization;

namespace SecretsMocker.Models.AKeyless;

public class AkeylessRotateInput
{
    [JsonProperty("payload")]
    public string Payload { get; set; }

    public static AkeylessRotateInput FromJson(string json) => JsonConvert.DeserializeObject<AkeylessRotateInput>(json, new JsonSerializerSettings
    {
        MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
        DateParseHandling = DateParseHandling.None,
        Converters =
        {
            new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
        },
    });

    public string ToJson() => JsonConvert.SerializeObject(this, new JsonSerializerSettings
    {
        MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
        DateParseHandling = DateParseHandling.None,
        Converters =
        {
            new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
        },
    });
}

//payload
//Secret credentials stored by Akeyless.You define these credentials when you set up the custom dynamic secret in the Akeyless Gateway.
//Examples:
// mongodb://user:password@host
// { "user":"foo","pass":"bar"}