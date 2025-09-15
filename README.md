# Tourism Manager

# 🌍 TourismManager App

**TourismManager** is a web-based application built with **ASP.NET Core MVC** that helps users explore, book, and manage tourism packages.  
It also provides an **Admin Panel** to manage packages, bookings, and users efficiently.

---

## ✨ Features

- **👤 User Registration & Login**
  - Secure login and signup system
  - Role-based access for Admins and Users

- **🏖️ Tourism Package Management**
  - Admin can add, update, and delete tourism packages
  - Users can browse packages with details like price, location, and duration

- **📅 Booking System**
  - Users can book packages directly from the site
  - View and manage your bookings

- **📊 Admin Dashboard**
  - View all users and bookings in one place
  - Approve or cancel bookings

- **🗄️ Database Integration**
  - Uses **SQL Server** for storing packages, users, and booking data
  - Repository pattern for clean and maintainable code

---

## 🧑‍💻 Admin Credentials (for Testing)

Use the following credentials to log in as admin:

- **Username:** `admin`  
- **Email:** `admin@gmail.com`  
- **Password:** `0000`



---

## 🛠️ Tech Stack

- **Framework:** ASP.NET Core MVC  
- **Language:** C#  
- **Database:** Microsoft SQL Server  
- **ORM:** Entity Framework Core  
- **Pattern:** Repository Pattern + Dependency Injection  
- **Frontend:** Razor Views, Bootstrap  


Populate Sample Data 
If you want to have pre-filled data in your tables:

Open SSMS

Go to the folder /DatabaseScripts in this project

Run .sql file 

This will insert default admin user, sample tourism packages, and example bookings.
