using System.Text;
using System.Xml.Linq;
using CIS.Cryptography;
using Spoleto.TrueApi.Edo.Models;
using Spoleto.TrueApi.Edo.Models.Options;
using Spoleto.TrueApi.Edo.Providers;

namespace Spoleto.TrueApi.Tests
{
    /// <summary>
    /// Вызовы стенда True API: нужны сертификат и настройки <see cref="TestOption"/>.
    /// </summary>
    public class TrueApiEdoProviderTests
    {
        private readonly TestOption _testOption = ConfigurationHelper.GetTestOption();

        static TrueApiEdoProviderTests()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);//support for "windows-1251"
        }

        private TrueApiEdoProviderOption CreateSettings()
        {
            var certificateList = CryptographyHelper.GetCertificates(true, true);
            var cert = certificateList.Single(x => x.Certificate.Thumbprint.ToLower() == _testOption.CertificateThumbprint);

            return new TrueApiEdoProviderOption
            {
                CertificateThumbprint = cert.Certificate.Thumbprint,
                ServiceUrl = _testOption.ServiceUrl,
                Inn = _testOption.Inn
            };
        }

        /// <summary>
        /// Договоры с оператором ГИС МТ хранятся в PDF, поэтому берём универсальный документ: УПД, УКД или квитанцию, они всегда XML.
        /// </summary>
        private static string FindUniversalDocumentId(EdoDocumentCollection list, string direction)
        {
            var documents = list.Items.SelectMany(x => x.Documents).ToList();
            foreach (var document in documents)
            {
                TestContext.Out.WriteLine($"{(int)document.Type} {document.Type} {document.Number} {document.Id}");
            }

            var documentId = documents.FirstOrDefault(x => Enum.IsDefined(x.Type))?.Id;
            if (documentId == null)
            {
                Assert.Ignore($"Среди {documents.Count} {direction} документов на стенде нет УПД, УКД и квитанций.");
            }

            return documentId!;
        }

        /// <summary>
        /// Кодировку берём из заголовка XML (у УПД windows-1251), а не угадываем: так читаются и УПД, и квитанции.
        /// Корень всех форматов ФНС - «Файл», поэтому кириллица в нём проверяет, что байты пришли без порчи.
        /// </summary>
        private static void AssertFnsXml(byte[] content)
        {
            var xml = XDocument.Load(new MemoryStream(content));
            TestContext.Out.WriteLine(xml.Root?.Attribute("ИдФайл")?.Value);
            Assert.That(xml.Root?.Name.LocalName, Is.EqualTo("Файл"));
        }

        [Test]
        public async Task GetIncomingDocumentListTest()
        {
            // Arrange
            var settings = CreateSettings();
            using var edoProvider = new TrueApiEdoProvider();

            // Act
            var data = await edoProvider.GetIncomingDocumentListAsync(settings, new EdoDocumentSearchModel { Limit = 10 });

            // Assert
            Assert.That(data, Is.Not.Null);
            Assert.That(data.Items, Has.Count.LessThanOrEqualTo(10));
        }

        [Test]
        public async Task GetIncomingDocumentTest()
        {
            // Arrange
            var settings = CreateSettings();
            using var edoProvider = new TrueApiEdoProvider();

            var list = await edoProvider.GetIncomingDocumentListAsync(settings, new EdoDocumentSearchModel { Limit = 50 });
            var documentId = FindUniversalDocumentId(list, "входящих");

            // Act
            var content = await edoProvider.GetIncomingDocumentAsync(settings, documentId);

            // Assert
            AssertFnsXml(content);
        }

        [Test]
        public async Task GetOutgoingDocumentListTest()
        {
            // Arrange
            var settings = CreateSettings();
            using var edoProvider = new TrueApiEdoProvider();

            // Act
            var data = await edoProvider.GetOutgoingDocumentListAsync(settings, new EdoDocumentSearchModel { Limit = 10 });

            // Assert
            Assert.That(data, Is.Not.Null);
            Assert.That(data.Items, Has.Count.LessThanOrEqualTo(10));
        }

        [Test]
        public async Task GetOutgoingDocumentTest()
        {
            // Arrange
            var settings = CreateSettings();
            using var edoProvider = new TrueApiEdoProvider();

            var list = await edoProvider.GetOutgoingDocumentListAsync(settings, new EdoDocumentSearchModel { Limit = 50 });
            var documentId = FindUniversalDocumentId(list, "исходящих");

            // Act
            var content = await edoProvider.GetOutgoingDocumentAsync(settings, documentId);

            // Assert
            AssertFnsXml(content);
        }
    }
}
