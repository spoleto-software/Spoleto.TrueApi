using System.Text.Json.Serialization;

namespace Spoleto.TrueApi
{
    /// <summary>
    /// Атрибут для создания карточки товара.
    /// </summary>
    public class NkAttributeModel : INkObject
    {
        /// <summary>
        /// Идентификатор атрибута
        /// </summary>
        [JsonPropertyName("attr_id")]
        public int AttributeId { get; set; }

        /// <summary>
        /// Наименование атрибута
        /// </summary>
        [JsonPropertyName("attr_name")]
        public string AttributeName { get; set; }

        /// <summary>
        /// Наименование группы, к которой относится атрибут
        /// </summary>
        [JsonPropertyName("attr_group_name")]
        public string AttributeGroupName { get; set; }

        /// <summary>
        /// Идентификатор группы, к которой относится атрибут
        /// </summary>
        [JsonPropertyName("attr_group_id")]
        public int AttributeGroupId { get; set; }

        /// <summary>
        /// Массив возможных значений типа атрибута
        /// </summary>
        [JsonPropertyName("attr_value_type")]
        public List<string> AttributeValueType { get; set; }

        /// <summary>
        /// Тип значения атрибута
        /// </summary>
        [JsonPropertyName("attr_field_type")]
        public NkAttributeFieldType AttributeFieldType { get; set; }

        /// <summary>
        /// Признак принадлежности атрибута ко второму слою атрибутов (атрибуты, необходимые для ввода товаров в оборот)
        /// </summary>
        /// <remarks>
        /// Принимает значения true - атрибут необходим /false-атрибут не необходим
        /// </remarks>
        [JsonPropertyName("second_layer")]
        public bool SecondLayer { get; set; }

        /// <summary>
        /// Массив возможных значений атрибута
        /// </summary>
        [JsonPropertyName("attr_preset")]
        public List<string> AttributePreset { get; set; }

        /// <summary>
        /// Тип атрибута
        /// </summary>
        /// <remarks>
        /// При наличии cat_id в запросе
        /// </remarks>
        [JsonPropertyName("attr_type")]
        public NkAttributeType AttributeType { get; set; }

        public override string ToString() => AttributeName;
    }
}
