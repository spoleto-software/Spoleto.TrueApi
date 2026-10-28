using System.Text.Json;
using System.Text.Json.Serialization;

namespace Spoleto.TrueApi.Edo.Converters
{
    /// <summary>
    /// The custom json converter for <see cref="DateTime"/> as Unix timestamp in seconds.
    /// </summary>
    /// <remarks>
    /// Чтение возвращает UTC время.
    /// </remarks>
    public class UnixDateTimeJsonConverter : JsonConverter<DateTime?>
    {
        public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null)
                return null;

            var seconds = reader.TokenType == JsonTokenType.String
                ? long.Parse(reader.GetString()!)
                : reader.GetInt64();

            return DateTimeOffset.FromUnixTimeSeconds(seconds).UtcDateTime;
        }

        public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
        {
            if (value == null)
            {
                writer.WriteNullValue();
            }
            else
            {
                writer.WriteNumberValue(new DateTimeOffset(value.Value).ToUnixTimeSeconds());
            }
        }
    }
}
