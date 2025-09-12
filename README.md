# 🏦 OnlineBank — ASP.NET Core Banking System

> A hands-on banking simulation built with ASP.NET Core MVC and Entity Framework.  

---

## 📌 Overview

**OnlineBank** is a learning-driven project that simulates core banking operations:

- 🔐 User authentication and session management
- 💳 Card creation, balance tracking, and CVV validation
- 💸 Transaction processing between cards
- 📊 Transaction history and wallet views
- 🧠 Type-safe CVV struct with EF Core `ValueConverter`
- 🧱 Architecture evolving toward SOLID and DI

---

## 🧩 Technologies Used

| Technology           | Purpose                                |
|----------------------|----------------------------------------|
| ASP.NET Core MVC     | Web framework                          |
| Entity Framework Core| ORM for database access                |
| SQL Server           | Relational database                    |
| Session Storage      | User state management                  |
| Custom Structs       | Domain modeling (e.g. `CardCVV`)       |
| ValueConverter       | EF Core integration for custom types   |

---

## 🧠 Architectural Highlights

- **Domain-first modeling**: `CardCVV` as a `readonly struct` with regex validation
- **Service layer**: Services like `UserService`, `CardService`, and `TransactionService` encapsulate business logic
- **Session-based auth**: Lightweight user tracking via `HttpContext.Session`
- **Precision control**: Financial fields use `.HasPrecision(18, 2)` for accuracy
- **Validation-first mindset**: Attributes and custom types enforce data integrity

---

## 🚀 Getting Started

1. Clone the repository  
   `git clone https://github.com/DEV-CMD-dev/OnlineBank.git`

2. Configure your connection string  
   Use `secret.json`, environment variables, or `appsettings.Development.json`

3. Apply database migrations  
   `dotnet ef database update`

4. Run the project  
   `dotnet run`
