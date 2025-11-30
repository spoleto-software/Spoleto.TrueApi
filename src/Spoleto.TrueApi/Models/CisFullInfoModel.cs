using System.Text.Json.Serialization;

namespace Spoleto.TrueApi
{
    /// <summary>
    /// Подробная информация о кодах идентификации (КИ), полученная по запросу результата заказа на список кодов идентификации.
    /// </summary>
    public class CisFullInfoModel
    {
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
        /// КИ
        /// </summary>
        /// <remarks>
        /// Параметр указан для метода Запрос цепочки движения кода идентификации
        /// </remarks>
        [JsonPropertyName("code")]
        public string Code { get; set; }

        /// <summary>
        /// Код статуса КИ
        /// </summary>
        [JsonPropertyName("status")]
        public CisStatus Status { get; set; }

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
        public EmissionType? EmissionType { get; set; }

        /// <summary>
        /// Максимальная цена розничной продажи
        /// </summary>
        /// <remarks>
        /// Только для пачки и блока. Для блока значение = maxRetailPrice*количество штук в блоке
        /// </remarks>
        [JsonPropertyName("maxRetailPrice")]
        public decimal? MaxRetailPrice { get; set; }

        /// <summary>
        /// Дата ввода в оборот (для ТГ "Табачная продукция" дата нанесения)
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
        public PackType PackageType { get; set; }

        /// <summary>
        /// Список дочерних КИ в агрегате
        /// </summary>
        [JsonPropertyName("child")]
        public List<string> Child { get; set; }

        /// <summary>
        /// Родительский КИ
        /// </summary>
        [JsonPropertyName("parent")]
        public string Parent { get; set; }

        /// <summary>
        /// ИНН производителя РФ для ТГ "Табачная продукция" и "АТП"
        /// </summary>
        /// <remarks>
        /// В ЛП для КИТУ не возвращается
        /// </remarks>
        [JsonPropertyName("producerInn")]
        public string ProducerInn { get; set; }

        /// <summary>
        /// Наименование производителя
        /// </summary>
        /// <remarks>
        /// В ЛП для КИТУ не возвращается
        /// </remarks>
        [JsonPropertyName("producerName")]
        public string ProducerName { get; set; }

        /// <summary>
        /// ИНН собственника товара
        /// </summary>
        /// <remarks>
        /// Данный параметр возвращается только владельцу продукции, ФОИВ и Оператору-ЦРПТ.
        /// Отображение данных о владельце настраивается параметром конфигурации.
        /// </remarks>
        [JsonPropertyName("ownerInn")]
        public string OwnerInn { get; set; }

        /// <summary>
        /// Наименование собственника товара
        /// </summary>
        /// <remarks>
        /// Данный параметр возвращается только владельцу продукции, ФОИВ и Оператору-ЦРПТ.
        /// Отображение данных о владельце настраивается параметром конфигурации.
        /// </remarks>
        [JsonPropertyName("ownerName")]
        public string OwnerName { get; set; }

        /// <summary>
        /// ИНН Российского производителя в ЛП
        /// </summary>
        [JsonPropertyName("participantInn")]
        public string ParticipantInn { get; set; }

        /// <summary>
        /// Наименование организации УОТ
        /// </summary>
        [JsonPropertyName("participantName")]
        public string ParticipantName { get; set; }

        /// <summary>
        /// Дата нанесения КИ
        /// </summary>
        /// <remarks>
        /// Возвращается в формате yyyy-MM-ddTHH:mm:ss.SSS’Z
        /// </remarks>
        [JsonPropertyName("receiptDate")]
        public DateTime? ReceiptDate { get; set; }

        /// <summary>
        /// Уникальный идентификатор товара (УИТ)
        /// </summary>
        /// <remarks>
        /// При наличии
        /// </remarks>
        [JsonPropertyName("uit_code")]
        public string UitCode { get; set; }

        /// <summary>
        /// Бренд
        /// </summary>
        [JsonPropertyName("brand")]
        public string Brand { get; set; }

        /// <summary>
        /// Производственный ветеринарный сопроводительный документ (обязательный для ТГ "Молочная продукция")
        /// </summary>
        [JsonPropertyName("prVetDocument")]
        public string PrVetDocument { get; set; }

        /// <summary>
        /// Товарная группа
        /// </summary>
        [JsonPropertyName("productGroup")]
        public ProductGroup? ProductGroup { get; set; }

        /// <summary>
        /// Идентификатор товарной группы УОТ
        /// </summary>
        /// <remarks>
        /// см.Справочник "Список поддерживаемых товарных групп" <see cref="ProductGroup"/>.
        /// </remarks>
        [JsonPropertyName("productGroupId")]
        public int? ProductGroupId { get; set; }

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
        /// Прописывается ИНН последнего продавца.
        /// Если ИНН последнего продавца = ИНН из токена, то признак выбытия от продавца = 1 иначе 0.
        /// Если запрос без токена, то параметр в ответе отсутствует
        /// </remarks>
        [JsonPropertyName("markWithdraw")]
        public bool? MarkWithdraw { get; set; }

        public override string ToString() => $"{Cis} - {ProductName}";
    }
}
