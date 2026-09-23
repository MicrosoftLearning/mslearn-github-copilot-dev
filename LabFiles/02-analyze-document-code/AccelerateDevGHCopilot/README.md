# Library App

## Description

Library App is a .NET console application for managing library operations such as searching for patrons, viewing loan details, renewing memberships, extending loans, and returning books. The solution is organized into separate layers for domain logic, infrastructure, and user interaction, making it easier to understand, maintain, and extend.

The application uses JSON files as a lightweight data store and follows a simple layered architecture:

- Application core contains the business rules and domain models
- Infrastructure handles file-based data access
- Console app provides the interactive menu-driven experience

## Project Structure

- src/
  - Library.ApplicationCore/
    - Entities/
      - Author.cs
      - Book.cs
      - BookItem.cs
      - Loan.cs
      - Patron.cs
    - Enums/
      - LoanExtensionStatus.cs
      - LoanReturnStatus.cs
      - MembershipRenewalStatus.cs
    - Interfaces/
      - ILoanRepository.cs
      - ILoanService.cs
      - IPatronRepository.cs
      - IPatronService.cs
    - Services/
      - LoanService.cs
      - PatronService.cs
    - Library.ApplicationCore.csproj
  - Library.Infrastructure/
    - Data/
      - JsonData.cs
      - JsonLoanRepository.cs
      - JsonPatronRepository.cs
    - Library.Infrastructure.csproj
  - Library.Console/
    - appSettings.json
    - CommonActions.cs
    - ConsoleApp.cs
    - ConsoleState.cs
    - Program.cs
    - Library.Console.csproj
- tests/
  - UnitTests/

## Key Classes and Interfaces

- Entities
  - Author: Represents a book author.
  - Book: Represents a library book.
  - BookItem: Represents an individual physical copy of a book.
  - Loan: Represents a patron's borrowing record.
  - Patron: Represents a library member.

- Enums
  - LoanExtensionStatus: Represents the result of extending a loan.
  - LoanReturnStatus: Represents the result of returning a loaned item.
  - MembershipRenewalStatus: Represents the result of renewing a patron membership.

- Interfaces
  - ILoanRepository: Defines the contract for reading and updating loans.
  - ILoanService: Defines loan-related business operations.
  - IPatronRepository: Defines the contract for reading and updating patron data.
  - IPatronService: Defines patron-related business operations.

- Services
  - LoanService: Handles loan return and extension logic.
  - PatronService: Handles membership renewal logic.

- Data access
  - JsonData: Loads and saves JSON data files and populates related objects.
  - JsonLoanRepository: Retrieves and updates loan records from JSON.
  - JsonPatronRepository: Searches for patrons and updates patron records in JSON.

- Console app
  - ConsoleApp: Contains the interactive user flow for searching patrons and managing loans.
  - Program: Configures dependency injection and starts the application.

## Usage

1. Open the solution in Visual Studio Code or Visual Studio.
2. Restore NuGet packages if needed.
3. Build the solution.
4. Run the console application:

   ```bash
   dotnet run --project src/Library.Console/Library.Console.csproj
