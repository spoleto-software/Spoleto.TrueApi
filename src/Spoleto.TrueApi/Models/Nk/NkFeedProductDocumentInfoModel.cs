using System.Text.Json.Serialization;

namespace Spoleto.TrueApi
{
    /// <summary>
    /// Карточки товара с XML (3.4.1).
    /// </summary>
    public class NkFeedProductDocumentInfoModel
    {
        /// <summary>
        /// Идентификатор товара.
        /// </summary>
        [JsonPropertyName("goodId")]
        public int GoodId { get; set; }

        /// <summary>
        /// XML товара для подписания.
        /// </summary>
        [JsonPropertyName("xml")]
        public string Xml { get; set; }
    }
}
