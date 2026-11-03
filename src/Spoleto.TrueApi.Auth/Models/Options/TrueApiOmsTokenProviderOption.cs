namespace Spoleto.TrueApi.Auth.Models.Options
{
    /// <summary>
    /// Настройки получения токена СУЗ.
    /// </summary>
    /// <param name="ServiceUrl">Адрес True API, например, https://markirovka.crpt.ru.</param>
    /// <param name="CertificateThumbprint">Отпечаток сертификата, которым подписываются данные ключа.</param>
    /// <param name="OmsConnection">Идентификатор соединения (внешнего подключения), выданный при регистрации установки интеграционного решения в СУЗ.</param>
    /// <param name="Inn">ИНН организации, если пользователь работает по МЧД от нескольких организаций. Иначе не указывается.</param>
    public record TrueApiOmsTokenProviderOption(string ServiceUrl, string CertificateThumbprint, string OmsConnection, string? Inn = null)
    {
    }
}
