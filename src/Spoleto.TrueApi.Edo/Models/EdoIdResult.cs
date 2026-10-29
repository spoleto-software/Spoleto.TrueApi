using System.Text.Json.Serialization;

namespace Spoleto.TrueApi.Edo.Models
{
    /// <summary>
    /// Ответ «ЭДО Лайт» с идентификатором созданного документа или события.
    /// </summary>
    internal record EdoIdResult
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        [JsonPropertyName("id")]
        public string? Id { get; set; }
    }
}
