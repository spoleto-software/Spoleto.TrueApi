using System.Text.Json.Serialization;

namespace Spoleto.TrueApi
{
    /// <summary>
    /// Фильтр для поиска карточек товара (3.4.1).
    /// </summary>
    public class NkFeedProductDocumentSearchModel
    {
        /// <summary>
        /// Массив идентификаторов товаров.
        /// </summary>
        [JsonPropertyName("goodIds")]
        public List<int> GoodIds { get; set; }

        /// <summary>
        /// Массив кодов товаров.
        /// </summary>
        /// <remarks>
        /// Указывается в строковом формате.
        /// </remarks>
        [JsonPropertyName("gtins")]
        public List<string> Gtins { get; set; }

        /// <summary>
        /// Признак согласия на публикацию товаров на сайте национальный-каталог.рф.
        /// </summary>
        /// <remarks>
        /// Возможные значения:<br/>
        /// «true» — согласны;<br/>
        /// «false» — не согласны (значение по умолчанию).<br/><br/>
        /// Для карточек товаров типа «Набор» не указывается
        /// </remarks>
        [JsonPropertyName("publicationAgreement")]
        public bool? PublicationAgreement { get; set; }
    }
}
