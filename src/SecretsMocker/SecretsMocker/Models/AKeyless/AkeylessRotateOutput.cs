using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Globalization;

namespace SecretsMocker.Models.AKeyless;

public class AkeylessRotateOutput
{
    [JsonProperty("payload")]
    public string Payload { get; set; }

    public static AkeylessRotateOutput FromJson(string json) => JsonConvert.DeserializeObject<AkeylessRotateOutput>(json, new JsonSerializerSettings
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
//New secret credentials to replace the existing credentials stored by Akeyless.
//Examples:
// mongodb://user:password@host
// { "user":"mun","pass":"goh"}