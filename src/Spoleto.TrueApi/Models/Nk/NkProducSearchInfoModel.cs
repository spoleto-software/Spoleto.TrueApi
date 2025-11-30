using System.Text.Json.Serialization;

namespace Spoleto.TrueApi
{
    /// <summary>
    /// Информация для поиска полной информации о товаре.
    /// </summary>
    public class NkProducSearchInfoModel : INkObject
    {
        /// <summary>
        /// Идентификатор товара в каталоге
        /// </summary>
        /// <remarks>
        /// Обязательно, если не указан gtin
        /// </remarks>
        [JsonPropertyName("good_id")]
        public string GoodId { get; set; }

        /// <summary>
        /// Глобальный код товара(штрих-код)
        /// </summary>
        /// <remarks>
        /// Обязательно, если не указан good_id
        /// </remarks>
        [JsonPropertyName("gtin")]
        public string Gtin { get; set; }

        /// <summary>
        /// Название продукта
        /// </summary>
        /// <remarks>
        /// Используется при запросе на поиск отсутствующего товара
        /// </remarks>
        [JsonPropertyName("product_name")]
        public string ProductName { get; set; }

        /// <summary>
        /// Идентификатор категории
        /// </summary>
        /// <remarks>
        /// Используется при запросе на поиск отсутствующего товара
        /// </remarks>
        [JsonPropertyName("cat_id")]
        public string CatId { get; set; }

        public override string ToString() => $"{nameof(GoodId)}: {GoodId}; {nameof(Gtin)}: {Gtin}";
    }
}
