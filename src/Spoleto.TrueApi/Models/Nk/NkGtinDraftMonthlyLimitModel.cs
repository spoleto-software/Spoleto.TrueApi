using System.Text.Json.Serialization;

namespace Spoleto.TrueApi
{
    /// <summary>
    /// Ежемесячное ограничение.
    /// </summary>
    public class NkGtinDraftMonthlyLimitModel
    {
        /// <summary>
        /// Общее количество черновиков кодов товаров, доступных для генерации в течение одного месяца
        /// </summary>
        [JsonPropertyName("limit")]
        public int Limit { get; set; }

        /// <summary>
        /// Количество черновиков кодов товаров, которые уже были сгенерированы в течение этого месяца
        /// </summary>
        [JsonPropertyName("usage")]
        public int Usage { get; set; }

        public override string ToString() => $"{Usage} из {Limit}";
    }
}
