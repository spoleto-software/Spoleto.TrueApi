using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Spoleto.TrueApi
{
    /// <summary>
    /// КИ в агрегате
    /// </summary>
    public class CisAggregateInfoModel2
    {
        /// <summary>
        /// КИ товара
        /// </summary>
        [JsonPropertyName("cis")]
        [Required]
        public string Cis { get; set; }

        /// <summary>
        /// Код товара
        /// </summary>
        [JsonPropertyName("gtin")]
        [Required]
        public string Gtin { get; set; }

        /// <summary>
        /// Производитель товара
        /// </summary>
        [JsonPropertyName("producerName")]
        public string ProducerName { get; set; }

        /// <summary>
        /// Статус товара/КИ
        /// </summary>
        [JsonPropertyName("status")]
        [Required]
        public CisStatus? Status { get; set; }

        /// <summary>
        /// Дата эмиссии
        /// </summary>
        /// <remarks>
        /// Возвращается в формате yyyy-MM-ddTHH:mm:ss.SSS’Z
        /// </remarks>
        [JsonPropertyName("emissionDate")]
        [Required]
        public DateTime? EmissionDate { get; set; }

        /// <summary>
        /// Дата ввода товара с КИ в оборот.
        /// </summary>
        /// <remarks>
        /// Возвращается в формате yyyy-MM-ddTHH:mm:ss.SSS’Z
        /// </remarks>
        [JsonPropertyName("producedDate")]
        public DateTime? ProducedDate { get; set; }

        /// <summary>
        /// Тип упаковки
        /// </summary>
        [JsonPropertyName("packageType")]
        [Required]
        public PackType? PackageType { get; set; }

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
        /// Наименование товара на этикетке
        /// </summary>
        [JsonPropertyName("productName")]
        public string ProductName { get; set; }

        /// <summary>
        /// Бренд
        /// </summary>
        [JsonPropertyName("brand")]
        public string Brand { get; set; }

        /// <summary>
        /// Следующие номера КИ.В случае перемаркировки товара
        /// </summary>
        [JsonPropertyName("nextCises")]
        public List<string> NextCises { get; set; }

        /// <summary>
        /// Предыдущие номера КИ. В случае перемаркировки товара
        /// </summary>
        [JsonPropertyName("prevCises")]
        public List<string> PrevCises { get; set; }

        /// <summary>
        /// Актуальное особое состояние КИ
        /// </summary>
        /// <remarks>
        /// см.Справочник "Статусы КИ" <see cref="CisStatus"/>
        /// </remarks>
        [JsonPropertyName("statusEx")]
        public string StatusEx { get; set; }

        /// <summary>
        /// Все КИ в агрегате.
        /// </summary>
        /// <remarks>
        /// В формате массива JSON
        /// </remarks>
        [JsonPropertyName("children")]
        public List<string> Children { get; set; }

        /// <summary>
        /// Краткая информация о дочерних КИ
        /// </summary>
        [JsonPropertyName("childrenMap")]
        public List<string> ChildrenMap { get; set; }

        /// <summary>
        /// Количество дочерних КИ
        /// </summary>
        [JsonPropertyName("countChildren")]
        public int? CountChildren { get; set; }

        /// <summary>
        /// Код идентификации упаковки, в которую агрегирован товар
        /// </summary>
        [JsonPropertyName("parent")]
        public string Parent { get; set; }

        /// <summary>
        /// Последний регистрационный номер документа, зафиксированный в ГИС МТ по этому КИ
        /// </summary>
        [JsonPropertyName("lastDocId")]
        public string LastDocId { get; set; }

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
        /// Дата ввода в оборот с КИ в оборот.
        /// </summary>
        /// <remarks>
        /// Возвращается в формате yyyy-MM-ddTHH:mm:ss.SSS’Z
        /// </remarks>
        [JsonPropertyName("introducedDate")]
        public DateTime? IntroducedDate { get; set; }

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
        /// Наименование текущего владельца товара
        /// </summary>
        [JsonPropertyName("agentName")]
        public string AgentName { get; set; }

        /// <summary>
        /// ИНН текущего владельца товара
        /// </summary>
        [JsonPropertyName("agentInn")]
        public string AgentInn { get; set; }

        /// <summary>
        /// Дата последнего изменения статуса.
        /// </summary>
        /// <remarks>
        /// Возвращается в формате yyyy-MM-ddTHH:mm:ss.SSS’Z
        /// </remarks>
        [JsonPropertyName("lastStatusChangeDate")]
        public DateTime? LastStatusChangeDate { get; set; }

        /// <summary>
        /// Вид товарооборота
        /// </summary>
        [JsonPropertyName("turnoverType")]
        public TurnoverType? TurnoverType { get; set; }

        /// <summary>
        /// Товарная группа
        /// </summary>
        [JsonPropertyName("productGroup")]
        public string ProductGroup { get; set; }

        public override string ToString() => $"{Cis} - {ProductName}";
    }
}
