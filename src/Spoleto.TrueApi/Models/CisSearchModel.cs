using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Spoleto.TrueApi
{
    /// <summary>
    /// Информация для получения сведений о КИ, находящихся на балансе у участника оборота товаров.
    /// </summary>
    public class CisSearchModel
    {
        /// <summary>
        /// Формат файла.
        /// </summary>
        [JsonPropertyName("format")]
        [Required]
        public DispenserFileFormat Format { get; } = DispenserFileFormat.CSV;

        /// <summary>
        /// Наименование выгрузки.
        /// </summary>
        /// <remarks>
        /// FILTERED_CIS_REPORT –получения списка КИ, принадлежащих участнику оборота товаров.
        /// </remarks>
        [JsonPropertyName("name")]
        [Required]
        public string Name { get; } = "FILTERED_CIS_REPORT";

        /// <summary>
        /// Вид выгрузки.
        /// </summary>
        /// <remarks>
        ///  SINGLE (однократная).
        /// </remarks>
        [JsonPropertyName("periodicity")]
        [Required]
        public DispenserPeriodicity Periodicity { get; } = DispenserPeriodicity.SINGLE;

        /// <summary>
        /// Указывается цифровой код товарной группы(см. "Справочник "Список поддерживаемых товарных групп"")
        /// </summary>
        [JsonConverter(typeof(ProductGroupJsonConverter))]
        [JsonPropertyName("productGroupCode")]
        [Required]
        public ProductGroup ProductGroupCode { get; set; }

        /// <summary>
        /// Строка параметров задания в формате JSON.
        /// </summary>
        [JsonConverter(typeof(CisSearchParamsModelJsonConverter))]
        [JsonPropertyName("params")]
        [Required]
        public CisSearchParamsModel Params { get; set; }

        public override string ToString() => $"{Format} - {Name}";
    }
}
