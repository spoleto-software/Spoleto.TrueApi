namespace Spoleto.TrueApi
{
    /// <summary>
    /// Причины вывода из оборота
    /// </summary>
    public enum WithdrawReason
    {
        /// <summary>
        /// испорчен
        /// </summary>
        KM_SPOILED,

        /// <summary>
        /// утерян
        /// </summary>
        KM_LOST,

        /// <summary>
        /// уничтожен
        /// </summary>
        KM_DESTROYED,

        /// <summary>
        /// выявлены ошибки описания товара
        /// </summary>
        DESCRIPTION_ERRORS,

        /// <summary>
        /// розничная продажа
        /// </summary>
        RETAIL,

        /// <summary>
        /// экспорт в страны ЕАЭС
        /// </summary>
        EEC_EXPORT,

        /// <summary>
        /// экспорт за пределы стран ЕАЭС
        /// </summary>
        BEYOND_EEC_EXPORT,

        /// <summary>
        /// возврат физическому лицу
        /// </summary>
        RETURN,

        /// <summary>
        ///  продажа по образцам, дистанционный способ продажи
        /// </summary>
        REMOTE_SALE,

        /// <summary>
        /// утрата или повреждение
        /// </summary>
        DAMAGE_LOSS,

        /// <summary>
        /// уничтожение
        /// </summary>
        DESTRUCTION,

        /// <summary>
        /// конфискация
        /// </summary>
        CONFISCATION,

        /// <summary>
        /// ликвидация предприятия
        /// </summary>
        LIQUIDATION,

        /// <summary>
        /// безвозмездная передача
        /// </summary>
        DONATION,

        /// <summary>
        ///  приобретение государственным предприятием
        /// </summary>
        STATE_ENTERPRISE,

        /// <summary>
        /// использование для собственных нужд покупателем
        /// </summary>
        NO_RETAIL_USE,

        /// <summary>
        /// использование для собственных нужд предприятия
        /// </summary>
        ENTERPRISE_USE,

        /// <summary>
        /// испорчен либо утерян СИ с КИ
        /// </summary>
        KM_SPOILED_OR_LOST,

        /// <summary>
        /// аннулирование не преобразованных в СИ с КИ по истечении срока
        /// </summary>
        KM_CANCELLATION,

        /// <summary>
        ///  аннулирование не преобразованных в СИ с КИ по истечении срока
        /// </summary>
        KM_CANCELLATION_BY_TERM,

        /// <summary>
        /// возврат товаров с поврежденным СИ/без СИ при розничной реализации
        /// </summary>
        RETAIL_RETURN,

        /// <summary>
        /// возврат товаров с поврежденным СИ/без СИ при дистанционном способе продажи
        /// </summary>
        REMOTE_SALE_RETURN
    }
}
