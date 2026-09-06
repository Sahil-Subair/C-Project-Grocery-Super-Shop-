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