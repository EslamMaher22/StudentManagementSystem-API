# Student Management System API (SMS API)

A comprehensive Web API built using .NET Core for managing academic departments and student records. This project adheres to professional software architecture guidelines, implementing the Repository Pattern and Unit of Work for a clean, decoupled, and highly maintainable codebase.

---

## Project Overview

This backend application provides robust CRUD operations and advanced data manipulation features for an educational or university portal, managing two primary domains:
Departments: Academic departments including features for full-text search, pagination, and fetching records by ID or specific names.
Students: Comprehensive student profile and enrollment records.

---

## Tech Stack

* Framework: .NET 8.0 / .NET Core Web API
* Database ORM: Entity Framework Core (EF Core)
* Architecture Patterns: Repository Pattern & Unit of Work (Clean Architecture layout)
* API Documentation: Swagger UI / OpenAPI
* Database Engine: SQL Server

---

## API Endpoints

### Department Endpoints

| Method | Endpoint | Description |
| :--- | :--- | :--- |
| `GET` | `/api/Department` | Retrieves all departments with optional search query and pagination filters. |
| `POST` | `/api/Department` | Adds a new department to the system. |
| `GET` | `/api/Department/{id}` | Fetches a specific department's details by its unique identifier (ID). |
| `PUT` | `/api/Department/{id}` | Updates an existing department's information. |
| `DELETE` | `/api/Department/{id}` | Deletes a specific department from the database by ID. |
| `GET` | `/api/Department/{name}` | Fetches department details using its unique academic name. |

### Student Endpoints

| Method | Endpoint | Description |
| :--- | :--- | :--- |
| `GET` | `/api/Students` | Retrieves a complete list of all registered students. |
| `POST` | `/api/Students` | Registers a new student into the system. |
| `GET` | `/api/Students/{id}` | Fetches a specific student's record by their unique ID. |
| `PUT` | `/api/Students/{id}` | Updates an existing student's profiling information. |
| `DELETE` | `/api/Students/{id}` | Removes/Deletes a student record from the system. |

---

## Project Structure

```text
📁 StudentManagementSystem-API
├── 📁 Controllers      # RESTful API endpoints and Routing handlers
├── 📁 Models           # Domain Entities and Database Schema Models
├── 📁 DTOs             # Data Transfer Objects for decoupled data requests
├── 📁 Repository       # Concrete Repository layers abstraction
├── 📁 UnitOfWorks      # Unit of Work pattern layer keeping transactions atomic
└── 📁 Mapconfig        # Object-to-Object AutoMapper configuration files
