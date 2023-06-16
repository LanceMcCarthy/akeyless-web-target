using System.Text.Json.Serialization;

namespace SecretsMocker.Models.AKeyless;

public class AkeylessCreds
{
    [JsonPropertyName("creds")]
    public string Creds { get; set; }

    [JsonPropertyName("expected_access_id")]
    public string ExpectedAccessId { get; set; }

    [JsonPropertyName("expected_item_name")]
    public string ExpectedItemName { get; set; }
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