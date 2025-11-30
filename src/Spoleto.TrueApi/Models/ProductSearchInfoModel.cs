using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Spoleto.TrueApi
{
    /// <summary>
    /// Фильтр для поиска карточки товара по КИ.
    /// </summary>
    public class ProductSearchInfoModel
    {
        /// <summary>
        /// Номер страницы вложений в агрегат первого слоя.
        /// </summary>
        /// <remarks>
        /// Значение по умолчанию: 1.
        /// Не используется товарной группой "Табачная продукция"
        /// </remarks>
        [JsonPropertyName("childrenPage")]
        public int? ChildrenPage { get; set; }

        /// <summary>
        /// Размер страницы вложений в агрегат первого слоя.
        /// </summary>
        /// <remarks>
        /// Значение по умолчанию: 50.
        /// Не используется товарной группой "Табачная продукция"
        /// </remarks>
        [JsonPropertyName("childrenLimit")]
        public int? ChildrenLimit { get; set; }

        /// <summary>
        /// Код идентификации
        /// </summary>
        [JsonPropertyName("cis")]
        [Required]
        public string Cis { get; set; }

        public override string ToString() => Cis;
    }
}
