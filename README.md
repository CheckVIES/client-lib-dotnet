# Deimdal.CheckVIES.Client

A .NET client library for the CheckVIES automated VAT verification API.

## Features
- Targeting .NET Standard 2.0 (compatible with .NET Framework, .NET Core, .NET 5/6/7/8/9/10+)
- Uses **System.Text.Json** for high-performance serialization
- Fully async/await API
- CancellationToken support
- Error handling with strongly-typed error responses
- Configurable base URL and API Key

## Installation

```bash
dotnet add package Deimdal.CheckVIES.Client
```

## Usage

### 1. Using Dependency Injection (Recommended)

In your `Program.cs` or `Startup.cs`:

```csharp
using Deimdal.CheckVIES.Client.Extensions;

// Register the client
services.AddCheckViesClient("YOUR_API_KEY");
```

Then inject `CheckViesClient` into your services:

```csharp
public class MyVatService
{
    private readonly ICheckViesClient _api;

    public MyVatService(ICheckViesClient api)
    {
        _api = api;
    }

    public async Task CheckVat(string vatNumber, string isoAlpha2)
    {
        // Start a check
        var startResult = await _api.StartAsync(new StartCheckRequestDto { 
            Number = vatNumber, 
            IsoAlpha2 = isoAlpha2 
        });

        // Get details
        var details = await _api.DetailsAsync(startResult.Id);
        
        if (details.State == RequestStateDto.Success && details.Result?.Valid == true)
        {
            Console.WriteLine($"VAT is valid! Owner: {details.Result.Name}");
        }
    }
}
```

### 2. Using Static Factory (Manual instantiation)

```csharp
var api = CheckViesClientFactory.Create("YOUR_API_KEY");
var result = await api.DetailsAsync(Guid.Parse("..."));
```

## Manual Testing

To run manual tests against the real API:
1. Create a test project or use the provided test project.
2. Use a real API key.
3. Call the API methods as shown in the examples above.

Note: Ensure you do not commit your real API key to source control.

## Build, Test, and Pack

### Build
```bash
dotnet build
```

### Run Tests
```bash
dotnet test
```

### Create NuGet Package
```bash
dotnet pack -c Release
```
