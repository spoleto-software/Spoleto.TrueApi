using System.Text.Json.Serialization;

namespace Spoleto.TrueApi
{
    /// <summary>
    /// Информация для поиска краткой информации по КИ.
    /// </summary>
    public class CisSearchShortInfoModel
    {
        /// <summary>
        /// КИ ГИС МТ
        /// </summary>
        [JsonPropertyName("cis")]
        public string Cis { get; set; }

        /// <summary>
        /// Код статуса КИ
        /// </summary>
        [JsonPropertyName("cisStatus")]
        public CisStatus? CisStatus { get; set; }

        /// <summary>
        /// Код товара
        /// </summary>
        /// <remarks>
        /// Если код товара менее 14 символов, то он дополняется ведущими нулями
        /// </remarks>
        [JsonPropertyName("gtin")]
        public string Gtin { get; set; }

        /// <summary>
        /// Серийный номер кода идентификации
        /// </summary>
        [JsonPropertyName("sn")]
        public string SerialNumber { get; set; }

        /// <summary>
        /// 10-ти значный код ТН ВЭД
        /// </summary>
        [JsonPropertyName("tnVed10")]
        public string TnVed10 { get; set; }

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
        /// Код типа производства
        /// </summary>
        [JsonPropertyName("emissionType")]
        public EmissionType? EmissionType { get; set; }

        /// <summary>
        /// Причина вывода из оборота
        /// </summary>
        [JsonPropertyName("withdrawReason")]
        public WithdrawReason? WithdrawReason { get; set; }

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
        /// Товарная группа
        /// </summary>
        /// <remarks>
        /// параметр обязательно указывать для товарных групп:
        /// milk – Молочная продукция;
        /// water – Упакованная вода</remarks>
        [JsonPropertyName("pg")]
        public string ProductGroup { get; set; }

        /// <summary>
        /// Направление сортировки
        /// </summary>
        /// <remarks>
        /// Направление сортировки: ASC – по возрастанию; DESC – по убыванию
        /// </remarks>
        [JsonPropertyName("order")]
        public string Order { get; set; }//todo: если будем использовать это свойство, то надо будет завести enum для возможных значений

        /// <summary>
        /// ИНН Российского производителя в МОТП
        /// </summary>
        [JsonPropertyName("producerInn")]
        public string ProducerInn { get; set; }

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
        /// Значение столбца
        /// </summary>
        /// <remarks>
        /// Значение столбца, "точки отсчета" (запись, с которой начинается выборка), по которому сортируются записи.
        /// Использовать только совместно с параметром uit
        /// </remarks>
        [JsonPropertyName("orderedColumnValue")]
        public string OrderedColumnValue { get; set; }

        /// <summary>
        /// Название столбца, по которому будет производиться сортировка.
        /// </summary>
        /// <remarks>
        /// Допустимое значение emd - дата эмиссии
        /// </remarks>
        [JsonPropertyName("orderColumn")]
        public string OrderColumn { get; set; }//todo: если будем использовать это свойство, то надо будет завести enum для возможных значений

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
        /// Выбор направления
        /// </summary>
        /// <remarks>
        /// PREV – Предыдущий раздел; NEXT – Следующий раздел
        /// </remarks>
        [JsonPropertyName("pageDir")]
        public string PageDir { get; set; }//todo: если будем использовать это свойство, то надо будет завести enum для возможных значений

        public override string ToString() => Gtin;
    }
}
