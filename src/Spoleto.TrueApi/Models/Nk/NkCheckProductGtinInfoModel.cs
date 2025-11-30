using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Spoleto.TrueApi
{
    /// <summary>
    /// Результат определения принадлежности товара с указанными кодом товара к маркируемым товарным группам
    /// </summary>
    public class NkCheckProductGtinInfoModel
    {
        /// <summary>
        /// Код товара, в отношении которого проводился поиск признака маркируемости товара
        /// </summary>
        [JsonPropertyName("gtin")]
        [Required]
        public string Gtin { get; set; }

        /// <summary>
        /// Код ТНВЭД, с которым связан указанный в запросе код товара.
        /// </summary>
        /// <remarks>
        /// При отсутствии товара в системе код ТН ВЭД не указывается
        /// </remarks>
        [JsonPropertyName("tnved")]
        public string Tnved { get; set; }

        /// <summary>
        /// Список кодов товаров
        /// </summary>
        [JsonPropertyName("is_marked")]
        [Required]
        public string IsMarked { get; set; }

        /// <summary>
        /// Коды ответов
        /// </summary>
        /// <remarks>
        /// Возможные значения:
        /// 0 — Товар не подлежит маркировке;
        /// 1 — Товар подлежит маркировке;
        /// 2 — Товар не найден
        /// 1 — Остаток подлежит маркировке (Для товарных остатков (товары с кодом товара, начинающимся на 029));
        /// 2 — Остаток не найден (Для товарных остатков (товары с кодом товара, начинающимся на 029))
        /// </remarks>
        [JsonPropertyName("is_marked_code")]
        [Required]
        public int IsMarkedCode { get; set; }

    }
}
