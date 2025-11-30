using System.Text.Json.Serialization;

namespace Spoleto.TrueApi.Documents
{
    /// <summary>
    /// Документ ввода в оборот. Импорт с ФТС (Одежда и обувь)
    /// </summary>
    public class SupplyImportFts : SupplyImportBase<SupplyImportFtsItem>
    {
        /// <summary>
        /// Тип документа
        /// </summary>
        [JsonIgnore]
        public override DocumentType DocumentType => DocumentType.LP_FTS_INTRODUCE;

        /// <summary>
        /// Регистрационный номер ДТ
        /// </summary>
        [JsonPropertyName("declaration_number")]
        public string DeclarationNumber { get; set; }

        /// <summary>
        /// Дата регистрации ДТ
        /// </summary>
        /// <remarks>
        /// Задается в формате dd.mm.yyyy.
        /// Диапазон даты начиная, с 01.01.2000 по дату создания документа
        /// </remarks>
        [JsonConverter(typeof(JavaScriptDateTimeJsonConverter))]
        [JsonPropertyName("declaration_date")]
        public DateTime? DeclarationDate { get; set; }
    }
}
