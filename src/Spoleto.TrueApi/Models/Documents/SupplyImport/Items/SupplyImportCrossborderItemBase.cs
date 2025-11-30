using System.Text.Json.Serialization;

namespace Spoleto.TrueApi.Documents
{
    /// <summary>
    /// Базовый класс товара для ввода в оборот. Трансграничная торговля
    /// </summary>
    public abstract class SupplyImportCrossborderItemBase : SupplyImportItemBase
    {
        /// <summary>
        /// Код идентификации
        /// </summary>
        [JsonPropertyName("ki")]
        public string Ki { get; set; }

        /// <summary>
        /// Код ТН ВЭД ЕАЭС товара
        /// </summary>
        [JsonPropertyName("tnved_code")]
        public string TnvedCode { get; set; }

        /// <summary>
        /// Разрешительная документация
        /// </summary>
        [JsonPropertyName("certificate_document_data")]

        public List<CertificateDocumentData> CertificateDocumentData { get; set; }


    }
}
