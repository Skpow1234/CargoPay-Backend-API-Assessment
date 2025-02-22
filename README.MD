# CargoPay Backend API Assessment 🚀

## **📌 Project Overview**

CargoPay-Backend-API-Assessment is a **RESTful API** built with **ASP.NET Core 8**, designed to manage a **card-based payment system**. It provides functionalities for card creation, payments, balance retrieval, and authentication. The project follows **Clean Architecture principles** and has been fully implemented to meet the requirements outlined in the provided assessment document.

✅ **Assessment Completion: 100%** 🎉

---

## **📢 Features**

### **✅ Card Management Module (60K Points)**

- [x] **Create a Card** with a **15-digit card number** and initial balance.
- [x] **Make a Payment** with fee calculations applied dynamically.
- [x] **Retrieve Card Balance** by card number or card ID.
- [x] **Get All Cards** with detailed information.
- [x] **Recharge Card Balance** securely.
- [x] **Delete a Card** (Restricted to Admins).

### **✅ Payment Fees Module (40K Points)**

- [x] **Implemented Universal Fees Exchange (UFE) Singleton**.
- [x] **Fee updates dynamically every hour.**
- [x] **Thread-safe fee retrieval** to avoid race conditions.
- [x] **Applied fee dynamically to every payment transaction.**

### **✅ Bonus Features (30K Points)**

- [x] **Performance Optimization** using **caching for balance retrieval**.
- [x] **JWT Authentication & Authorization** with **Role-Based Access Control (RBAC)**.
- [x] **Swagger API Documentation Enabled** for easy testing.
- [x] **Thread-Safe Shared Resources** for handling concurrency.

---

## **🛠️ Installation & Setup**

### **1️⃣ Clone the Repository**

```sh
git clone https://github.com/Skpow1234/CargoPay-Backend-API-Assessment.git
cd CargoPay-Backend-API-Assessment
```

### **2️⃣ Configure Environment Variables**

Create an **appsettings.json** file in `CargoPay.Presentation`:

```json
{
  "Jwt": {
    "Key": "YOUR_SECRET_KEY_HERE",
    "Issuer": "CargoPayIssuer",
    "Audience": "CargoPayAudience"
  },
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=CargoPayDB;Trusted_Connection=True;"
  }
}
```

### **3️⃣ Run Database Migrations**

```sh
dotnet ef database update --project CargoPay.Infrastructure --startup-project CargoPay.Presentation
```

### **4️⃣ Build & Run the Project**

```sh
dotnet build
dotnet run --project CargoPay.Presentation
```

---

## **📖 API Documentation**

📌 **Swagger UI is available at:**  
👉 `http://localhost:5000/swagger`

### **🛡️ Authentication (JWT)**

#### 🔹 `POST /api/auth/login`

```json
{
  "username": "admin",
  "password": "password"
}
```

📌 **Returns a JWT Token** for authentication.

### **💳 Card Management**

#### 🔹 `POST /api/cards/create`

```json
{
  "cardNumber": "123456789012345",
  "balance": 100.00
}
```

```json
📌 Creates a new card.

 🔹 `POST /api/cards/pay`

```

```json
{
  "cardNumber": "123456789012345",
  "amount": 50.00
}
```

📌 **Processes a payment and applies a dynamic fee.**

#### 🔹 `GET /api/cards/by-number/{cardNumber}/balance`

📌 **Retrieves the current balance of a card.**

#### 🔹 `GET /api/cards/cards`

📌 **Retrieves all registered cards.**

### **🔋 Payment Fees & Transactions**

#### 🔹 `GET /api/fees/current`

📌 **Retrieves the current transaction fee.**

#### 🔹 `POST /api/cards/recharge-balance`

```json
{
  "cardNumber": "123456789012345",
  "amount": 20.00
}
```

📌 **Recharges the balance of a card.**

#### 🔹 `DELETE /api/cards/{id}` *(Admin Only)*

📌 **Deletes a card (Restricted to Admin users).**

---

## **🔒 Security & Authentication**

- Uses **JWT Authentication** for secure access.
- Implements **Role-Based Access Control (RBAC)** (Admin/User roles).
- Protects **critical endpoints with `[Authorize(Roles = "Admin")]`**.

---

## **🛠️ Technologies Used**

- **.NET 8 (ASP.NET Core Web API)** 🚀
- **Entity Framework Core + SQL Server** 🗄️
- **FluentValidation** for request validation ✅
- **JWT Authentication** 🔐
- **Background Services (`FeeUpdateService`)** for fee updates ⏳
- **Swagger** for API Documentation 📜
- **Caching (IMemoryCache)** for performance optimization ⚡
- **Clean Architecture Principles** 🏗️

---

🛠️ Docker Setup (For Investigation Purposes)

📌 This section is NOT part of the assessment but is included for investigation purposes.

```bash
1️⃣ Build & Run Using Docker Compose

docker-compose up --build -d

2️⃣ Verify Running Containers

docker ps

3️⃣ Check API Logs

docker logs -f cargo-pay-api

4️⃣ Stop & Remove Containers

docker-compose down
```

---

## **📌 Conclusion**

✅ **CargoPay-Backend-API-Assessment is 100% complete!** 🎯
✅ **Fully meets all assessment requirements.** 🏆
✅ **Includes all bonus optimizations.** 🔥
