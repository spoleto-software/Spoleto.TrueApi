# Spoleto.TrueApi

**Spoleto.TrueApi** is an unofficial library to work with True API.


## Installation

To install the library, use the following package manager, for example, NuGet:

```bash
Install-Package Spoleto.TrueApi
```

## Usage
### Creating an address resolver


```csharp
var trueApiProvider = new TrueApiProvider();
```

### Метод получения квитанций результата обработки универсального документа по идентификатору документа.

```csharp
var result = await trueApiProvider.GetProcessingResultAsync(settings, "ON_NSCHFDOPPR_file_name");

// or sync version:
var result = trueApiProvider.GetProcessingResult(settings, "ON_NSCHFDOPPR_file_name");
```