# Spoleto.TrueApi

**Spoleto.TrueApi** is an unofficial library to work with [True API](https://docs.crpt.ru/gismt/True_API/).

**Spoleto.TrueApi.Auth** is an unofficial library to work with authentication of [True API](https://docs.crpt.ru/gismt/True_API/).

**Spoleto.TrueApi.Edo** is an unofficial library to work with «ЭДО Лайт» methods of [True API](https://docs.crpt.ru/gismt/True_API/).


## Installation

To install the library, use the following package manager, for example, NuGet:

Spoleto.TrueApi:
```bash
Install-Package Spoleto.TrueApi
```

Spoleto.TrueApi.Auth
```bash
Install-Package Spoleto.TrueApi.Auth
```

Spoleto.TrueApi.Edo
```bash
Install-Package Spoleto.TrueApi.Edo
```

## Usage
### Creating an provider


```csharp
var trueApiTokenProvider = new TrueApiTokenProvider()
var trueApiProvider = new TrueApiProvider(new HttpClient(), true, trueApiTokenProvider);

// or default:
var trueApiProvider = new TrueApiProvider();
```

### Метод получения квитанций результата обработки универсального документа по идентификатору документа.

```csharp
var result = await trueApiProvider.GetProcessingResultAsync(settings, "ON_NSCHFDOPPR_file_name");

// or sync version:
var result = trueApiProvider.GetProcessingResult(settings, "ON_NSCHFDOPPR_file_name");
```

### Методы «ЭДО Лайт»

```csharp
var settings = new TrueApiEdoProviderOption
{
    ServiceUrl = "https://markirovka.crpt.ru",
    CertificateThumbprint = "thumbprint",
    Inn = "7777777777"
};

using var edoProvider = new TrueApiEdoProvider();

// Список входящих документов:
var list = await edoProvider.GetIncomingDocumentListAsync(settings, new EdoDocumentSearchModel { Limit = 10, Status = EdoDocumentStatus.DeliveredSignatureRequired });

// Файл входящего документа как есть: XML для УПД, УКД и квитанций (для УПД - windows-1251), PDF для договоров с оператором ГИС МТ:
var file = await edoProvider.GetIncomingDocumentAsync(settings, list.Items[0].Documents[0].Id);

// XML читается в кодировке из своего заголовка. Для windows-1251 в .NET один раз на старте нужно зарегистрировать кодовые страницы:
Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
var xml = XDocument.Load(new MemoryStream(file));

// Исходящие документы - те же методы с Outgoing:
var outgoingList = await edoProvider.GetOutgoingDocumentListAsync(settings, new EdoDocumentSearchModel { Limit = 10 });
var outgoingFile = await edoProvider.GetOutgoingDocumentAsync(settings, outgoingList.Items[0].Documents[0].Id);

// Файл документа с открепленной подписью из ZIP-архива документа (архив есть только у УПД, УПДи, УКД и УКДи):
var signed = await edoProvider.GetIncomingSignedDocumentInfoAsync(settings, list.Items[0].Documents[0].Id);
var xml = signed.Content; // XML строкой, кодировку из заголовка библиотека учла сама; FileName - ИдФайл
var signature = Convert.ToBase64String(signed.Signature); // RawContent и Signature - файлы .xml и .p7s как есть, подпись сделана по RawContent
var outgoingSigned = await edoProvider.GetOutgoingSignedDocumentInfoAsync(settings, outgoingList.Items[0].Documents[0].Id);

// Сведения из файла информации продавца УПД / УПДи (формат 970), которые повторяет информация покупателя:
var sellerInfo = await edoProvider.GetIncomingSellerDocumentInfoAsync(settings, list.Items[0].Documents[0].Id);
var sellerFileId = sellerInfo.FileId; // ИдФайл, например, для запроса квитанций результата обработки
var outgoingSellerInfo = await edoProvider.GetOutgoingSellerDocumentInfoAsync(settings, outgoingList.Items[0].Documents[0].Id);

// Подписание черновика исходящего УПД / УПДи / УКД / УКДи сертификатом CertificateThumbprint, после чего документ уходит получателю.
// Библиотека скачивает файл черновика и подписывает его байты как есть открепленной подписью:
var drafts = await edoProvider.GetOutgoingDocumentListAsync(settings, new EdoDocumentSearchModel { Status = EdoDocumentStatus.Draft });
await edoProvider.SignOutgoingDocumentAsync(settings, drafts.Items[0].Documents[0].Id);

// Отправка УПД формата 970: библиотека формирует XML (windows-1251), подписывает его и загружает.
// С RevisionNumber документ уходит как УПДи. С isDraft: true - черновик без подписи, его потом отправляет SignOutgoingDocumentAsync.
var seller = new EdoOrganization
{
    OrganizationIdentificationInfo = new() { LegalPerson = new() { Name = "ООО «Продавец»", Inn = "7777777777", Kpp = "777701001" } },
    Address = new() { RussianAddress = new() { RegionCode = "77", Region = "г. Москва" } }
};
var upd = new EdoSellerUniversalTransferDocument
{
    SenderEdoId = "2LT-0000000001", // идентификаторы участников ЭДО входят в имя файла, ИдФайл библиотека формирует сама
    RecipientEdoId = "2LT-0000000002",
    Function = EdoDocumentFunction.InvoiceAndPrimaryDocument,
    DocumentNumber = "15",
    DocumentDate = new DateTime(2026, 4, 1),
    DocumentCreator = seller,
    Sellers = [seller],
    Buyers = [new() { OrganizationIdentificationInfo = new() { LegalPerson = new() { Name = "ООО «Покупатель»", Inn = "0000000000", Kpp = "000001001" } } }],
    Items =
    [
        new()
        {
            ProductName = "Кроссовки", UnitCode = "796", UnitName = "шт", Quantity = 1, Price = 100, SumWithoutVat = 100,
            TaxRate = EdoTaxRate.TwentyTwoPercent, Vat = 22, Sum = 122,
            AdditionalInfo = new() { ItemIdentificationNumberList = [new() { MarkItems = ["010460000000000121abc"] }] }
        }
    ],
    TransferInfo = new() { TransferDetails = new() { Date = new DateTime(2026, 4, 2) } },
    Signers = [new() { JobTitle = "Директор", AuthorityConfirmationType = EdoAuthorityConfirmationType.ElectronicSignatureData, Person = new() { Surname = "Иванов", FirstName = "Иван" } }]
};
var posted = await edoProvider.PostUniversalTransferDocumentAsync(settings, upd);
var updId = posted.Id; // FileId - ИдФайл, Content и RawContent - отправленный XML

// Титул покупателя к входящему УПД / УПДи формата 970: библиотека скачивает файл продавца, берёт из него реквизиты, подпись,
// отправителя и получателя, формирует XML, подписывает и загружает. Метод для УПД или УПДи выбирается по файлу продавца.
var buyerTitle = new EdoBuyerUniversalTransferDocument
{
    DocumentId = list.Items[0].Documents[0].Id, // входящий УПД, который ждёт подписи
    DocumentCreator = new() { OrganizationIdentificationInfo = new() { LegalPerson = new() { Name = "ООО «Покупатель»", Inn = "0000000000", Kpp = "000001001" } } },
    AcceptanceInfo = new() { OperationName = "Товары приняты без претензий", Date = new DateTime(2026, 4, 3) }, // по умолчанию - приняты без расхождений
    Signers = [new() { JobTitle = "Директор", AuthorityConfirmationType = EdoAuthorityConfirmationType.ElectronicSignatureData, Person = new() { Surname = "Петров", FirstName = "Петр" } }]
};
var receipt = await edoProvider.ReceiptUniversalTransferDocumentAsync(settings, buyerTitle);
var titleId = receipt.Id; // идентификатор, который вернул сервис; сам документ остаётся под прежним идентификатором
// УПД с кодами маркировки после титула покупателя уходит в ГИС МТ: статус «Подписан: Отправлен в ГИС МТ». УПД без кодов остаётся «Подписан».
```
