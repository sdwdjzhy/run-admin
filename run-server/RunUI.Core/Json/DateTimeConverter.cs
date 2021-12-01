using System.Text.Json;
using System.Text.Json.Serialization;

namespace RunUI
{

    /// <summary>
    /// </summary>

    public class DateTimeConverter : JsonConverter<DateTime>
    {
        private readonly string format;

        public DateTimeConverter(string format = AppConst.DateTimeFormat)
        {
            this.format = format;
        }

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                if (DateTime.TryParse(reader.GetString(), out DateTime date))
                    return date;
            }
            return reader.GetDateTime();
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(format));
        }
    }
}
