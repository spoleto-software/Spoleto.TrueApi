using System.Text.Json.Serialization;

namespace Spoleto.TrueApi
{
    /// <summary>
    /// Ошибки подписания карточек товара.
    /// </summary>
    public class NkFeedProductSignErrorModel
    {
        /// <summary>
        /// Идентификатор товара.
        /// </summary>
        [JsonPropertyName("goodId")]
        public int GoodId { get; set; }

        /// <summary>
        /// Текст ошибки.
        /// </summary>
        [JsonPropertyName("message")]
        public string Message { get; set; }

        public override string ToString() => $"{nameof(GoodId)}: {GoodId}. {Message}";
    }
}