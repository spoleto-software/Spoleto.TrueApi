using System.Text.Json.Serialization;

namespace Spoleto.TrueApi.Documents
{
    public class CommonError
    {
        //[JsonPropertyName("errorCode")]
        //public string ErrorCode { get; set; }

        /// <summary>
        /// Текст ошибки
        /// </summary>
        [JsonPropertyName("errorMessage")]
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Значение параметра, в котором обнаружена ошибка
        /// </summary>
        [JsonPropertyName("errorObject")]
        public string ErrorObject { get; set; }
    }
}