using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Spoleto.TrueApi.Documents
{
    /// <summary>
    /// Товар для ввода в оборот. Импорт c ФТС (Мех)
    /// </summary>
    public class SupplyImportFtsFurItem : SupplyImportFtsItemBase
    {
        /// <summary>
        /// Тип упаковки
        /// </summary>
        /// <remarks>
        /// UNIT - КИ; LEVEL1-99 - КИТУ; АТК - агрегированный таможенный код
        /// </remarks>
        [JsonPropertyName("pack_type")]
        [Required]
        public PackType PackType => PackType.UNIT;

        /// <summary>
        ///  Код ТН ВЭД
        /// </summary>
        [JsonPropertyName("tnved_code")]
        public string TnvedCode { get; set; }

        /// <summary>
        /// Вид меха
        /// </summary>
        /// <remarks>
        /// Обязательный параметр при значении кода ТН ВЭД равном "4303109080"
        /// </remarks>
        [JsonPropertyName("fur_kind")]
        public int? FurKind { get; set; }
    }
}
