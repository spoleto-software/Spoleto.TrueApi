using CIS.Cryptography;
using Spoleto.TrueApi.Auth.Models.Options;
using Spoleto.TrueApi.Auth.Providers;

namespace Spoleto.TrueApi.Tests
{
    /// <summary>
    /// Токен СУЗ на стенде True API: нужны сертификат и настройки <see cref="TestOption"/>, в том числе <see cref="TestOption.OmsConnection"/>.
    /// </summary>
    public class TrueApiOmsTokenProviderTests
    {
        private readonly TestOption _testOption = ConfigurationHelper.GetTestOption();

        [Test]
        public async Task GetTokenTest()
        {
            // Arrange
            if (string.IsNullOrEmpty(_testOption.OmsConnection))
            {
                Assert.Ignore("Не задан TestOption.OmsConnection: идентификатор соединения выдаётся при регистрации установки интеграционного решения в СУЗ.");
            }

            var cert = CryptographyHelper.GetCertificates(true, true).Single(x => x.Certificate.Thumbprint.ToLower() == _testOption.CertificateThumbprint);
            var settings = new TrueApiOmsTokenProviderOption(_testOption.ServiceUrl, cert.Certificate.Thumbprint, _testOption.OmsConnection);

            using var tokenProvider = new TrueApiOmsTokenProvider();

            // Act
            var token = await tokenProvider.GetTokenAsync(settings);
            var cached = await tokenProvider.GetTokenAsync(settings);

            // Assert
            TestContext.Out.WriteLine(token.Token);
            Assert.Multiple(() =>
            {
                Assert.That(token.Token, Is.Not.Empty);
                Assert.That(cached, Is.SameAs(token), "Второй вызов берёт токен из кэша, без новой подписи.");
            });
        }
    }
}
