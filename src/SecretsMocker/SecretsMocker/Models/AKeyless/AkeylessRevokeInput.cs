using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Globalization;

namespace SecretsMocker.Models.AKeyless;

public class AkeylessRevokeInput
{
    [JsonProperty("payload")]
    public string Payload { get; set; }

    [JsonProperty("ids")]
    public List<string> Ids { get; set; }

    public static AkeylessRevokeInput FromJson(string json) => JsonConvert.DeserializeObject<AkeylessRevokeInput>(json, new JsonSerializerSettings
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

//payload(Optional)
//Secret credentials stored by Akeyless.You define these credentials when you set up the custom dynamic secret in the Akeyless Gateway.
//Examples:
// mongodb://user:password@host
// {"user":"foo","pass":"bar"}

//ids
//A list of IDs to revoke. These IDs were previously received in response to POST /sync/create operations.
//Examples:
// ["foo", "bar"]