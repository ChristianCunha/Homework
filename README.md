# Project Homework

## Description

Create a single Web API implemented in .Net Core to calculate Net, Gross, VAT amounts for purchases in Austria.

## Propose

The proposed exercise claim to create a single api capable of validating the data sent and calculating the response according to previously informed valid rates.
The calculations can be verified through the link [here](https://www.calkoo.com/en/vat-calculator)

## Requirements

- Single api
- The API provides an error with meaningful error messages
- The solution needs to be implemented in .NET Core latest version
- The solution needs to use dependency injection (DI) software design pattern
- The API must fulfil the REST API standards
- The application needs to use Nuget package manager
- No database usage

## Get Started

Homework.sln                 <---- Solution file

Homework.Api                 <---- Folder with presentation layler
Homework.Application         <---- Folder with business layler
Homework.Domain              <---- Folder with domain layler

Homework.Api.Tests           <---- Folder with presentation layler tests
Homework.Application.Tests   <---- Folder with business layler tests
Homework.Domain.Tests        <---- Folder with domain layler tests

## Solution - Single API

```csharp
[HttpGet]
[Route("calculate")]
public ActionResult<PurchaseResponse> Calculate([FromQuery] PurchaseRequest request)
```

## Brainstorm: possible different approach for a full solution with data storage [Not Implemented]

For the replication of the case presented in the [link](https://www.calkoo.com/en/vat-calculator) we would have a different approach with three APIs described below.

# CountryController
```csharp
[HttpGet]
[Route("countries")]
public async Task<ActionResult<IEnumerable<CountryResponse>>> GetCountries()
```

# VatRatesController
```csharp
[HttpGet]
[Route("vatrates/{id}")]
public async Task<ActionResult<IEnumerable<VatRateResponse>>> GetVatRatesById(Guid id)
```

# PurchaseController
```csharp
[HttpGet]
[Route("calculate")]
public async Task<ActionResult<PurchaseResponse>> Calculate([FromQuery] PurchaseRequest request)
```
