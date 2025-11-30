using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Spoleto.TrueApi
{
    /// <summary>
    /// Детали операции, связанной с обработкой исходного документа
    /// </summary>
    public record OperationDetail
    {
        /// <summary>
        /// Товарные группы
        /// </summary>
        [JsonPropertyName("productGroups")]
        public string[] ProductGroups { get; set; }

        /// <summary>
        /// Флаг обработки документа на данной стадии
        /// </summary>
        [JsonPropertyName("successful")]
        [Required]
        public string Successful { get; set; }

        /// <summary>
        /// Идентификатор транспортного пакета (ТП) в ГИС МТ
        /// </summary>
        [JsonPropertyName("tpInternalId")]
        public string TpInternalId { get; set; }

        /// <summary>
        /// Тип входящего документа
        /// </summary>
        [JsonPropertyName("documentType")]
        public string DocumentType { get; set; }

        /// <summary>
        /// Имя файла УПД\УКД без расширения
        /// </summary>
        [JsonPropertyName("documentName")]
        public string DocumentName { get; set; }

        /// <summary>
        /// Номер входящего документа
        /// </summary>
        [JsonPropertyName("documentnumber")]
        public string DocumentNumber { get; set; }

        /// <summary>
        /// Дата входящего документа
        /// </summary>
        [JsonPropertyName("documentDateTime")]
        public string DocumentDateTime { get; set; }

        /// <summary>
        /// Объект описания ошибок обработки документа
        /// </summary>
        [JsonPropertyName("errors")]
        public ErrorObject[] Errors { get; set; }
    }
}
