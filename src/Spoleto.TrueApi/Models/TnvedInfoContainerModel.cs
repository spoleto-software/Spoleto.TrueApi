using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Spoleto.TrueApi
{
    /// <summary>
    /// Список десятизначных кодов ТН ВЭД
    /// </summary>
    public class TnvedInfoContainerModel
    {
        /// <summary>
        /// Список кодов  ТН ВЭД
        /// </summary>
        [JsonPropertyName("records")]
        [Required]
        public List<TnvedInfoModel> Records { get; set; }

        /// <summary>
        /// Общее количество кодов ТН ВЭД, подходящие под параметры фильтрации
        /// </summary>
        [JsonPropertyName("total")]
        [Required]
        public long Total { get; set; }

        public override string ToString() => $"Records.Count = {Records?.Count}; Total = {Total}";
    }
}
