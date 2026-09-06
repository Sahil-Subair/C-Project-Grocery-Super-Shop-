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