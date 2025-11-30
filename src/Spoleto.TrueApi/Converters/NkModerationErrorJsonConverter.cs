using System.Text.Json;
using System.Text.Json.Serialization;

namespace Spoleto.TrueApi
{
    /// <summary>
    /// The custom json converter for <see cref="NkModerationErrorModel"/>.
    /// </summary>
    public class NkModerationErrorJsonConverter : JsonConverter<NkModerationErrorModel>
    {
        public override NkModerationErrorModel Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            var errorModel = new NkModerationErrorModel();

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    return errorModel;
                }

                // Get the key.
                if (reader.TokenType != JsonTokenType.PropertyName)
                {
                    throw new JsonException();
                }

                string propertyName = reader.GetString();

                reader.Read();

                if (reader.TokenType == JsonTokenType.StartArray)
                {
                    var listJsonConverter = (JsonConverter<List<string>>)options.GetConverter(typeof(List<string>));
                    List<string> listErrors;
                    if (listJsonConverter != null)
                    {
                        listErrors = listJsonConverter.Read(ref reader, typeof(List<string>), options);
                    }
                    else
                    {
                        listErrors = JsonSerializer.Deserialize<List<string>>(ref reader, options);
                    }

                    errorModel.Add(propertyName, listErrors);
                }
                else if (reader.TokenType == JsonTokenType.Number)
                {
                    if (propertyName == "totalErrors")
                        errorModel.TotalErrors = reader.GetInt32();
                }
                else if (reader.TokenType == JsonTokenType.String)
                {
                    if (propertyName == "totalErrors")
                        errorModel.TotalErrors = int.Parse(reader.GetString());
                    else if (propertyName == "commonError")
                        errorModel.CommonError = reader.GetString();
                }
                else
                {
                    throw new JsonException();
                }
            }

            throw new JsonException();
        }

        public override void Write(Utf8JsonWriter writer, NkModerationErrorModel value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
