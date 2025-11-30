using System.Text.Json.Serialization;

namespace Spoleto.TrueApi
{
    /// <summary>
    /// Краткая информация о кодах идентификации (КИ).
    /// </summary>
    public class CisShortInfoModel
    {
        /// <summary>
        /// КИ товара
        /// </summary>
        [JsonPropertyName("cis")]
        public string Cis { get; set; }

        /// <summary>
        /// КИ без скобок
        /// </summary>
        [JsonPropertyName("cisWithoutBrackets")]
        public string CisWithoutBrackets { get; set; }

        /// <summary>
        /// КИ запрашиваемых потребительских /групповых /транспортных упаковок
        /// </summary>
        [JsonPropertyName("requestedCis")]
        public string RequestedCis { get; set; }

        /// <summary>
        /// Дата нанесения
        /// </summary>
        /// <remarks>
        /// звращается в формате yyyy-MMddTHH:mm:ss.SSS’Z. Параметр не возвращается для КИТУ, АТК
        /// </remarks>
        [JsonPropertyName("applicationDate")]
        public DateTime? ApplicationDate { get; set; }

        /// <summary>
        /// Список дочерних КИ в агрегате
        /// </summary>
        /// <remarks>
        /// Возвращается список КИ только верхнего уровня
        /// </remarks>
        [JsonPropertyName("children")]
        public List<string> Children { get; set; }

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
        /// <remarks>
        /// Возможные значения:
        /// LOCAL — производство РФ;
        /// FOREIGN — ввезён в РФ;
        /// REMAINS — маркировка остатков;
        /// CROSSBORDER — ввезён из стран
        /// ЕАЭС
        /// </remarks>
        [JsonPropertyName("emissionType")]
        public string EmissionType { get; set; }

        /// <summary>
        /// Код товара
        /// </summary>
        [JsonPropertyName("gtin")]
        public string Gtin { get; set; }

        /// <summary>
        /// ИНН собственника товара
        /// </summary>
        [JsonPropertyName("ownerInn")]
        public string OwnerInn { get; set; }

        /// <summary>
        /// Тип упаковки
        /// </summary>
        /// <remarks>
        /// Если значение параметра = «BOX» («Транспортная упаковка»), то в ответе не возвращается параметр «applicationDate» («Дата нанесения»)
        /// </remarks>
        [JsonPropertyName("extendedPackageType")]
        public string ExtendedPackageType { get; set; }

        /// <summary>
        /// Код идентификации упаковки, в которую агрегирован товар
        /// </summary>
        [JsonPropertyName("parent")]
        public string Parent { get; set; }

        /// <summary>
        /// Фактический объём выпущенной продукции, мл
        /// </summary>
        /// <remarks>
        /// Возвращается для товарных групп «Безалкогольное пиво», «Пиво, напитки, изготавливаемые на основе пива, слабоалкогольные напитки»
        /// </remarks>
        [JsonPropertyName("volumeSpecialPack")]
        public string VolumeSpecialPack { get; set; }

        /// <summary>
        /// ИНН производителя
        /// </summary>
        /// <remarks>
        /// Параметр не возвращается для агрегатов товарных групп «Велосипеды и велосипедные рамы», «Духи и туалетная вода», «Медицинские изделия», «Молочная продукция», «Обувные товары», «Пиво, напитки, изготавливаемые на основе пива, слабоалкогольные напитки», «Предметы одежды, бельё постельное, столовое, туалетное и кухонное», «Упакованная вода», «Фотокамеры(кроме кинокамер), фотовспышки и лампы-вспышки», «Шины и покрышки пневматические резиновые новые»
        /// </remarks>
        [JsonPropertyName("producerInn")]
        public string ProducerInn { get; set; }

        /// <summary>
        /// Товарная группа
        /// </summary>
        [JsonPropertyName("productGroup")]
        public string ProductGroup { get; set; }

        /// <summary>
        /// Товарная группа
        /// </summary>
        [JsonPropertyName("productGroupId")]
        public int? ProductGroupId { get; set; }

        /// <summary>
        /// Дата производства
        /// </summary>
        /// <remarks>
        /// Возвращается в формате yyyy-MMddTHH:mm:ss.SSS’Z. Параметр не возвращается для КИТУ и АТК (кроме товарных групп «Альтернативная табачная продукция», «Никотиносодержащая продукция», «Табачная продукция»)
        /// </remarks>
        [JsonPropertyName("producedDate")]
        public DateTime? ProducedDate { get; set; }

        /// <summary>
        /// Дата вывода из оборота.
        /// </summary>
        /// <remarks>
        /// Возвращается в формате yyyy-MM-ddTHH:mm:ss.SSS’Z
        /// </remarks>
        [JsonPropertyName("receiptDate")]
        public DateTime? ReceiptDate { get; set; }

        /// <summary>
        /// Статус товара/КИ
        /// </summary>
        [JsonPropertyName("status")]
        public CisStatus? Status { get; set; }

        /// <summary>
        /// Расширенный статус КИ, КИН, КИГУ
        /// </summary>
        [JsonPropertyName("statusEx")]
        public string StatusEx { get; set; }

        /// <summary>
        /// Код товарной номенклатуры (10 знаков)
        /// </summary>
        [JsonPropertyName("tnVedEaes")]
        public string TnVedEaes { get; set; }

        /// <summary>
        /// Код товарной позиции ТН ВЭД ЕАЭС товара
        /// </summary>
        [JsonPropertyName("tnVedEaesGroup")]
        public string TnVedEaesGroup { get; set; }

        /// <summary>
        /// Причина выбытия
        /// </summary>
        [JsonPropertyName("withdrawReason")]
        public string WithdrawReason { get; set; }

        /// <summary>
        /// Причина выбытия
        /// </summary>
        [JsonPropertyName("licences")]
        public List<Licence> Licences { get; set; }

        /// <summary>
        /// Номер серии товара
        /// </summary>
        [JsonPropertyName("batchNumber")]
        public string BatchNumber { get; set; }

        /// <summary>
        /// Номер серии товара
        /// </summary>
        [JsonPropertyName("partyNumber")]
        public string PartyNumber { get; set; }

        /// <summary>
        /// Дополнительные атрибуты «cis» («КИ потребительских / групповых / транспортных упаковок»)
        /// </summary>
        [JsonPropertyName("specialAttributes")]
        public SpecialAttribute SpecialAttributes { get; set; }


        public override string ToString() => $"{Cis}";//$"{Cis} - {ProductName}";
    }
}
