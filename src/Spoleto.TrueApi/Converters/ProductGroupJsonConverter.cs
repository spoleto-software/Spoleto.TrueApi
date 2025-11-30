using System.Text.Json;
using System.Text.Json.Serialization;

namespace Spoleto.TrueApi
{
    /// <summary>
    /// The custom json converter for <see cref="ProductGroup"/>.
    /// </summary>
    public class ProductGroupJsonConverter : JsonConverter<ProductGroup>
    {
        public override ProductGroup Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var valueToString = reader.GetString();

            if (Enum.TryParse<ProductGroup>(valueToString, out var value))
                return value;

            var intValue = Convert.ToInt32(valueToString);
            return (ProductGroup)intValue;
        }

        public override void Write(Utf8JsonWriter writer, ProductGroup value, JsonSerializerOptions options)
        {
            var valueAsIntToString = ((int)value).ToString();

            writer.WriteStringValue(valueAsIntToString);
        }
    }
}
