using System.Text.Json.Serialization;

namespace Spoleto.TrueApi
{
    /// <summary>
    /// Сертификат
    /// </summary>
    public class CertificateModel
    {
        /// <summary>
        /// Дата сертификата.
        /// </summary>
        /// <remarks>
        /// В формате yyyy-MM-ddTHH:mm:ss.SSS’Z
        /// </remarks>
        [JsonPropertyName("date")]
        public DateTime? Date { get; set; }

        /// <summary>
        /// Номер сертификата
        /// </summary>
        [JsonPropertyName("number")]
        public string Number { get; set; }

        /// <summary>
        /// Тип сертификата
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; }
    }
}
