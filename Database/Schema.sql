CREATE DATABASE GrocerySuperShopDB;
GO

USE GrocerySuperShopDB;
GO

CREATE TABLE Users (
    UserID INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(100) NOT NULL UNIQUE,
    FullName NVARCHAR(150) NOT NULL,
    Password NVARCHAR(100) NOT NULL,
    Role NVARCHAR(50) NOT NULL -- 'Admin' or 'Customer'
);
GO

-- Seed default admin account
INSERT INTO Users (Username, FullName, Password, Role) 
VALUES ('admin', 'System Administrator', 'admin123', 'Admin');
GO


USE GrocerySuperShopDB;
GO

-- Drop existing tables to refresh the schema cleanly
DROP TABLE IF EXISTS SalesData;
DROP TABLE IF EXISTS Users;
GO

CREATE TABLE Users (
    UserID INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(100) NOT NULL UNIQUE,
    FullName NVARCHAR(150) NOT NULL,
    Password NVARCHAR(100) NOT NULL,
    Role NVARCHAR(50) NOT NULL, -- 'SuperAdmin', 'Admin', 'Customer'
    IsApproved INT DEFAULT 1     -- 0 = Pending Approval, 1 = Approved
);
GO

CREATE TABLE SalesData (
    SaleID INT IDENTITY(1,1) PRIMARY KEY,
    ShopName NVARCHAR(150) NOT NULL,
    WeeklySales DECIMAL(18,2) DEFAULT 0,
    MonthlySales DECIMAL(18,2) DEFAULT 0,
    YearlySales DECIMAL(18,2) DEFAULT 0,
    CommissionEarned DECIMAL(18,2) DEFAULT 0,
    TaxPayed DECIMAL(18,2) DEFAULT 0
);
GO

-- Seed Default Super Admin account (pre-approved)
INSERT INTO Users (Username, FullName, Password, Role, IsApproved) 
VALUES ('superadmin', 'Super Administrator', 'admin123', 'SuperAdmin', 1);
GO

-- Seed some sample sales data for the Sales dashboard
INSERT INTO SalesData (ShopName, WeeklySales, MonthlySales, YearlySales, CommissionEarned, TaxPayed)
VALUES 
('Green Valley Grocery', 12500.00, 48000.00, 560000.00, 28000.00, 8400.00),
('Fresh Mart Express', 15200.50, 62100.00, 710000.00, 35500.00, 10650.00);
GO


USE GrocerySuperShopDB;
GO

-- Drop tables if they already exist to reset cleanly
DROP TABLE IF EXISTS Offers;
DROP TABLE IF EXISTS ProductSales;
DROP TABLE IF EXISTS Inventory;
DROP TABLE IF EXISTS Products;
GO

-- 1. Core Products Table
CREATE TABLE Products (
    ProductID INT IDENTITY(1,1) PRIMARY KEY,
    ProductName NVARCHAR(150) NOT NULL,
    Category NVARCHAR(100) NOT NULL,
    Price DECIMAL(18,2) NOT NULL
);
GO

-- 2. Inventory Table (Stock)
CREATE TABLE Inventory (
    InventoryID INT IDENTITY(1,1) PRIMARY KEY,
    ProductID INT FOREIGN KEY REFERENCES Products(ProductID) ON DELETE CASCADE,
    StockAmount INT NOT NULL
);
GO

-- 3. Product Sales Table
CREATE TABLE ProductSales (
    SaleRecordID INT IDENTITY(1,1) PRIMARY KEY,
    ProductID INT FOREIGN KEY REFERENCES Products(ProductID) ON DELETE CASCADE,
    TotalSold INT NOT NULL,
    TotalEarned DECIMAL(18,2) NOT NULL
);
GO

-- 4. Offers Table
CREATE TABLE Offers (
    OfferID INT IDENTITY(1,1) PRIMARY KEY,
    OfferTitle NVARCHAR(150) NOT NULL,
    DiscountPercentage INT NOT NULL,
    ValidUntil DATE NOT NULL
);
GO

-- Seed Sample Data
INSERT INTO Products (ProductName, Category, Price) VALUES ('Organic Milk 1L', 'Dairy', 2.50);
INSERT INTO Products (ProductName, Category, Price) VALUES ('Whole Wheat Bread', 'Bakery', 1.80);
INSERT INTO Products (ProductName, Category, Price) VALUES ('Fresh Apples (1kg)', 'Produce', 3.00);
GO

INSERT INTO Inventory (ProductID, StockAmount) VALUES (1, 150);
INSERT INTO Inventory (ProductID, StockAmount) VALUES (2, 80);
INSERT INTO Inventory (ProductID, StockAmount) VALUES (3, 200);
GO

INSERT INTO ProductSales (ProductID, TotalSold, TotalEarned) VALUES (1, 320, 800.00);
INSERT INTO ProductSales (ProductID, TotalSold, TotalEarned) VALUES (2, 140, 252.00);
INSERT INTO ProductSales (ProductID, TotalSold, TotalEarned) VALUES (3, 410, 1230.00);
GO

INSERT INTO Offers (OfferTitle, DiscountPercentage, ValidUntil) VALUES ('Weekend Dairy Bash', 15, '2026-12-31');
GO




-- Create Reviews table if it doesn't exist yet
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Reviews' and xtype='U')
CREATE TABLE Reviews (
    ReviewID INT IDENTITY(1,1) PRIMARY KEY,
    ShopName NVARCHAR(100) NOT NULL,
    ReviewText NVARCHAR(MAX) NOT NULL
);

-- Insert 10 sample reviews
INSERT INTO Reviews (ShopName, ReviewText) VALUES
('Green Valley Grocery', 'The organic vegetables and fresh fruits at Green Valley are always top-notch and reasonably priced.'),
('Green Valley Grocery', 'Great neighborhood market, but the checkout queue can get a bit long during evening hours.'),
('Daily Fresh SuperShop', 'Found all my weekly grocery essentials in one go. The imported snacks section is amazing!'),
('Daily Fresh SuperShop', 'Very clean aisles and polite staff. Always a pleasant shopping experience here.'),
('City Supermart', 'The fresh meat and poultry counter is exceptionally clean. Best quality in town.'),
('City Supermart', 'Prices on household cleaning supplies are very competitive compared to other local stores.'),
('Mega Mart Express', 'Open late hours which is a lifesaver! Got what I needed right before midnight.'),
('Mega Mart Express', 'The dairy products are always well-stocked and fresh. Highly recommended.'),
('Corner Bazaar', 'A cozy little grocery shop with friendly owners. They always have fresh bread every morning.'),
('Corner Bazaar', 'Good variety of spices and local condiments. Very convenient location.');