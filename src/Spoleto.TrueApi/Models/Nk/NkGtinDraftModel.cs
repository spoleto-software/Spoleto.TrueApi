using System.Text.Json.Serialization;

namespace Spoleto.TrueApi
{
    /// <summary>
    /// Черновики кодов товаров.
    /// </summary>
    public class NkGtinDraftModel : INkObject
    {
        /// <summary>
        /// Ежемесячное ограничение
        /// </summary>
        [JsonPropertyName("monthly-limit")]
        public NkGtinDraftMonthlyLimitModel MonthlyLimit { get; set; }

        /// <summary>
        /// Список черновиков
        /// </summary>
        /// <remarks>
        /// При наличии сгенерированных черновиков
        /// </remarks>
        [JsonPropertyName("drafts")]
        public List<NkGtinResultModel> Drafts { get; set; }
    }
}
