namespace Spoleto.TrueApi
{
    /// <summary>
    /// Статусы фида
    /// </summary>
    public enum NkFeedStatusType
    {
        /// <summary>
        /// запрос не принят
        /// </summary>
        Rejected,

        /// <summary>
        /// запрос получен, данные на модерации
        /// </summary>
        Received,

        /// <summary>
        /// товары прошли модерацию
        /// </summary>
        Moderated,

        /// <summary>
        /// одобренные модератором товары подписаны
        /// </summary>
        Signed,

        /// <summary>
        /// новый статус. Его нет в документации. Иногда такой статус приходит, если отправить создание фида методом feed, и сразу же запросить feed-status
        /// </summary>
        Processing
    }
}
