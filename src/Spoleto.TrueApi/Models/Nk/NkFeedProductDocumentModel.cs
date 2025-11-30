using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Spoleto.TrueApi
{
    /// <summary>
    /// Карточки товара с XML (3.4.1).
    /// </summary>
    public class NkFeedProductDocumentModel : INkObject
    {
        /// <summary>
        /// Массив объектов.
        /// </summary>
        [JsonPropertyName("xmls")]
        [Required]
        public List<NkFeedProductDocumentInfoModel> GoodXmls { get; set; }

        /// <summary>
        /// Массив ошибок.
        /// </summary>
        [JsonPropertyName("errors")]
        [Required]
        public List<NkFeedProductDocumentErrorModel> Errors { get; set; }
    }
}
