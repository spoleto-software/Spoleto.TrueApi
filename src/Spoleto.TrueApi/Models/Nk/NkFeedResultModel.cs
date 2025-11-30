using System.Text.Json.Serialization;

namespace Spoleto.TrueApi
{
    /// <summary>
    /// Результат создания фида - пакета обновлений.
    /// </summary>
    /// <remarks>
    /// Фиды используются для создания и обновленния карточек товаров.
    /// </remarks>
    public class NkFeedResultModel : INkObject
    {
        /// <summary>
        /// Идентификатор фида
        /// </summary>
        [JsonPropertyName("feed_id")]
        public string FeedId { get; set; }

        public override string ToString() => FeedId.ToString();
    }
}
