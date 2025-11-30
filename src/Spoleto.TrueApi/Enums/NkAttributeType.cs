namespace Spoleto.TrueApi
{
    public enum NkAttributeType
    {
        /// <summary>
        /// вернуть все атрибуты (значение по умолчанию)
        /// </summary>
        a,

        /// <summary>
        /// вернуть только обязательные атрибуты
        /// </summary>
        m,

        /// <summary>
        /// вернуть только рекомендуемые атрибуты
        /// </summary>
        r,

        /// <summary>
        /// вернуть только опциональные атрибуты
        /// </summary>
        o
    }
}
