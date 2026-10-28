using System.Text;
using System.Xml.Linq;
using CIS.Cryptography;
using Spoleto.TrueApi.Edo.Enums;
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

        private static string FindDocumentId(EdoDocumentCollection list, string direction, Func<EdoDocumentType, bool> isSuitable, string suitableTypes)
        {
            var documents = list.Items.SelectMany(x => x.Documents).ToList();
            foreach (var document in documents)
            {
                TestContext.Out.WriteLine($"{(int)document.Type} {document.Type} {document.Number} {document.Id}");
            }

            var documentId = documents.FirstOrDefault(x => isSuitable(x.Type))?.Id;
            if (documentId == null)
            {
                Assert.Ignore($"Среди {documents.Count} {direction} документов на стенде нет {suitableTypes}.");
            }

            return documentId!;
        }

        /// <summary>
        /// Договоры с оператором ГИС МТ хранятся в PDF, поэтому берём универсальный документ: УПД, УКД или квитанцию, они всегда XML.
        /// </summary>
        private static string FindUniversalDocumentId(EdoDocumentCollection list, string direction)
            => FindDocumentId(list, direction, x => Enum.IsDefined(x), "УПД, УКД и квитанций");

        /// <summary>
        /// ZIP-архив с подписями есть только у УПД и УКД. Берём УПД или УПДи: в их архиве информация продавца находится и без имени архива.
        /// </summary>
        private static string FindUpdDocumentId(EdoDocumentCollection list, string direction)
            => FindDocumentId(list, direction,
                x => x is >= EdoDocumentType.UPDWithDOPWaybill and <= EdoDocumentType.UPD970WithSCHFDOPBuyerTitleInvoiceWaybill
                    or >= EdoDocumentType.UPDiWithDOPWaybillCorrected and <= EdoDocumentType.UPDi970WithSCHFDOPBuyerTitleInvoiceCorrectedWaybill,
                "УПД и УПДи");

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

        /// <summary>
        /// Имя файла в архиве повторяет ИдФайл. Подпись - CMS в DER: первый байт 0x30 (SEQUENCE), а не Base64-текст.
        /// </summary>
        private static void AssertSignedFnsXml(EdoSignedDocumentInfo document)
        {
            TestContext.Out.WriteLine($"{document.FileName}: файл {document.Content.Length} байт, подпись {document.Signature.Length} байт");
            AssertFnsXml(document.RawContent);

            var xml = XDocument.Parse(document.Content);
            Assert.Multiple(() =>
            {
                Assert.That(xml.Root?.Name.LocalName, Is.EqualTo("Файл"));
                Assert.That(xml.Root?.Attribute("ИдФайл")?.Value, Is.EqualTo(document.FileName));
                Assert.That(document.Signature, Is.Not.Empty);
                Assert.That(document.Signature[0], Is.EqualTo(0x30));
            });
        }

        /// <summary>
        /// ИдФайл информации продавца УПД начинается с ON_NSCHFDOPPR, даты - строки ДД.ММ.ГГГГ, как в файле.
        /// </summary>
        private static void AssertSellerDocumentInfo(EdoSellerDocumentInfo info)
        {
            TestContext.Out.WriteLine($"{info.FileId}: {info.Function} № {info.DocumentNumber} от {info.DocumentDate}, УПДи: {info.IsUPDi}, УИД: {info.DocumentUid}");

            Assert.Multiple(() =>
            {
                Assert.That(info.FileId, Does.StartWith("ON_NSCHFDOPPR"));
                Assert.That(info.DateCreation, Does.Match(@"^\d{2}\.\d{2}\.\d{4}$"));
                Assert.That(info.DocumentDate, Does.Match(@"^\d{2}\.\d{2}\.\d{4}$"));
                Assert.That(info.Signature, Is.Not.Empty);
            });
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

        [Test]
        public async Task GetIncomingSignedDocumentInfoTest()
        {
            // Arrange
            var settings = CreateSettings();
            using var edoProvider = new TrueApiEdoProvider();

            var list = await edoProvider.GetIncomingDocumentListAsync(settings, new EdoDocumentSearchModel { Limit = 50 });
            var documentId = FindUpdDocumentId(list, "входящих");

            // Act
            var document = await edoProvider.GetIncomingSignedDocumentInfoAsync(settings, documentId);

            // Assert
            AssertSignedFnsXml(document);
        }

        [Test]
        public async Task GetOutgoingSignedDocumentInfoTest()
        {
            // Arrange
            var settings = CreateSettings();
            using var edoProvider = new TrueApiEdoProvider();

            var list = await edoProvider.GetOutgoingDocumentListAsync(settings, new EdoDocumentSearchModel { Limit = 50 });
            var documentId = FindUpdDocumentId(list, "исходящих");

            // Act
            var document = await edoProvider.GetOutgoingSignedDocumentInfoAsync(settings, documentId);

            // Assert
            AssertSignedFnsXml(document);
        }

        [Test]
        public async Task GetIncomingSellerDocumentInfoTest()
        {
            // Arrange
            var settings = CreateSettings();
            using var edoProvider = new TrueApiEdoProvider();

            var list = await edoProvider.GetIncomingDocumentListAsync(settings, new EdoDocumentSearchModel { Limit = 50 });
            var documentId = FindUpdDocumentId(list, "входящих");

            // Act
            var info = await edoProvider.GetIncomingSellerDocumentInfoAsync(settings, documentId);

            // Assert
            AssertSellerDocumentInfo(info);
        }

        [Test]
        public async Task GetOutgoingSellerDocumentInfoTest()
        {
            // Arrange
            var settings = CreateSettings();
            using var edoProvider = new TrueApiEdoProvider();

            var list = await edoProvider.GetOutgoingDocumentListAsync(settings, new EdoDocumentSearchModel { Limit = 50 });
            var documentId = FindUpdDocumentId(list, "исходящих");

            // Act
            var info = await edoProvider.GetOutgoingSellerDocumentInfoAsync(settings, documentId);

            // Assert
            AssertSellerDocumentInfo(info);
        }
    }
}
