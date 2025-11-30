using System.Text.Json.Serialization;

namespace Spoleto.TrueApi
{
    /// <summary>
    /// Содержит внутри себя детали ошибки
    /// </summary>
    public record ErrorData
    {
        /// <summary>
        /// ИНН участника оборота товаров
        /// </summary>
        [JsonPropertyName("participant")]
        public string[] Participant { get; set; }

        /// <summary>
        /// Список кодов идентификации, из-за которых возникла ошибка обработки документа
        /// </summary>
        [JsonPropertyName("cis")]
        public string[] Cis { get; set; }

        /// <summary>
        /// Список кодов упаковок, из-за которых возникла ошибка обработки документа
        /// </summary>
        [JsonPropertyName("pack")]
        public string[] Pack { get; set; }

        /// <summary>
        /// Номер документа
        /// </summary>
        [JsonPropertyName("invoice")]
        public string[] Invoice { get; set; }

        /// <summary>
        /// Дата документа
        /// </summary>
        [JsonPropertyName("invoiceDate")]
        public string[] InvoiceDate { get; set; }

        /// <summary>
        /// Номер исправления
        /// </summary>
        [JsonPropertyName("fixnumber")]
        public string[] FixNumber { get; set; }

        /// <summary>
        /// Дата исправления
        /// </summary>
        [JsonPropertyName("fixDate")]
        public string[] FixDate { get; set; }

        /// <summary>
        /// Количество значений массива
        /// </summary>
        [JsonPropertyName("count")]
        public int? Count { get; set; }

        /// <summary>
        /// Служебная информация
        /// </summary>
        [JsonPropertyName("tpe")]
        public string Tpe { get; set; }
    }
}
