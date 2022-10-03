using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SecretsMocker.Models.AKeyless;

public class AkeylessCreateOutput
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("response")]
    public string Response { get; set; }

    public static AkeylessCreateOutput FromJson(string json) => JsonConvert.DeserializeObject<AkeylessCreateOutput>(json, new JsonSerializerSettings
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

//id
//A unique identifier for the temporary credentials, which is required during a `POST /sync/revoke' operation.
//Examples:
// tmp.user1234
// f2fa1940-8d7e-41d4-a688-8d915795e88b

//response
//A JSON object that includes any fields required by the particular use case.
//Examples:
// { "cert":"foobarcert", "private_key":"foobarkey" }
// { "password":"strongpassword!"}