using System.Text.Json.Serialization;

namespace Spoleto.TrueApi
{
    /// <summary>
    /// Атрибуты для Краткая информация о кодах идентификации (КИ).
    /// </summary>
    public class SpecialAttribute
    {
        /// <summary>
        /// Максимальная цена розничной продажи
        /// </summary>
        [JsonPropertyName("maxRetailPrice")]
        public string MaxRetailPrice { get; set; }

        /// <summary>
        /// Максимальная цена розничной продажи
        /// </summary>
        [JsonPropertyName("expirationDate")]
        public DateTime? ExpirationDate { get; set; }

        /// <summary>
        /// Идентификатор ВСД
        /// </summary>
        [JsonPropertyName("prVetDocument")]
        public string PrVetDocument { get; set; }

        /// <summary>
        /// Дата ввода товара в оборот или формирования агрегата
        /// </summary>
        /// <remarks>
        /// Возвращается в формате yyyy-MM-ddTHH:mm:ss.SSS’Z
        /// </remarks>
        [JsonPropertyName("introducedDate")]
        public DateTime? IntroducedDate { get; set; }

        /// <summary>
        /// Объём
        /// </summary>
        [JsonPropertyName("capacity")]
        public string Capacity { get; set; }

        /// <summary>
        /// Следующие КИ, после перемаркировки
        /// </summary>
        [JsonPropertyName("nextCis")]
        public string NextCis { get; set; }

        /// <summary>
        /// Предыдущие КИ, до перемаркировки
        /// </summary>
        [JsonPropertyName("prevCis")]
        public string PrevCis { get; set; }

        /// <summary>
        /// Вид товарооборота
        /// </summary>
        /// <remarks>
        /// Возможные значения: «SELLING» — «Продажа»; «CONTRACT» — «Передача по АКС»
        /// </remarks>
        [JsonPropertyName("turnoverType")]
        public string TurnoverType { get; set; }

        /// <summary>
        /// Тип возврата в оборот
        /// </summary>
        [JsonPropertyName("retType")]
        public string RetType { get; set; }

        /// <summary>
        /// ИНН / УНБ экспортёра
        /// </summary>
        [JsonPropertyName("expNum")]
        public string ExpNum { get; set; }

        /// <summary>
        /// Наименование экспортёра
        /// </summary>
        [JsonPropertyName("expName")]
        public string ExpName { get; set; }

        /// <summary>
        /// Признак импортного товара, ввезённого в РФ после 01.07.2020
        /// </summary>
        [JsonPropertyName("remainsImport")]
        public string RemainsImport { get; set; }

        /// <summary>
        /// Код принятого решения из ДТ
        /// </summary>
        [JsonPropertyName("ftsDecisionCode")]
        public string FtsDecisionCode { get; set; }

        /// <summary>
        /// Количество единиц употребления в потребительской упаковке /заявленный объём
        /// </summary>
        [JsonPropertyName("quantityInPack")]
        public string QuantityInPack { get; set; }

        /// <summary>
        /// Счётчик проданного и возвращённого товара участниками оборота товаров
        /// </summary>
        [JsonPropertyName("soldCount")]
        public string SoldCount { get; set; }

        /// <summary>
        /// Описание другой причины вывода из оборота
        /// </summary>
        [JsonPropertyName("eliminationReasonOther")]
        public string EliminationReasonOther { get; set; }

        /// <summary>
        /// Описание другой причины вывода из оборота
        /// </summary>
        /// <remarks>
        /// Возвращается в граммах, первые три цифры — значение кг, последние три цифры — десятые, сотые и тысячные.
        /// </remarks>
        [JsonPropertyName("productWeightGr")]
        public int ProductWeightGr { get; set; }

        /// <summary>
        /// Заводской серийный номер
        /// </summary>
        [JsonPropertyName("manufacturerSerialNumber")]
        public string ManufacturerSerialNumber { get; set; }

        /// <summary>
        /// Сведения о сертификатах и декларациях
        /// </summary>
        [JsonPropertyName("approvementDocument")]
        public ApprovementDocument ApprovementDocument { get; set; }


    }
}
