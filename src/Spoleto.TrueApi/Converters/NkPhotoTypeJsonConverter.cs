using System.Text.Json;
using System.Text.Json.Serialization;

namespace Spoleto.TrueApi
{
    /// <summary>
    /// The custom json converter for <see cref="NkPhotoType"/>.
    /// </summary>
    public class NkPhotoTypeJsonConverter : JsonConverter<NkPhotoType>
    {
        public override NkPhotoType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var valueToString = reader.GetString();

            return valueToString switch
            {
                "7" => NkPhotoType.left7,
                "19" => NkPhotoType.right19,
                "13" => NkPhotoType.back13,
                "3ds" => NkPhotoType.series3ds,
                _ => Enum.Parse<NkPhotoType>(valueToString)
            };
        }

        public override void Write(Utf8JsonWriter writer, NkPhotoType value, JsonSerializerOptions options)
        {
            var valueToString = value switch
            {
                NkPhotoType.left7 => "7",
                NkPhotoType.right19 => "19",
                NkPhotoType.back13 => "13",
                NkPhotoType.series3ds => "3ds",
                _ => value.ToString()

            };

            writer.WriteStringValue(valueToString);
        }
    }
}
