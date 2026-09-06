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