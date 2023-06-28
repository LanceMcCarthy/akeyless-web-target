using System.Text.Json;
using System.Text.Json.Serialization;
using SecretsMocker.Models.AKeyless;

namespace SecretsMocker.Models.Converters;

public class ClientInfoConverter : JsonConverter<ClientInfo>
{
    public override bool HandleNull => true;

    public override ClientInfo Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options)
    {
        var contents = reader.GetString();

        return string.IsNullOrEmpty(contents)
            ? null
            : JsonSerializer.Deserialize<ClientInfo>(contents);
    }

    public override void Write(
        Utf8JsonWriter writer,
        ClientInfo value,
        JsonSerializerOptions options)
    {
        var text = JsonSerializer.Serialize(value);
        writer.WriteStringValue(text);
    }
}