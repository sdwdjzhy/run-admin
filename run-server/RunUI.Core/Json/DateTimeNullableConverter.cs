using System.Text.Json;
using System.Text.Json.Serialization;

namespace RunUI
{
    public class DateTimeNullableConverter : JsonConverter<DateTime?>
    {
        private string format;

        public DateTimeNullableConverter(string format = AppConst.DateTimeFormat)
        {
            this.format = format;
        }

        public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var str = reader.GetString();
            return str.ToDateTimeNullable();
        }

        public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
        {
            if (value == null)
            {
                writer.WriteNullValue();
                return;
            }
            writer.WriteStringValue(value?.ToString(format));
        }
    }
}
