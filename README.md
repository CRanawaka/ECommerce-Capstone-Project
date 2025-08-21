# E-Commerce Lite API (.NET 8)

This repository contains the backend API for a lightweight e-commerce platform, built with .NET 8, ASP.NET Core, and Entity Framework Core. The project is built following Clean Architecture principles.

## Features

* **Authentication**: JWT-based user registration and login.
* **Products**: Publicly accessible product catalog.
* **Admin**: Role-based authorization for an "Admin" user to manage products (Create, Update, Delete).
* **Shopping Cart**: Secure endpoints for authenticated users to manage their shopping cart.
* **Orders**: Secure endpoints for users to place orders from their cart and view their order history.

## Tech Stack

* **Framework**: ASP.NET Core 8
* **Architecture**: Clean Architecture (Core, Infrastructure, API layers)
* **Database**: Entity Framework Core 8 with SQL Server
* **Authentication**: JWT Bearer Tokens
* **Testing**: xUnit with `Microsoft.AspNetCore.Mvc.Testing` for integration tests.
* **API Documentation**: Swagger/OpenAPI with XML comments.

## Getting Started

### Prerequisites

* .NET 8 SDK
* SQL Server (e.g., Express, Developer Edition)
* An API testing tool like Postman or the Swagger UI.

### Running Locally

1.  Clone the repository:
    ```bash
    git clone [https://github.com/YOUR_USERNAME/ECommerceProject.git](https://github.com/YOUR_USERNAME/ECommerceProject.git)
    ```
2.  Navigate to the project directory:
    ```bash
    cd ECommerceProject
    ```
3.  Update the connection string in `src/ECommerce.Api/appsettings.Development.json` to point to your local SQL Server instance.
4.  Run the database migrations:
    ```bash
    dotnet ef database update --project src/ECommerce.Infrastructure --startup-project src/ECommerce.Api
    ```
5.  Run the API project:
    ```bash
    dotnet run --project src/ECommerce.Api
    ```
6.  The API will be available at `https://localhost:7001` (or a similar port). Navigate to `/swagger` to view the interactive documentation.

## API Endpoints

A brief overview of the main endpoints:
* `POST /api/auth/register` - Register a new user.
* `POST /api/auth/login` - Log in and receive a JWT.
* `GET /api/products` - Get a list of all products.
* `POST /api/products` - **[Admin]** Create a new product.
* `GET /api/cart` - **[Auth]** Get the current user's shopping cart.
* `POST /api/orders` - **[Auth]** Create an order from the current user's cart.
