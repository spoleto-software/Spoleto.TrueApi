namespace Spoleto.TrueApi
{
    /// <summary>
    /// Статусы документов.
    /// </summary>
    public enum DocumentStatus
    {
        /// <summary>
        /// Проверяется
        /// </summary>
        IN_PROGRESS,

        /// <summary>
        /// Обработан
        /// </summary>
        CHECKED_OK,

        /// <summary>
        /// Обработан с ошибками
        /// </summary>
        CHECKED_NOT_OK,

        /// <summary>
        /// Техническая ошибка
        /// </summary>
        PROCESSING_ERROR,

        /// <summary>
        /// Не определен
        /// </summary>
        UNDEFINED,

        /// <summary>
        /// Принят. Только для документа 'Отгрузка'
        /// </summary>
        ACCEPTED,

        /// <summary>
        /// Аннулирован. Только для документа 'Отмена отгрузки'
        /// </summary>
        CANCELLED,

        /// <summary>
        /// Ожидает приемку. Только для документа 'Отгрузка'. Устанавливается при успешной обработке документа 'Отгрузка товара'
        /// </summary>
        WAIT_ACCEPTANCE,

        /// <summary>
        /// Обработан с ошибками
        /// </summary>
        PARSE_ERROR,

        /// <summary>
        /// Ожидает регистрации участника в ГИС МТ. Только для документа 'Отгрузка'. Устанавливается при успешной обработке документа 'Отгрузка товара' в сторону незарегистрированного участника
        /// </summary>
        WAIT_PARTICIPANT_REGISTRATION,

        /// <summary>
        /// Ожидает продолжения процессинга документа
        /// </summary>
        WAIT_FOR_CONTINUATION
    }
}
