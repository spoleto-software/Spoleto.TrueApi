using Spoleto.TrueApi.Auth.Models;
using Spoleto.TrueApi.Auth.Models.Options;

namespace Spoleto.TrueApi.Auth.Providers
{
    /// <summary>
    /// Токен аутентификации в СУЗ (Станции управления заказами).
    /// </summary>
    public interface ITrueApiOmsTokenProvider
    {
        OmsTokenModel GetToken(TrueApiOmsTokenProviderOption settings)
            => GetTokenAsync(settings).GetAwaiter().GetResult();//https://docs.microsoft.com/en-us/archive/msdn-magazine/2015/july/async-programming-brownfield-async-development#the-blocking-hack

        Task<OmsTokenModel> GetTokenAsync(TrueApiOmsTokenProviderOption settings);

        /// <summary>
        /// Сбросить токен, например, когда СУЗ ответила 401: следующий вызов <see cref="GetTokenAsync"/> запросит новый.
        /// </summary>
        void SetTokenExpired();
    }
}
