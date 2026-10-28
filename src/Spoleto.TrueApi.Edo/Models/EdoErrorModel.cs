using System.Text.Json.Serialization;

namespace Spoleto.TrueApi.Edo.Models
{
    /// <summary>
    /// Ответ сервиса с ошибкой.
    /// </summary>
    /// <remarks>
    /// Поле «code» не читается: в ответах встречается и строкой, и числом.
    /// </remarks>
    internal record EdoErrorModel
    {
        /// <summary>
        /// Сообщение об ошибке.
        /// </summary>
        [JsonPropertyName("error_message")]
        public string? ErrorMessage { get; set; }

        /// <summary>
        /// Описание ошибки.
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; set; }

        /// <summary>
        /// Ошибки.
        /// </summary>
        [JsonPropertyName("errors")]
        public List<EdoErrorModel>? Errors { get; set; }

        public override string? ToString()
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
                return ErrorMessage;

            if (Errors?.Count > 0)
                return string.Join("; ", Errors);

            return Description;
        }
    }
}
