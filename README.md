# General API - Users, Product, Sales, and Cart

## 📌 Description

This API provides resources for managing users, products, sales, and shopping carts.

## 🛠 Technologies Used

- .NET 8
- PostgreSQL
- MongoDB
- RabbitMQ

## 📋 Prerequisites

Clone this project, using the web URL: https://github.com/euclidesbrasil/AMBEV.git

Before downloading the project, make sure you have installed:

- **Visual Studio** (Version used: Microsoft Visual Studio Community 2022 - Version 17.10.1, preferably after version 17.8)
- **PostgreSQL** (Version used: 17.2-3) [Download here](https://www.enterprisedb.com/downloads/postgres-postgresql-downloads)
- **MongoDB Community** (Version used: 7.0.16) [Download here](https://www.mongodb.com/try/download/community-edition/releases)
- **RabbitMQ** (Version used: 4.0.5) [Download here](https://www.rabbitmq.com/docs/install-windows)

## 🚀 Setup Before Running

### 1. PostgreSQL Configuration

In the **Ambev.General.Api** project, open the `appsettings.json` file and adjust the `DefaultConnection` section with your local database credentials:

```json
"DefaultConnection": "Host=localhost;Port=5432;Database=AMBEVDOTNET;Username=postgres;Password=admin"
```

### 2. MongoDB Configuration

In the same `appsettings.json` file, there is a `ConnectionString` section that defines the connection to MongoDB. Adjust it if necessary according to your environment.

### 3. Running the Project

Simply run the project to start the API. On the first execution, the database will be created automatically and initial data will be loaded.

## 🔐 Authentication

To use the endpoints, you need to obtain an authentication token. Use the initial credentials:

- **Username:** admin
- **Password:** @mbev

### Obtaining the Token:
Make a POST request to the `/auth/login` endpoint with the following payload:

```json
{
  "username": "admin",
  "password": "@mbev"
}
```

The API will return a JWT token, which should be used in authenticated requests.

## 📌 Available Endpoints

### 🔹 Authentication
- `POST /auth/login` - Obtain a JWT token for authentication.

### 🔹 Branches
- `GET /branchs/{id}` - Get a branch by ID.
- `GET /branchs` - List all branches.
- `POST /Branchs` - Create a new branch.
- `PUT /Branchs?id={id}` - Update an existing branch.
- `DELETE /Branchs?id={id}` - Delete a branch.

### 🔹 Carts
- `GET /Carts` - List all carts.
- `GET /carts/{id}` - Get a cart by ID.
- `POST /Carts` - Create a new cart.
- `PUT /carts/{id}` - Update a cart.
- `DELETE /carts/{id}` - Delete a cart.

### 🔹 Customers
- `GET /customers` - List all customers.
- `GET /customers/{id}` - Get a customer by ID.
- `POST /Customers` - Create a new customer.
- `PUT /Customers?id={id}` - Update an existing customer.
- `DELETE /Customers?id={id}` - Delete a customer.

### 🔹 Products
- `GET /products/categories` - List product categories.
- `GET /products/category/{category}` - List products by category.
- `POST /Products` - Create a new product.
- `PUT /Products?id={id}` - Update a product.
- `DELETE /Products?id={id}` - Delete a product.

### 🔹 Sales
- `GET /sales` - List all sales.
- `GET /sales/{id}` - Get a sale by ID.
- `POST /Sales` - Create a new sale.
- `PUT /Sales?id={id}` - Update an existing sale.

### 🔹 Users
- `GET /users` - List all users.
- `GET /users/{id}` - Get a user by ID.
- `POST /Users` - Create a new user.
- `PUT /Users?id={id}` - Update a user.

## 📄 License

This project is licensed under the MIT license.


# Developer Evaluation Project

`READ CAREFULLY`

## Instructions
**The test below will have up to 7 calendar days to be delivered from the date of receipt of this manual.**

- The code must be versioned in a public Github repository and a link must be sent for evaluation once completed
- Upload this template to your repository and start working from it
- Read the instructions carefully and make sure all requirements are being addressed
- The repository must provide instructions on how to configure, execute and test the project
- Documentation and overall organization will also be taken into consideration

## Use Case
**You are a developer on the DeveloperStore team. Now we need to implement the API prototypes.**

As we work with `DDD`, to reference entities from other domains, we use the `External Identities` pattern with denormalization of entity descriptions.

Therefore, you will write an API (complete CRUD) that handles sales records. The API needs to be able to inform:

* Sale number
* Date when the sale was made
* Customer
* Total sale amount
* Branch where the sale was made
* Products
* Quantities
* Unit prices
* Discounts
* Total amount for each item
* Cancelled/Not Cancelled

It's not mandatory, but it would be a differential to build code for publishing events of:
* SaleCreated
* SaleModified
* SaleCancelled
* ItemCancelled

If you write the code, **it's not required** to actually publish to any Message Broker. You can log a message in the application log or however you find most convenient.

### Business Rules

* Purchases above 4 identical items have a 10% discount
* Purchases between 10 and 20 identical items have a 20% discount
* It's not possible to sell above 20 identical items
* Purchases below 4 items cannot have a discount

These business rules define quantity-based discounting tiers and limitations:

1. Discount Tiers:
   - 4+ items: 10% discount
   - 10-20 items: 20% discount

2. Restrictions:
   - Maximum limit: 20 items per product
   - No discounts allowed for quantities below 4 items

## Overview
This section provides a high-level overview of the project and the various skills and competencies it aims to assess for developer candidates. 

See [Overview](/.doc/overview.md)

## Tech Stack
This section lists the key technologies used in the project, including the backend, testing, frontend, and database components. 

See [Tech Stack](/.doc/tech-stack.md)

## Frameworks
This section outlines the frameworks and libraries that are leveraged in the project to enhance development productivity and maintainability. 

See [Frameworks](/.doc/frameworks.md)

<!-- 
## API Structure
This section includes links to the detailed documentation for the different API resources:
- [API General](./docs/general-api.md)
- [Products API](/.doc/products-api.md)
- [Carts API](/.doc/carts-api.md)
- [Users API](/.doc/users-api.md)
- [Auth API](/.doc/auth-api.md)
-->

## Project Structure
This section describes the overall structure and organization of the project files and directories. 

See [Project Structure](/.doc/project-structure.md)