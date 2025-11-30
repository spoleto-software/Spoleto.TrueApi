using System.Text.Json.Serialization;

namespace Spoleto.TrueApi
{
    /// <summary>
    /// Ошибки при получении карточек товара с XML (3.4.1).
    /// </summary>
    public class NkFeedProductDocumentErrorModel
    {
        /// <summary>
        /// Идентификатор товара.
        /// </summary>
        [JsonPropertyName("goodId")]
        public int? GoodId { get; set; }

        /// <summary>
        /// Код товара.
        /// </summary>
        /// <remarks>
        /// При неудачном поиске товара по коду товара.
        /// </remarks>
        [JsonPropertyName("GTIN")]
        public string Gtin { get; set; }

        /// <summary>
        /// Текст ошибки.
        /// </summary>
        [JsonPropertyName("message")]
        public string Message { get; set; }

        public override string ToString() => $"{(GoodId != null ? nameof(GoodId) + ": " + GoodId.ToString() + ". " : null)}{(Gtin != null ? nameof(Gtin) + ": " + Gtin + ". " : null)}{Message}";
    }
}
