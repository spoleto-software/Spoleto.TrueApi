namespace Spoleto.TrueApi
{
    public class TrueApiNkProviderOption
    {
        public string ApiKey { get; set; }

        public string ServiceUrl { get; set; }

        public TrueApiNkProviderOption(string apiKey, string serviceUrl)
        {
            ApiKey = apiKey;
            ServiceUrl = serviceUrl;
        }
    }
}
