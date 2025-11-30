using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Spoleto.TrueApi
{
    /// <summary>
    /// Информация для поиска кода товарной группы по КИ товара
    /// </summary>
    public class ProductGroupSearchModel
    {
        /// <summary>
        /// Список кодов товаров
        /// </summary>
        [JsonPropertyName("data")]
        [Required]
        public List<string> Data { get; set; }
    }
}
