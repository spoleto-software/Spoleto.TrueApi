using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Spoleto.TrueApi
{
    /// <summary>
    /// Атрибуты полной информации о товаре.
    /// </summary>
    public class NkProductAttributeModel
    {
        /// <summary>
        /// Идентификатор атрибута
        /// </summary>
        [JsonPropertyName("attr_id")]
        [Required]
        public int AttributeId { get; set; }

        /// <summary>
        /// Наименование атрибута
        /// </summary>
        [JsonPropertyName("attr_name")]
        [Required]
        public string AttributeName { get; set; }

        /// <summary>
        /// Идентификатор значения атрибута
        /// </summary>
        [JsonPropertyName("attr_value_id")]
        public int? AttributeValueId { get; set; }

        /// <summary>
        /// Значение атрибута
        /// </summary>
        [JsonPropertyName("attr_value")]
        [Required]
        public string AttributeValue { get; set; }

        /// <summary>
        /// Идентификатор значения атрибута
        /// </summary>
        [JsonPropertyName("value_id")]
        [Required]
        public long? ValueId { get; set; }

        /// <summary>
        /// Массив возможных значений типа атрибута.
        /// Не массив, а конкретный тип атрибута!
        /// </summary>
        [JsonPropertyName("attr_value_type")]
        [Required]
        public string AttributeValueType { get; set; }

        /// <summary>
        /// Идентификатор группы, к которой относится атрибут
        /// </summary>
        [JsonPropertyName("attr_group_id")]
        [Required]
        public int AttributeGroupId { get; set; }

        /// <summary>
        /// Наименование группы, к которой относится атрибут
        /// </summary>
        [JsonPropertyName("attr_group_name")]
        [Required]
        public string AttributeGroupName { get; set; }

        /// <summary>
        /// Дата измерения атрибута
        /// </summary>
        /// <remarks>
        /// UTC.yyyy-MM-ddTHH:mm:ss.SSS’Z
        /// </remarks>
        [JsonPropertyName("measure_date")]
        public DateTime? MeasureDate { get; set; }

        /// <summary>
        /// Дата публикации атрибута
        /// </summary>
        /// <remarks>
        /// UTC.yyyy-MM-ddTHH:mm:ss.SSS’Z
        /// </remarks>
        [JsonPropertyName("published_date")]
        public DateTime? PublishedDate { get; set; }

        /// <summary>
        /// Дата, с которой действительно значение атрибута
        /// </summary>
        /// <remarks>
        /// UTC.yyyy-MM-ddTHH:mm:ss.SSS’Z
        /// </remarks>
        [JsonPropertyName("effective_date")]
        public DateTime? EffectiveDate { get; set; }

        /// <summary>
        /// Дата, с которой недействительно значение атрибута
        /// </summary>
        /// <remarks>
        /// UTC.yyyy-MM-ddTHH:mm:ss.SSS’Z
        /// </remarks>
        [JsonPropertyName("expired_date")]
        public DateTime? ExpiredDate { get; set; }

        /// <summary>
        /// Идентификатор локации, в которой было проведено измерение
        /// </summary>
        [JsonPropertyName("location_id")]
        public string LocationId { get; set; }

        /// <summary>
        /// Внутренний идентификатор локации для компании, в которой было проведено измерение
        /// </summary>
        /// <remarks>
        /// Отображается только компании, которой принадлежит локация
        /// </remarks>
        [JsonPropertyName("party_location_id")]
        public string PartyLocationId { get; set; }

        /// <summary>
        /// Уровень упаковки
        /// </summary>
        [JsonPropertyName("level")]
        public string Level { get; set; }

        /// <summary>
        /// Код товара (Штрих-код)
        /// </summary>
        [JsonPropertyName("gtin")]
        public string Gtin { get; set; }

        /// <summary>
        /// Мультипликатор
        /// </summary>
        [JsonPropertyName("multiplier")]
        public int? Multiplier { get; set; }

        /// <summary>
        /// Номер сертификата
        /// </summary>
        /// <remarks>
        /// Только у атрибутов из группы "Сертификаты"
        /// </remarks>
        [JsonPropertyName("certificate_number")]
        public string CertificateNumber { get; set; }

        /// <summary>
        /// Дата начала срока действия
        /// </summary>
        /// <remarks>
        /// yyyy-MM-ddTHH:mm:ss.SSS’Z
        /// </remarks>
        [JsonPropertyName("certificate_issued_date")]
        public DateTime? CertificateIssuedDate { get; set; }

        /// <summary>
        /// Дата окончания срока действия
        /// </summary>
        /// <remarks>
        /// yyyy-MM-ddTHH:mm:ss.SSS’Z
        /// </remarks>
        [JsonPropertyName("certificate_valid_until_date")]
        public DateTime? CertificateValidUntilDate { get; set; }

        /// <summary>
        /// Заявитель
        /// </summary>
        [JsonPropertyName("certificate_applicant")]
        public string CertificateApplicant { get; set; }

        /// <summary>
        /// Изготовитель
        /// </summary>
        [JsonPropertyName("certificate_manufacturer")]
        public string CertificateManufacturer { get; set; }

        /// <summary>
        /// Продукция
        /// </summary>
        [JsonPropertyName("certificate_product_description")]
        public string CertificateProductDescription { get; set; }

        public override string ToString() => $"{AttributeName} - {AttributeValue}";
    }
}
