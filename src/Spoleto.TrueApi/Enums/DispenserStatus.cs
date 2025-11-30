namespace Spoleto.TrueApi
{
    /// <summary>
    /// Текущий статус ответа запроса на выгрузку.
    /// </summary>
    public enum DispenserStatus
    {
        /// <summary>
        /// подготовка
        /// </summary>
        PREPARATION,

        /// <summary>
        /// выполнено
        /// </summary>
        COMPLETED,

        /// <summary>
        /// отменено
        /// </summary>
        CANCELED,
        /// <summary>
        /// архив
        /// </summary>
        ARCHIVE,

        /// <summary>
        /// ошибка
        /// </summary>
        FAILED
    }
}
