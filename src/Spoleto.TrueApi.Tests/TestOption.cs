namespace Spoleto.TrueApi.Tests
{
    public record TestOption
    {
        public string CertificateThumbprint { get; set; }

        public string FileId { get; set; }

        public string ServiceUrl { get; set; }

        public string Inn { get; set; }
    }
}
