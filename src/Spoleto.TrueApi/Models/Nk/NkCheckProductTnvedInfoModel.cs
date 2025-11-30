using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Spoleto.TrueApi
{
    /// <summary>
    /// Результат определения принадлежности товара с указанными кодами ТНВЭД к маркируемым товарным группам
    /// </summary>
    public class NkCheckProductTnvedInfoModel
    {
        /// <summary>
        /// Код ТНВЭД, с которым связан указанный в запросе код товара.
        /// </summary>
        /// <remarks>
        /// При отсутствии товара в системе код ТН ВЭД не указывается
        /// </remarks>
        [JsonPropertyName("tnved")]
        public string Tnved { get; set; }

        /// <summary>
        /// Информация о маркировке товара с указанным кода товара или кодом ТН ВЭД
        /// </summary>
        [JsonPropertyName("is_marked")]
        [Required]
        public string IsMarked { get; set; }

        /// <summary>
        /// Коды ответов
        /// </summary>
        /// <remarks>
        /// Возможные значения:
        /// 0 — Товар с указанным кодом ТНВЭД не подлежит маркировке;
        /// 1 — Товар с указанным кодом ТНВЭД подлежит маркировке;
        /// 2 — ТНВЭД не найден;
        /// 3 — По указанному коду ТНВЭД невозможно установить необходимость маркировки. Уточните код ТНВЭД товара
        /// </remarks>
        [JsonPropertyName("is_marked_code")]
        [Required]
        public int IsMarkedCode { get; set; }
    }
}
