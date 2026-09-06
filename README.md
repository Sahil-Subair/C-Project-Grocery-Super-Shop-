# Grocery Super Shop Management System

A comprehensive desktop application built with **C# Windows Forms** and **SQL Server (LocalDB)** designed to streamline grocery super shop operations, featuring customer management, product search, inventory checking, shopping cart functionalities, checkout workflows with single payment selection, order history, and administrative tools.

---

## Table of Contents

1. [Project Overview](#1-project-overview)
2. [Key Features & User Flow](#2-key-features--user-flow)
3. [Architecture & Design Strategy](#3-architecture--design-strategy)
4. [Database & Schema](#4-database--schema)
5. [Installation & Setup](#5-installation--setup)
6. [Screenshots](#6-screenshots)
7. [Video Demonstration](#7-video-demonstration)
8. [Project Report](#8-project-report)

---

## 1. Project Overview

## Project Overview

Think of the Grocery Super Shop Management System as a complete, end-to-end retail platform brought to life with C# Windows Forms and SQL Server. It’s designed to handle everything a real grocery business needs by organizing operations into three distinct, user-friendly areas.

For everyday shoppers, there’s an intuitive customer dashboard where they can easily browse products, check real-time stock availability, manage their shopping carts, and breeze through a secure checkout using a single, clear payment selection. Behind the scenes, individual store managers get a dedicated Shop Admin portal to update local product listings, maintain inventory levels, and monitor their store's orders. Meanwhile, the Super Admin console gives top-level administrators full control over vendor account approvals, business sales analytics, security suspensions, and a centralized hub to read and manage customer feedback. Holding the entire system together is a reliable relational database that ensures every product, user account, order, and review stays neatly tracked and secure.

---

## 2. Key Features & User Flow

* **Product Catalog & Search:** Instantly load and filter inventory items by Product ID or category name.
* **Stock Validation:** Real-time checking against shop inventory limits during ordering to prevent over-allocation.
* **Shopping Cart Management:** Dynamic addition, quantity management, and item removal via an in-memory data bridge (`CartManager`).
* **Secure Checkout Process:** Collects customer details (Name, Contact, Email, Address) alongside a structured payment selection via dropdown (Cash on Delivery, Bkash / Nagad, Credit / Debit Card) to avoid multi-selection errors.
* **Order History & Invoicing:** Generates automated summaries upon successful checkout with options to print invoices and submit shop reviews.

---

## 3. Architecture & Design Strategy

* **Language/Framework:** C#, .NET Windows Forms (`WinFormsApp1`)
* **Database Driver:** `System.Data.SqlClient`
* **UI Structure:** Multi-form architecture (`CustomerDashboard`, `ProductDetailsForm`, `CartDashboard`, `CheckoutForm`, `OrderHistoryDashboard`)
* **State Management:** Utilizes a static `CartManager` class with a persistent `DataTable` container to pass order lines smoothly across transaction forms.

---

## 4. Database & Schema

Run the following complete SQL Server script to create your database, tables, and seed initial records:

```sql
-- Create the database if it doesn't already exist
CREATE DATABASE GrocerySuperShopDB;
GO

USE GrocerySuperShopDB;
GO

-- 1. Products Table
CREATE TABLE Products (
    ProductID INT IDENTITY(1,1) PRIMARY KEY,
    ProductName VARCHAR(100) NOT NULL,
    Category VARCHAR(50) NOT NULL,
    Price DECIMAL(18,2) NOT NULL
);
GO

-- 2. Inventory Table (Tracks stock amounts for products)
CREATE TABLE Inventory (
    InventoryID INT IDENTITY(1,1) PRIMARY KEY,
    ProductID INT FOREIGN KEY REFERENCES Products(ProductID) ON DELETE CASCADE,
    StockAmount INT NOT NULL
);
GO

-- 3. Reviews Table (Stores feedback/reviews submitted from OrderHistoryDashboard)
CREATE TABLE Reviews (
    ReviewID INT IDENTITY(1,1) PRIMARY KEY,
    ShopName VARCHAR(100) NOT NULL,
    ReviewText VARCHAR(MAX) NOT NULL,
    ReviewDate DATETIME DEFAULT GETDATE()
);
GO

-- 4. Orders Table (Stores customer checkout and shipping details)
CREATE TABLE Orders (
    OrderID INT IDENTITY(1,1) PRIMARY KEY,
    FullName VARCHAR(100) NOT NULL,
    Contact VARCHAR(50) NOT NULL,
    Email VARCHAR(100) NOT NULL,
    ShippingAddress VARCHAR(255) NOT NULL,
    PaymentMethod VARCHAR(50) NOT NULL,
    OrderDate DATETIME DEFAULT GETDATE(),
    TotalAmount DECIMAL(18,2) NOT NULL
);
GO

-- 5. OrderItems Table (Stores the individual products linked to each order)
CREATE TABLE OrderItems (
    OrderItemID INT IDENTITY(1,1) PRIMARY KEY,
    OrderID INT FOREIGN KEY REFERENCES Orders(OrderID) ON DELETE CASCADE,
    ProductID INT FOREIGN KEY REFERENCES Products(ProductID),
    Quantity INT NOT NULL,
    UnitPrice DECIMAL(18,2) NOT NULL
);
GO

-- Initial Sample Data to populate your Product & Inventory grids
INSERT INTO Products (ProductName, Category, Price) VALUES 
('Organic Milk', 'Dairy', 3.50),
('Whole Wheat Bread', 'Bakery', 2.50),
('Fresh Apples', 'Produce', 4.00);
GO

INSERT INTO Inventory (ProductID, StockAmount) VALUES 
(1, 50),
(2, 30),
(3, 100);
GO

```
## 5.Installation & Setup

1. **Clone or Download** this repository to your local machine.
2. Open **SQL Server Management Studio (SSMS)** or Visual Studio SQL Server Object Explorer and execute the database schema script to create your database and tables.
3. Open the solution file in **Visual Studio**.
4. Verify your connection string matches your local environment:
   ```csharp
   private string connString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=GrocerySuperShopDB;Integrated Security=True";

## 6.Screenshots
<img width="269" height="215" alt="Login Form" src="https://github.com/user-attachments/assets/c1aca6af-94b5-4404-a9ba-332dfbd70c7b" />

<img width="269" height="260" alt="Register Form" src="https://github.com/user-attachments/assets/6f010f63-7a65-4ddd-a57c-ee9e4067e1b4" />

<img width="640" height="362" alt="Super Admin Dashboard" src="https://github.com/user-attachments/assets/aef85180-e272-4744-b02b-b33db8480b9e" />

<img width="470" height="275" alt="Amin Approvals Dashboard" src="https://github.com/user-attachments/assets/bb45c545-bad5-4538-87b6-1dfb4931c983" />

<img width="581" height="271" alt="Sales Dashboard" src="https://github.com/user-attachments/assets/a333e060-fa6d-4bfa-b56e-a838cb704ad4" />

<img width="509" height="302" alt="Manage Reviews Dashboard" src="https://github.com/user-attachments/assets/a2b7563f-5945-41e2-a8a6-685e9813e316" />

<img width="619" height="377" alt="Admin Dashboard" src="https://github.com/user-attachments/assets/77b3fd7c-6bb9-42aa-baf2-8b545cf60b62" />

<img width="245" height="229" alt="Add new product Dashboard" src="https://github.com/user-attachments/assets/ae36c5d7-c178-4081-95e9-9771e0056341" />

<img width="507" height="301" alt="Inventory Dashboard" src="https://github.com/user-attachments/assets/693edd89-dd71-4599-a022-54b985ce3dbf" />

<img width="245" height="197" alt="Order Product Dashboard" src="https://github.com/user-attachments/assets/1f41ce92-7bfd-4293-a689-ceebc48484dd" />

<img width="500" height="283" alt="Shop Sales Dashboard" src="https://github.com/user-attachments/assets/0de6c090-c96e-4c79-9085-f3d17632b44d" />

<img width="511" height="308" alt="Offers Dashboard" src="https://github.com/user-attachments/assets/18329a17-b97a-463c-b8b7-b1f14db795f5" />

<img width="244" height="212" alt="Add Offer Dashboard" src="https://github.com/user-attachments/assets/c2e5d650-9846-4d8f-9555-b3019e211286" />

<img width="579" height="365" alt="Customer Dashboard" src="https://github.com/user-attachments/assets/b7d00280-1265-4215-acc0-7b7aa13717b3" />

<img width="267" height="260" alt="Product Details Dashboard" src="https://github.com/user-attachments/assets/6a10535a-2dc3-4e5a-8a6e-faa467c8b00a" />

<img width="473" height="305" alt="Cart Dashboard" src="https://github.com/user-attachments/assets/f8815f9c-9453-4cc2-937e-16d2430e434e" />

<img width="471" height="441" alt="Checkout Dashboard" src="https://github.com/user-attachments/assets/cd72f2fa-567c-4b5c-b227-66b79bd7c787" />

<img width="466" height="345" alt="Order History and Invoice Dashboard" src="https://github.com/user-attachments/assets/46b4a493-2283-4f18-bbb8-1e551e62f0b5" />

<img width="245" height="184" alt="Add Review Dashboard" src="https://github.com/user-attachments/assets/8473fec9-b7bd-4f48-be08-c955ff2d8a18" />

## 7.Video Demonstration

https://github.com/user-attachments/assets/a0107a10-bb0b-491e-a84e-9e7df12255c7

## 8.Project Report



