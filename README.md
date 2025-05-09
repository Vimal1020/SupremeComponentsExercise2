# SupremeComponentsExercise2

A simple ASP.NET Core 8 Web API for managing products in-memory with support for filtering, pagination, and global error handling.

---

## Table of Contents

* [Features](#features)
* [Prerequisites](#prerequisites)
* [Getting Started](#getting-started)

  * [Clone the Repository](#clone-the-repository)
  * [Build and Run](#build-and-run)
* [API Endpoints](#api-endpoints)

  * [Search & List Products](#search--list-products)
  * [Get Product by ID](#get-product-by-id)
  * [Create Product](#create-product)
  * [Update Product](#update-product)
  * [Delete Product](#delete-product)
* [Filtering & Pagination](#filtering--pagination)
* [Error Handling Middleware](#error-handling-middleware)
* [Project Structure](#project-structure)
* [Custom License](#custom-license)

---

## Features

* **CRUD** operations on products (GUID-based IDs)
* **Search & filtering** by category, price range, and keyword
* **Pagination** with custom response headers
* **Global error handling** middleware for consistent API error responses
* In-memory data store (no external database or configuration files required)

## Prerequisites

* [.NET 8.0 SDK](https://dotnet.microsoft.com/download)
* A code editor (e.g., Visual Studio 2022+, VS Code)

## Getting Started

### Clone the Repository

```bash
git clone https://github.com/your-username/SupremeComponentsExercise2.git
cd SupremeComponentsExercise2
```

### Build and Run

```bash
# Restore packages
dotnet restore

# Build the solution
dotnet build

# Run the API
dotnet run --project SupremeComponentsExercise2
```

By default, the API listens on `https://localhost:5001` and `http://localhost:5000`.

---

## API Endpoints

Base URL: `https://localhost:5001/api/products`

### Search & List Products

```
GET /api/products?Category={category}&MinPrice={min}&MaxPrice={max}&SearchTerm={term}&PageNumber={page}&PageSize={size}
```

* **Query Parameters** (all optional):

  * `Category` (string)
  * `MinPrice` (decimal)
  * `MaxPrice` (decimal)
  * `SearchTerm` (string)
  * `PageNumber` (int, default 1)
  * `PageSize` (int, default 10)

* **Response Headers** (pagination metadata):

  * `X-Pagination-TotalCount`
  * `X-Pagination-PageSize`
  * `X-Pagination-CurrentPage`
  * `X-Pagination-TotalPages`

* **Sample Request**:

  ```bash
  curl "https://localhost:5001/api/products?Category=Electronics&PageNumber=1&PageSize=5"
  ```

### Get Product by ID

```
GET /api/products/{guid}
```

* **Parameter**:

  * `{guid}`: product `ProductId` (GUID string)

* **Sample Request**:

  ```bash
  curl "https://localhost:5001/api/products/3fa85f64-5717-4562-b3fc-2c963f66afa6"
  ```

### Create Product

```
POST /api/products
Content-Type: application/json

{
  "name": "New Product",
  "category": "Category",
  "quantity": 100,
  "price": 9.99
}
```

* **Response**: newly created product with a GUID `ProductId`.

* **Sample Request**:

  ```bash
  curl -X POST https://localhost:5001/api/products \
    -H "Content-Type: application/json" \
    -d '{"name":"Widget","category":"Gadgets","quantity":50,"price":19.99}'
  ```

### Update Product

```
PUT /api/products/{guid}
Content-Type: application/json

{
  "name": "Updated Name",
  "category": "Updated Category",
  "quantity": 75,
  "price": 17.99
}
```

* **Sample Request**:

  ```bash
  curl -X PUT https://localhost:5001/api/products/3fa85f64-5717-4562-b3fc-2c963f66afa6 \
    -H "Content-Type: application/json" \
    -d '{"name":"Widget Pro","category":"Gadgets","quantity":75,"price":29.99}'
  ```

### Delete Product

```
DELETE /api/products/{guid}
```

* **Sample Request**:

  ```bash
  curl -X DELETE https://localhost:5001/api/products/3fa85f64-5717-4562-b3fc-2c963f66afa6
  ```

---

## Filtering & Pagination

Use the `SearchProductParameters` model to filter by:

* `Category`
* `MinPrice`, `MaxPrice`
* `SearchTerm` (partial name match)

Control pagination with `PaginationParameters`:

* `PageNumber` (default: 1)
* `PageSize` (default: 10)

See `Helpers/HttpResponseExtensions.cs` for how headers are added.

---

## Error Handling Middleware

Unhandled exceptions are caught by `Middlewares/ErrorHandlingMiddleware.cs`, returning a consistent `ApiErrorResponse`:

```csharp
public class ApiErrorResponse
{
    public string Message { get; set; }
    public string Details { get; set; }
}
```

---

## Project Structure

```
Controllers/      – API controllers
DTOs/             – Data Transfer Objects
Helpers/          – Extension methods (e.g., pagination headers)
Interfaces/       – Service interfaces
Middlewares/      – Custom middleware (error handling)
Models/           – Domain models, parameters, paged result
Services/         – In-memory service implementation
Program.cs        – App startup and DI registration
```

---

## Custom License

This project uses a custom license. Please refer to the `LICENSE.md` file for details.