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

The Grocery Super Shop System provides an intuitive interface for customers to browse items, check available stock, manage their shopping carts, and place secure orders using precise payment options. It bridges frontend Windows Forms components directly with a relational SQL database to persist data reliably.

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
