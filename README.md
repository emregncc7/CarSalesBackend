# Car Sales Backend Project

This is a .NET Core backend project for a car sales application, built using N-tier architecture principles.

## Project Structure

The solution consists of several layers:

- **Core**: Contains common utilities and base classes used across the project
- **Entities**: Contains domain models and DTOs
- **DataAccess**: Handles data persistence using Entity Framework Core
- **Business**: Contains business logic and validation rules
- **WebAPI**: REST API endpoints for the application
- **ConsoleUI**: Console application for testing purposes

## Technologies Used

- .NET Core
- Entity Framework Core
- RESTful API
- N-tier Architecture
- SOLID Principles

## Getting Started

1. Clone the repository
2. Open the solution in Visual Studio
3. Update the connection string in `WebAPI/appsettings.json`
4. Run the following commands in Package Manager Console:
   ```
   Update-Database
   ```
5. Build and run the WebAPI project

## Project Layers

### Core Layer
Contains base classes, utilities, and cross-cutting concerns that are used throughout the application.

### Entities Layer
Contains the domain models and DTOs (Data Transfer Objects) used in the application.

### DataAccess Layer
Implements data access using Entity Framework Core, including:
- Database context
- Entity configurations
- Repository implementations

### Business Layer
Contains business logic, including:
- Managers/Services
- Validation rules
- Business rules

### WebAPI Layer
Provides REST API endpoints for:
- Car operations
- User operations
- Authentication/Authorization 