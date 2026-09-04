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

---

##  1. Project Overview
The **Grocery Super Shop Management System** streamlines online grocery ordering and store administration. It provides secure user authentication, product search and filtering, dynamic item quantity selection, a fully functional shopping cart, and a complete e-commerce checkout workflow.

---

##  2. Key Features & User Flow
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

##  3. Architecture & Design Strategy
* **Code-Behind UI Pattern**: To eliminate Visual Studio Designer synchronization errors and maintain strict control over layout bounds, all controls (Labels, Textboxes, Buttons, DataGridViews, NumericUpDowns) are instantiated, positioned, and event-wired programmatically in the form code-behind files (`.cs`).
* **In-Memory Data Handling**: Utilizes `DataTable` structures for dynamic data binding across the cart and checkout interfaces.

---

##  4. Database & Schema
* **Engine**: SQL Server / LocalDB.
* **Schema File**: Available in the repository as `DatabaseSchema.sql`.
* **Key Queries**: Implements parameterized CRUD operations, filtering queries, and aggregation logic for order totals.

---

##  5. Installation & Setup
1. Clone the repository:
 
---

##  6. Screenshots

<img width="303" height="304" alt="Login Form" src="https://github.com/user-attachments/assets/46762db3-e3ff-481b-9c4e-5ff67631f5f0" />

<img width="407" height="375" alt="Register Form" src="https://github.com/user-attachments/assets/16af5902-2dc2-4008-b52c-7973f37dabce" />

<img width="799" height="480" alt="Super Admin Dashboard" src="https://github.com/user-attachments/assets/ce3095fc-5d47-4a12-99f1-0b0f6734ba0b" />

<img width="798" height="480" alt="Admin Dashboard" src="https://github.com/user-attachments/assets/add01d79-07c4-4263-9c66-c12211f49f94" />

<img width="798" height="480" alt="Offers Dashboard" src="https://github.com/user-attachments/assets/cfc426de-e453-446d-9a0e-deafd589aec4" />

<img width="783" height="541" alt="Customer Dashboard" src="https://github.com/user-attachments/assets/5095c179-38a7-4008-997e-a2cac7b863ef" />

<img width="775" height="531" alt="Item Details Dashboard" src="https://github.com/user-attachments/assets/82b03fa5-e297-462d-9f15-16a3063d9a7d" />

<img width="633" height="483" alt="Shopping Cart Dashboard" src="https://github.com/user-attachments/assets/c09254ac-89fa-49ed-b25e-696b3ee8ebe3" />

<img width="533" height="691" alt="Checkout Dashboard" src="https://github.com/user-attachments/assets/d31b89cf-3f5b-431c-870d-1b6fb313d188" />


---

##  7. Video Demonstration



https://github.com/user-attachments/assets/418a609d-77dc-48a0-85e4-395fe4eaef37


---

##  8. Project Report

[Project Report.pdf](https://github.com/user-attachments/files/31845198/Project.Report.pdf)



