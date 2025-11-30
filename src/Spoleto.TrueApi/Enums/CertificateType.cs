namespace Spoleto.TrueApi
{
    /// <summary>
    /// Виды документов обязательной сертификации
    /// </summary>
    public enum CertificateType
    {
        /// <summary>
        /// сертификат соответствия
        /// </summary>
        CONFORMITY_CERTIFICATE,

        /// <summary>
        /// декларация соответствия
        /// </summary>
        CONFORMITY_DECLARATION,

        /// <summary>
        /// сертификат или декларация соответствия
        /// </summary>
        CONFORMITY_CERTIFICATE_OR_DECLARATION
    }
}
