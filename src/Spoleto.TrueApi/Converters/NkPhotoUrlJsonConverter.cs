using System.Text.Json;
using System.Text.Json.Serialization;

namespace Spoleto.TrueApi
{
    /// <summary>
    /// The custom json converter for the property <see cref="NkImageCreatingModel.PhotoUrl"/>.
    /// </summary>
    public class NkPhotoUrlJsonConverter : JsonConverter<string>
    {
        public override string Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.StartArray) // read as array and convert to string with separator ","
            {
                var urls = new List<string>();
                do
                {
                    reader.Read();
                    if (reader.TokenType == JsonTokenType.String)
                        urls.Add(reader.GetString());
                }
                while (reader.TokenType != JsonTokenType.EndArray);

                return string.Join(",", urls);
            }
            else
            {
                return reader.GetString();
            }
        }

        public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
        {
            if (value == null)
            {
                writer.WriteNullValue();
            }
            else if (value.Contains(",")) // write as array
            {
                var urls = value.Split(",");
                writer.WriteStartArray();

                foreach (var url in urls)
                    writer.WriteStringValue(url);

                writer.WriteEndArray();
            }
            else
            {
                writer.WriteStringValue(value);
            }
        }
    }
}
