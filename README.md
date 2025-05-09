# SupremeComponentsExercise2

A simple ASP.NET Core 8 Web API for managing products in-memory with support for filtering, pagination, and robust error handling.

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
* [Error Handling](#error-handling)
  * [Controller-Level Error Handling](#controller-level-error-handling)
  * [Global Error Handling Middleware](#global-error-handling-middleware)
* [Project Structure](#project-structure)
* [Custom License](#custom-license)

---

## Features

* **CRUD** operations on products (GUID-based IDs)
* **Search & filtering** by category, price range, and keyword
* **Pagination** with custom response headers
* **Robust error handling** at both controller and middleware levels
* **RESTful API** design with proper HTTP status codes
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

* **Response Codes**:
  * `200 OK`: Product found and returned
  * `404 Not Found`: No product exists with the specified ID

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
* **Response Codes**:
  * `201 Created`: Product successfully created
  * `400 Bad Request`: Invalid product data provided

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

* **Response Codes**:
  * `200 OK`: Product successfully updated
  * `404 Not Found`: No product exists with the specified ID
  * `400 Bad Request`: Invalid product data provided

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

* **Response Codes**:
  * `204 No Content`: Product successfully deleted
  * `404 Not Found`: No product exists with the specified ID

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

## Error Handling

The API implements a robust, layered approach to error handling:

### Controller-Level Error Handling

The controllers return appropriate HTTP status codes based on operation results:

* `200 OK` for successful operations
* `201 Created` for successful resource creation
* `204 No Content` for successful deletion
* `400 Bad Request` for invalid input data
* `404 Not Found` for resource not found scenarios

This approach provides precise error responses for expected scenarios without relying on exceptions.

### Global Error Handling Middleware

For unexpected errors, the `ErrorHandlingMiddleware` catches exceptions and returns structured error responses:

```json
{
  "statusCode": 404,
  "message": "Product with ID abc123 not found.",
  "details": "Stack trace (development environment only)"
}
```

The middleware handles different exception types with appropriate status codes:

* `ArgumentNullException/ArgumentException` → `400 Bad Request`
* `KeyNotFoundException` → `404 Not Found`
* `Exception` (all others) → `500 Internal Server Error`

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