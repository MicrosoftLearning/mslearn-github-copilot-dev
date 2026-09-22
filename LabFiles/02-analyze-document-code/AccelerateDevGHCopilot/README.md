Searched codebase for "I need you to generate the contents of a README.md file that I can use for the current code repository. Use "Library App" as the project title. The README file should include the following sections: Description, Project Structure, Key Classes and Interfaces, Usage, License. Format all sections as raw markdown. Use a bullet list with indents to represent the project structure. Do not include ".gitignore" or the ".github", "bin", and "obj" folders. I want add the suggested content to the README.md file that's open in the editor.", 9 results

# Library App

## Description

Library App is a small .NET console application that demonstrates a clean‑architecture style split between domain, infrastructure, and UI. It manages authors, books, physical book items, patrons and loans using JSON files as a simple data store. The project is suitable for learning repository/service patterns, unit testing, and small data persistence scenarios.

## Project Structure

- AccelerateDevGHCopilot.sln
- src/
  - Library.ApplicationCore/
    - Entities/ — domain model classes (Author, Book, BookItem, Patron, Loan)
    - Enums/ — domain enums and helpers
    - Interfaces/ — repository and service interfaces
    - Services/ — business logic implementations (`LoanService`, `PatronService`)
    - Library.ApplicationCore.csproj
  - Library.Console/
    - appSettings.json
    - CommonActions.cs
    - ConsoleApp.cs
    - ConsoleState.cs
    - Program.cs
    - Json/ — sample JSON data files: Authors.json, Books.json, BookItems.json, Patrons.json, Loans.json
    - Library.Console.csproj
  - Library.Infrastructure/
    - Data/
      - JsonData.cs
      - JsonPatronRepository.cs
      - JsonLoanRepository.cs
    - Library.Infrastructure.csproj
- tests/
  - UnitTests/
    - LoanFactory.cs
    - PatronFactory.cs
    - ApplicationCore/ — unit tests for services
    - UnitTests.csproj
- LICENSE

## Key Classes and Interfaces

- Entities
  - `Author` — author metadata
  - `Book` — book metadata (title, author, ISBN, etc.)
  - `BookItem` — physical copy information (condition, acquisition date)
  - `Patron` — library user (membership dates, image, loans)
  - `Loan` — loan transaction (dates, status, returned)
- Interfaces
  - `IPatronRepository` — patron data operations
  - `ILoanRepository` — loan data operations
  - `IPatronService` — patron-related business operations
  - `ILoanService` — loan-related business operations
- Services
  - `PatronService` — membership and patron-centric rules
  - `LoanService` — checkout, return, extend loan logic
- Infrastructure / Data access
  - `JsonData` — loads/saves JSON data, provides object population helpers
  - `JsonPatronRepository` — patron repository backed by JSON
  - `JsonLoanRepository` — loan repository backed by JSON

## Usage

Prerequisites:
- .NET 9 SDK installed (verify with `dotnet --version`)

Build:
```bash
cd LabFiles/02-analyze-document-code/AccelerateDevGHCopilot
dotnet build
```

Run the console app:
```bash
cd src/Library.Console
dotnet run --project Library.Console.csproj
```

Run tests:
```bash
cd tests/UnitTests
dotnet test
```

Data:
- JSON data files live under `src/Library.Console/Json` and are read/written by `JsonData`.

## License

This project uses the repository LICENSE file for licensing details.