namespace Spoleto.TrueApi
{
    /// <summary>
    /// Статусы КИ.
    /// </summary>
    public enum CisStatus
    {
        /// <summary>
        /// Эмитирован
        /// </summary>
        EMITTED,

        /// <summary>
        /// Эмитирован. Получен
        /// </summary>
        APPLIED,

        /// <summary>
        /// Applied but not paid.
        /// </summary>
        APPLIED_NOT_PAID,

        /// <summary>
        /// Введён в оборот
        /// </summary>
        INTRODUCED,

        /// <summary>
        /// Списан
        /// </summary>
        WRITTEN_OFF,

        /// <summary>
        /// Выведен из оборота
        /// </summary>
        RETIRED,

        /// <summary>
        /// Выведен из оборота (только для ТГ "Табачная продукция" и "Альтернативная табачная продукция")
        /// </summary>
        WITHDRAWN,

        /// <summary>
        /// Возвращён в оборот (только для ТГ "Табачная продукция" и "Альтернативная табачная продукция")
        /// </summary>
        INTRODUCED_RETURNED,

        /// <summary>
        /// Расформирован (только для КИТУ, АТК и набора всех ТГ, кроме "Табачная продукция" и "Альтернативная табачная продукция")
        /// </summary>
        DISAGGREGATION,

        /// <summary>
        /// Расформирован (только для КИТУ и АТК ТГ "Табачная продукция" и "Альтернативная табачная продукция")
        /// </summary>
        DISAGGREGATED
    }
}
