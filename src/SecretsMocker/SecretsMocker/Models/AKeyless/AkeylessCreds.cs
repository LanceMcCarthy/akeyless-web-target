using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SecretsMocker.Models.AKeyless;

public class AkeylessCreds
{
    [JsonProperty("creds")]
    public string Creds { get; set; }

    [JsonProperty("expected_access_id")]
    public string ExpectedAccessId { get; set; }

    [JsonProperty("expected_item_name")]
    public string ExpectedItemName { get; set; }

    public static AkeylessCreds FromJson(string json) => JsonConvert.DeserializeObject<AkeylessCreds>(json, new JsonSerializerSettings
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

//creds
//A temporary JWT token issued and signed by Akeyless that is included in the AkeylessCreds header of every POST /sync/create and POST /sync/revoke request.

// expected_access_id
// The initial access ID used for the Akeyless Gateway (not the user credentials).
// Example:
// "p-1234"

//expected_item_name
//(Optional) The item name of the custom dynamic secret.This can be helpful if a single Akeyless Gateway runs multiple custom dynamic secrets, and the custom dynamic secret implementation should only respond to one of them.
//Example:
// "/custom-producer-foo"