using System.Text.Json;
using System.Text.Json.Serialization;

namespace Spoleto.TrueApi
{
    /// <summary>
    /// The custom json converter for <see cref="DateTime"/> with "yyyy-MM-ddTHH:mm:ss.fffZ" format.
    /// </summary>
    public class JavaScriptDateTimeJsonConverter : JsonConverter<DateTime?>
    {
        public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null)
                return null;

            var valueToString = reader.GetString();
            var dateTime = DateTime.Parse(valueToString);

            return dateTime;
        }

        public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
        {
            if (value == null)
            {
                writer.WriteNullValue();
            }
            else
            {
                var valueToString = value.Value.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");

                writer.WriteStringValue(valueToString);
            }
        }
    }
}
