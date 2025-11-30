using System.Text.Json.Serialization;

namespace Spoleto.TrueApi
{
    /// <summary>
    /// Подробная общедоступная информация о кодах идентификации (КИ).
    /// </summary>
    public class CisPublicInfoModel
    {
        /// <summary>
        /// applicationDate
        /// </summary>
        /// <remarks>
        /// Нет в описании
        /// </remarks>
        [JsonPropertyName("applicationDate")]
        public DateTime ApplicationDate { get; set; }

        /// <summary>
        /// introducedDate
        /// </summary>
        /// <remarks>
        /// Нет в описании
        /// </remarks>
        [JsonPropertyName("introducedDate")]
        public DateTime IntroducedDate { get; set; }


        /// <summary>
        /// КИ из запроса
        /// </summary>
        /// <remarks>
        /// При наличии параметра в ответе
        /// </remarks>
        [JsonPropertyName("requestedCis")]
        public string RequestedCis { get; set; }

        /// <summary>
        /// КИ ГИС МТ
        /// </summary>
        [JsonPropertyName("cis")]
        public string Cis { get; set; }

        /// <summary>
        /// Код товара
        /// </summary>
        /// <remarks>
        /// Если код товара менее 14 символов, то он дополняется ведущими нулями
        /// </remarks>
        [JsonPropertyName("gtin")]
        public string Gtin { get; set; }

        /// <summary>
        /// 10-ти значный код ТН ВЭД
        /// </summary>
        [JsonPropertyName("tnVedEaes")]
        public string TnVedEaes { get; set; }

        /// <summary>
        /// 4-значный код ТН ВЭД
        /// </summary>
        [JsonPropertyName("tnVedEaesGroup")]
        public string TnVedEaesGroup { get; set; }

        /// <summary>
        /// Наименование продукции
        /// </summary>
        [JsonPropertyName("productName")]
        public string ProductName { get; set; }

        /// <summary>
        /// Идентификатор товарной группы
        /// </summary>
        /// <remarks>
        /// см.Справочник "Список поддерживаемых товарных групп" <see cref="ProductGroup"/>.
        /// </remarks>
        [JsonPropertyName("productGroupId")]
        public int? ProductGroupId { get; set; }

        /// <summary>
        /// Наименование товарной группы
        /// </summary>
        [JsonPropertyName("productGroup")]
        //public ProductGroup? ProductGroup { get; set; }
        public string ProductGroup { get; set; }

        /// <summary>
        /// Дата ввода товара в оборот (дата нанесения для ТГ «Табачная продукция» и "Альтернативная табачная продукция")
        /// </summary>
        /// <remarks>
        /// Возвращается в формате yyyy-MM-ddTHH:mm:ss.SSS’Z
        /// </remarks>
        [JsonPropertyName("producedDate")]
        public DateTime? ProducedDate { get; set; }

        /// <summary>
        /// Дата эмиссии
        /// </summary>
        /// <remarks>
        /// Возвращается в формате yyyy-MM-ddTHH:mm:ss.SSS’Z
        /// </remarks>
        [JsonPropertyName("emissionDate")]
        public DateTime? EmissionDate { get; set; }


        /// <summary>
        /// Тип эмиссии
        /// </summary>
        [JsonPropertyName("emissionType")]
        //public EmissionType? EmissionType { get; set; }
        public string EmissionType { get; set; }

        /// <summary>
        /// Тип упаковки
        /// </summary>
        [JsonPropertyName("generalPackageType")]
        //public PackType? PackageType { get; set; }
        public string GeneralPackageType { get; set; }


        /// <summary>
        /// Бренд
        /// </summary>
        [JsonPropertyName("brand")]
        public string Brand { get; set; }

        /// <summary>
        /// Тип упаковки
        /// </summary>
        [JsonPropertyName("packageType")]
        //public PackType? PackageType { get; set; }
        public string PackageType { get; set; }

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
        /// Код статуса КИ
        /// </summary>
        [JsonPropertyName("status")]
        //public CisStatus? Status { get; set; }
        public string Status { get; set; }

        /// <summary>
        /// Актуальное особое состояние КИ
        /// </summary>
        /// <remarks>
        /// Cм. Справочник "Статусы КИ". Не возвращается для ТГ «Табачная продукция» и «АТП»
        /// </remarks>
        [JsonPropertyName("statusEx")]
        public string StatusEx { get; set; }

        /// <summary>
        /// Признак выбытия от невладельца
        /// </summary>
        /// <remarks>
        /// Указывается ИНН последнего продавца.
        /// Если запрос выполнен без токена, то параметр в ответе не возвращается.
        /// Возвращается только для ТГ «Обувные товары», «Табачная продукция» и "АТП"
        /// </remarks>
        [JsonPropertyName("markWithdraw")]
        public bool? MarkWithdraw { get; set; }

        /// <summary>
        /// Родительский КИ
        /// </summary>
        /// <remarks>
        /// Возможные значения:«SELLING» — «Продажа»;«CONTRACT» — «Передача по АКС»
        /// </remarks>
        [JsonPropertyName("turnoverType")]
        public string TurnoverType { get; set; }

        /// <summary>
        /// Сведения о разрешительной документации
        /// </summary>
        [JsonPropertyName("certDoc")]
        public List<CertificateModel> CertDoc { get; set; }

        /// <summary>
        /// Дочерний КИ в агрегате при наличии (только 1 слой)
        /// </summary>
        [JsonPropertyName("child")]
        public List<string> Child { get; set; }

        /// <summary>
        /// Родительский КИ
        /// </summary>
        [JsonPropertyName("parent")]
        public string Parent { get; set; }

        /// <summary>
        /// Максимальная цена розничной продажи
        /// </summary>
        /// <remarks>
        /// Возвращается для ТГ «Табачная продукция» и "Альтернативная табачная продукция" только для пачки и блока.
        /// Если у блока или пачки МРЦ отсутствует, то параметр не возвращается
        /// </remarks>
        [JsonPropertyName("maxRetailPrice")]
        public decimal? MaxRetailPrice { get; set; }

        /// <summary>
        /// ИНН производителя РФ для ТГ "Табачная продукция" и "АТП"
        /// </summary>
        /// <remarks>
        /// Возвращается только для КИТУ ТГ "Табачная продукция" и "АТП",
        /// для остальных ТГ не возвращается вне зависимости от статуса КИТУ и типа приватности метода
        /// </remarks>
        [JsonPropertyName("producerInn")]
        public string ProducerInn { get; set; }

        /// <summary>
        /// Наименование производителя
        /// </summary>
        /// <remarks>
        /// Возвращается только для КИТУ ТГ "Табачная продукция" и "АТП",
        /// для остальных ТГ не возвращается вне зависимости от статуса КИТУ и типа приватности метода
        /// </remarks>
        [JsonPropertyName("producerName")]
        public string ProducerName { get; set; }

        /// <summary>
        /// ID производственного ВСД (для продукции, произведённой в РФ) или ID транспортного ВСД (для продукции, произведённой вне РФ)
        /// </summary>
        /// <remarks>
        /// Обязательный параметр для ТГ "Молочная продукция"
        /// </remarks>
        [JsonPropertyName("prVetDocument")]
        public string PrVetDocument { get; set; }

        /// <summary>
        /// Наименование экспортёра
        /// </summary>
        [JsonPropertyName("exporterName")]
        public string ExporterName { get; set; }


        /// <summary>
        /// Дата срока годности
        /// </summary>
        /// <remarks>
        /// Возвращается только для ТГ «Молочная продукция»
        /// </remarks>
        [JsonPropertyName("expirationDate")]
        public DateTime? ExpirationDate { get; set; }

        /// <summary>
        /// Причина вывода из оборота
        /// </summary>
        [JsonPropertyName("withdrawReason")]
        public string WithdrawReason { get; set; }

        /// <summary>
        /// Причина вывода из оборота
        /// </summary>
        [JsonPropertyName("withdrawReasonOther")]
        public string WithdrawReasonOther { get; set; }



        public override string ToString() => $"{Cis} - {ProductName}";
    }
}
