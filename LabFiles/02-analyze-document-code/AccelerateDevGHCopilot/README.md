# Library App

## Description

Library App is a .NET-based library management application designed to help users track books, patrons, and loans. The solution follows a clean architecture by separating domain entities, interfaces, business services, infrastructure, and console presentation logic. It stores library data in JSON files and provides a simple console workflow for performing common library tasks.

## Project Structure

- `AccelerateDevGHCopilot.sln` - Solution file for the library application.
- `src/`
  - `Library.ApplicationCore/`
    - `Entities/` - Core domain models such as `Author`, `Book`, `BookItem`, `Patron`, and `Loan`.
    - `Enums/` - Enumerations used to represent loan and membership statuses.
    - `Interfaces/` - Contracts for repositories and services.
    - `Services/` - Business logic for loan and patron management.
  - `Library.Console/`
    - `CommonActions.cs` - Shared helper actions for console workflows.
    - `ConsoleApp.cs` - Main application flow for interacting with the user.
    - `ConsoleState.cs` - Tracks state within the console interface.
    - `Program.cs` - Entry point for the application.
  - `Library.Infrastructure/`
    - `Data/` - Repository implementations that persist data to JSON files.
- `tests/`
  - `UnitTests/`
    - `ApplicationCore/` - Unit tests for the domain services and business logic.
    - `LoanFactory.cs` - Test data factory for loan scenarios.
    - `PatronFactory.cs` - Test data factory for patron scenarios.

## Key Classes and Interfaces

- **Entities**
  - `Author` - Represents the author of a book.
  - `Book` - Represents a library book.
  - `BookItem` - Represents an individual copy of a book.
  - `Patron` - Represents a library member.
  - `Loan` - Represents a borrowing transaction.
- **Interfaces**
  - `ILoanRepository` - Defines data access responsibilities for loans.
  - `ILoanService` - Defines loan-related business operations.
  - `IPatronRepository` - Defines data access responsibilities for patrons.
  - `IPatronService` - Defines patron-related business operations.
- **Services**
  - `LoanService` - Handles loan creation, return, and extension logic.
  - `PatronService` - Handles membership and patron search operations.
- **Console and Infrastructure**
  - `ConsoleApp` - Coordinates user interaction and application flow.
  - `JsonData` - Loads and saves JSON-backed data.
  - `JsonLoanRepository` - Implements loan persistence.
  - `JsonPatronRepository` - Implements patron persistence.

## Usage

1. Open the solution file in Visual Studio or your preferred .NET editor.
2. Restore dependencies and build the solution:

   ```bash
   dotnet restore
   dotnet build
   ```

3. Run the console application:

   ```bash
   dotnet run --project src/Library.Console/Library.Console.csproj
   ```

4. Follow the on-screen prompts to search for patrons, view loan details, and manage book returns.
5. Run the unit tests to validate functionality:

   ```bash
   dotnet test tests/UnitTests/UnitTests.csproj
   ```

## License

This project is licensed under the MIT License. See the `LICENSE` file for details.
