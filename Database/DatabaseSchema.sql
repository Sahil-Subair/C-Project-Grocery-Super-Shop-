-- Database Schema for Grocery Super Shop Management System
-- Compatible with SQL Server / LocalDB

CREATE DATABASE GrocerySuperShopDB;
GO

USE GrocerySuperShopDB;
GO

-- 1. Users Table (Handles authentication and roles: Admin, Vendor, Customer)
CREATE TABLE Users (
    UserID INT IDENTITY(1,1) PRIMARY KEY,
    Username VARCHAR(50) NOT NULL UNIQUE,
    Password VARCHAR(100) NOT NULL,
    Role VARCHAR(20) NOT NULL, -- Options: 'Admin', 'Vendor', 'Customer'
    FullName VARCHAR(100),
    Contact VARCHAR(20),
    Email VARCHAR(100),
    Address TEXT
);
GO

-- 2. Products Table (Manages inventory items displayed in dashboards and details view)
CREATE TABLE Products (
    ProductID INT IDENTITY(1,1) PRIMARY KEY,
    ProductName VARCHAR(100) NOT NULL,
    Category VARCHAR(50) NOT NULL,
    Price DECIMAL(18,2) NOT NULL,
    Stock INT NOT NULL,
    Rating DECIMAL(3,1) DEFAULT 5.0,
    Status VARCHAR(20) DEFAULT 'Available'
);
GO

-- 3. Orders Table (Captures checkout data like Name, Contact, Address, Email, and Payment Method)
CREATE TABLE Orders (
    OrderID INT IDENTITY(1,1) PRIMARY KEY,
    CustomerName VARCHAR(100) NOT NULL,
    Contact VARCHAR(20) NOT NULL,
    Address TEXT NOT NULL,
    Email VARCHAR(100) NOT NULL,
    PaymentMethod VARCHAR(50) NOT NULL, -- 'Cash On Delivery', 'Bkash/Nagad', 'Credit/Debit Card'
    Subtotal DECIMAL(18,2) NOT NULL,
    DeliveryCharge DECIMAL(18,2) DEFAULT 5.00,
    TotalAmount DECIMAL(18,2) NOT NULL,
    OrderDate DATETIME DEFAULT GETDATE()
);
GO

-- 4. OrderItems Table (Stores items associated with each checkout transaction)
CREATE TABLE OrderItems (
    OrderItemID INT IDENTITY(1,1) PRIMARY KEY,
    OrderID INT FOREIGN KEY REFERENCES Orders(OrderID) ON DELETE CASCADE,
    ProductName VARCHAR(100) NOT NULL,
    Category VARCHAR(50),
    Amount INT NOT NULL,
    Price DECIMAL(18,2) NOT NULL
);
GO

-- ==========================================
-- Sample Test Data (For Run Verification & Viva)
-- ==========================================

-- Test Credentials matching Admin and Vendor roles
INSERT INTO Users (Username, Password, Role, FullName, Contact, Email, Address) VALUES
('admin', 'admin123', 'Admin', 'System Administrator', '01700000000', 'admin@grocery.com', 'Headquarters'),
('vendor', 'vendor123', 'Vendor', 'Store Vendor', '01800000000', 'vendor@grocery.com', 'Branch Store'),
('customer', 'cust123', 'Customer', 'John Doe', '01900000000', 'john@gmail.com', '123 Main Street');
GO

-- Sample Inventory Items
INSERT INTO Products (ProductName, Category, Price, Stock, Rating, Status) VALUES
('Fresh Milk', 'Dairy', 3.50, 50, 4.8, 'Available'),
('Whole Wheat Bread', 'Bakery', 2.80, 30, 4.5, 'Available'),
('Organic Apples', 'Fruits', 4.00, 100, 4.9, 'Available'),
('Free Range Eggs', 'Dairy', 5.20, 40, 4.7, 'Available');
GO