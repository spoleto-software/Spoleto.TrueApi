using System.Text.Json.Serialization;

namespace Spoleto.TrueApi
{
    //todo: после тестов понять нужен ли кастомный тип NkModerationErrorModel
    /// <summary>
    /// Ошибки обнаруженные при валидации контента.
    /// </summary>
    public class NkModerationErrorModel : Dictionary<string, List<string>>
    {
        /// <summary>
        /// Общее количество ошибок
        /// </summary>
        [JsonPropertyName("totalErrors")]
        public int TotalErrors { get; set; }

        /// <summary>
        /// Общая ошибка при разборе информации
        /// </summary>
        [JsonPropertyName("commonError")]
        public string CommonError { get; set; }

        public override string ToString() => $"TotalErrors: {TotalErrors}";
    }
}
