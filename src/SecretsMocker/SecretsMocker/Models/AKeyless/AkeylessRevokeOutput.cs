using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Globalization;

namespace SecretsMocker.Models.AKeyless;

public class AkeylessRevokeOutput
{
    [JsonProperty("revoked")]
    public List<string> Revoked { get; set; }

    [JsonProperty("message")]
    public string Message { get; set; }

    public static AkeylessRevokeOutput FromJson(string json) => JsonConvert.DeserializeObject<AkeylessRevokeOutput>(json, new JsonSerializerSettings
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

//revoked
//A list of revoked IDs.
//Example
// ["foo", "bar"]

//message
//An optional message in case any of the specified IDs were not properly revoked.This field should only be used for error handling.
//Example:
// "id foo was not removed: user does not exist"