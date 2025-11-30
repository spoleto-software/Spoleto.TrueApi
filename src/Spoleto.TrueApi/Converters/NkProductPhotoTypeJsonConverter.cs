using System.Text.Json;
using System.Text.Json.Serialization;

namespace Spoleto.TrueApi
{
    /// <summary>
    /// The custom json converter for <see cref="NkProductPhotoType"/>.
    /// </summary>
    public class NkProductPhotoTypeJsonConverter : JsonConverter<NkProductPhotoType>
    {
        public override NkProductPhotoType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var valueToString = reader.GetString();

            return valueToString switch
            {
                "3ds" => NkProductPhotoType.series3ds,
                _ => Enum.Parse<NkProductPhotoType>(valueToString)
            };
        }

        public override void Write(Utf8JsonWriter writer, NkProductPhotoType value, JsonSerializerOptions options)
        {
            var valueToString = value switch
            {
                NkProductPhotoType.series3ds => "3ds",
                _ => value.ToString()

            };

            writer.WriteStringValue(valueToString);
        }
    }
}
