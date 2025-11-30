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
        CROSSBORDER
    }
}
