using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Spoleto.TrueApi
{
    /// <summary>
    /// Ответ на отзыв на товар.
    /// </summary>
    public class NkProductReviewReplyModel
    {
        /// <summary>
        /// Идентификатор отзыва-ответа
        /// </summary>
        [JsonPropertyName("review_id")]
        [Required]
        public int ReviewId { get; set; }

        /// <summary>
        /// Автор (имя, фамилия, псевдоним)
        /// </summary>
        [JsonPropertyName("review_author")]
        [Required]
        public string ReviewAuthor { get; set; }

        /// <summary>
        /// Рейтинг отзыва-ответа
        /// </summary>
        [JsonPropertyName("review_rating")]
        [Required]
        public decimal ReviewRating { get; set; }

        /// <summary>
        /// Текст отзыва-ответа
        /// </summary>
        [JsonPropertyName("review_text")]
        [Required]
        public string ReviewText { get; set; }

        /// <summary>
        /// Дата создания отзыва
        /// </summary>
        /// <remarks>
        /// UTC.yyyy-MM-ddTHH:mm:ss.SSS’Z
        /// </remarks>
        [JsonPropertyName("review_date")]
        [Required]
        public DateTime ReviewDate { get; set; }

        /// <summary>
        /// Ссылка на фотографию автора
        /// </summary>
        [JsonPropertyName("review_author_img")]
        [Required]
        public string ReviewAuthorImg { get; set; }

        public override string ToString() => ReviewText;
    }
}
