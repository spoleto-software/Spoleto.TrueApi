using System.Text.Json.Serialization;

namespace Spoleto.TrueApi
{
    /// <summary>
    /// Категории товара.
    /// </summary>
    public class NkCategoryModel : INkObject
    {
        /// <summary>
        /// Идентификатор категории
        /// </summary>
        [JsonPropertyName("cat_id")]
        public int CategoryId { get; set; }

        /// <summary>
        /// Наименование категории
        /// </summary>
        [JsonPropertyName("cat_name")]
        public string CategoryName { get; set; }

        /// <summary>
        /// Идентификатор родительской категории
        /// </summary>
        [JsonPropertyName("cat_parent_id")]
        public int CategoryParentId { get; set; }

        /// <summary>
        /// Уровень в дереве категорий
        /// </summary>
        [JsonPropertyName("cat_level")]
        public int CategoryLevel { get; set; }

        public override string ToString() => CategoryName;
    }
}
