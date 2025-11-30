using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Spoleto.TrueApi
{
    /// <summary>
    /// Операция, связанная с обработкой исходного документа
    /// </summary>
    public record Operation
    {
        /// <summary>
        /// Идентификатор операции в ГИС МТ
        /// </summary>
        [JsonPropertyName("operationId")]
        [Required]
        public string OperationId { get; set; }

        /// <summary>
        /// Дата совершения операции в ГИС МТ
        /// Дата формате UnixDataTime (в миллисекундах)
        /// </summary>
        [JsonPropertyName("operationDate")]
        [Required]
        public long OperationDate { get; set; }

        /// <summary>
        /// Тип операции
        /// </summary>
        [JsonPropertyName("operationType")]
        [Required]
        public string OperationType { get; set; }

        /// <summary>
        /// Идентификатор документа (внутренней квитанции) в ГИС МТ
        /// </summary>
        [JsonPropertyName("docId")]
        public string DocId { get; set; }

        /// <summary>
        /// Дата формирования документа (внутренней квитанции) в ГИС МТ
        /// Дата формате UnixDataTime (в миллисекундах)
        /// </summary>
        [JsonPropertyName("docDate")]
        public long? DocDate { get; set; }

        /// <summary>
        /// Хеш документа (внутренней квитанции) в ГИС МТ
        /// </summary>
        [JsonPropertyName("docHash")]
        public string DocHash { get; set; }

        /// <summary>
        /// Детали
        /// </summary>
        [JsonPropertyName("details")]
        public OperationDetail[] Details { get; set; }
    }
}
