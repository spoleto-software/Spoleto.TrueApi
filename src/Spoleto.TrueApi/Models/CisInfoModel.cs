using System.Text.Json.Serialization;

namespace Spoleto.TrueApi
{
    /// <summary>
    /// Подробная информация о кодах идентификации (КИ).
    /// </summary>
    public class CisInfoModel
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
        /// Группа ТН ВЭД
        /// </summary>
        [JsonPropertyName("tnVedEaesGroup")]
        public string TnVedEaesGroup { get; set; }

        /// <summary>
        /// Наименование продукции
        /// </summary>
        [JsonPropertyName("productName")]
        public string ProductName { get; set; }

        /// <summary>
        /// Дата ввода товара в оборот
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
        /// ИНН Российского производителя.
        /// </summary>
        /// <remarks>
        /// В ЛП для агрегатов не возвращается
        /// </remarks>
        [JsonPropertyName("producerInn")]
        public string ProducerInn { get; set; }

        /// <summary>
        /// Наименование производителя
        /// </summary>
        /// <remarks>
        /// В ЛП для агрегатов не возвращается
        /// </remarks>
        [JsonPropertyName("producerName")]
        public string ProducerName { get; set; }

        /// <summary>
        /// ИНН собственника товара
        /// </summary>
        /// <remarks>
        /// Данный параметр возвращается только владельцу продукции, ФОИВ и Оператору.
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
        /// Родительский КИ
        /// </summary>
        [JsonPropertyName("parent")]
        public string Parent { get; set; }

        /// <summary>
        /// Список дочерних КИ в агрегате
        /// </summary>
        [JsonPropertyName("child")]
        public List<string> Child { get; set; }

        /// <summary>
        /// Идентификатор товарной группы
        /// </summary>
        /// <remarks>
        /// см.Справочник "Список поддерживаемых товарных групп" <see cref="ProductGroup"/>.
        /// </remarks>
        [JsonPropertyName("productGroupId")]
        public int ProductGroupId { get; set; }

        /// <summary>
        /// Наименование товарной группы
        /// </summary>
        [JsonPropertyName("productGroup")]
        public ProductGroup ProductGroup { get; set; }

        public override string ToString() => $"{Cis} - {ProductName}";
    }
}
