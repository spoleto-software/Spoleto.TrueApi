namespace Spoleto.TrueApi
{
    /// <summary>
    /// Виды первичного документа
    /// </summary>
    /// <remarks>
    /// Вид первичного документа зависит от причины вывода из оборота.
    /// </remarks>
    public enum PrimaryDocumentType
    {
        #region Розничная реализация RETAIL

        /// <summary>
        /// Кассовый чек
        /// </summary>
        /// <remarks>
        /// Применяется для следующих причин вывода из оборота:
        /// Розничная реализация RETAIL,
        /// REMOTE_SALE – Продажа по образцам, дистанционный способ продажи
        /// </remarks>
        RECEIPT,

        /// <summary>
        /// Товарный чек
        /// </summary>
        /// <remarks>
        /// Применяется для следующих причин вывода из оборота:
        /// Розничная реализация RETAIL,
        /// REMOTE_SALE – Продажа по образцам, дистанционный способ продажи
        /// </remarks>
        SALES_RECEIPT,

        /// <summary>
        /// прочее (с указанием наименования вручную)
        /// </summary>
        /// <remarks>
        /// Применяется для следующих причин вывода из оборота:
        /// Розничная реализация RETAIL,
        /// EEC_EXPORT – Экспорт в страны ЕАЭС,
        /// RETURN – Возврат физическому лицу,
        /// REMOTE_SALE – Продажа по образцам, дистанционный способ продажи,
        /// DAMAGE_LOSS – Утрата или повреждение,
        /// DESTRUCTION - Уничтожение,
        /// CONFISCATION – Конфискация,
        /// LIQUIDATION – Ликвидация предприятия,
        /// ENTERPRISE_USE – Использование для собственных нужд предприятия
        /// </remarks>
        OTHER,
        #endregion

        #region BEYOND_EEC_EXPORT – Экспорт за пределы стран ЕАЭС

        /// <summary>
        /// Таможенная декларация на товары
        /// </summary>
        /// <remarks>
        /// BEYOND_EEC_EXPORT – Экспорт за пределы стран ЕАЭС
        /// </remarks>
        CUSTOMS_DECLARATION,
        #endregion

        #region EEC_EXPORT – Экспорт в страны ЕАЭС

        /// <summary>
        /// Товарная накладная
        /// </summary>
        /// <remarks>
        /// Применяется для следующих причин вывода из оборота:
        /// EEC_EXPORT – Экспорт в страны ЕАЭС,
        /// REMOTE_SALE – Продажа по образцам, дистанционный способ продажи,
        /// CONFISCATION – Конфискация,
        /// LIQUIDATION – Ликвидация предприятия
        /// </remarks>
        CONSIGNMENT_NOTE,

        /// <summary>
        /// Универсальный передаточный документ
        /// </summary>
        /// <remarks>
        /// Применяется для следующих причин вывода из оборота:
        /// EEC_EXPORT – Экспорт в страны ЕАЭС,
        /// REMOTE_SALE – Продажа по образцам, дистанционный способ продажи,
        /// CONFISCATION – Конфискация,
        /// LIQUIDATION – Ликвидация предприятия
        /// </remarks>
        UTD,

        #endregion

        #region RETURN – Возврат физическому

        #endregion,

        #region REMOTE_SALE – Продажа по образцам, дистанционный способ продажи

        #endregion

        #region DAMAGE_LOSS – Утрата или повреждение

        /// <summary>
        /// Акт уничтожения (утраты/утилизации)
        /// </summary>
        /// <remarks>
        /// Применяется для следующих причин вывода из оборота:
        /// DAMAGE_LOSS – Утрата или повреждение,
        /// DESTRUCTION - Уничтожение,
        /// ENTERPRISE_USE – Использование для собственных нужд предприятия
        /// </remarks>
        DESTRUCTION_ACT,

        #endregion

        #region DESTRUCTION - Уничтожение

        #endregion

        #region CONFISCATION – Конфискация

        #endregion

        #region LIQUIDATION – Ликвидация предприятия

        #endregion

        #region ENTERPRISE_USE – Использование для собственных нужд предприятия

        #endregion
    }
}
