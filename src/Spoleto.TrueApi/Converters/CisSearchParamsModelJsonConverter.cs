using System.Text.Json;
using System.Text.Json.Serialization;
using Spoleto.Common.Helpers;

namespace Spoleto.TrueApi
{
    /// <summary>
    /// The custom json converter for <see cref="CisSearchParamsModel"/>.
    /// </summary>
    public class CisSearchParamsModelJsonConverter : JsonConverter<CisSearchParamsModel>
    {
        public override CisSearchParamsModel Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var json = reader.GetString();

            var value = JsonHelper.FromJson<CisSearchParamsModel>(json);

            return value;
        }

        public override void Write(Utf8JsonWriter writer, CisSearchParamsModel value, JsonSerializerOptions options)
        {
            var json = JsonHelper.ToJson(value);

            writer.WriteStringValue(json);
        }
    }
}
