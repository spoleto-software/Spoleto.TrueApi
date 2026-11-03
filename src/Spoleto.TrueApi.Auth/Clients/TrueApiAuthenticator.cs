using System.Net;
using System.Net.Http.Headers;
using Spoleto.RestClient;
using Spoleto.RestClient.Authentication;
using Spoleto.TrueApi.Auth.Models.Options;
using Spoleto.TrueApi.Auth.Providers;

namespace Spoleto.TrueApi.Auth.Clients
{
    /// <summary>
    /// UUID-токен True API для запросов <see cref="TrueApiRestClient"/>: токен берётся у <see cref="ITrueApiTokenProvider"/> по настройкам вызова.
    /// </summary>
    /// <remarks>
    /// Токен хранит провайдер токенов, а аутентификатор только передаёт его в запрос. На 401 клиент сбрасывает токен
    /// (<see cref="SetExpired"/>) и повторяет запрос с новым один раз, повторный 401 становится исключением.
    /// </remarks>
    public sealed class TrueApiAuthenticator : IDynamicAuthenticator
    {
        private readonly ITrueApiTokenProvider _tokenProvider;
        private readonly TrueApiTokenProviderOption _settings;

        /// <param name="tokenProvider">Провайдер токенов.</param>
        /// <param name="settings">Настройки, по которым запрашивается токен.</param>
        public TrueApiAuthenticator(ITrueApiTokenProvider tokenProvider, TrueApiTokenProviderOption settings)
        {
            _tokenProvider = tokenProvider ?? throw new ArgumentNullException(nameof(tokenProvider));
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        /// <inheritdoc />
        public async Task Authenticate(IRestClient client, HttpRequestMessage request)
        {
            var token = await _tokenProvider.GetTokenAsync(_settings).ConfigureAwait(false);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.UuidToken);
        }

        /// <inheritdoc />
        public Task<bool> IsExpired(HttpResponseMessage response) => Task.FromResult(response.StatusCode == HttpStatusCode.Unauthorized);

        /// <inheritdoc />
        public void SetExpired() => _tokenProvider.SetTokenExpired();

        /// <summary>
        /// Токен остаётся у провайдера токенов: освобождать нечего.
        /// </summary>
        public void Dispose()
        {
        }
    }
}
