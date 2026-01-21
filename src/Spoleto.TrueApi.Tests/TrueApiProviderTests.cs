using CIS.Cryptography;

namespace Spoleto.TrueApi.Tests
{
    public class Tests
    {
        private readonly TestOption _testOption = ConfigurationHelper.GetTestOption();


        [Test]
        public async Task GetDocumentByIdTest()
        {
            // Arrange
            var certificateList = CryptographyHelper.GetCertificates(true, true);
            var cert = certificateList.Single(x => x.Certificate.Thumbprint.ToLower() == _testOption.CertificateThumbprint);
            var settings = new TrueApiProviderOption
            {
                CertificateThumbprint = cert.Certificate.Thumbprint,
                ServiceUrl = _testOption.ServiceUrl
            };

            var trueApiProvider = new TrueApiProvider();

            // Act
            var data = await trueApiProvider.GetDocumentByIdAsync(settings, null, _testOption.FileId);

            // Assert
            Assert.That(data, Is.Not.Null);
        }
    }
}