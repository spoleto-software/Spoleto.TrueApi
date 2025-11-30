using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Spoleto.TrueApi
{
    /// <summary>
    /// Подробная информация о товаре.
    /// </summary>
    public class ProductFullInfoModel
    {
        ///// <summary>
        ///// ИНН агента или комиссионера
        ///// </summary>
        //[JsonPropertyName("agentInn")]
        //public string AgentInn { get; set; }

        ///// <summary>
        ///// Наименование агента или комиссионера
        ///// </summary>
        //[JsonPropertyName("agentName")]
        //public string AgentName { get; set; }

        ///// <summary>
        ///// КИ в агрегате
        ///// </summary>'
        //[JsonPropertyName("children")]
        //public List<CisAggregateInfoModel> Children { get; set; }

        /// <summary>
        /// Код идентификации, используемый для фильтрации по списку КИ
        /// </summary>
        [JsonPropertyName("cis")]
        [Required]
        public string Cis { get; set; }

        ///// <summary>
        ///// Количество дочерних КИ
        ///// </summary>
        //[JsonPropertyName("countChildren")]
        //public int? CountChildren { get; set; }

        ///// <summary>
        ///// Дата внесения изменений в КИТУ.
        ///// </summary>
        ///// <remarks>
        ///// В формате yyyy-MM-ddTHH:mm:ss.SSS’Z
        ///// </remarks>
        //[JsonPropertyName("dateChange")]
        //public DateTime? DateChange { get; set; }

        ///// <summary>
        ///// Дата эмиссии
        ///// </summary>
        ///// <remarks>
        ///// Возвращается в формате yyyy-MM-ddTHH:mm:ss.SSS’Z
        ///// </remarks>
        //[JsonPropertyName("emissionDate")]
        //[Required]
        //public DateTime? EmissionDate { get; set; }

        ///// <summary>
        ///// Тип эмиссии
        ///// </summary>
        //[JsonPropertyName("emissionType")]
        //[Required]
        //public EmissionType? EmissionType { get; set; }

        ///// <summary>
        ///// Дата истечения срока годности.
        ///// </summary>
        ///// <remarks>
        ///// Возвращается в формате yyyy-MM-ddTHH:mm:ss.SSS’Z (обязательный для ТГ "Молочная продукция")
        ///// </remarks>
        //[JsonPropertyName("expireDate")]
        //public DateTime? ExpireDate { get; set; }

        ///// <summary>
        ///// Наименование экспортера
        ///// </summary>
        //[JsonPropertyName("exporterName")]
        //public string ExporterName { get; set; }

        ///// <summary>
        ///// Уникальный идентификатор экспортера в национальной системе учета налогоплательщиков
        ///// </summary>
        //[JsonPropertyName("exporterTaxpayerId")]
        //public string ExporterTaxpayerId { get; set; }

        ///// <summary>
        ///// Код товара
        ///// </summary>
        //[JsonPropertyName("gtin")]
        //public string Gtin { get; set; }

        ///// <summary>
        ///// Дата ввода в оборот с КИ в оборот.
        ///// </summary>
        ///// <remarks>
        ///// Возвращается в формате yyyy-MM-ddTHH:mm:ss.SSS’Z
        ///// </remarks>
        //[JsonPropertyName("introducedDate")]
        //public DateTime? IntroducedDate { get; set; }

        ///// <summary>
        ///// Последний регистрационный номер документа, зафиксированный в ГИС МТ по этому КИ
        ///// </summary>
        //[JsonPropertyName("lastDocId")]
        //public string LastDocId { get; set; }

        ///// <summary>
        ///// Следующие номера КИ при перемаркировке
        ///// </summary>
        //[JsonPropertyName("nextCises")]
        //public List<string> NextCises { get; set; }

        ///// <summary>
        ///// ИНН собственника товара
        ///// </summary>
        //[JsonPropertyName("ownerInn")]
        //public string OwnerInn { get; set; }

        ///// <summary>
        ///// Наименование собственника товара
        ///// </summary>
        //[JsonPropertyName("ownerName")]
        //public string OwnerName { get; set; }

        ///// <summary>
        ///// Тип упаковки
        ///// </summary>
        //[JsonPropertyName("packageType")]
        //[Required]
        //public PackType? PackageType { get; set; }

        ///// <summary>
        ///// КИТУ вышестоящего уровня
        ///// </summary>
        //[JsonPropertyName("parent")]
        //public string Parent { get; set; }

        ///// <summary>
        ///// ИНН участника, осуществившего эмиссию КИ
        ///// </summary>
        //[JsonPropertyName("participantInn")]
        //public string ParticipantInn { get; set; }

        ///// <summary>
        ///// Наименование участника оборота товаров
        ///// </summary>
        //[JsonPropertyName("participantName")]
        //public string ParticipantName { get; set; }

        ///// <summary>
        ///// Производственный ветеринарный сопроводительный документ(обязательный для ТГ "Молочная продукция")
        ///// </summary>
        //[JsonPropertyName("prVetDocument")]
        //public string PrVetDocument { get; set; }

        ///// <summary>
        ///// Предыдущие номера КИ. В случае перемаркировки товара
        ///// </summary>
        //[JsonPropertyName("prevCises")]
        //public List<string> PrevCises { get; set; }

        ///// <summary>
        ///// Дата ввода товара с КИ в оборот.
        ///// </summary>
        ///// <remarks>
        ///// Возвращается в формате yyyy-MM-ddTHH:mm:ss.SSS’Z
        ///// </remarks>
        //[JsonPropertyName("producedDate")]
        //public DateTime? ProducedDate { get; set; }

        ///// <summary>
        ///// Наименование товара на этикетке
        ///// </summary>
        //[JsonPropertyName("productName")]
        //public string ProductName { get; set; }

        ///// <summary>
        ///// Дата вывода из оборота.
        ///// </summary>
        ///// <remarks>
        ///// Возвращается в формате yyyy-MM-ddTHH:mm:ss.SSS’Z
        ///// </remarks>
        //[JsonPropertyName("receiptDate")]
        //public DateTime? ReceiptDate { get; set; }

        ///// <summary>
        ///// Код товара и Серийный номер КИ
        ///// </summary>
        //[JsonPropertyName("sgtin")]
        //public string Sgtin { get; set; }

        /// <summary>
        /// Статус товара/КИ
        /// </summary>
        [JsonPropertyName("status")]
        [Required]
        public CisStatus? Status { get; set; }

        /// <summary>
        /// Актуальное особое состояние КИ
        /// </summary>
        /// <remarks>
        /// см.Справочник "Статусы КИ" <see cref="CisStatus"/>
        /// </remarks>
        [JsonPropertyName("statusEx")]
        public string StatusEx { get; set; }

        ///// <summary>
        ///// Код товарной номенклатуры (10 знаков)
        ///// </summary>
        //[JsonPropertyName("tnVedEaes")]
        //public string TnVedEaes { get; set; }

        ///// <summary>
        ///// Код товарной позиции ТН ВЭД ЕАС товара
        ///// </summary>
        //[JsonPropertyName("tnVedEaesGroup")]
        //public string TnVedEaesGroup { get; set; }

        ///// <summary>
        ///// КИ, Обязательный, если не указан uitu
        ///// </summary>
        //[JsonPropertyName("uit")]
        //public string Uit { get; set; }

        ///// <summary>
        ///// КИТУ, Обязательный, если не указан uit
        ///// </summary>
        //[JsonPropertyName("uitu")]
        //public string Uitu { get; set; }

        public override string ToString() => $"{Cis}";
    }
}
