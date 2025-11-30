using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Spoleto.TrueApi
{
    /// <summary>
    /// Карточка товара.
    /// </summary>
    public class ProductInfoModel
    {
        /// <summary>
        /// Код идентификации, используемый для фильтрации по списку КИ
        /// </summary>
        [JsonPropertyName("cis")]
        [Required]
        public string Cis { get; set; }

        /// <summary>
        /// Код товара
        /// </summary>
        [JsonPropertyName("gtin")]
        public string Gtin { get; set; }

        /// <summary>
        /// Индивидуальный идентификационный код потребительской упаковки для прослеживаемости в
        /// (код товара + индивидуальный серийный номер вторичной (потребительской) упаковки или первичной упаковки,
        /// в случае отсутствия вторичной)
        /// </summary>
        [JsonPropertyName("sgtin")]
        public string Sgtin { get; set; }

        /// <summary>
        /// Код товарной номенклатуры (10 знаков)
        /// </summary>
        [JsonPropertyName("tnvd")]
        public string Tnvd { get; set; }

        /// <summary>
        /// Код товарной позиции ТН ВЭД ЕАЭС товара (первые 4 символа)
        /// </summary>
        [JsonPropertyName("tnvedGroup")]
        public string TnvedGroup { get; set; }

        /// <summary>
        /// Производитель товара
        /// </summary>
        [JsonPropertyName("producerName")]
        public string ProducerName { get; set; }

        /// <summary>
        /// Название продукта
        /// </summary>
        [JsonPropertyName("productName")]
        public string ProductName { get; set; }

        /// <summary>
        /// ИНН производителя/импортёра товара
        /// </summary>
        [JsonPropertyName("producerInn")]
        public string ProducerInn { get; set; }

        /// <summary>
        /// ИНН собственника товара
        /// </summary>
        [JsonPropertyName("ownerInn")]
        public string OwnerInn { get; set; }

        /// <summary>
        /// Наименование собственника товара
        /// </summary>
        [JsonPropertyName("ownerName")]
        public string OwnerName { get; set; }

        /// <summary>
        /// ИНН текущего владельца товара
        /// </summary>
        [JsonPropertyName("agentInn")]
        public string AgentInn { get; set; }

        /// <summary>
        /// Наименование текущего владельца товара
        /// </summary>
        [JsonPropertyName("agentName")]
        public string AgentName { get; set; }

        /// <summary>
        /// ИНН предыдущего владельца товара
        /// </summary>
        [JsonPropertyName("previousAgentInn")]
        public string PreviousAgentInn { get; set; }

        /// <summary>
        /// Наименование предыдущего владельца товара
        /// </summary>
        [JsonPropertyName("previousAgentName")]
        public string PreviousAgentName { get; set; }

        /// <summary>
        /// Вид товарооборота
        /// </summary>
        [JsonPropertyName("turnoverType")]
        public TurnoverType? TurnoverType { get; set; }

        /// <summary>
        /// Актуальное особое состояние КИ
        /// </summary>
        /// <remarks>
        /// см.Справочник "Статусы КИ" <see cref="CisStatus"/>
        /// </remarks>
        [JsonPropertyName("statusEx")]
        public string StatusEx { get; set; }

        /// <summary>
        /// Регистрационный номер документа
        /// </summary>
        [JsonPropertyName("docNum")]
        public string DocNum { get; set; }

        /// <summary>
        /// Дата эмиссии КИ
        /// </summary>
        /// <remarks>
        /// Возвращается в формате yyyy-MM-ddTHH:mm:ss.SSS’Z
        /// </remarks>
        [JsonPropertyName("emissionDate")]
        [Required]
        public DateTime? EmissionDate { get; set; }

        /// <summary>
        /// Дата нанесения.
        /// </summary>
        /// <remarks>
        /// Возвращается в формате yyyy-MM-ddTHH:mm:ss.SSS’Z
        /// </remarks>
        [JsonPropertyName("producedDate")]
        public DateTime? ProducedDate { get; set; }

        /// <summary>
        /// Дата ввода в оборот.
        /// </summary>
        /// <remarks>
        /// Возвращается в формате yyyy-MM-ddTHH:mm:ss.SSS’Z
        /// </remarks>
        [JsonPropertyName("introducedDate")]
        public DateTime? IntroducedDate { get; set; }

        /// <summary>
        /// Тип эмиссии
        /// </summary>
        [JsonPropertyName("emissionType")]
        public EmissionType? EmissionType { get; set; }

        /// <summary>
        /// Тип производственного заказа
        /// </summary>
        [JsonPropertyName("prodOrderType")]
        public ProductOrderType? ProdOrderType { get; set; }

        /// <summary>
        /// Последний регистрационный номер документа, зафиксированный в ГИС МТ по этому КИ
        /// </summary>
        [JsonPropertyName("lastDocId")]
        public string LastDocId { get; set; }

        /// <summary>
        /// Наименование товара
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// Бренд
        /// </summary>
        [JsonPropertyName("brand")]
        public string Brand { get; set; }

        /// <summary>
        /// Модель
        /// </summary>
        [JsonPropertyName("model")]
        public string Model { get; set; }

        /// <summary>
        /// Сертификат
        /// </summary>
        [JsonPropertyName("certDoc")]
        public CertificateModel CertDoc { get; set; }

        /// <summary>
        /// Предыдущие номера КИ при перемаркировке
        /// </summary>
        [JsonPropertyName("prevCises")]
        public List<string> PrevCises { get; set; }

        /// <summary>
        /// Следующие номера КИ при перемаркировке
        /// </summary>
        [JsonPropertyName("nextCises")]
        public List<string> NextCises { get; set; }

        /// <summary>
        /// Статус товара/КИ
        /// </summary>
        [JsonPropertyName("status")]
        [Required]
        public CisStatus? Status { get; set; }

        /// <summary>
        /// Список перемаркировок данного товара
        /// </summary>
        [JsonPropertyName("remarks")]
        public List<ProductRemarkModel> Remarks { get; set; }

        /// <summary>
        /// Список дочерних КИ
        /// </summary>'
        [JsonPropertyName("cisChildren")]
        public List<string> CisChildren { get; set; }

        /// <summary>
        /// КИ в агрегате
        /// </summary>'
        [JsonPropertyName("children")]
        public List<CisAggregateInfoModel2> Children { get; set; }

        /// <summary>
        /// Маркированный товар
        /// </summary>'
        [JsonPropertyName("childrenDetails")]
        public List<string> ChildrenDetails { get; set; }

        /// <summary>
        /// Количество дочерних КИ
        /// </summary>
        [JsonPropertyName("countChildren")]
        public int? CountChildren { get; set; }

        /// <summary>
        /// КИТУ вышестоящего уровня
        /// </summary>
        [JsonPropertyName("uitu")]
        public string Uitu { get; set; }

        /// <summary>
        /// Код идентификации упаковки, в которую агрегирован товар
        /// </summary>
        [JsonPropertyName("parent")]
        public string Parent { get; set; }

        /// <summary>
        /// Тип упаковки
        /// </summary>
        [JsonPropertyName("packType")]
        [Required]
        public PackType? PackType { get; set; }

        /// <summary>
        /// Причина вывода из оборота
        /// </summary>
        [JsonPropertyName("withdrawReason")]
        public WithdrawReason? WithdrawReason { get; set; }

        /// <summary>
        /// Дата вывода из оборота.
        /// </summary>
        /// <remarks>
        /// Возвращается в формате yyyy-MM-ddTHH:mm:ss.SSS’Z
        /// </remarks>
        [JsonPropertyName("receiptDate")]
        public DateTime? ReceiptDate { get; set; }

        /// <summary>
        /// Дата истечения срока годности.
        /// </summary>
        /// <remarks>
        /// Возвращается в формате yyyy-MM-ddTHH:mm:ss.SSS’Z (обязательный для ТГ "Молочная продукция")
        /// </remarks>
        [JsonPropertyName("expireDate")]
        public DateTime? ExpireDate { get; set; }

        /// <summary>
        /// Производственный ветеринарный сопроводительный документ(обязательный для ТГ "Молочная продукция")
        /// </summary>
        [JsonPropertyName("prVetDocument")]
        public string PrVetDocument { get; set; }

        /// <summary>
        /// Наименование экспортера
        /// </summary>
        [JsonPropertyName("exporterName")]
        public string ExporterName { get; set; }

        /// <summary>
        /// Уникальный идентификатор экспортера в национальной системе учета налогоплательщиков
        /// </summary>
        [JsonPropertyName("exporterTaxpayerId")]
        public string ExporterTaxpayerId { get; set; }

        /// <summary>
        /// Вид документа, подтверждающего соответствие
        /// </summary>
        [JsonPropertyName("certificateType")]
        public CertificateType CertificateType { get; set; }

        /// <summary>
        /// Номер документа, подтверждающего соответствие
        /// </summary>
        [JsonPropertyName("certificateNumber")]
        public string CertificateNumber { get; set; }

        /// <summary>
        /// Дата документа, подтверждающего соответствие.
        /// </summary>
        /// <remarks>
        /// В формате yyyy-MM-ddTHH:mm:ss.SSS’Z
        /// </remarks>
        [JsonPropertyName("certificateDate")]
        public DateTime? CertificateDate { get; set; }

        /// <summary>
        /// Товарная группа
        /// </summary>
        [JsonPropertyName("productGroup")]
        [Required]
        public string ProductGroup { get; set; }

        /// <summary>
        /// Цвет.
        /// </summary>
        /// <remarks>
        /// Обязателен для ТГ "Обувные товары"
        /// </remarks>
        [JsonPropertyName("color")]
        public string Color { get; set; }

        /// <summary>
        /// Размер.
        /// </summary>
        /// <remarks>
        /// Обязателен для ТГ "Обувные товары"
        /// </remarks>
        [JsonPropertyName("productSize")]
        public string ProductSize { get; set; }

        public override string ToString() => $"{Cis} - {ProductName}";
    }
}
