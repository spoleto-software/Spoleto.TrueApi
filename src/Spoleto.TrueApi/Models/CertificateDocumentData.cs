using System.Text.Json.Serialization;

namespace Spoleto.TrueApi
{
    /// <summary>
    /// Сертификат
    /// </summary>
    public class CertificateDocumentData
    {
        /// <summary>
        /// Дата сертификата.
        /// </summary>
        /// <remarks>
        /// В формате yyyy-MM-dd
        /// </remarks>
        [JsonPropertyName("certificate_date")]
        public string CertificateDate { get; set; }

        /// <summary>
        /// Номер сертификата
        /// </summary>
        [JsonPropertyName("certificate_number")]
        public string CertificateNumber { get; set; }

        /// <summary>
        /// Тип сертификата
        /// </summary>
        [JsonPropertyName("certificate_type")]
        public string CertificateType { get; set; }
    }
}
