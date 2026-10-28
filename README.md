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
```
