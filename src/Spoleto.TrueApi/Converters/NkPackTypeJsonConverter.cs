using System.Text.Json;
using System.Text.Json.Serialization;

namespace Spoleto.TrueApi
{
    /// <summary>
    /// The custom json converter for <see cref="NkPackType"/>.
    /// </summary>
    public class NkPackTypeJsonConverter : JsonConverter<NkPackType>
    {
        public override NkPackType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var valueToString = reader.GetString().Replace("-", "_");

            return Enum.Parse<NkPackType>(valueToString);
        }

        public override void Write(Utf8JsonWriter writer, NkPackType value, JsonSerializerOptions options)
        {
            var valueToString = value.ToString().Replace("_", "-");

            writer.WriteStringValue(valueToString);
        }
    }
}
