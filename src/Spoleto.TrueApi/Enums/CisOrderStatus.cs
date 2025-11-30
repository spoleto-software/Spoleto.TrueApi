namespace Spoleto.TrueApi
{
    /// <summary>
    /// Статусы заказов.
    /// </summary>
    public enum CisOrderStatus
    {
        /// <summary>
        /// Зарегистрирован
        /// </summary>
        NEW,

        /// <summary>
        /// Производство
        /// </summary>
        PRODUCTION,

        /// <summary>
        /// Ошибка в проверке
        /// </summary>
        VALIDATION_FAILED,

        /// <summary>
        /// Заказ в процессе выполнения
        /// </summary>
        IN_PROGRESS,

        /// <summary>
        /// Заказ выполнен
        /// </summary>
        SUCCESS,

        /// <summary>
        /// При выполнении заказа возникла ошибка
        /// </summary>
        ERROR,

        /// <summary>
        /// Заказ устарел
        /// </summary>
        RESULT_EXPIRED
    }
}
