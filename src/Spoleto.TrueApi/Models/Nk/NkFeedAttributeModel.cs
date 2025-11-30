using System.Text.Json.Serialization;

namespace Spoleto.TrueApi
{
    /// <summary>
    /// Атрибуты фида.
    /// </summary>
    public class NkFeedAttributeModel
    {
        /// <summary>
        /// Идентификатор значения атрибута
        /// </summary>
        /// <remarks>
        /// Обязательный для обновления существующих атрибутов товара
        /// </remarks>
        [JsonPropertyName("attr_value_id")]
        public int? AttributeValueId { get; set; }

        /// <summary>
        /// Идентификатор атрибута
        /// </summary>
        /// <remarks>
        /// Обязательный при создании товара
        /// </remarks>
        [JsonPropertyName("attr_id")]
        public int? AttributeId { get; set; }

        /// <summary>
        /// Значение атрибута
        /// </summary>
        /// <remarks>
        /// Обязательный при создании товара; необязательный при редактировании и удалении
        /// </remarks>
        [JsonPropertyName("attr_value")]
        public string AttributeValue { get; set; }

        /// <summary>
        /// Массив возможных значений типа атрибута.
        /// Не массив, а конкретный тип атрибута!
        /// </summary>
        [JsonPropertyName("attr_value_type")]
        public string AttributeValueType { get; set; }

        /// <summary>
        /// Код товара (Штрих-код)
        /// </summary>
        [JsonPropertyName("gtin")]
        public string Gtin { get; set; }

        /// <summary>
        /// Признак удаления товара
        /// </summary>
        /// <remarks>
        /// Со значением 1 - удаление атрибута товара.
        /// Доступно только при редактировании существующего товара.
        /// При передаче данного параметра обязательно указать attr_value_id
        /// </remarks>
        [JsonPropertyName("delete")]
        public int? Delete { get; set; }

        public override string ToString() => AttributeValue;
    }
}
