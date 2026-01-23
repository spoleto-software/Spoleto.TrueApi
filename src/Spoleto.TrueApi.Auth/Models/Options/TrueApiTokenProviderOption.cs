namespace Spoleto.TrueApi.Auth.Models.Options
{
    public record TrueApiTokenProviderOption(string ServiceUrl, string CertificateThumbprint, string? Inn = null)
    {
    }
}
