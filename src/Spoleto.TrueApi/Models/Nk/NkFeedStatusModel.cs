using System.Text.Json.Serialization;

namespace Spoleto.TrueApi
{
    /// <summary>
    /// Статус фида - пакета обновлений.
    /// </summary>
    public class NkFeedStatusModel : INkObject
    {
        /// <summary>
        /// Идентификатор фида
        /// </summary>
        [JsonPropertyName("feed_id")]
        public int FeedId { get; set; }

        /// <summary>
        /// Идентификатор статуса фида
        /// </summary>
        /// <remarks>
        /// Возможные значения: 0 —запрос не принят; 1 —запрос получен, данные на модерации; 2 —товары прошли модерацию; 3 —одобренные модератором товары подписаны; 4 - в обработке.
        /// </remarks>
        [JsonPropertyName("status_id")]
        public int StatusId { get; set; }

        /// <summary>
        /// Статус фида
        /// </summary>
        [JsonPropertyName("status")]
        public NkFeedStatusType Status { get; set; }

        /// <summary>
        /// Время создания фида
        /// </summary>
        /// <remarks>
        /// В формате yyyy-MM-ddTHH:mm:ss.SSS’Z
        /// </remarks>
        [JsonPropertyName("received_at")]
        public DateTime ReceivedAt { get; set; }

        /// <summary>
        /// Время перехода фида в текущий статус
        /// </summary>
        /// <remarks>
        /// yyyy-MM-ddTHH:mm:ss.SSS’Z
        /// </remarks>
        [JsonPropertyName("status_updated_at")]
        public DateTime StatusUpdatedAt { get; set; }

        /// <summary>
        /// Ошибки при валидации контента
        /// </summary>
        [JsonConverter(typeof(NkModerationErrorJsonConverter))]
        [JsonPropertyName("result")]
        public NkModerationErrorModel Result { get; set; }

        /// <summary>
        /// Список дополнительных сообщений
        /// </summary>
        [JsonPropertyName("item")]
        public List<NkModerationInfoModel> Item { get; set; }

        public override string ToString() => $"{FeedId} - {Status}";
    }
}
