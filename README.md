# Grocery Super Shop Management System

A desktop-based e-commerce and inventory management application built using **C# Windows Forms (.NET)** with a code-driven UI architecture.

---

## 📋 Table of Contents
1. [Project Overview](#-project-overview)
2. [Key Features & User Flow](#-key-features--user-flow)
3. [Architecture & Design Strategy](#-architecture--design-strategy)
4. [Database & Schema](#-database--schema)
5. [Installation & Setup](#-installation--setup)
6. [Screenshots](#-screenshots)
7. [Video Demonstration](#-video-demonstration)
8. [Project Report](#-project-report)
9. [Author & Contributions](#-author--contributions)

---

## 🛒 1. Project Overview
The **Grocery Super Shop Management System** streamlines online grocery ordering and store administration. It provides secure user authentication, product search and filtering, dynamic item quantity selection, a fully functional shopping cart, and a complete e-commerce checkout workflow.

---

## 🚀 2. Key Features & User Flow
* **Authentication**: Secure Login and registration system supporting role-based dashboards.
* **Customer Dashboard**: Browse products, view details, search items, and check current stock.
* **Item Details**: Inspect product attributes (Category, Price, Rating, Status) and choose a specific purchase quantity via a dynamic `NumericUpDown` counter.
* **Shopping Cart (`CartForm`)**: View selected items in a grid layout, track real-time subtotal calculations, and remove unwanted entries.
* **Checkout Flow (`CheckoutForm`)**: 
  * Captures customer information (Name, Contact, Address, E-mail).
  * Payment method selection (**Cash On Delivery**, **Bkash/Nagad**, **Credit/Debit Card**).
  * Automated financial calculation: **Subtotal + $5.00 Delivery Charge**.
  * Order summary preview table and automated return to the dashboard upon successful placement ("ORDER PLACED").

---

## 🏛️ 3. Architecture & Design Strategy
* **Code-Behind UI Pattern**: To eliminate Visual Studio Designer synchronization errors and maintain strict control over layout bounds, all controls (Labels, Textboxes, Buttons, DataGridViews, NumericUpDowns) are instantiated, positioned, and event-wired programmatically in the form code-behind files (`.cs`).
* **In-Memory Data Handling**: Utilizes `DataTable` structures for dynamic data binding across the cart and checkout interfaces.

---

## 🗄️ 4. Database & Schema
* **Engine**: SQL Server / LocalDB.
* **Schema File**: Available in the repository as `DatabaseSchema.sql`.
* **Key Queries**: Implements parameterized CRUD operations, filtering queries, and aggregation logic for order totals.

---

## ⚙️ 5. Installation & Setup
1. Clone the repository:
   ```bash
   git clone [https://github.com/YOUR_USERNAME/YOUR_REPOSITORY.git](https://github.com/YOUR_USERNAME/YOUR_REPOSITORY.git)
