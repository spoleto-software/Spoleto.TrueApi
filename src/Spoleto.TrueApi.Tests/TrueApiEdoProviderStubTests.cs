using System.IO.Compression;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Xml.Linq;
using Spoleto.TrueApi.Auth.Models;
using Spoleto.TrueApi.Auth.Models.Options;
using Spoleto.TrueApi.Auth.Providers;
using Spoleto.TrueApi.Edo.Enums;
using Spoleto.TrueApi.Edo.Models;
using Spoleto.TrueApi.Edo.Models.Options;
using Spoleto.TrueApi.Edo.Providers;

namespace Spoleto.TrueApi.Tests
{
    /// <summary>
    /// <see cref="TrueApiEdoProvider"/> на подставном транспорте: адреса, заголовки, повтор после 401, ошибки. Стенд не нужен.
    /// </summary>
    public class TrueApiEdoProviderStubTests
    {
        private const string ServiceUrl = "https://markirovka.sandbox.crptech.ru";

        // Второй пример ответа из описания метода 9.2.8 True API v.746.0, плюс суммы и тип строками, как в таблице параметров.
        private const string DocumentListJson = """
            {
              "items": [
                {
                  "id": "33a333ee-3cb3-3aae-3f33-d33ce333333c",
                  "created_at": 1582097164,
                  "date": 1582059600,
                  "documents": [
                    {
                      "id": "220262cf-0fe7-4d3f-996a-038181ba7812",
                      "created_at": 1776253164,
                      "date": 1775077200,
                      "number": "УПД № группсы от 02.04.2026",
                      "processed_at": 1776253164,
                      "revision": null,
                      "status": 3,
                      "total_price": 100,
                      "total_vat_amount": 0,
                      "type": 520,
                      "is_title_signed": false,
                      "created_by": 0,
                      "created_hub": false,
                      "is_closed_lm_part": null,
                      "package_status": null,
                      "uni_new_msg": null
                    }
                  ],
                  "group_id": "4d44444e-4d44-4444-4e44-a4cd444ed444",
                  "number": "номер документа",
                  "recipient": {
                    "id": 999999999,
                    "extra_id": "00000000-23c4-3685-0000-000000000000",
                    "inn": "0000000000",
                    "kpp": "111111111",
                    "ogrn": "1111111111111",
                    "name": "ООО",
                    "fio": null,
                    "address": null,
                    "legal_address": null,
                    "status": "REGISTERED"
                  },
                  "status": 1,
                  "total_price": "1930500",
                  "total_vat_amount": "279500",
                  "type": "504",
                  "create_time_stamp": 1582059600,
                  "export_time_stamp": 1582097180,
                  "product_group": -1,
                  "active_type": 0
                }
              ],
              "has_next_page": true
            }
            """;

        private const string DocumentId = "220262cf-0fe7-4d3f-996a-038181ba7812";

        private const string SellerFileName = "ON_NSCHFDOPPR_2BM-0000000000-000000000_2BM-7777777777-777777777_20260402_" + DocumentId;

        // Архив УПД: информация продавца, информация покупателя и квитанция, у каждого файла подпись.
        private static readonly string[] DocumentArchiveEntries =
        [
            SellerFileName + ".xml",
            SellerFileName + ".p7s",
            "ON_NSCHFDOPPOK_2BM-7777777777-777777777_2BM-0000000000-000000000_20260403_" + DocumentId + ".xml",
            "ON_NSCHFDOPPOK_2BM-7777777777-777777777_2BM-0000000000-000000000_20260403_" + DocumentId + ".p7s",
            "DP_IZVPOL_2BM-7777777777-777777777_2BM-0000000000-000000000_20260402_" + DocumentId + ".xml",
            "DP_IZVPOL_2BM-7777777777-777777777_2BM-0000000000-000000000_20260402_" + DocumentId + ".p7s"
        ];

        static TrueApiEdoProviderStubTests()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);//support for "windows-1251"
        }

        private static TrueApiEdoProviderOption CreateSettings() => new()
        {
            ServiceUrl = ServiceUrl,
            CertificateThumbprint = "thumbprint",
            Inn = "7777777777"
        };

        private static HttpResponseMessage CreateResponse(HttpStatusCode statusCode, string content, string mediaType)
            => new(statusCode) { Content = new StringContent(content, Encoding.UTF8, mediaType) };

        /// <summary>
        /// Файл .xml в архиве - XML в windows-1251 с ИдФайл по своему имени, остальные файлы - байты своего имени. Так по содержимому видно, какой файл вернулся.
        /// </summary>
        private static HttpResponseMessage CreateArchiveResponse(string? archiveFileName, params string[] entryNames)
            => CreateArchiveResponse(archiveFileName, entryNames.ToDictionary(x => x, CreateEntryContent));

        private static byte[] CreateEntryContent(string entryName)
            => entryName.EndsWith(".xml", StringComparison.Ordinal)
                ? Encoding.GetEncoding("windows-1251").GetBytes(CreateEntryXml(Path.GetFileNameWithoutExtension(entryName)))
                : Encoding.UTF8.GetBytes(entryName);

        private static string CreateEntryXml(string fileName) => $"""<?xml version="1.0" encoding="windows-1251"?><Файл ИдФайл="{fileName}"/>""";

        private static HttpResponseMessage CreateArchiveResponse(string? archiveFileName, IReadOnlyDictionary<string, byte[]> entries)
        {
            using var archiveStream = new MemoryStream();
            using (var archive = new ZipArchive(archiveStream, ZipArchiveMode.Create, true))
            {
                foreach (var (entryName, content) in entries)
                {
                    using var entryStream = archive.CreateEntry(entryName).Open();
                    entryStream.Write(content);
                }
            }

            var response = new HttpResponseMessage(HttpStatusCode.OK) { Content = new ByteArrayContent(archiveStream.ToArray()) };
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/zip");
            if (archiveFileName != null)
            {
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment") { FileName = $"\"{archiveFileName}\"" };
            }

            return response;
        }

        /// <summary>
        /// Информация продавца УПД формата 970: только элементы и атрибуты, которые повторяет информация покупателя.
        /// </summary>
        private static string CreateSellerXml(string? correctionNumber)
        {
            var correction = correctionNumber == null ? "" : $"""<ИспрДок НомИспр="{correctionNumber}" ДатаИспр="03.04.2026"/>""";

            return $"""
                <?xml version="1.0" encoding="windows-1251"?>
                <Файл ИдФайл="{SellerFileName}" ВерсФорм="5.03" ВерсПрог="Spoleto">
                  <Документ КНД="1115131" Функция="СЧФДОП" НаимДокОпр="Счет-фактура и документ об отгрузке товаров (выполнении работ), передаче имущественных прав (документ об оказании услуг)" ДатаИнфПр="02.04.2026" ВремИнфПр="10.15.00" УИД="0000000000-7777777777-000001">
                    <СвСчФакт НомерДок="15" ДатаДок="01.04.2026" КодОКВ="643">{correction}</СвСчФакт>
                  </Документ>
                </Файл>
                """;
        }

        private static HttpResponseMessage CreateSellerArchiveResponse(string sellerXml, byte[] signature)
            => CreateArchiveResponse(SellerFileName + ".zip", new Dictionary<string, byte[]>
            {
                [SellerFileName + ".xml"] = Encoding.GetEncoding("windows-1251").GetBytes(sellerXml),
                [SellerFileName + ".p7s"] = signature
            });

        [Test]
        public async Task GetIncomingDocumentList_BuildsQueryAndParsesResponse()
        {
            // Arrange
            var handler = new StubHttpMessageHandler(CreateResponse(HttpStatusCode.OK, DocumentListJson, "application/json"));
            var tokenProvider = new FakeTokenProvider();
            using var edoProvider = new TrueApiEdoProvider(new HttpClient(handler), true, tokenProvider);
            var searchModel = new EdoDocumentSearchModel
            {
                Limit = 10,
                Offset = 20,
                CreatedFrom = DateTimeOffset.FromUnixTimeSeconds(1582059600).UtcDateTime,
                PartnerInn = "0000000000",
                Status = EdoDocumentStatus.DeliveredSignatureRequired,
                Type = EdoDocumentType.UPD970WithSCHFDOPInvoiceWaybill,
                Folder = EdoFolder.Documents,
                SortBy = EdoDocumentSortField.CreatedAt,
                Asc = true
            };

            // Act
            var data = await edoProvider.GetIncomingDocumentListAsync(CreateSettings(), searchModel);

            // Assert
            var request = handler.Requests.Single();
            Assert.Multiple(() =>
            {
                Assert.That(request.Uri.ToString(), Is.EqualTo(ServiceUrl + "/api/v3/true-api/elk/incoming-documents"
                    + "?limit=10&offset=20&created_from=1582059600&partner_inn=0000000000&status=3&type=524&folder=0&sortBy=created_at&asc=true"));
                Assert.That(request.Authorization, Is.EqualTo("Bearer uuid-1"));
                Assert.That(request.Accept, Is.EqualTo("application/json"));
                Assert.That(tokenProvider.LastOption, Is.EqualTo(new TrueApiTokenProviderOption(ServiceUrl, "thumbprint", "7777777777", true)));

                Assert.That(data.HasNextPage, Is.True);
                var group = data.Items.Single();
                Assert.That(group.Status, Is.EqualTo(EdoDocumentStatus.Sent));
                Assert.That(group.Type, Is.EqualTo(EdoDocumentType.UPDWithSCHFDOPInvoiceWaybill));
                Assert.That(group.TotalPrice, Is.EqualTo(1930500m));
                Assert.That(group.CreatedAt, Is.EqualTo(DateTimeOffset.FromUnixTimeSeconds(1582097164).UtcDateTime));
                Assert.That(group.Recipient!.Id, Is.EqualTo(999999999));
                Assert.That(group.Recipient.Inn, Is.EqualTo("0000000000"));

                var document = group.Documents.Single();
                Assert.That(document.Id, Is.EqualTo("220262cf-0fe7-4d3f-996a-038181ba7812"));
                Assert.That(document.Type, Is.EqualTo(EdoDocumentType.UPD970WithDOPWaybill));
                Assert.That(document.Status, Is.EqualTo(EdoDocumentStatus.DeliveredSignatureRequired));
                Assert.That(document.Revision, Is.Null);
                Assert.That(document.IsTitleSigned, Is.False);
            });
        }

        [TestCase(false, "incoming-documents")]
        [TestCase(true, "outgoing-documents")]
        public async Task GetDocumentList_WithoutSearchModel_HasNoQuery(bool isOutgoing, string documentsPath)
        {
            // Arrange
            var handler = new StubHttpMessageHandler(CreateResponse(HttpStatusCode.OK, """{"items":[],"has_next_page":false}""", "application/json"));
            using var edoProvider = new TrueApiEdoProvider(new HttpClient(handler), true, new FakeTokenProvider());

            // Act
            var data = isOutgoing
                ? await edoProvider.GetOutgoingDocumentListAsync(CreateSettings())
                : await edoProvider.GetIncomingDocumentListAsync(CreateSettings());

            // Assert
            Assert.That(handler.Requests.Single().Uri.ToString(), Is.EqualTo(ServiceUrl + "/api/v3/true-api/elk/" + documentsPath));
            Assert.That(data.Items, Is.Empty);
        }

        [TestCase(false, "incoming-documents")]
        [TestCase(true, "outgoing-documents")]
        public async Task GetDocument_ReturnsFileBytesAsIs(bool isOutgoing, string documentsPath)
        {
            // Arrange
            var xml = Encoding.GetEncoding("windows-1251").GetBytes("<?xml version=\"1.0\" encoding=\"windows-1251\"?><Файл ИдФайл=\"ON_NSCHFDOPPR\"/>");
            var response = new HttpResponseMessage(HttpStatusCode.OK) { Content = new ByteArrayContent(xml) };
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            var handler = new StubHttpMessageHandler(response);
            using var edoProvider = new TrueApiEdoProvider(new HttpClient(handler), true, new FakeTokenProvider());

            // Act
            var content = isOutgoing
                ? await edoProvider.GetOutgoingDocumentAsync(CreateSettings(), "f2aab44e-9cc3-4b37-a4ff-c8edf0b6e57b")
                : await edoProvider.GetIncomingDocumentAsync(CreateSettings(), "f2aab44e-9cc3-4b37-a4ff-c8edf0b6e57b");

            // Assert
            var request = handler.Requests.Single();
            Assert.Multiple(() =>
            {
                Assert.That(request.Uri.ToString(), Is.EqualTo(ServiceUrl + "/api/v3/true-api/elk/" + documentsPath + "/f2aab44e-9cc3-4b37-a4ff-c8edf0b6e57b/content"));
                Assert.That(request.Accept, Is.EqualTo("*/*"));
                Assert.That(content, Is.EqualTo(xml));
                Assert.That(XDocument.Load(new MemoryStream(content)).Root!.Attribute("ИдФайл")!.Value, Is.EqualTo("ON_NSCHFDOPPR"));
            });
        }

        [TestCase(false, "incoming-documents")]
        [TestCase(true, "outgoing-documents")]
        public async Task GetSignedDocumentInfo_ReadsFileNamedAfterArchive(bool isOutgoing, string documentsPath)
        {
            // Arrange
            var handler = new StubHttpMessageHandler(CreateArchiveResponse(SellerFileName + ".zip", DocumentArchiveEntries));
            using var edoProvider = new TrueApiEdoProvider(new HttpClient(handler), true, new FakeTokenProvider());

            // Act
            var document = isOutgoing
                ? await edoProvider.GetOutgoingSignedDocumentInfoAsync(CreateSettings(), DocumentId)
                : await edoProvider.GetIncomingSignedDocumentInfoAsync(CreateSettings(), DocumentId);

            // Assert
            var request = handler.Requests.Single();
            Assert.Multiple(() =>
            {
                Assert.That(request.Uri.ToString(), Is.EqualTo(ServiceUrl + "/api/v3/true-api/elk/" + documentsPath + "/" + DocumentId));
                Assert.That(request.Accept, Is.EqualTo("*/*"));
                Assert.That(document.FileName, Is.EqualTo(SellerFileName));
                Assert.That(document.Content, Is.EqualTo(CreateEntryXml(SellerFileName)));
                Assert.That(document.RawContent, Is.EqualTo(CreateEntryContent(SellerFileName + ".xml")));
                Assert.That(Encoding.UTF8.GetString(document.Signature), Is.EqualTo(SellerFileName + ".p7s"));
            });
        }

        [TestCase(null)]
        [TestCase(DocumentId + ".zip")]
        public async Task GetSignedDocumentInfo_ArchiveNotNamedAfterDocument_ReadsSellerFile(string? archiveFileName)
        {
            // Arrange
            var handler = new StubHttpMessageHandler(CreateArchiveResponse(archiveFileName, DocumentArchiveEntries));
            using var edoProvider = new TrueApiEdoProvider(new HttpClient(handler), true, new FakeTokenProvider());

            // Act
            var document = await edoProvider.GetIncomingSignedDocumentInfoAsync(CreateSettings(), DocumentId);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(document.FileName, Is.EqualTo(SellerFileName));
                Assert.That(document.Content, Is.EqualTo(CreateEntryXml(SellerFileName)));
                Assert.That(document.RawContent, Is.EqualTo(CreateEntryContent(SellerFileName + ".xml")));
                Assert.That(Encoding.UTF8.GetString(document.Signature), Is.EqualTo(SellerFileName + ".p7s"));
            });
        }

        [TestCase("windows-1251", false)]
        [TestCase("utf-8", false)]
        [TestCase("utf-8", true)]
        public async Task GetSignedDocumentInfo_DecodesXmlByDeclaredEncoding(string encodingName, bool withBom)
        {
            // Arrange
            var encoding = Encoding.GetEncoding(encodingName);
            var xml = $"""<?xml version="1.0" encoding="{encodingName}"?><Файл ИдФайл="{SellerFileName}"/>""";
            byte[] rawContent = withBom ? [.. encoding.GetPreamble(), .. encoding.GetBytes(xml)] : encoding.GetBytes(xml);
            var handler = new StubHttpMessageHandler(CreateArchiveResponse(SellerFileName + ".zip", new Dictionary<string, byte[]>
            {
                [SellerFileName + ".xml"] = rawContent,
                [SellerFileName + ".p7s"] = [0x30]
            }));
            using var edoProvider = new TrueApiEdoProvider(new HttpClient(handler), true, new FakeTokenProvider());

            // Act
            var document = await edoProvider.GetIncomingSignedDocumentInfoAsync(CreateSettings(), DocumentId);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(document.Content, Is.EqualTo(xml));
                Assert.That(document.RawContent, Is.EqualTo(rawContent));
            });
        }

        [Test]
        public void GetSignedDocumentInfo_WithoutSellerFile_ThrowsWithArchiveContents()
        {
            // Arrange
            var handler = new StubHttpMessageHandler(CreateArchiveResponse(null, DocumentArchiveEntries.Skip(2).ToArray()));
            using var edoProvider = new TrueApiEdoProvider(new HttpClient(handler), true, new FakeTokenProvider());

            // Act
            var ex = Assert.ThrowsAsync<InvalidDataException>(() => edoProvider.GetIncomingSignedDocumentInfoAsync(CreateSettings(), DocumentId));

            // Assert
            Assert.That(ex!.Message, Does.Contain(DocumentArchiveEntries[2]));
        }

        [Test]
        public void GetSignedDocumentInfo_WithoutSignature_Throws()
        {
            // Arrange
            var handler = new StubHttpMessageHandler(CreateArchiveResponse(SellerFileName + ".zip", SellerFileName + ".xml"));
            using var edoProvider = new TrueApiEdoProvider(new HttpClient(handler), true, new FakeTokenProvider());

            // Act
            var ex = Assert.ThrowsAsync<InvalidDataException>(() => edoProvider.GetOutgoingSignedDocumentInfoAsync(CreateSettings(), DocumentId));

            // Assert
            Assert.That(ex!.Message, Does.Contain(SellerFileName + ".p7s"));
        }

        [TestCase(false, "incoming-documents", null, false)]
        [TestCase(true, "outgoing-documents", "1", true)]
        public async Task GetSellerDocumentInfo_ParsesSellerFile(bool isOutgoing, string documentsPath, string? correctionNumber, bool isUPDi)
        {
            // Arrange
            var signature = new byte[] { 0x30, 0x82, 0x01, 0x00 };
            var handler = new StubHttpMessageHandler(CreateSellerArchiveResponse(CreateSellerXml(correctionNumber), signature));
            using var edoProvider = new TrueApiEdoProvider(new HttpClient(handler), true, new FakeTokenProvider());

            // Act
            var info = isOutgoing
                ? await edoProvider.GetOutgoingSellerDocumentInfoAsync(CreateSettings(), DocumentId)
                : await edoProvider.GetIncomingSellerDocumentInfoAsync(CreateSettings(), DocumentId);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(handler.Requests.Single().Uri.ToString(), Is.EqualTo(ServiceUrl + "/api/v3/true-api/elk/" + documentsPath + "/" + DocumentId));
                Assert.That(info.FileId, Is.EqualTo(SellerFileName));
                Assert.That(info.DateCreation, Is.EqualTo("02.04.2026"));
                Assert.That(info.TimeCreation, Is.EqualTo("10.15.00"));
                Assert.That(info.DocumentName, Does.StartWith("Счет-фактура и документ об отгрузке"));
                Assert.That(info.Function, Is.EqualTo(EdoDocumentFunction.InvoiceAndPrimaryDocument));
                Assert.That(info.DocumentNumber, Is.EqualTo("15"));
                Assert.That(info.DocumentDate, Is.EqualTo("01.04.2026"));
                Assert.That(info.DocumentUid, Is.EqualTo("0000000000-7777777777-000001"));
                Assert.That(info.IsUPDi, Is.EqualTo(isUPDi));
                Assert.That(info.Signature, Is.EqualTo(signature));
            });
        }

        [TestCase("Функция=\"СЧФДОП\"", "Функция=\"КСЧФ\"", "КСЧФ")]
        [TestCase(" ДатаДок=\"01.04.2026\"", "", "ДатаДок")]
        [TestCase("СвСчФакт", "СвКСчФ", "СвСчФакт")]
        public void GetSellerDocumentInfo_InvalidSellerFile_ThrowsWithReason(string oldValue, string newValue, string expectedReason)
        {
            // Arrange
            var sellerXml = CreateSellerXml(null).Replace(oldValue, newValue);
            var handler = new StubHttpMessageHandler(CreateSellerArchiveResponse(sellerXml, [0x30]));
            using var edoProvider = new TrueApiEdoProvider(new HttpClient(handler), true, new FakeTokenProvider());

            // Act
            var ex = Assert.ThrowsAsync<InvalidDataException>(() => edoProvider.GetIncomingSellerDocumentInfoAsync(CreateSettings(), DocumentId));

            // Assert
            Assert.That(ex!.Message, Does.Contain(expectedReason).And.Contain(SellerFileName));
        }

        [Test]
        public async Task Unauthorized_RefreshesTokenOnce()
        {
            // Arrange
            var handler = new StubHttpMessageHandler(
                CreateResponse(HttpStatusCode.Unauthorized, """{"error_message":"Токен не действителен. Необходимо получить новый токен аутентификации"}""", "application/json"),
                CreateResponse(HttpStatusCode.OK, """{"items":[],"has_next_page":false}""", "application/json"));
            var tokenProvider = new FakeTokenProvider();
            using var edoProvider = new TrueApiEdoProvider(new HttpClient(handler), true, tokenProvider);

            // Act
            var data = await edoProvider.GetIncomingDocumentListAsync(CreateSettings());

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(data.HasNextPage, Is.False);
                Assert.That(tokenProvider.ExpiredCount, Is.EqualTo(1));
                Assert.That(handler.Requests.Select(x => x.Authorization), Is.EqualTo(new[] { "Bearer uuid-1", "Bearer uuid-2" }));
            });
        }

        [Test]
        public void Unauthorized_AfterRefresh_ThrowsWithoutLoop()
        {
            // Arrange
            var handler = new StubHttpMessageHandler(
                CreateResponse(HttpStatusCode.Unauthorized, """{"error_message":"unauthorized"}""", "application/json"),
                CreateResponse(HttpStatusCode.Unauthorized, """{"error_message":"unauthorized"}""", "application/json"));
            var tokenProvider = new FakeTokenProvider();
            using var edoProvider = new TrueApiEdoProvider(new HttpClient(handler), true, tokenProvider);

            // Act
            var ex = Assert.ThrowsAsync<HttpRequestException>(() => edoProvider.GetIncomingDocumentAsync(CreateSettings(), "id"));

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(ex!.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
                Assert.That(ex.Message, Is.EqualTo("unauthorized"));
                Assert.That(handler.Requests, Has.Count.EqualTo(2));
                Assert.That(tokenProvider.ExpiredCount, Is.EqualTo(1));
            });
        }

        [TestCase("""{"error_message":"Документ не найден в ГИС МТ"}""", "Документ не найден в ГИС МТ")]
        [TestCase("""{"code":500,"error_message":"Внутренняя ошибка"}""", "Внутренняя ошибка")]
        [TestCase("""{"errors":[{"error_message":"Первая"},{"error_message":"Вторая"}]}""", "Первая; Вторая")]
        public void Error_UsesServiceMessage(string errorJson, string expectedMessage)
        {
            // Arrange
            var handler = new StubHttpMessageHandler(CreateResponse(HttpStatusCode.NotFound, errorJson, "application/json"));
            using var edoProvider = new TrueApiEdoProvider(new HttpClient(handler), true, new FakeTokenProvider());

            // Act
            var ex = Assert.ThrowsAsync<HttpRequestException>(() => edoProvider.GetIncomingDocumentAsync(CreateSettings(), "id"));

            // Assert
            Assert.That(ex!.Message, Is.EqualTo(expectedMessage));
            Assert.That(ex.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        private sealed record StubRequest(Uri Uri, string? Authorization, string Accept);

        private sealed class StubHttpMessageHandler : HttpMessageHandler
        {
            private readonly Queue<HttpResponseMessage> _responses;

            public StubHttpMessageHandler(params HttpResponseMessage[] responses)
            {
                _responses = new Queue<HttpResponseMessage>(responses);
            }

            public List<StubRequest> Requests { get; } = [];

            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                Requests.Add(new StubRequest(request.RequestUri!, request.Headers.Authorization?.ToString(), request.Headers.Accept.ToString()));

                return Task.FromResult(_responses.Dequeue());
            }
        }

        private sealed class FakeTokenProvider : ITrueApiTokenProvider
        {
            private int _issuedCount;

            public int ExpiredCount { get; private set; }

            public TrueApiTokenProviderOption? LastOption { get; private set; }

            public Task<UnitedTokenModel> GetTokenAsync(TrueApiTokenProviderOption settings)
            {
                LastOption = settings;
                _issuedCount++;

                return Task.FromResult(new UnitedTokenModel { UuidToken = $"uuid-{_issuedCount}", Token = "jwt" });
            }

            public void SetTokenExpired() => ExpiredCount++;
        }
    }
}
