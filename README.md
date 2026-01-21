# Spoleto.TrueApi

**Spoleto.TrueApi** is an unofficial library to work with [True API](https://docs.crpt.ru/gismt/True_API/).
**Spoleto.TrueApi.Auth** is an unofficial library to work with authentication of [True API](https://docs.crpt.ru/gismt/True_API/).


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

## Usage
### Creating an address resolver


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
