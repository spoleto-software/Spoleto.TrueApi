using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Spoleto.TrueApi
{
    /// <summary>
    /// Карточка товара, в том числе неопубликованная карточка.
    /// </summary>
    public class NkFeedProductFullInfoModel : NkProductFullInfoModel
    {
        /// <summary>
        /// Статус карточки товара
        /// </summary>
        [JsonPropertyName("good_status")]
        [Required]
        public string GoodStatus { get; set; }

        /// <summary>
        /// Статус карточки товара
        /// </summary>
        [JsonPropertyName("good_detailed_status")]
        [Required]
        public List<string> GoodDetailedStatus { get; set; }
    }
}
