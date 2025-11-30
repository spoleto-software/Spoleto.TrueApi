using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Spoleto.TrueApi
{
    /// <summary>
    /// Категории товара. Используется для получения полной информации о товаре.
    /// </summary>
    public class NkProductCategoryModel : INkObject
    {
        /// <summary>
        /// Идентификатор категории, в которой расположен товар, исключая родителей этой категории
        /// </summary>
        [JsonPropertyName("cat_id")]
        [Required]
        public int CategoryId { get; set; }

        /// <summary>
        /// Наименование категории, в которой расположен товар и/или
        /// </summary>
        [JsonPropertyName("cat_name")]
        [Required]
        public string CategoryName { get; set; }

        /// <summary>
        /// Идентификатор категории торговой сети, в которой расположен товар
        /// </summary>
        /// <remarks>
        /// Только для владельца сети, если указан party_id в запросе
        /// </remarks>
        [JsonPropertyName("party_cat_id")]
        public int PartyCategoryId { get; set; }

        /// <summary>
        /// Наименование категории торговой сети, в которой расположен товар
        /// </summary>
        [JsonPropertyName("party_cat_name")]
        public string PartyCategoryName { get; set; }

        public override string ToString() => CategoryName;
    }
}
