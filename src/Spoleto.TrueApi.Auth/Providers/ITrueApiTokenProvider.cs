using Spoleto.TrueApi.Auth.Models;
using Spoleto.TrueApi.Auth.Models.Options;

namespace Spoleto.TrueApi.Auth.Providers
{
    public interface ITrueApiTokenProvider
    {
        UnitedTokenModel GetToken(TrueApiTokenProviderOption settings)
            => GetTokenAsync(settings).GetAwaiter().GetResult();//https://docs.microsoft.com/en-us/archive/msdn-magazine/2015/july/async-programming-brownfield-async-development#the-blocking-hack

        Task<UnitedTokenModel> GetTokenAsync(TrueApiTokenProviderOption settings);

        void SetTokenExpired();
    }
}