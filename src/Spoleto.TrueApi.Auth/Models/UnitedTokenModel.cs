using System.Text.Json.Serialization;

namespace Spoleto.TrueApi.Auth.Models
{
    /// <summary>
    ///  Пакет успешного ответа на POST запрос получения UUID токена.
    /// </summary>
    public class UnitedTokenModel
    {
        /// <summary>
        /// UUID Токен.
        /// </summary>
        [JsonPropertyName("uuidToken")]
        public string UuidToken { get; set; }
    }

}