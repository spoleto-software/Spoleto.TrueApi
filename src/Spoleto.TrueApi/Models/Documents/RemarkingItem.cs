using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Spoleto.TrueApi.Documents
{
    /// <summary>
    /// Товар для перемаркировки
    /// </summary>
    public class RemarkingItem
    {
        /// <summary>
        /// Дата повторной маркировки
        /// </summary>
        /// <remarks>
        /// Задаётся в формате yyyy-MM-ddTHH:mm:ss.SSS’Z
        /// </remarks>
        [JsonConverter(typeof(JavaScriptDateTimeJsonConverter))]
        [JsonPropertyName("remarking_date")]
        [Required]
        public DateTime? RemarkingDate { get; set; }

        /// <summary>
        /// Код причины повторной маркировки
        /// </summary>
        [JsonPropertyName("remarking_cause")]
        [Required]
        public RemarkingCause RemarkingCause { get; set; }

        /// <summary>
        /// Код вида документа обязательной сертификации
        /// </summary>
        [JsonPropertyName("certificate_document")]
        public CertificateType CertificateDocument { get; set; }

        /// <summary>
        /// Номер документа обязательной сертификации
        /// </summary>
        [JsonPropertyName("certificate_document_number")]
        public string CertificateDocumentNumber { get; set; }

        /// <summary>
        /// Дата документа обязательной сертификации
        /// </summary>
        /// <remarks>
        /// Задается в формате yyyy-MM-ddTHH:mm:ss.SSS’Z.
        /// Диапазон даты, начиная с 2000-01-01 по дату создания документа
        /// </remarks>
        [JsonConverter(typeof(JavaScriptDateTimeJsonConverter))]
        [JsonPropertyName("certificate_document_date")]
        public DateTime? CertificateDocumentDate { get; set; }

        /// <summary>
        /// Предыдущий уникальный КИ/КИК
        /// </summary>
        /// <remarks>
        /// Не указывается при remarking_cause = RETAIL_RETURN или REMOTE_SALE_RETURN
        /// </remarks>
        [JsonPropertyName("last_uin")]
        public string LastUin { get; set; }

        /// <summary>
        /// Новый уникальный КИ/КИК
        /// </summary>
        [JsonPropertyName("new_uin")]
        [Required]
        public string NewUin { get; set; }

        /// <summary>
        /// Вид первичного документа
        /// </summary>
        /// <remarks>
        /// Возможные значения:
        /// RECEIPT – кассовый чек;
        /// SALES_RECEIPT – товарный чек;
        /// OTHER – прочее.
        /// Параметр обязательный, если:
        ///     remarking_cause = "Возврат от розничного покупателя";
        ///     remarking_cause = "Возврат в случае дистанционной продажи" + paid = true;
        /// параметр не заполняется, если:
        ///     remarking_cause = "Возврат в случае дистанционной продажи" + paid = false
        /// </remarks>
        [JsonPropertyName("primary_document_type")]
        public PrimaryDocumentType PrimaryDocumentType { get; set; }

        /// <summary>
        /// Наименование первичного документа
        /// </summary>
        /// <remarks>
        /// Заполняется, если вид первичного документа указан OTHER.
        /// Параметр обязательный, если:
        ///     remarking_cause = "Возврат от розничного покупателя";
        ///     remarking_cause = "Возврат в случае дистанционной продажи" + paid = true;
        /// параметр не заполняется, если:
        ///     remarking_cause = "Возврат в случае дистанционной продажи" + paid = false
        /// </remarks>
        [JsonPropertyName("primary_document_custom_name")]
        public string PrimaryDocumentCustomName { get; set; }

        /// <summary>
        /// Дата первичного документа
        /// </summary>
        /// <remarks>
        /// Задается в формате yyyy-MM-ddTHH:mm:ss.SSS’Z.
        /// Параметр обязательный, если:
        ///     remarking_cause = "Возврат от розничного покупателя";
        ///     remarking_cause = "Возврат в случае дистанционной продажи" + paid = true;
        /// параметр не заполняется, если:
        ///     remarking_cause = "Возврат в случае дистанционной продажи" + paid = false
        /// </remarks>
        [JsonConverter(typeof(JavaScriptDateTimeJsonConverter))]
        [JsonPropertyName("primary_document_date")]
        public DateTime? PrimaryDocumentDate { get; set; }

        /// <summary>
        /// Номер первичного документа
        /// </summary>
        /// <remarks>
        /// Параметр обязательный, если:
        ///     remarking_cause = "Возврат от розничного покупателя";
        ///     remarking_cause = "Возврат в случае дистанционной продажи" + paid = true;
        /// параметр не заполняется, если:
        ///     remarking_cause = "Возврат в случае дистанционной продажи" + paid = false
        /// </remarks>
        [JsonPropertyName("primary_document_number")]
        public string PrimaryDocumentNumber { get; set; }

        /// <summary>
        /// Код товарной номенклатуры (10 знаков)
        /// </summary>
        /// <remarks>
        /// Параметр доступен и обязателен только при remarking_cause = RETAIL_RETURN или REMOTE_SALE_RETURN
        /// </remarks>
        [JsonPropertyName("tnved_10")]
        public int Tnved10 { get; set; }

        /// <summary>
        /// Товар оплачен
        /// </summary>
        /// <remarks>
        /// Признак оплаты товара:
        /// true – оплачен;
        /// false – не оплачен
        /// </remarks>
        [JsonPropertyName("paid")]
        public bool? Paid { get; set; }
    }
}
