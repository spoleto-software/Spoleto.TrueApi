using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Spoleto.TrueApi
{
    public class CisSearchParamsModel
    {
        /// <summary>
        /// ИНН участника.
        /// </summary>
        [JsonPropertyName("participantInn")]
        [Required]
        public string ParticipantInn { get; set; }

        /// <summary>
        /// Тип упаковки.
        /// </summary>
        [JsonPropertyName("packageType")]
        [Required]
        public List<PackType> PackageType { get; set; }

        /// <summary>
        /// Статус КМ.
        /// </summary>
        [JsonPropertyName("status")]
        [Required]
        public CisStatus? Status { get; set; }

        /// <summary>
        /// Период отбора по дате эмиссии.
        /// </summary>
        [JsonPropertyName("emissionPeriod")]
        public DatePeriodModel EmissionPeriod { get; set; }

        /// <summary>
        /// Период отбора по дате нанесения.
        /// </summary>
        [JsonPropertyName("appliedPeriod")]
        public DatePeriodModel AppliedPeriod { get; set; }

        /// <summary>
        /// Код товара, по которому осуществляется поиск
        /// </summary>
        [JsonPropertyName("includeGtin")]
        public List<string> IncludeGtin { get; set; }

        /// <summary>
        /// Код товара, по которому выполняется исключение из поиска.
        /// </summary>
        /// <remarks>
        /// Если параметр <see cref="IncludeGtin"/> ("Код товара, по котором осуществляется поиск") заполнен,<br/>
        /// то параметр <see cref="ExcludeGtin"/> ("Код товара, по которому выполняется исключение из поиска") может быть пустым.
        /// </remarks>
        [JsonPropertyName("excludeGtin")]
        public List<string> ExcludeGtin { get; set; }

        public override string ToString() => ParticipantInn;

    }
}