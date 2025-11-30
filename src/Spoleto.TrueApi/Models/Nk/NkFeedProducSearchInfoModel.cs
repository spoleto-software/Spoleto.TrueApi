using System.Text.Json.Serialization;

namespace Spoleto.TrueApi
{
    /// <summary>
    /// Информация для поиска карточки товара, в том числе неопубликованной карточки.
    /// </summary>
    public class NkFeedProducSearchInfoModel : INkObject
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

        public override string ToString() => $"{nameof(GoodId)}: {GoodId}; {nameof(Gtin)}: {Gtin}";
    }
}
