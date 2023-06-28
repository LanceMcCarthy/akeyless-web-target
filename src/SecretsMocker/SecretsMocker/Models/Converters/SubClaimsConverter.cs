using System.Text.Json;
using System.Text.Json.Serialization;
using SecretsMocker.Models.AKeyless;

namespace SecretsMocker.Models.Converters;

public class SubClaimsConverter : JsonConverter<SubClaims>
{
    public override bool HandleNull => true;

    public override SubClaims Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options)
    {
        var contents = reader.GetString();

        return string.IsNullOrEmpty(contents)
            ? null
            : JsonSerializer.Deserialize<SubClaims>(contents);
    }

    public override void Write(
        Utf8JsonWriter writer,
        SubClaims value,
        JsonSerializerOptions options)
    {
        var text = JsonSerializer.Serialize(value);
        writer.WriteStringValue(text);
    }
}