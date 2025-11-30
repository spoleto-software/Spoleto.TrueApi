namespace Spoleto.TrueApi
{
    /// <summary>
    /// Периодичность регулярной выгрузки.
    /// </summary>
    public enum DispenserPeriod
    {
        /// <summary>
        /// полминуты
        /// </summary>
        HALF_MIN,

        /// <summary>
        /// 10 минут
        /// </summary>
        TEN_MINUTES,

        /// <summary>
        /// день
        /// </summary>
        DAY,

        /// <summary>
        /// неделя
        /// </summary>
        WEEK,

        /// <summary>
        /// месяц
        /// </summary>
        MONTH,

        /// <summary>
        /// квартал
        /// </summary>
        QUARTER,

        /// <summary>
        /// год
        /// </summary>
        YEAR
    }
}
