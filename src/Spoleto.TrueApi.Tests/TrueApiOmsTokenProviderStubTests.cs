using System.Net;
using System.Text;
using System.Text.Json;
using Spoleto.TrueApi.Auth.Models.Options;
using Spoleto.TrueApi.Auth.Providers;

namespace Spoleto.TrueApi.Tests
{
    /// <summary>
    /// <see cref="TrueApiOmsTokenProvider"/> на подставном транспорте: подпись подменена, поэтому КриптоПро и стенд не нужны.
    /// </summary>
    public class TrueApiOmsTokenProviderStubTests
    {
        private const string ServiceUrl = "https://markirovka.sandbox.crptech.ru";

        private const string OmsConnection = "11b1abc1-f1ee-11db-1a11-f11ac11111e1";

        private static TrueApiOmsTokenProviderOption CreateSettings(string omsConnection = OmsConnection, string? inn = null) => new(ServiceUrl, "thumbprint", omsConnection, inn);

        private static HttpResponseMessage CreateResponse(HttpStatusCode statusCode, string content, string mediaType = "application/json")
            => new(statusCode) { Content = new StringContent(content, Encoding.UTF8, mediaType) };

        private static HttpResponseMessage CreateKeyResponse(int number) => CreateResponse(HttpStatusCode.OK, $$"""{"uuid":"uuid-{{number}}","data":"DATA{{number}}"}""");

        private static HttpResponseMessage CreateTokenResponse(int number) => CreateResponse(HttpStatusCode.OK, $$"""{"token":"oms-{{number}}"}""");

        private static string ToBase64(string value) => Convert.ToBase64String(Encoding.UTF8.GetBytes(value));

        [TestCase(null)]
        [TestCase("7777777777")]
        public async Task GetToken_SignsKeyAndRequestsOmsToken(string? inn)
        {
            // Arrange
            var handler = new StubHttpMessageHandler(CreateKeyResponse(1), CreateTokenResponse(1));
            using var tokenProvider = new FakeSigningOmsTokenProvider(handler);

            // Act
            var token = await tokenProvider.GetTokenAsync(CreateSettings(inn: inn));

            // Assert
            var body = JsonDocument.Parse(handler.Requests[1].Body!).RootElement;
            Assert.Multiple(() =>
            {
                Assert.That(token.Token, Is.EqualTo("oms-1"));
                Assert.That(handler.Requests.Select(x => $"{x.Method} {x.Uri}"), Is.EqualTo(new[]
                {
                    $"GET {ServiceUrl}/api/v3/true-api/auth/key",
                    $"POST {ServiceUrl}/api/v3/true-api/auth/simpleSignIn/{OmsConnection}"
                }));
                Assert.That(handler.Requests.Select(x => x.Authorization), Is.All.Null, "Токен запрашивается без токена.");
                Assert.That(tokenProvider.SignedData, Is.EqualTo(new[] { ToBase64("DATA1") }), "Подписываются данные ключа в base64.");
                Assert.That(body.GetProperty("uuid").GetString(), Is.EqualTo("uuid-1"));
                Assert.That(body.GetProperty("data").GetString(), Is.EqualTo("signed:" + ToBase64("DATA1")));
                Assert.That(body.TryGetProperty("inn", out var innProperty) ? innProperty.GetString() : null, Is.EqualTo(inn));
                Assert.That(body.TryGetProperty("unitedToken", out _), Is.False, "Признак единого токена True API в запрос токена СУЗ не входит.");
            });
        }

        [Test]
        public async Task GetToken_CachedUntilRefreshTime()
        {
            // Arrange
            var handler = new StubHttpMessageHandler(CreateKeyResponse(1), CreateTokenResponse(1), CreateKeyResponse(2), CreateTokenResponse(2));
            using var tokenProvider = new FakeSigningOmsTokenProvider(handler);

            // Act
            var first = await tokenProvider.GetTokenAsync(CreateSettings());
            tokenProvider.UtcNow += TimeSpan.FromHours(9);
            var cached = await tokenProvider.GetTokenAsync(CreateSettings());
            tokenProvider.UtcNow += TimeSpan.FromMinutes(50);
            var renewed = await tokenProvider.GetTokenAsync(CreateSettings());

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(first.Token, Is.EqualTo("oms-1"));
                Assert.That(cached.Token, Is.EqualTo("oms-1"), "Через 9 часов токен ещё действует.");
                Assert.That(renewed.Token, Is.EqualTo("oms-2"), "За 10 минут до истечения 10 часов токен обновляется.");
                Assert.That(handler.Requests, Has.Count.EqualTo(4));
            });
        }

        [Test]
        public async Task GetToken_OtherSettings_RequestsNewToken()
        {
            // Arrange
            var handler = new StubHttpMessageHandler(CreateKeyResponse(1), CreateTokenResponse(1), CreateKeyResponse(2), CreateTokenResponse(2));
            using var tokenProvider = new FakeSigningOmsTokenProvider(handler);

            // Act
            var first = await tokenProvider.GetTokenAsync(CreateSettings());
            var other = await tokenProvider.GetTokenAsync(CreateSettings("22b2abc2-f2ee-22db-2a22-f22ac22222e2"));

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(first.Token, Is.EqualTo("oms-1"));
                Assert.That(other.Token, Is.EqualTo("oms-2"));
                Assert.That(handler.Requests[3].Uri, Is.EqualTo(new Uri($"{ServiceUrl}/api/v3/true-api/auth/simpleSignIn/22b2abc2-f2ee-22db-2a22-f22ac22222e2")));
            });
        }

        [Test]
        public async Task SetTokenExpired_RequestsNewToken()
        {
            // Arrange
            var handler = new StubHttpMessageHandler(CreateKeyResponse(1), CreateTokenResponse(1), CreateKeyResponse(2), CreateTokenResponse(2));
            using var tokenProvider = new FakeSigningOmsTokenProvider(handler);
            await tokenProvider.GetTokenAsync(CreateSettings());

            // Act
            tokenProvider.SetTokenExpired();
            var token = await tokenProvider.GetTokenAsync(CreateSettings());

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(token.Token, Is.EqualTo("oms-2"));
                Assert.That(handler.Requests, Has.Count.EqualTo(4));
            });
        }

        [Test]
        public async Task GetToken_ServiceError_ThrowsServiceMessageAndKeepsNoToken()
        {
            // Arrange
            var handler = new StubHttpMessageHandler(
                CreateKeyResponse(1),
                CreateResponse(HttpStatusCode.BadRequest, """{"error_message":"Ошибка при проверке подписи"}"""),
                CreateKeyResponse(2),
                CreateTokenResponse(2));
            using var tokenProvider = new FakeSigningOmsTokenProvider(handler);

            // Act
            var ex = Assert.ThrowsAsync<HttpRequestException>(() => tokenProvider.GetTokenAsync(CreateSettings()));
            var token = await tokenProvider.GetTokenAsync(CreateSettings());

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(ex!.Message, Is.EqualTo("Ошибка при проверке подписи"));
                Assert.That(ex.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
                Assert.That(token.Token, Is.EqualTo("oms-2"), "После ошибки токен запрашивается заново.");
            });
        }

        /// <summary>
        /// Ответ старого адреса «auth/cert» на неизвестный omsConnection: описание идёт в текст исключения следом за кодом ошибки.
        /// </summary>
        private const string ConnectionNotFoundJson = """{"code":"003","error_message":"oms-connection-not-found","description":"Идентификатор соединения для указанного УОТ не найден"}""";

        [Test]
        public void GetToken_ServiceErrorWithDescription_ThrowsBoth()
        {
            // Arrange
            var handler = new StubHttpMessageHandler(CreateKeyResponse(1), CreateResponse(HttpStatusCode.BadRequest, ConnectionNotFoundJson));
            using var tokenProvider = new FakeSigningOmsTokenProvider(handler);

            // Act
            var ex = Assert.ThrowsAsync<HttpRequestException>(() => tokenProvider.GetTokenAsync(CreateSettings()));

            // Assert
            Assert.That(ex!.Message, Is.EqualTo("oms-connection-not-found: Идентификатор соединения для указанного УОТ не найден"));
        }

        [Test]
        public void GetToken_ErrorWithOkStatus_ThrowsServiceDescription()
        {
            // Arrange
            var handler = new StubHttpMessageHandler(CreateKeyResponse(1), CreateResponse(HttpStatusCode.OK, ConnectionNotFoundJson));
            using var tokenProvider = new FakeSigningOmsTokenProvider(handler);

            // Act
            var ex = Assert.ThrowsAsync<InvalidDataException>(() => tokenProvider.GetTokenAsync(CreateSettings()));

            // Assert
            Assert.That(ex!.Message, Is.EqualTo($"True API не вернул токен СУЗ для соединения {OmsConnection}: oms-connection-not-found: Идентификатор соединения для указанного УОТ не найден"));
        }

        [Test]
        public void GetToken_EmptyToken_Throws()
        {
            // Arrange
            var handler = new StubHttpMessageHandler(CreateKeyResponse(1), CreateResponse(HttpStatusCode.OK, "{}"));
            using var tokenProvider = new FakeSigningOmsTokenProvider(handler);

            // Act
            var ex = Assert.ThrowsAsync<InvalidDataException>(() => tokenProvider.GetTokenAsync(CreateSettings()));

            // Assert
            Assert.That(ex!.Message, Is.EqualTo($"True API не вернул токен СУЗ для соединения {OmsConnection}."));
        }

        [Test]
        public void GetToken_WithoutOmsConnection_ThrowsWithoutRequests()
        {
            // Arrange
            var handler = new StubHttpMessageHandler();
            using var tokenProvider = new FakeSigningOmsTokenProvider(handler);

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(() => tokenProvider.GetTokenAsync(CreateSettings("")));
            Assert.That(handler.Requests, Is.Empty);
        }

        private sealed record StubRequest(HttpMethod Method, Uri Uri, string? Authorization, string? Body);

        private sealed class StubHttpMessageHandler(params HttpResponseMessage[] responses) : HttpMessageHandler
        {
            private readonly Queue<HttpResponseMessage> _responses = new(responses);

            public List<StubRequest> Requests { get; } = [];

            protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                // Тело читаем здесь: после отправки клиент освобождает запрос вместе с телом.
                var body = request.Content == null ? null : await request.Content.ReadAsStringAsync(cancellationToken);
                Requests.Add(new StubRequest(request.Method, request.RequestUri!, request.Headers.Authorization?.ToString(), body));

                var response = _responses.Dequeue();
                response.RequestMessage = request;

                return response;
            }
        }

        /// <summary>
        /// Провайдер, который подписывает без КриптоПро (вместо подписи - «signed:» и данные) и живёт по своим часам.
        /// </summary>
        private sealed class FakeSigningOmsTokenProvider(HttpMessageHandler handler) : TrueApiOmsTokenProvider(new HttpClient(handler), true)
        {
            public List<string> SignedData { get; } = [];

            public DateTime UtcNow { get; set; } = new(2026, 11, 3, 9, 0, 0, DateTimeKind.Utc);

            protected override string SignAttached(string base64Data, string certificateThumbprint)
            {
                SignedData.Add(base64Data);

                return "signed:" + base64Data;
            }

            protected override DateTime GetUtcNow() => UtcNow;
        }
    }
}
