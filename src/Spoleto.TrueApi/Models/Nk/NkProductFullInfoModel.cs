using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Spoleto.TrueApi
{
    /// <summary>
    /// Полная информация о товаре.
    /// </summary>
    public class NkProductFullInfoModel : INkObject
    {
        /// <summary>
        /// Идентификатор товара
        /// </summary>
        [JsonPropertyName("good_id")]
        [Required]
        public int GoodId { get; set; }

        /// <summary>
        /// Наименование товара
        /// </summary>
        [JsonPropertyName("good_name")]
        [Required]
        public string GoodName { get; set; }

        /// <summary>
        /// Изображение товара
        /// </summary>
        [JsonPropertyName("good_img")]
        [Required]
        public string GoodImg { get; set; }

        /// <summary>
        /// Массив содержащий информацию о штрих-кодах
        /// </summary>
        /// <remarks>
        [JsonPropertyName("identified_by")]
        public List<NkProductIdentifierModel> IdentifiedBy { get; set; }

        /// <summary>
        /// Массив категорий
        /// </summary>
        [JsonPropertyName("categories")]
        public List<NkProductCategoryModel> Categories { get; set; }

        /// <summary>
        /// Идентификатор бренда для торговой сети
        /// </summary>
        /// <remarks>
        /// Только для владельца сети, если указан party_id в запросе
        /// </remarks>
        [JsonPropertyName("party_brand_id")]
        public string PartyBrandId { get; set; }

        /// <summary>
        /// Идентификатор бренда
        /// </summary>
        [JsonPropertyName("brand_id")]
        [Required]
        public int BrandId { get; set; }

        /// <summary>
        /// Наименование бренда
        /// </summary>
        [JsonPropertyName("brand_name")]
        [Required]
        public string BrandName { get; set; }

        /// <summary>
        /// Рейтинг товара
        /// </summary>
        [JsonPropertyName("good_rating")]
        [Required]
        public decimal? GoodRating { get; set; }

        /// <summary>
        /// Массив с изображениями
        /// </summary>
        [JsonPropertyName("good_images")]
        [Required]
        public List<NkProductImageModel> GoodImages { get; set; }

        /// <summary>
        /// Массив атрибутов
        /// </summary>
        /// <remarks>
        /// Приватные атрибуты отдаются только те, которые принадлежат аккаунту apikey
        /// </remarks>
        [JsonPropertyName("good_attrs")]
        public List<NkProductAttributeModel> GoodAttributes { get; set; }

        /// <summary>
        /// Массив с отзывами
        /// </summary>
        [JsonPropertyName("good_reviews")]
        [Required]
        public List<NkProductReviewModel> GoodReviews { get; set; }

        /// <summary>
        /// Количество отзывов
        /// </summary>
        [JsonPropertyName("good_reviews_count")]
        [Required]
        public int GoodReviewsCount { get; set; }

        /// <summary>
        /// Ссылка на страницу товара
        /// </summary>
        [JsonPropertyName("good_url")]
        [Required]
        public string GoodUrl { get; set; }

        /// <summary>
        /// Массив цен на товар по торговым сетям данного аккаунта
        /// </summary>
        [JsonPropertyName("good_prices")]
        [Required]
        public List<NkProductLocationModel> GoodPrices { get; set; }

        public override string ToString() => GoodName;
    }
}
