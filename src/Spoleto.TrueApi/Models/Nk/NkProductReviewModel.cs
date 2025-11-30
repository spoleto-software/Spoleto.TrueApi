using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Spoleto.TrueApi
{
    /// <summary>
    /// Отзыв на товар.
    /// </summary>
    public class NkProductReviewModel
    {
        /// <summary>
        /// Идентификатор отзыва
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
        /// Рейтинг отзыва
        /// </summary>
        [JsonPropertyName("review_rating")]
        [Required]
        public decimal ReviewRating { get; set; }

        /// <summary>
        /// Текст отзыва
        /// </summary>
        [JsonPropertyName("review_text")]
        [Required]
        public string ReviewText { get; set; }

        /// <summary>
        /// дата создания отзыва
        /// </summary>
        /// <remarks>
        /// UTC.yyyy-MM-ddTHH:mm:ss.SSS’Z
        /// </remarks>
        [JsonPropertyName("review_date")]
        [Required]
        public DateTime ReviewDate { get; set; }

        /// <summary>
        /// ссылка на фотографию автора
        /// </summary>
        [JsonPropertyName("review_author_img")]
        [Required]
        public string ReviewAuthorImg { get; set; }

        /// <summary>
        /// Массив с ответами на отзыв
        /// </summary>
        /// <remarks>
        /// Если отзыв имеет ответы (т.е. отзывы с review_parent_id = review_id данного/родительского отзыва)
        /// </remarks>
        [JsonPropertyName("review_replies")]
        public List<NkProductReviewReplyModel> ReviewReplies { get; set; }

        public override string ToString() => ReviewText;
    }
}
