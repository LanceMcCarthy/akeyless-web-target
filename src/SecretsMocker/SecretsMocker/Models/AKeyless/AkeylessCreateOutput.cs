using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace SecretsMocker.Models.AKeyless;

public class AkeylessCreateOutput
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("response")]
    public string Response { get; set; }
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