namespace Spoleto.TrueApi
{
    /// <summary>
    /// Типы эмиссии
    /// </summary>
    public enum EmissionType
    {
        /// <summary>
        /// Производство РФ
        /// </summary>
        LOCAL,

        /// <summary>
        /// Ввезён в РФ
        /// </summary>
        FOREIGN,

        /// <summary>
        /// Маркировка остатков
        /// </summary>
        REMAINS,

        /// <summary>
        /// Ввезён из стран ЕАЭС
        /// </summary>
        CROSSBORDER,

        /// <summary>
        /// Перемаркировка
        /// </summary>
        REMARK,

        /// <summary>
        /// Принят на комиссию от физического лица
        /// </summary>
        COMMISSION,

        /// <summary>
        /// Маркировка вне производства или импорта
        /// </summary>
        REAPPLY
    }
}
