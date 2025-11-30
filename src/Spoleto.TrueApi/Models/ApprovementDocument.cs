using System.Text.Json.Serialization;

namespace Spoleto.TrueApi
{
    /// <summary>
    /// Атрибуты для Краткая информация о кодах идентификации (КИ).
    /// </summary>
    public class ApprovementDocument
    {
        /// <summary>
        /// Дата ДТ
        /// </summary>
        [JsonPropertyName("declarationDate")]
        public string DeclarationDate { get; set; }

        /// <summary>
        /// Регистрационный номер ДТ
        /// </summary>
        [JsonPropertyName("declarationId")]
        public string DeclarationId { get; set; }

        /// <summary>
        /// ID ДТ
        /// </summary>
        [JsonPropertyName("declarationRegNumber")]
        public string DeclarationRegNumber { get; set; }

        ///// <summary>
        ///// Сертификаты / декларации соответствия
        ///// </summary>
        //[JsonPropertyName("certDoc")]
        //public List<string> CertDoc { get; set; }

        ///// <summary>
        ///// Тип сертификата См. "Справочник «Виды разрешительной документации»"
        ///// </summary>
        //[JsonPropertyName("type")]
        //public string Type { get; set; }

        ///// <summary>
        ///// Номер сертификата
        ///// </summary>
        //[JsonPropertyName("number")]
        //public string Number { get; set; }

        ///// <summary>
        ///// Дата сертификата
        ///// </summary>
        //[JsonPropertyName("date")]
        //public string Date { get; set; }
    }
}
