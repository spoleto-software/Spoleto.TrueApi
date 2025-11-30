using System.Text.Json.Serialization;

namespace Spoleto.TrueApi
{
    /// <summary>
    /// Информация о подписании карточек товара.
    /// </summary>
    public class NkFeedProductSignModel : INkObject
    {
        /// <summary>
        /// Массив числовых идентификаторов товаров, для которых прошла валидация и проверка карточки товара (карточка товара переведена в статус «Опубликована»).
        /// </summary>
        [JsonPropertyName("signed")]
        public List<int> Signed { get; set; }

        /// <summary>
        /// Массив ошибок.
        /// </summary>
        [JsonPropertyName("errors")]
        public List<NkFeedProductSignErrorModel> Errors { get; set; }
    }
}
