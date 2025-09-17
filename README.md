
# 📐 COBS - Custom Order Billing System Project

The **COBS** (Custom Order Billing System) project is a **.NET 8 Application** designed as a technical assessment to evaluate backend skills in architecture, data management, security, and clean code.  
It implements a role-based system for managing customers, orders, invoices, wallets, and transactions using **ASP.NET Core**, **Entity Framework Core**, and **JWT Authentication**.

![dotnet-version](https://img.shields.io/badge/dotnet%20version-net8.0-blue)

---

## ⭐ Introduction
COBS is a backend API that simulates a simple billing and order management platform.  

The system supports two main roles:
- **Customer**: Can manage their own profile, view and pay invoices, place orders, and check wallet transactions.  
- **Manager**: Has access to all customers, orders, invoices, and can manage wallet deposits.  

The solution is structured following **Clean Architecture principles**, with use cases isolated, validation applied using **FluentValidation**, and error handling centralized via a **Global Exception Middleware**.

---

## 🔎 Core Features
- **Authentication & Authorization**
  - Login with email (JWT token issued).
  - Role-based access control (Customer vs. Manager).
- **Customer Management**
  - Create customers (restricted to Manager).
  - Get current user profile.
  - Get all customers / Get by ID (Manager only).
- **Orders**
  - Customers can create their own orders.
  - Orders automatically generate linked invoices.
  - Managers can view all orders or filter by customer.
- **Invoices**
  - Auto-created upon order placement.
  - Customers can pay their invoices via wallet balance.
  - Managers can query invoices of all customers.
- **Wallet**
  - Customers have wallet balances.
  - Managers can deposit into customer wallets.
- **Transactions**
  - Full history of wallet operations (deposits, payments) per user.
- **Error Handling**
  - Centralized exception middleware for consistent responses.
- **Swagger UI**
  - Interactive API documentation with JWT authentication support.

---

## ✅ Technical Features
- **.NET 8 Web API** with **Entity Framework Core (SQL Server)**.
- **CQRS** with **MediatR** for decoupled request/response handling.
- **FluentValidation** for input validation.
- **JWT Authentication** for secure access.
- **Role-based Authorization** at controller/action level.
- **Global Exception Middleware** for error handling.
- **InMemory EF Core** support for unit testing.
- **Swagger (OpenAPI)** for interactive API documentation.
- **Unit Tests** written with **xUnit, Moq, FluentAssertions** ensuring 100% code coverage.

---

## 🧑‍💻 Development and Deployment Features
- **Local Development**:
  - SQL Server LocalDB for persistence.
  - EF Core migrations.
  - Swagger UI for exploring endpoints.
- **Testing**:
  - Unit tests for all commands, queries, services, and controllers.
  - 100% code coverage.
- **Deployment Ready**:
  - Configurable `appsettings.json` (connection string, JWT secret, etc.).
  - HTTPS enforced.
  - Can be containerized via Docker if needed.

---

## 💾 Getting Started

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- SQL Server (LocalDB or full version)

### Installation
1. Clone the repository:
   ```bash
   git clone https://github.com/Shahinmm69/CustomerOrderBillingSystem.git
   cd cobs
   ```
2. Update database:
   ```bash
   dotnet ef database update
   ```
3. Run the application:
   ```bash
   dotnet run --project Cobs.Presentation
   ```

### Running Tests
```bash
dotnet test
```

### Access Swagger UI
```
https://localhost:7149/swagger/index.html
```

## 🩷 Follow Me!

You can connect with me on the social media and communication channels listed below.

[![LinkedIn][linkedin-shield]][linkedin-url]
[![Telegram][telegram-shield]][telegram-url]
[![WhatsApp][whatsapp-shield]][whatsapp-url]
[![Gmail][gmail-shield]][gmail-url]
![GitHub followers](https://img.shields.io/github/followers/Shahinmm69)

<p align="right">(<a href="#readme-top">back to top</a>)</p>


<!-- MARKDOWN LINKS & IMAGES -->
<!-- https://www.markdownguide.org/basic-syntax/#reference-style-links -->
[linkedin-shield]: https://img.shields.io/badge/-LinkedIn-black.svg?logo=linkedin&color=555
[linkedin-url]: https://www.linkedin.com/in/shahin-maboudi-moghaddam

[telegram-shield]: https://img.shields.io/badge/-Telegram-black.svg?logo=telegram&color=fff
[telegram-url]: https://t.me/Shahin_graff

[whatsapp-shield]: https://img.shields.io/badge/-WhatsApp-black.svg?logo=whatsapp&color=fff
[whatsapp-url]: https://wa.me/989304199911

[gmail-shield]: https://img.shields.io/badge/-Gmail-black.svg?logo=gmail&color=fff
[gmail-url]: mailto:s.maboudi69@gmail.com
