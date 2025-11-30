using System.Text.Json.Serialization;

namespace Spoleto.TrueApi
{
    /// <summary>
    /// Информация для поиска списка товаров с подробной информации, которые доступны в данный момент времени участнику оборота товаров.
    /// </summary>
    public class ProductSearchFullInfoModel
    {
        /// <summary>
        /// Номер страницы вложений в агрегат первого слоя.
        /// </summary>
        /// <remarks>
        /// Значение по умолчанию: 1.
        /// Не используется товарной группой "Табачная продукция"
        /// </remarks>
        [JsonPropertyName("childrenPage")]
        public int? ChildrenPage { get; set; }

        /// <summary>
        /// Размер страницы вложений в агрегат первого слоя.
        /// </summary>
        /// <remarks>
        /// Значение по умолчанию: 50.
        /// Не используется товарной группой "Табачная продукция"
        /// </remarks>
        [JsonPropertyName("childrenLimit")]
        public int? ChildrenLimit { get; set; }

        /// <summary>
        /// Товарная группа: параметр обязательно указывать для товарных групп: milk – Молочная продукция; water – Упакованная вода
        /// </summary>
        [JsonPropertyName("pg")]
        public string ProductGroup { get; set; }

        /// <summary>
        /// Флаг использование кэша.
        /// </summary>
        /// <remarks>
        /// При включенном флаге УОТ получает в ответ не более установленного лимита кода идентификации 1524
        /// </remarks>
        [JsonPropertyName("cache")]
        public bool? Cache { get; set; }

        /// <summary>
        /// Код идентификации, используемый для фильтрации по списку КИ
        /// </summary>
        [JsonPropertyName("cis")]
        public string Cis { get; set; }

        /// <summary>
        /// Режим фильтрации по КИ
        /// </summary>
        [JsonPropertyName("cisMatchMode")]
        public CisMatchModel? CisMatchMode { get; set; }

        /// <summary>
        /// Дата эмиссии от
        /// </summary>
        /// <remarks>
        /// Задается в формате yyyy-MM-ddTHH:mm:ss.SSS’Z
        /// </remarks>
        [JsonConverter(typeof(JavaScriptDateTimeJsonConverter))]
        [JsonPropertyName("emissionDateFrom")]
        public DateTime? EmissionDateFrom { get; set; }

        /// <summary>
        /// Дата эмиссии до
        /// </summary>
        /// <remarks>
        /// Задается в формате yyyy-MM-ddTHH:mm:ss.SSS’Z
        /// </remarks>
        [JsonConverter(typeof(JavaScriptDateTimeJsonConverter))]
        [JsonPropertyName("emissionDateTo")]
        public DateTime? EmissionDateTo { get; set; }

        /// <summary>
        /// Код товара
        /// </summary>
        /// <remarks>
        /// Если код товара менее 14 символов, то он дополняется ведущими нулями
        /// </remarks>
        [JsonPropertyName("gtin")]
        public string Gtin { get; set; }

        /// <summary>
        /// ИНН производителя
        /// </summary>
        [JsonPropertyName("producerInn")]
        public string ProducerInn { get; set; }

        /// <summary>
        /// Серийный номер кода идентификации
        /// </summary>
        [JsonPropertyName("sn")]
        public string SerialNumber { get; set; }

        /// <summary>
        /// ИНН владельца
        /// </summary>
        [JsonPropertyName("ownerInn")]
        public string OwnerInn { get; set; }

        /// <summary>
        /// Тип упаковки.
        /// </summary>
        [JsonPropertyName("cisPackageType")]
        public PackType? CisPackageType { get; set; }

        /// <summary>
        /// 10-ти значный код ТН ВЭД
        /// </summary>
        [JsonPropertyName("tnVed10")]
        public string TnVed10 { get; set; }

        /// <summary>
        /// Код типа производства
        /// </summary>
        [JsonPropertyName("emissionType")]
        public EmissionType? EmissionType { get; set; }

        /// <summary>
        /// Выбор направления
        /// </summary>
        /// <remarks>
        /// PREV – Предыдущий раздел; NEXT – Следующий раздел
        /// </remarks>
        [JsonPropertyName("pageDir")]
        public string PageDir { get; set; }//todo: если будем использовать это свойство, то надо будет завести enum для возможных значений

        /// <summary>
        /// Уникальный идентификатор товара (УИТ)
        /// </summary>
        /// <remarks>
        /// "точка отсчета"(запись, с которой начнется выборка), по которому сортируются записи.
        /// Использовать только совместно с параметром orderedColumnValue
        /// </remarks>
        [JsonPropertyName("uit")]
        public string Uit { get; set; }

        /// <summary>
        /// Направление сортировки
        /// </summary>
        /// <remarks>
        /// Направление сортировки: ASC – по возрастанию; DESC – по убыванию
        /// </remarks>
        [JsonPropertyName("order")]
        public string Order { get; set; }//todo: если будем использовать это свойство, то надо будет завести enum для возможных значени

        /// <summary>
        /// Название столбца, по которому будет производиться сортировка.
        /// </summary>
        /// <remarks>
        /// Допустимое значение emd - дата эмиссии
        /// </remarks>
        [JsonPropertyName("orderColumn")]
        public string OrderColumn { get; set; }//todo: если будем использовать это свойство, то надо будет завести enum для возможных значений

        /// <summary>
        /// Значение столбца
        /// </summary>
        /// <remarks>
        /// Значение столбца, "точки отсчета" (запись, с которой начинается выборка), по которому сортируются записи.
        /// Использовать только совместно с параметром uit
        /// </remarks>
        [JsonPropertyName("orderedColumnValue")]
        public string OrderedColumnValue { get; set; }

        /// <summary>
        /// Максимальное количество записей
        /// </summary>
        /// <remarks>
        /// Максимальное количество записей, которое вернется в качестве ответа, не более 10000 записей.
        /// (По умолчанию 10 записей)
        /// </remarks>
        [JsonPropertyName("limit")]
        public int? Limit { get; set; }

        /// <summary>
        /// Состояние КИ
        /// </summary>
        /// <remarks>
        /// Состояние КИ:
        /// PACKED- в агрегате (агрегирован);
        /// NOT_PACKED – не в агрегате (дезагрегирован);
        /// ANY – любое состояние
        /// </remarks>
        [JsonPropertyName("cisAggregationState")]
        public string CisAggregationState { get; set; }//todo: если будем использовать это свойство, то надо будет завести enum для возможных значений

        /// <summary>
        /// Фильтрация по единичным товарным упаковкам
        /// </summary>
        /// <remarks>
        /// Возможные значения:
        /// ALL – товары и упаковки;
        /// UNIT – только товары (UNIT, BUNDLE);
        /// PACK – только упаковки (LEVEL1, LEVEL2, LEVEL3, LEVEL4, LEVEL5, ATK, SET).
        /// См. "Справочник "Типы упаковки"".
        /// </remarks>
        [JsonPropertyName("packs")]
        public string Packs { get; set; }//todo: если будем использовать это свойство, то надо будет завести enum для возможных значений

        /// <summary>
        /// Показывать товары и товарные упаковки только в одном статусе для агрегации
        /// </summary>
        [JsonPropertyName("aggregation")]
        public bool? Aggregation { get; set; }

        /// <summary>
        /// Количество единиц товаров в упаковке
        /// </summary>
        [JsonPropertyName("countChildren")]
        public int? CountChildren { get; set; }

        /// <summary>
        /// Способ вывода дерева списка КИ/КИТУ
        /// </summary>
        /// <remarks>
        /// Возможные значения:
        /// NO_TREE – Не возвращать вложенные КИ;
        /// ONE_LEVEL – Возвращать только первый уровень вложенности;
        /// ALL_TREE – Возвращать все дерево
        /// </remarks>
        [JsonPropertyName("tree")]
        public string Tree { get; set; }//todo: если будем использовать это свойство, то надо будет завести enum для возможных значений

        /// <summary>
        /// UituStatus
        /// </summary>
        [JsonPropertyName("uituStatus")]
        public string UituStatus { get; set; }

        /// <summary>
        /// Производственный ветеринарный сопроводительный документ(обязательный для ТГ "Молочная продукция")
        /// </summary>
        [JsonPropertyName("prVetDoc")]
        public string PrVetDoc { get; set; }

        /// <summary>
        /// Тип документа
        /// </summary>
        [JsonPropertyName("docType")]
        public DocumentType? DocType { get; set; }

        /// <summary>
        /// Дополнительный статус товара
        /// </summary>
        /// <remarks>
        /// Возможные значения:
        /// WAIT_SHIPMENT – ожидает подтверждения приемки;
        /// WAIT_TRANSFER_TO_OWNER – ожидает передачу собственнику(производство по контракту);
        /// WAIT_REMARK – КИ списан после нанесения на товар(товар ожидает перемаркировку);
        /// WITHDRAW – вывод из оборота;
        /// REMARK_RETIRED – перемаркирован
        /// </remarks>
        [JsonPropertyName("statusExt")]
        public string StatusEx { get; set; }//todo: если будем использовать это свойство, то надо будет завести enum для возможных значений

        /// <summary>
        /// Статус товара/КИ
        /// </summary>
        /// <remarks>
        /// Возможные значения:
        /// INTRODUCED – в обороте;
        /// RETIRED – выбыл
        /// </remarks>
        [JsonPropertyName("cisStatus")]
        public string CisStatus { get; set; }//todo: если будем использовать это свойство, то надо будет завести enum для возможных значений

        /// <summary>
        /// Тип реестра
        /// </summary>
        [JsonPropertyName("registryType")]
        public string RegistryType { get; set; }

        /// <summary>
        /// ИНН текущего владельца
        /// </summary>
        [JsonPropertyName("agentInn")]
        public string AgentInn { get; set; }

        /// <summary>
        /// ИНН предыдущего владельца
        /// </summary>
        [JsonPropertyName("previousAgentInn")]
        public string PreviousAgentInn { get; set; }

        /// <summary>
        /// Вид товарооборота
        /// </summary>
        [JsonPropertyName("turnoverType")]
        public TurnoverType? TurnoverType { get; set; }

        public override string ToString() => Gtin;
    }
}
