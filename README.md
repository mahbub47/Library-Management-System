# Library Management System

A modern backend system for managing library operations, built with **ASP.NET Core** and **Entity Framework Core**.

## 🎯 Project Overview

This Library Management System is a robust RESTful API that provides comprehensive library operations management. It enables efficient handling of books, members, and loan tracking with built-in audit logging for compliance and transparency.

## ✨ Features

- **📚 Book Management**
  - Add, update, search, and delete books
  - Track available copies
  - Search by title or author

- **👥 Member Management**
  - Register and manage library members
  - Activate/deactivate memberships
  - Update member information

- **📋 Loan Management**
  - Track book loans and due dates
  - Manage borrowing records
  - Monitor loan history

- **📝 Audit Logging**
  - Automatic tracking of all data changes
  - Records entity modifications with timestamps
  - Maintains audit trail for accountability

## 🛠️ Tech Stack

- **Framework**: ASP.NET Core
- **Language**: C#
- **Database ORM**: Entity Framework Core
- **Database**: SQL Server
- **API Style**: RESTful

## 📦 Project Structure


## 📡 API Endpoints

### Books
- `POST /api/books` - Create a new book
- `GET /api/books` - Get all books
- `GET /api/books/{bookId}` - Get book by ID
- `GET /api/books/search?title=&author=` - Search books
- `PUT /api/books/{bookId}` - Update book

### Members
- `POST /api/members` - Create a new member
- `GET /api/members` - Get all members
- `GET /api/members/{memberId}` - Get member by ID
- `PUT /api/members/{memberId}` - Update member
- `PATCH /api/members/{memberId}/deactivate` - Deactivate membership
- `DELETE /api/members/{memberId}` - Delete member

## 🚀 Getting Started

### Prerequisites
- .NET 6.0 or higher
- SQL Server
- Visual Studio 2022 or Visual Studio Code

### Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/mahbub47/Library-Management-System.git
   cd Library-Management-System
