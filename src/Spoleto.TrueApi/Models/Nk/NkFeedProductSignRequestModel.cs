using System.Text.Json.Serialization;

namespace Spoleto.TrueApi
{
    /// <summary>
    /// Параметры запроса для подписи карточек товара (3.4.3)
    /// </summary>
    public class NkFeedProductSignRequestModel
    {
        /// <summary>
        /// Идентификатор товара.
        /// </summary>
        [JsonPropertyName("goodId")]
        public int GoodId { get; set; }

        /// <summary>
        /// Карточка товара в формате XML, закодированная в base64.
        /// </summary>
        /// <remarks>
        /// Формирование XML для подписания осуществляется методом <see cref="Spoleto.TrueApi.ITrueApiNkProvider.GetFeedProductDocumentList">feed-product-document</see>.
        /// </remarks>
        [JsonPropertyName("base64Xml")]
        public string Base64Xml { get; set; }

        /// <summary>
        /// Открепленная подпись, закодированная в base64 и соответствующая типу CAdES в формате pkcs7.
        /// </summary>
        /// <remarks>
        /// Формирование XML для подписания осуществляется методом <see cref="Spoleto.TrueApi.ITrueApiNkProvider.GetFeedProductDocumentList">feed-product-document</see>.
        /// </remarks>
        [JsonPropertyName("signature")]
        public string Signature { get; set; }
    }
}
