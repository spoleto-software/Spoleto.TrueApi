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

        /// <summary>
        /// Адрес API СУЗ с версией
        /// Без него тесты методов СУЗ пропускаются.
        /// </summary>
        public string? OmsUrl { get; set; }

        /// <summary>
        /// Идентификатор СУЗ (omsId) из настроек СУЗ. Без него тесты методов СУЗ пропускаются.
        /// </summary>
        public string? OmsId { get; set; }

        /// <summary>
        /// Заказ на стенде СУЗ с кодами в буфере. Без него тест получения кодов из заказа пропускается.
        /// </summary>
        public string? OmsOrderId { get; set; }

        /// <summary>
        /// GTIN товара из заказа <see cref="OmsOrderId"/>.
        /// </summary>
        public string? OmsOrderGtin { get; set; }
    }
}
