using System.Text.Json;
using System.Text.Json.Serialization;

namespace RunUI
{
    /// <summary>
    /// </summary>

    public class EmptyStringConverter : JsonConverter<string?>
    {

        public override string Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return reader.GetString() ?? string.Empty;
        }

        public override void Write(Utf8JsonWriter writer, string? value, JsonSerializerOptions options)
        {
            if (value == null)
            {
                writer.WriteStringValue("");
            }
            else
            {
                writer.WriteStringValue(value);
            }
        }
    }
}
