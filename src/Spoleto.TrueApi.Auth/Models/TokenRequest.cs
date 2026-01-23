using System.Text.Json.Serialization;

namespace Spoleto.TrueApi.Auth.Models
{
    public record TokenRequest
    {
        /// <summary>
        /// Уникальный идентификатор сгенерированных случайных данных.
        /// </summary>
        [JsonPropertyName("uuid")]
        public string? Uuid { get; set; }

        /// <summary>
        /// Подписанные УКЭП зарегистрированного участника оборота товаров случайные данные в base64 (присоединённая электронная подпись).
        /// </summary>
        /// <remarks>
        /// Если <see cref="UnitedToken"/> = «true», передаётся подписанный ИНН участника оборота товаров, под которым запрашивается авторизация.
        /// </remarks>
        [JsonPropertyName("data")]
        public string Data { get; set; }

        /// <summary>
        /// ИНН организации, под которой требуется авторизация пользователя по МЧД
        /// </summary>
        /// <remarks>
        /// Передаётся в случае, если у физического лица (пользователя организации) есть несколько активных МЧД
        /// в разных организациях для получения аутентификационного токена на конкретную организацию.<br/>
        /// При <see cref="UnitedToken"/> = «true» строго не заполнен.
        /// </remarks>
        [JsonPropertyName("inn")]
        public string? Inn { get; set; }

        /// <summary>
        /// Реквизиты действующего аттестата соответствия объекта информатизации, выданного органом по аттестации объектов информатизации
        /// </summary>
        [JsonPropertyName("details")]
        public string? Details { get; set; }

        /// <summary>
        /// Признак запроса единого токена в виде UUID.
        /// </summary>
        /// <remarks>
        /// Возможные значения:<br/>
        /// true — запрос токена в формате UUID (единый токен аутентификации);<br/>
        /// false — запрос токена в формате jwt (по умолчанию).
        /// </remarks>
        [JsonPropertyName("unitedToken")]
        public bool UnitedToken { get; set; }
    }
}
