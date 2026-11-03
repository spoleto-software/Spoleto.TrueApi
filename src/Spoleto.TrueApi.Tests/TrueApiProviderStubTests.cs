using System.IO.Compression;
using System.Net;
using System.Text;
using Spoleto.TrueApi.Auth.Models;
using Spoleto.TrueApi.Auth.Models.Options;
using Spoleto.TrueApi.Auth.Providers;

namespace Spoleto.TrueApi.Tests
{
    /// <summary>
    /// <see cref="TrueApiProvider"/>, <see cref="TrueApiNkProvider"/> и <see cref="TrueApiTokenProvider"/> на подставном транспорте: стенд не нужен.
    /// </summary>
    public class TrueApiProviderStubTests
    {
        private const string ServiceUrl = "https://markirovka.sandbox.crptech.ru";

        private static TrueApiProviderOption CreateSettings() => new() { ServiceUrl = ServiceUrl, CertificateThumbprint = "thumbprint", Inn = "7777777777" };

        private static HttpResponseMessage CreateResponse(HttpStatusCode statusCode, string content, string mediaType = "application/json")
            => new(statusCode) { Content = new StringContent(content, Encoding.UTF8, mediaType) };

        private static HttpResponseMessage CreateZipResponse(string json)
        {
            using var archiveStream = new MemoryStream();
            using (var archive = new ZipArchive(archiveStream, ZipArchiveMode.Create, true))
            {
                using var entryStream = archive.CreateEntry("result.json").Open();
                entryStream.Write(Encoding.UTF8.GetBytes(json));
            }

            return new HttpResponseMessage(HttpStatusCode.OK) { Content = new ByteArrayContent(archiveStream.ToArray()) };
        }

        [Test]
        public async Task Unauthorized_RefreshesTokenOnce()
        {
            // Arrange
            var handler = new StubHttpMessageHandler(
                CreateResponse(HttpStatusCode.Unauthorized, """{"error_message":"unauthorized"}"""),
                CreateResponse(HttpStatusCode.OK, "{}"));
            var tokenProvider = new FakeTokenProvider();
            var trueApiProvider = new TrueApiProvider(new HttpClient(handler), true, tokenProvider);

            // Act
            var result = await trueApiProvider.GetProcessingResultAsync(CreateSettings(), "ON_NSCHFDOPPR_1");

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(tokenProvider.ExpiredCount, Is.EqualTo(1));
                Assert.That(handler.Requests.Select(x => x.Authorization), Is.EqualTo(new[] { "Bearer uuid-1", "Bearer uuid-2" }));
                Assert.That(handler.Requests.Select(x => x.Accept), Is.All.EqualTo("application/json"));
            });
        }

        [Test]
        public void Unauthorized_AfterRefresh_ThrowsWithoutLoop()
        {
            // Arrange
            // Третий ответ ловит цикл: повторный «unauthorized» раньше повторялся без предела, и третий запрос получил бы успех.
            var handler = new StubHttpMessageHandler(
                CreateResponse(HttpStatusCode.Unauthorized, """{"error_message":"unauthorized"}"""),
                CreateResponse(HttpStatusCode.Unauthorized, """{"error_message":"unauthorized"}"""),
                CreateResponse(HttpStatusCode.OK, "{}"));
            var tokenProvider = new FakeTokenProvider();
            var trueApiProvider = new TrueApiProvider(new HttpClient(handler), true, tokenProvider);

            // Act
            var ex = Assert.ThrowsAsync<HttpRequestException>(() => trueApiProvider.GetProcessingResultAsync(CreateSettings(), "ON_NSCHFDOPPR_1"));

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(ex!.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
                Assert.That(ex.Message, Is.EqualTo("unauthorized"));
                Assert.That(handler.Requests, Has.Count.EqualTo(2));
                Assert.That(tokenProvider.ExpiredCount, Is.EqualTo(1));
            });
        }

        [Test]
        public void Error_UsesServiceMessage()
        {
            // Arrange
            var handler = new StubHttpMessageHandler(CreateResponse(HttpStatusCode.BadRequest, """{"code":"400","error_message":"Неверный идентификатор файла"}"""));
            var trueApiProvider = new TrueApiProvider(new HttpClient(handler), true, new FakeTokenProvider());

            // Act
            var ex = Assert.ThrowsAsync<HttpRequestException>(() => trueApiProvider.GetProcessingResultAsync(CreateSettings(), "file"));

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(ex!.Message, Is.EqualTo("Неверный идентификатор файла"));
                Assert.That(ex.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
                Assert.That(ex.Data["RequestUri"], Is.EqualTo(new Uri($"{ServiceUrl}/api/v3/true-api/documents/edo/tpr/ud?fileId=file")));
            });
        }

        /// <summary>
        /// Повтор после 401 читает ответ так же, как первая попытка: раньше он терял признак архива и разбирал ZIP как JSON.
        /// </summary>
        [Test]
        public async Task ZipResponse_AfterUnauthorized_ReadsArchive()
        {
            // Arrange
            var handler = new StubHttpMessageHandler(CreateResponse(HttpStatusCode.Unauthorized, """{"error_message":"unauthorized"}"""), CreateZipResponse("[]"));
            var trueApiProvider = new TrueApiProvider(new HttpClient(handler), true, new FakeTokenProvider());

            // Act
#pragma warning disable CS0618 // Метод устарел, но архив в ответе есть только у него.
            var result = await trueApiProvider.GetCisFullInfoListAsync(CreateSettings(), "order");
#pragma warning restore CS0618

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Empty);
                Assert.That(handler.Requests, Has.Count.EqualTo(2));
            });
        }

        /// <summary>
        /// Ключ API Национального каталога - в строке запроса, поэтому в исключении адрес без неё.
        /// </summary>
        [Test]
        public void NkError_RequestUriWithoutApiKey()
        {
            // Arrange
            var handler = new StubHttpMessageHandler(CreateResponse(HttpStatusCode.Forbidden, """{"error_message":"Неверный ключ API"}"""));
            using var nkProvider = new TrueApiNkProvider(new HttpClient(handler), true);

            // Act
            var ex = Assert.ThrowsAsync<HttpRequestException>(() => nkProvider.GetBrandsAsync(new TrueApiNkProviderOption("secret-key", "https://nk.test/api")));

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(handler.Requests.Single().Uri.Query, Is.EqualTo("?apikey=secret-key"));
                Assert.That(handler.Requests.Single().Authorization, Is.Null);
                Assert.That(ex!.Message, Is.EqualTo("Неверный ключ API"));
                Assert.That(ex.StatusCode, Is.EqualTo(HttpStatusCode.Forbidden));
                Assert.That(ex.Data["RequestUri"], Is.EqualTo(new Uri("https://nk.test/api/brands")));
            });
        }

        /// <summary>
        /// Ошибка получения ключа: до подписи в КриптоПро дело не доходит, поэтому тест работает и без неё.
        /// </summary>
        [Test]
        public void TokenProvider_KeyError_ThrowsServiceMessage()
        {
            // Arrange
            var handler = new StubHttpMessageHandler(CreateResponse(HttpStatusCode.ServiceUnavailable, "Сервис недоступен", "text/plain"));
            var tokenProvider = new TrueApiTokenProvider(new HttpClient(handler), true);

            // Act
            var ex = Assert.ThrowsAsync<HttpRequestException>(() => tokenProvider.GetTokenAsync(new TrueApiTokenProviderOption(ServiceUrl, "thumbprint")));

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(ex!.Message, Is.EqualTo("Сервис недоступен"));
                Assert.That(ex.StatusCode, Is.EqualTo(HttpStatusCode.ServiceUnavailable));
                Assert.That(handler.Requests.Single().Uri, Is.EqualTo(new Uri($"{ServiceUrl}/api/v3/true-api/auth/key")));
                Assert.That(handler.Requests.Single().Authorization, Is.Null);
            });
        }

        private sealed record StubRequest(HttpMethod Method, Uri Uri, string? Authorization, string Accept);

        private sealed class StubHttpMessageHandler(params HttpResponseMessage[] responses) : HttpMessageHandler
        {
            private readonly Queue<HttpResponseMessage> _responses = new(responses);

            public List<StubRequest> Requests { get; } = [];

            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                Requests.Add(new StubRequest(request.Method, request.RequestUri!, request.Headers.Authorization?.ToString(), request.Headers.Accept.ToString()));

                var response = _responses.Dequeue();
                response.RequestMessage = request;

                return Task.FromResult(response);
            }
        }

        private sealed class FakeTokenProvider : ITrueApiTokenProvider
        {
            private int _issuedCount;

            public int ExpiredCount { get; private set; }

            public Task<UnitedTokenModel> GetTokenAsync(TrueApiTokenProviderOption settings)
            {
                _issuedCount++;

                return Task.FromResult(new UnitedTokenModel { UuidToken = $"uuid-{_issuedCount}", Token = "jwt" });
            }

            public void SetTokenExpired() => ExpiredCount++;
        }
    }
}
