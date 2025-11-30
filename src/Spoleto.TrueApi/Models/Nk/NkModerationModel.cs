using System.Text.Json.Serialization;

namespace Spoleto.TrueApi
{
    /// <summary>
    /// Результат отправки на модерацию карточки товаров в статусе "Черновик".
    /// </summary>
    public class NkModerationModel : INkObject
    {
        /// <summary>
        /// ID шаблона товара в каталоге
        /// </summary>
        [JsonPropertyName("good_draft_id")]
        public int GoodDraftId { get; set; }

        /// <summary>
        /// Текст ошибки
        /// </summary>
        /// <remarks>
        /// Параметр указывается при наличии ошибки
        /// </remarks>
        [JsonPropertyName("error")]
        public string Error { get; set; }
    }
}
