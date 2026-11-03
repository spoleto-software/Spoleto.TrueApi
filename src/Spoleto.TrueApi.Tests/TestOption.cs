namespace Spoleto.TrueApi.Tests
{
    public record TestOption
    {
        public string CertificateThumbprint { get; set; }

        public string FileId { get; set; }

        public string ServiceUrl { get; set; }

        public string Inn { get; set; }

        /// <summary>
        /// Идентификатор соединения СУЗ (omsConnection). Без него тест токена СУЗ пропускается.
        /// </summary>
        public string? OmsConnection { get; set; }
    }
}
