# 🛒 E-Commerce Payment System - C# Console App

This project is a simple yet powerful payment and transaction management system built using **C#** with a **SOLID**, modular architecture. It supports **admin/user roles**, **JSON-based data persistence**, and simulates features common to digital wallets or basic e-commerce platforms.

---

## 🚀 Features

### 👤 User Features
- Register and login with username/password
- View current balance
- Send money to other users
- View personal transaction history

### 🛡️ Admin Panel
- View all users and their balances
- Promote/Demote users to/from admin
- Delete users
- View all transactions
- View transaction history of any user

### 💾 Persistence
- All users and transactions are stored in:
  - `users.json`
  - `transactions.json`

---

## 🧱 Architecture

- **Clean code & SOLID principles**
- **Modular folders**:
  - `Core/Entities`: Business models (User, Transaction)
  - `Core/Interfaces`: Interfaces for services and repositories
  - `Infrastructure/Repositories`: Data layer (JSON I/O)
  - `Infrastructure/Services`: Business logic
  - `App`: Menu and console UI
- **Extensible** for features like password hashing, database, API integration

---

## 🔧 Setup & Run

### 1. Requirements
- .NET 6.0 SDK or newer

### 2. Build and Run

```bash
git clone https://github.com/your-repo/payment-system
cd payment-system
dotnet build
dotnet run
```
