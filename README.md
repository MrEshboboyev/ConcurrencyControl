# Concurrency Control with EF Core, ASP.NET Core, and PostgreSQL

This repository demonstrates how to implement and solve concurrency control problems using Entity Framework (EF) Core in an ASP.NET Core application, adhering to the Clean Architecture principles. It focuses on managing concurrency with `RowVersion` in a PostgreSQL database, ensuring data integrity and consistency in multi-user environments.

## Features

- **Optimistic Concurrency Control:**
  Learn how EF Core leverages the `RowVersion` column for handling concurrency conflicts effectively.

- **Clean Architecture Implementation:**
  The project is structured using Clean Architecture principles, promoting separation of concerns and maintainability.

- **PostgreSQL Integration:**
  Demonstrates how to configure EF Core with PostgreSQL, including advanced configurations like `RowVersion` for concurrency.

- **ASP.NET Core API:**
  A RESTful API built with ASP.NET Core showcasing the concurrency control mechanism in action.

- **Code-First Migrations:**
  Uses EF Core Code-First Migrations to set up and manage the PostgreSQL database schema.

## Getting Started

### Prerequisites

1. [.NET SDK](https://dotnet.microsoft.com/download) (6.0 or later)
2. [PostgreSQL](https://www.postgresql.org/) database
3. Any IDE or text editor (e.g., Visual Studio, Rider, or Visual Studio Code)

### Setup Instructions

1. Clone the repository:
   ```bash
   git clone https://github.com/MrEshboboyev/ConcurrencyControl.git
   cd ConcurrencyControl
   ```

2. Configure the database connection:
   - Open `appsettings.json` in the API project.
   - Set the `ConnectionStrings.DefaultConnection` to point to your PostgreSQL database.

3. Apply migrations to set up the database schema:
   ```bash
   dotnet ef database update
   ```

4. Run the application:
   ```bash
   dotnet run
   ```

5. Use tools like Postman or Swagger to interact with the API and test concurrency scenarios.

## Highlights of Concurrency Control

### Using `RowVersion`

The `RowVersion` column is configured as a concurrency token, ensuring that:
- EF Core checks the `RowVersion` during updates and deletes.
- Conflicts are detected if the `RowVersion` in the database differs from the entity being updated.

### Conflict Resolution

- **Automatic Conflict Detection:**
  EF Core automatically throws a `DbUpdateConcurrencyException` when a concurrency conflict occurs.

- **Custom Conflict Resolution:**
  The application provides mechanisms to resolve conflicts gracefully by reloading the entity or merging changes.

## Repository Structure

- **Domain Layer:**
  Contains core business entities, such as `BankAccount`, and shared abstractions.

- **Application Layer:**
  Defines use cases and application logic for handling concurrency.

- **Infrastructure Layer:**
  Manages database interactions using EF Core and PostgreSQL.

- **Presentation Layer:**
  Exposes the API endpoints using ASP.NET Core controllers.

## Key Technologies

- ASP.NET Core 6
- Entity Framework Core
- PostgreSQL
- Clean Architecture
- Optimistic Concurrency

## Contributing

Contributions are welcome! Please open an issue or submit a pull request for any improvements or fixes.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.
