using System.Text.Json.Serialization;

namespace Spoleto.TrueApi.Documents
{
    /// <summary>
    /// Базовый класс документа трансграничной торговли
    /// </summary>
    public abstract class SupplyImportCrossborderBase<T> : SupplyImportBase<T> where T : SupplyImportItemBase
    {
        /// <summary>
        /// Номер налогоплательщика отправителя (8/9/12/14 цифр)
        /// </summary>
        [JsonPropertyName("sender_tax_number")]
        public string SenderTaxNumber { get; set; }

        /// <summary>
        /// Наименование экспортера
        /// </summary>
        [JsonPropertyName("exporter_name")]
        public string ExporterName { get; set; }

        /// <summary>
        /// Код страны экспортера
        /// </summary>
        [JsonPropertyName("country_oksm")]
        public string CountryOksm { get; set; }

        /// <summary>
        /// Номер первичного документа, подтверждающего перемещение товара
        /// </summary>
        [JsonPropertyName("primary_document_number")]
        public string PrimaryDocumentNumber { get; set; }

        /// <summary>
        /// Дата импорта
        /// </summary>
        [JsonPropertyName("import_date")]
        [JsonConverter(typeof(JavaScriptDateTimeJsonConverter))]
        public DateTime? ImportDate { get; set; }

        /// <summary>
        /// Дата первичного документа, подтверждающего перемещение товара
        /// </summary>
        [JsonPropertyName("primary_document_date")]
        [JsonConverter(typeof(JavaScriptDateTimeJsonConverter))]
        public DateTime? PrimaryDocumentDate { get; set; }
    }
}
