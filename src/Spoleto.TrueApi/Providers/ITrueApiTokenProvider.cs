namespace Spoleto.TrueApi
{
    public interface ITrueApiTokenProvider
    {
        TokenModel GetToken(TrueApiProviderOption settings)
            => GetTokenAsync(settings).GetAwaiter().GetResult();//https://docs.microsoft.com/en-us/archive/msdn-magazine/2015/july/async-programming-brownfield-async-development#the-blocking-hack

        Task<TokenModel> GetTokenAsync(TrueApiProviderOption settings);
    }
}