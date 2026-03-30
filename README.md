# ✨ Quantity Measurement Application (UC1–UC18)

A **layered, enterprise-grade .NET backend system** for performing quantity operations (Length, Weight, Volume, Temperature) with full **database persistence, REST APIs, validation, and audit tracking**.

---

# 🚀 Tech Stack

* 💻 **.NET 8 (ASP.NET Core Web API)**
* 🧠 **C# (OOP + DSA concepts)**
* 🗄️ **SQL Server (ADO.NET → EF Core)**
* 🔗 **Layered Architecture (N-Tier)**
* 📦 **Swagger (API Testing)**
* 🛡️ **Global Exception Middleware**

---

# 🏗️ Architecture Overview

```
Presentation Layer (Controllers / API)
        ↓
Business Layer (Services / Logic)
        ↓
Repository Layer (EF Core)
        ↓
Database (SQL Server)
```

---

# 📂 Project Structure

```
QuantityMeasurementApp
│
├── QuantityMeasurementConsole        → API Layer (Controllers + Middleware)
├── QuantityMeasurementBusinessLayer → Business Logic
├── QuantityMeasurementRepository    → Data Access (EF Core)
├── QuantityMeasurementModel         → DTOs & Entities
├── QuantityMeasurementTests         → Unit Tests
```

---

# ⚙️ Features Implemented

# 📘 UC1 – UC14 (Core Quantity Measurement System)

---

## 🔹 UC1: Basic Length Conversion

* Convert between:

  * Feet ↔ Inches
  * Yard ↔ Feet
* Established base unit concept (Inches)

✅ Introduced:

* Unit normalization logic
* Conversion factors

---

## 🔹 UC2: Equality Comparison

* Compare two quantities after converting to base unit

```text
1 ft == 12 inch → TRUE
```

✅ Introduced:

* Precision comparison (`Math.Abs`)
* Floating-point tolerance handling

---

## 🔹 UC3: Additional Length Units

* Added:

  * Yard
  * Centimeter

✅ Extended:

* Conversion system
* Unit mapping logic

---

## 🔹 UC4: Length Addition

* Add two quantities of same type

```text
1 ft + 2 inch → 14 inch → converted result
```

✅ Introduced:

* Arithmetic on normalized base values

---

## 🔹 UC5: Generic Unit Conversion

* Unified conversion logic across:

  * Length
  * Weight
  * Volume

✅ Introduced:

* Generic conversion method
* Switch-based unit handling

---

## 🔹 UC6: Weight Measurement Support

* Units:

  * Gram
  * Kilogram
  * Tonne

✅ Introduced:

* Multi-category support
* Base unit = grams

---

## 🔹 UC7: Volume Measurement Support

* Units:

  * Litre
  * Millilitre
  * Gallon

✅ Introduced:

* Volume conversion system
* Real-world unit mapping

---

## 🔹 UC8: Temperature Conversion

* Units:

  * Celsius
  * Fahrenheit
  * Kelvin

⚠️ Special Handling:

* Temperature uses formula-based conversion (NOT linear like others)

✅ Introduced:

* Non-linear conversion logic

---

## 🔹 UC9: Restriction on Temperature Arithmetic

* Prevent invalid operations:

```text
Temperature + Temperature ❌
```

✅ Introduced:

* Business rule validation
* Domain constraints

---

## 🔹 UC10: Subtraction Operation

* Subtract two quantities

```text
10 ft - 2 ft → 8 ft
```

✅ Extended:

* Arithmetic operations support

---

## 🔹 UC11: Division Operation

* Divide quantities

```text
10 ft / 2 ft → 5
```

⚠️ Edge Case:

* Division by zero handling

✅ Introduced:

* Exception handling

---

## 🔹 UC12: Refactoring with Interfaces

* Introduced abstraction:

```csharp
IMeasurable
```

✅ Achieved:

* Loose coupling
* Extensible unit system

---

## 🔹 UC13: Generic Quantity<T> Architecture

* Replaced primitive handling with:

```csharp
Quantity<TUnit>
```

✅ Benefits:

* Type safety
* Compile-time checks
* Clean extensibility

---

## 🔹 UC14: Advanced Conversion System

* Pattern matching instead of dynamic
* Clean separation of:

  * ConvertToBase()
  * ConvertFromBase()

✅ Introduced:

* Strong architecture design
* Scalable conversion engine

---

# 🧠 Core Concepts Mastered (UC1–UC14)

✔ OOP Principles (Abstraction, Encapsulation)
✔ Generic Programming
✔ Domain-driven logic
✔ Clean conversion architecture
✔ Edge case handling
✔ Precision control

---

## 🔹 UC15 (N-Tier Architecture)

* Separation of concerns
* Interface-based design
* Dependency Injection
* Cache repository (in-memory)

---

## 🔹 UC16 (Database Integration - ADO.NET)

* SQL Server integration
* Parameterized queries (SQL Injection safe)
* Persistent storage of operations
* Audit logging
* CRUD operations
* Transaction-ready design

---

## 🔹 UC17 (REST API + EF Core Refactor)

* ASP.NET Core Web API (RESTful endpoints)
* Swagger UI integration for API testing
* DTO-based request/response handling
* Global Exception Handling Middleware
* Clean API response structure
* Validation using Data Annotations

### 🔥 Major Upgrade

* ❌ Removed ADO.NET dependency for API operations
* ✅ Introduced **Entity Framework Core (ORM)**

---

### 🧱 Architecture Flow

```
Controller → Service → Repository → Database
```

---

### 🗄️ EF Core Integration

* `ApplicationDbContext` implemented
* `DbSet<QuantityMeasurementEntity>` used
* LINQ-based queries (no raw SQL)
* Migration-based DB creation
* SQL Server LocalDB integration

---

### 📦 Repository Layer

* Interface: `IQuantityMeasurementRepository`
* Implementation: `QuantityMeasurementRepository`

Supports:

* Save logs
* Fetch history
* Filter by operation
* Count operations

---

### 🧠 Service Layer Enhancements

* Refactored for API usage
* Central logging via `SaveLog()`
* Exception-safe logic
* Handles success + failure cases

---

### 📊 Logging System 🔥

| Operation | Input      | Result | Status  |
| --------- | ---------- | ------ | ------- |
| Add       | 2ft + 24in | 4ft    | Success |
| Convert   | 1ft → in   | 12in   | Success |
| Divide    | 10 / 0     | Error  | Failed  |

---

### 🛡️ Exception Handling

* Global middleware
* Structured error responses
* Prevents API crashes

---

### 📡 API Capabilities

* Arithmetic operations
* Conversion system
* History tracking
* Operation filtering
* Aggregation (count)

---

### ⚙️ Configuration

* `appsettings.json` for DB connection
* DI configured in `Program.cs`
* DbContext + Repository registered

---

### 🧪 API Testing

* Swagger UI enabled
* All endpoints functional

---

### 🧠 Problems Solved

✔ Namespace conflicts
✔ Multi-project setup
✔ EF Core integration
✔ Migrations setup
✔ DTO validation issues
✔ Null constraint handling
✔ Broken project references

---

### 🏁 Outcome

✔ Fully functional Web API
✔ EF Core ORM integrated
✔ Real database persistence
✔ Logging system
✔ Clean architecture
✔ Production-ready backend

---

# 📊 Database Design

## 🧾 QuantityMeasurements Table

| Column        | Description             |
| ------------- | ----------------------- |
| OperationId   | Unique ID               |
| OperationType | Add / Convert / Compare |
| InputType     | Length / Weight / etc   |
| OutputType    | Target type             |
| InputData     | Input JSON              |
| ResultData    | Output JSON             |
| IsSuccess     | Operation status        |
| ErrorMessage  | Error if failed         |
| CreatedAt     | Timestamp               |

---

## 🕵️ Audit Table

| Column      | Description      |
| ----------- | ---------------- |
| AuditId     | Auto increment   |
| OperationId | Linked operation |
| ActionType  | INSERT / DELETE  |
| OldValue    | Before state     |
| NewValue    | After state      |
| ChangedAt   | Timestamp        |

---

# 🌐 API Endpoints

## 🔹 Operations

| Method | Endpoint                 |
| ------ | ------------------------ |
| POST   | `/api/quantity/compare`  |
| POST   | `/api/quantity/add`      |
| POST   | `/api/quantity/subtract` |
| POST   | `/api/quantity/divide`   |
| POST   | `/api/quantity/convert`  |

---

## 🔹 History & Analytics

| Method | Endpoint                                 |
| ------ | ---------------------------------------- |
| GET    | `/api/quantity/history`                  |
| GET    | `/api/quantity/history/operation/{type}` |
| GET    | `/api/quantity/count/{type}`             |

---

# 📦 Sample Request

```json
{
  "first": {
    "value": 1,
    "unit": "Feet",
    "measurementType": "length"
  },
  "second": {
    "value": 12,
    "unit": "Inches",
    "measurementType": "length"
  }
}
```

---

# 📤 Sample Response

```json
{
  "success": true,
  "data": 2,
  "message": "Addition successful"
}
```

---

# 🛡️ Error Handling

* Global exception middleware
* Proper HTTP status codes (400 / 500)
* Structured error responses

---

# 🔥 Key Highlights

✔ Clean layered architecture
✔ EF Core ORM implemented
✔ Fully working REST API
✔ Database + Logging system
✔ Validation + Exception handling
✔ Swagger integration

---

🔐 UC18: Google Authentication & JWT Security Integration
🎯 Objective

Enhance the Quantity Measurement API with secure authentication and authorization mechanisms using:

JWT (JSON Web Tokens)
Google OAuth 2.0
API Security using Authorization Middleware
🧠 Key Concepts Covered
🔑 JWT Token Generation & Validation
🔐 REST API Security
🌐 Google OAuth 2.0 Authentication
🛡️ Authorization using [Authorize]
🔒 Secure API Access Control
🔑 Token-based Authentication Flow
🏗️ Implementation Overview
1️⃣ JWT Authentication Setup
Installed package:
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer --version 8.0.0
Configured JWT in Program.cs
Added middleware:
app.UseAuthentication();
app.UseAuthorization();
2️⃣ appsettings.json Configuration
"Jwt": {
  "Key": "THIS_IS_MY_SUPER_SECRET_KEY_1234567890",
  "Issuer": "QuantityMeasurementAPI",
  "Audience": "QuantityMeasurementUsers"
}
3️⃣ AuthController Implementation

Created authentication endpoints:

Endpoint	Description
/api/Auth/login	Generates JWT using email & password
/api/Auth/google	Accepts Google ID token and returns JWT
4️⃣ Google OAuth Integration
Used:
GoogleJsonWebSignature.ValidateAsync(idToken)
Extracted user info from payload
Generated internal JWT token after validation
5️⃣ JWT Token Generation

Implemented in UserService:

Created claims
Used SymmetricSecurityKey
Generated signed token using HmacSha256
6️⃣ Securing APIs

All endpoints in QuantityController are protected:

[Authorize]

👉 Only authenticated users can access:

Add
Subtract
Compare
Convert
History APIs
7️⃣ Swagger Authentication Support
Added Bearer token support in Swagger
Users can test secured APIs using:
Bearer <JWT_TOKEN>
🔄 Authentication Flow
User → Login / Google Auth → Receive JWT → 
Attach Token → Access Protected APIs
🧪 Testing
🔹 Login
POST /api/Auth/login
{
  "email": "user@gmail.com",
  "password": "123456"
}
🔹 Google Authentication
POST /api/Auth/google
{
  "idToken": "GOOGLE_TOKEN"
}
🔹 Access Protected API
Authorization: Bearer YOUR_JWT_TOKEN

---
🏁 Outcome

✔ Secure API with JWT authentication
✔ Google login integration working
✔ All endpoints protected
✔ Token-based access implemented
✔ Industry-level backend security achieved

---

# 👨‍💻 Author

**Gulshan Thakur**

---

# 😈 Final Status

```text
UC1 → UC18 COMPLETED ✅
```
