using System.Net;
using System.Text.Json;
using CIS.Cryptography;
using Spoleto.TrueApi.Auth.Models.Options;
using Spoleto.TrueApi.Auth.Providers;

namespace Spoleto.TrueApi.Tests
{
    /// <summary>
    /// Токен <see cref="TrueApiOmsTokenProvider"/> в методе СУЗ получения кодов по заказу
    /// GET «codes» с токеном в заголовке «clientToken».
    /// </summary>
    /// <remarks>
    /// Нужны сертификат и настройки <see cref="TestOption"/>: <see cref="TestOption.OmsConnection"/>, <see cref="TestOption.OmsUrl"/> и <see cref="TestOption.OmsId"/>.
    /// </remarks>
    public class TrueApiOmsTokenGetCisInfoTests
    {
        /// <summary>
        /// Проверка учетных данных УОТ не пройдена: так СУЗ отвечает на выдуманный токен.
        /// </summary>
        private const int CredentialsCheckFailedErrorCode = 1090;

        /// <summary>
        /// Маркер безопасности не найден.
        /// </summary>
        private const int TokenNotFoundErrorCode = 1100;

        /// <summary>
        /// Время действия маркера безопасности истекло.
        /// </summary>
        private const int TokenExpiredErrorCode = 1160;

        private readonly TestOption _testOption = ConfigurationHelper.GetTestOption();

        private readonly HttpClient _httpClient = new();

        // Один провайдер на все тесты: каждый новый токен - это подпись в КриптоПро.
        private readonly TrueApiOmsTokenProvider _tokenProvider = new();

        [SetUp]
        public void SetUp()
        {
            if (string.IsNullOrEmpty(_testOption.OmsConnection) || string.IsNullOrEmpty(_testOption.OmsUrl) || string.IsNullOrEmpty(_testOption.OmsId))
            {
                Assert.Ignore("Не заданы TestOption.OmsConnection, OmsUrl или OmsId: идентификатор соединения, адрес API СУЗ и идентификатор СУЗ.");
            }
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _tokenProvider.Dispose();
            _httpClient.Dispose();
        }

        /// <summary>
        /// Коды по заказу, которого нет: стенд не меняется, а СУЗ отвечает ошибкой заказа, если токен принят, и ошибкой токена, если нет.
        /// </summary>
        /// <remarks>
        /// Сначала тот же запрос уходит с выдуманным токеном. СУЗ должна его отклонить, иначе ошибка заказа ничего не говорит о токене:
        /// запрос остановился раньше проверки токена.
        /// </remarks>
        [Test]
        public async Task GetCisInfo_TokenAccepted()
        {
            // Arrange
            var uri = CreateCodesUri(Guid.NewGuid().ToString(), 0, 1);

            // Act
            var control = await SendAsync(uri, Guid.NewGuid().ToString());
            if (!control.IsTokenRejected)
            {
                Assert.Inconclusive($"СУЗ не отклонила выдуманный токен, поэтому ответ ничего не скажет о токене из True API: {control}");
            }

            var response = await InvokeAsync(uri);

            // Assert
            Assert.That(response.IsTokenRejected, Is.False, $"СУЗ отклонила токен из True API: {response}");
        }

        /// <summary>
        /// Коды из заказа <see cref="TestOption.OmsOrderId"/>, в котором они уже готовы.
        /// </summary>
        [Test]
        [Explicit("Забирает код из буфера заказа на стенде СУЗ.")]
        public async Task GetCisInfoTest()
        {
            // Arrange
            if (string.IsNullOrEmpty(_testOption.OmsOrderId) || string.IsNullOrEmpty(_testOption.OmsOrderGtin))
            {
                Assert.Ignore("Не заданы TestOption.OmsOrderId и OmsOrderGtin: заказ на стенде СУЗ с кодами в буфере и GTIN товара из него.");
            }

            var uri = CreateCodesUri(_testOption.OmsOrderId!, long.Parse(_testOption.OmsOrderGtin!), 1);

            // Act
            var response = await InvokeAsync(uri);

            // Assert
            Assert.That(response.IsSuccessStatusCode, Is.True, response.ToString());
            using var document = JsonDocument.Parse(response.Content);
            Assert.That(document.RootElement.GetProperty("codes").GetArrayLength(), Is.EqualTo(1));
        }

        private string GetCertificateThumbprint()
            => CryptographyHelper.GetCertificates(true, true).Single(x => x.Certificate.Thumbprint.ToLower() == _testOption.CertificateThumbprint).Certificate.Thumbprint;

        /// <summary>
        /// Адрес метода, как в OmsProvider: «codes» с omsId и полями CisOrderOmsSearchInfoModel, GTIN - 14 цифр с ведущими нулями.
        /// </summary>
        private Uri CreateCodesUri(string orderId, long gtin, int quantity)
            => new($"{_testOption.OmsUrl!.TrimEnd('/')}/codes?omsId={Uri.EscapeDataString(_testOption.OmsId!)}&orderId={Uri.EscapeDataString(orderId)}&gtin={gtin:D14}&quantity={quantity}");

        private async Task<string> GetTokenAsync()
        {
            var settings = new TrueApiOmsTokenProviderOption(_testOption.ServiceUrl, GetCertificateThumbprint(), _testOption.OmsConnection!);
            var token = await _tokenProvider.GetTokenAsync(settings);

            return token.Token!;
        }

        /// <summary>
        /// Запрос с токеном из True API. Как в OmsProvider, на ошибку 1160 запрос повторяется с новым токеном, но один раз.
        /// </summary>
        /// <remarks>
        /// Провайдер хранит токен у себя, поэтому новый токен - только после <see cref="ITrueApiOmsTokenProvider.SetTokenExpired"/>.
        /// </remarks>
        private async Task<OmsResponse> InvokeAsync(Uri uri)
        {
            var response = await SendAsync(uri, await GetTokenAsync());
            if (response.HasErrorCode(TokenExpiredErrorCode))
            {
                _tokenProvider.SetTokenExpired();
                response = await SendAsync(uri, await GetTokenAsync());
            }

            return response;
        }

        /// <summary>
        /// GET, как в OmsProvider: токен - в заголовке «clientToken». Тела у GET нет, поэтому нет и подписи «X-Signature».
        /// </summary>
        private async Task<OmsResponse> SendAsync(Uri uri, string token)
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, uri);
            request.Headers.Accept.ParseAdd("application/json");
            request.Headers.Add("clientToken", token);

            using var response = await _httpClient.SendAsync(request);
            var omsResponse = new OmsResponse(response.StatusCode, await response.Content.ReadAsStringAsync());

            TestContext.Out.WriteLine($"GET {uri.PathAndQuery}: {omsResponse}");
            TestContext.Out.WriteLine();

            return omsResponse;
        }

        /// <summary>
        /// Ответ СУЗ как есть.
        /// </summary>
        private sealed record OmsResponse(HttpStatusCode StatusCode, string Content)
        {
            public bool IsSuccessStatusCode => (int)StatusCode is >= 200 and <= 299;

            /// <summary>
            /// СУЗ отклонила токен: ответ 401 или 403, или ошибка токена в «globalErrors».
            /// </summary>
            public bool IsTokenRejected => StatusCode is HttpStatusCode.Unauthorized or HttpStatusCode.Forbidden
                || HasErrorCode(CredentialsCheckFailedErrorCode) || HasErrorCode(TokenNotFoundErrorCode) || HasErrorCode(TokenExpiredErrorCode);

            /// <summary>
            /// Ошибка с кодом в «globalErrors» - там, где её ищет OmsProvider.
            /// </summary>
            public bool HasErrorCode(int errorCode)
            {
                try
                {
                    using var document = JsonDocument.Parse(Content);
                    var root = document.RootElement;

                    return root.ValueKind == JsonValueKind.Object
                        && root.TryGetProperty("globalErrors", out var errors)
                        && errors.ValueKind == JsonValueKind.Array
                        && errors.EnumerateArray().Any(x => x.ValueKind == JsonValueKind.Object
                            && x.TryGetProperty("errorCode", out var code)
                            && code.ValueKind == JsonValueKind.Number
                            && code.GetInt32() == errorCode);
                }
                catch (JsonException)
                {
                    return false;
                }
            }

            public override string ToString() => $"{(int)StatusCode} {StatusCode}, {Content}";
        }
    }
}
