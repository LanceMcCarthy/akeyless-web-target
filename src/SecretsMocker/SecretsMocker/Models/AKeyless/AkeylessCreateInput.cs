using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Globalization;

namespace SecretsMocker.Models.AKeyless;

public class AkeylessCreateInput
{
    [JsonProperty("payload")]
    public string Payload { get; set; }

    [JsonProperty("input")]
    public Input Input { get; set; }

    [JsonProperty("client_info")]
    public ClientInfo ClientInfo { get; set; }

    public static AkeylessCreateInput FromJson(string json) => JsonConvert.DeserializeObject<AkeylessCreateInput>(json, new JsonSerializerSettings
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
//(Optional) Secret credentials stored by Akeyless.You define these credentials when you set up the custom dynamic secret in the Akeyless Gateway.
//Examples:
// mongodb://user:password@host
// {"user":"foo","pass":"bar"}

//input
//(Optional) User input provided with the current
//get-dynamic-secret-value operation. This is a JSON object, and its format depends on the information provided by the user.
//Examples:
// { "domain":"foo.example.com", "use_staging":true }
// { "project_id":42}

//client_info
//Information about the user requesting the credentials. It includes the user's Akeyless access ID, as well as any sub-claims.
//Examples:
// { "access_id": "p-1234", "sub_claims": { "claim1": ["value1"] } }