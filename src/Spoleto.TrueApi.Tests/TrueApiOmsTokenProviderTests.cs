using System.Text;
using System.Text.Json;
using CIS.Cryptography;
using Spoleto.TrueApi.Auth.Models.Options;
using Spoleto.TrueApi.Auth.Providers;

namespace Spoleto.TrueApi.Tests
{
    /// <summary>
    /// Токен СУЗ на стенде True API: нужны сертификат и настройки <see cref="TestOption"/>, в том числе <see cref="TestOption.OmsConnection"/>.
    /// </summary>
    public class TrueApiOmsTokenProviderTests
    {
        private readonly TestOption _testOption = ConfigurationHelper.GetTestOption();

        private string GetOmsConnection()
        {
            if (string.IsNullOrEmpty(_testOption.OmsConnection))
            {
                Assert.Ignore("Не задан TestOption.OmsConnection: идентификатор соединения выдаётся при регистрации установки интеграционного решения в СУЗ.");
            }

            return _testOption.OmsConnection!;
        }

        private string GetCertificateThumbprint()
            => CryptographyHelper.GetCertificates(true, true).Single(x => x.Certificate.Thumbprint.ToLower() == _testOption.CertificateThumbprint).Certificate.Thumbprint;

        [Test]
        public async Task GetTokenTest()
        {
            // Arrange
            var settings = new TrueApiOmsTokenProviderOption(_testOption.ServiceUrl, GetCertificateThumbprint(), GetOmsConnection());

            using var tokenProvider = new TrueApiOmsTokenProvider();

            // Act
            var token = await tokenProvider.GetTokenAsync(settings);
            var cached = await tokenProvider.GetTokenAsync(settings);

            // Assert
            TestContext.Out.WriteLine(token.Token);
            Assert.Multiple(() =>
            {
                Assert.That(token.Token, Is.Not.Empty);
                Assert.That(cached, Is.SameAs(token), "Второй вызов берёт токен из кэша, без новой подписи.");
            });
        }

        /// <summary>
        /// Диагностика ошибки токена СУЗ. Тот же сертификат и omsConnection - через новый метод True API без ИНН и с ИНН из настроек,
        /// и через старый адрес «auth/cert», которым сейчас пользуется приложение.
        /// </summary>
        /// <remarks>
        /// Печатает код и тело каждого ответа как есть: в тексте исключения провайдера есть только «error_message», а «code» и «description» теряются.
        /// Сам токен не печатается.
        /// </remarks>
        [Test]
        [Explicit("Диагностика: три подписи в КриптоПро и три запроса токена СУЗ.")]
        public async Task GetToken_Diagnostics()
        {
            var omsConnection = Uri.EscapeDataString(GetOmsConnection());
            var thumbprint = GetCertificateThumbprint();
            using var httpClient = new HttpClient();

            await RequestTokenAsync(httpClient, thumbprint, "/api/v3/true-api/auth/key", $"/api/v3/true-api/auth/simpleSignIn/{omsConnection}", null);
            await RequestTokenAsync(httpClient, thumbprint, "/api/v3/true-api/auth/key", $"/api/v3/true-api/auth/simpleSignIn/{omsConnection}", _testOption.Inn);
            await RequestTokenAsync(httpClient, thumbprint, "/api/v3/auth/cert/key", $"/api/v3/auth/cert/{omsConnection}", null);
        }

        private async Task RequestTokenAsync(HttpClient httpClient, string thumbprint, string keyPath, string tokenPath, string? inn)
        {
            var serviceUrl = _testOption.ServiceUrl.TrimEnd('/');

            var key = JsonDocument.Parse(await httpClient.GetStringAsync(serviceUrl + keyPath)).RootElement;
            var data = Convert.ToBase64String(Encoding.UTF8.GetBytes(key.GetProperty("data").GetString()!));

            var body = new Dictionary<string, string?>
            {
                ["uuid"] = key.GetProperty("uuid").GetString(),
                ["data"] = CryptographyHelper.SignBase64Data(data, thumbprint: thumbprint)
            };
            if (!string.IsNullOrEmpty(inn))
            {
                body["inn"] = inn;
            }

            using var response = await httpClient.PostAsync(serviceUrl + tokenPath, new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json"));
            var content = await response.Content.ReadAsStringAsync();

            TestContext.Out.WriteLine($"POST {tokenPath}{(string.IsNullOrEmpty(inn) ? "" : $", inn {inn}")}: {(int)response.StatusCode} {response.ReasonPhrase}, {response.Content.Headers.ContentType}");
            TestContext.Out.WriteLine(response.IsSuccessStatusCode ? $"токен получен, ответ {content.Length} символов" : content);
            TestContext.Out.WriteLine();
        }
    }
}
