using System.Text.Json;
using System.Text.Json.Serialization;
using SecretsMocker.Models.AKeyless;

namespace SecretsMocker.Models.Converters;

public class InputConverter : JsonConverter<Input>
{
    public override bool HandleNull => true;

    public override Input Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options)
    {
        var contents = reader.GetString();

        return string.IsNullOrEmpty(contents)
            ? null
            : JsonSerializer.Deserialize<Input>(contents);
    }

    public override void Write(
        Utf8JsonWriter writer,
        Input value,
        JsonSerializerOptions options)
    {
        var text = JsonSerializer.Serialize(value);
        writer.WriteStringValue(text);
    }
}