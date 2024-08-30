# SaOnlineMart

SaOnlineMart is a web application for online shopping.

## Setup Instructions

### Prerequisites
- [.NET Core SDK](https://dotnet.microsoft.com/download) (Version 6.0 or higher)
- [MySQL](https://www.mysql.com/downloads/) (Version 8.0 or higher)
- [Visual Studio Code](https://code.visualstudio.com/)

### Step 1: Clone the Repository
Clone the project repository to your local machine:
git clone https://github.com/Yrie1852/SaOnlineMart.git
cd SaOnlineMart

CREATE DATABASE SaOnlineMart;
USE SaOnlineMart;

CREATE TABLE Users (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Username VARCHAR(255) NOT NULL,
    PasswordHash VARCHAR(255) NOT NULL,
    Email VARCHAR(255) NOT NULL UNIQUE
);

CREATE TABLE Products (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(255) NOT NULL,
    Description TEXT,
    Price DECIMAL(18, 2) NOT NULL
);

CREATE TABLE Orders (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    UserId INT,
    OrderDate DATETIME NOT NULL,
    FOREIGN KEY (UserId) REFERENCES Users(Id)
);

CREATE TABLE OrderItems (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    OrderId INT,
    ProductId INT,
    Quantity INT NOT NULL,
    FOREIGN KEY (OrderId) REFERENCES Orders(Id),
    FOREIGN KEY (ProductId) REFERENCES Products(Id)
);

Update Connection String
### Step 3: Build and Run the Application

