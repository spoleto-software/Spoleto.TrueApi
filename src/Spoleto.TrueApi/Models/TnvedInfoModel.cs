using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Spoleto.TrueApi
{
    /// <summary>
    /// Десятизначные коды ТН ВЭД
    /// </summary>
    public class TnvedInfoModel
    {
        /// <summary>
        /// 10-ти значный код ТН ВЭД
        /// </summary>
        [JsonPropertyName("code")]
        [Required]
        public string Code { get; set; }

        /// <summary>
        /// Текстовое описание кода ТН ВЭД
        /// </summary>
        [JsonPropertyName("description")]
        [Required]
        public string Description { get; set; }

        public override string ToString() => $"{Code} - {Description}";
    }
}
