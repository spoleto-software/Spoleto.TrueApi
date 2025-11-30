using System.Text.Json.Serialization;

namespace Spoleto.TrueApi
{
    /// <summary>
    /// Лицензии для Краткая информация о кодах идентификации (КИ).
    /// </summary>
    public class Licence
    {
        /// <summary>
        /// Номер лицензии на пользование недрами
        /// </summary>
        /// <remarks>
        /// Возвращается для товарной группы «Упакованная вода»
        /// </remarks>
        [JsonPropertyName("licenceNumber")]
        public string LicenceNumber { get; set; }

        /// <summary>
        /// Дата выдачи лицензии
        /// </summary>
        /// <remarks>
        /// Возвращается для товарной группы «Упакованная вода» в формате yyyyMM-ddTHH:mm:ss.SSSZ
        /// </remarks>
        [JsonPropertyName("licenceDate")]
        public DateTime? LicenceDate { get; set; }
    }
}
