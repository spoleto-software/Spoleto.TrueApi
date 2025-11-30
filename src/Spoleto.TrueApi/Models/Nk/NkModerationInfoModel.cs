using System.Text.Json.Serialization;

namespace Spoleto.TrueApi
{
    /// <summary>
    /// Список дополнительных сообщений валидации контента.
    /// </summary>
    public class NkModerationInfoModel
    {
        /// <summary>
        /// Глобальный штрих-код к которому относится сообщение.
        /// Если связанного штрих-кода нет, то значение null.
        /// </summary>
        [JsonPropertyName("gtin")]
        public string Gtin { get; set; }

        /// <summary>
        /// Идентификатор товара в каталоге, к которому относится сообщение.
        /// Если связанного товара нет, то значение null.
        /// </summary>
        [JsonPropertyName("good_id")]
        public int? GoodId { get; set; } //todo: or string?

        /// <summary>
        /// Идентификатор атрибута в каталоге, к которому относится сообщение.
        /// Если связанного атрибута нет, то значение null;
        /// </summary>
        [JsonPropertyName("attribute_id")]
        public int? AttributeId { get; set; } //todo: or string?

        /// <summary>
        /// Название атрибута в каталоге, к которому относится сообщение.
        /// Значение может не передаваться
        /// </summary>
        [JsonPropertyName("attribute_name")]
        public string AttributeName { get; set; }

        /// <summary>
        /// числовой код ошибки
        /// </summary>
        [JsonPropertyName("status_code")]
        public int StatusCode { get; set; }

        /// <summary>
        /// Текст статуса
        /// </summary>
        [JsonPropertyName("status_message")]
        public string StatusMessage { get; set; }

        /// <summary>
        /// Текст сообщения
        /// </summary>
        [JsonPropertyName("message")]
        public string Message { get; set; }

        public override string ToString() => Message;
    }
}
