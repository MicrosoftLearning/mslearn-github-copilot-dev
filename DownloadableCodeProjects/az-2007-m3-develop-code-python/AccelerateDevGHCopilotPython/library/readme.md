# Library App

## Description

Library App is a modular Python application designed to help library staff manage core operations such as book loans, patron management, and inventory tracking. The project follows a clean architecture, separating domain logic, data access, and user interaction through a console interface. Data is stored in JSON files, making the app easy to set up and run in any environment.

## Project Structure

- Library/
  - readme.md
  - application_core/
    - entities/
      - author.py
      - book_item.py
      - book.py
      - loan.py
      - patron.py
    - enums/
      - loan_extension_status.py
      - loan_return_status.py
      - membership_renewal_status.py
      - ...
    - interfaces/
      - iloan_repository.py
      - iloan_service.py
      - ipatron_repository.py
      - ipatron_service.py
      - ...
    - services/
      - loan_service.py
      - patron_service.py
      - ...
  - console/
    - book_repository.py
    - common_actions.py
    - console_app.py
    - console_state.py
    - main.py
  - infrastructure/
    - json_data.py
    - json_loan_repository.py
    - json_patron_repository.py
    - Json/
      - Authors.json
      - Books.json
      - BookItems.json
      - Loans.json
      - Patrons.json
  - tests/
    - __init__.py
    - test_loan_service.py
    - test_patron_service.py

## Key Classes and Interfaces

- **Entities (application_core/entities/):**
  - `Author`, `Book`, `BookItem`, `Patron`, `Loan`: Represent core library objects.
- **Enums (application_core/enums/):**
  - `LoanExtensionStatus`, `LoanReturnStatus`, `MembershipRenewalStatus`: Enumerations for domain-specific statuses.
- **Interfaces (application_core/interfaces/):**
  - `ILoanRepository`, `ILoanService`, `IPatronRepository`, `IPatronService`: Define abstractions for repositories and services.
- **Services (application_core/services/):**
  - `LoanService`, `PatronService`: Business logic for managing loans, patrons, and books.
- **Console (console/):**
  - `ConsoleApp`: Main entry point for the console interface.
  - `main.py`: Launches the application.
- **Infrastructure (infrastructure/):**
  - `json_data.py`: Utilities for JSON file operations.
  - `json_loan_repository.py`, `json_patron_repository.py`: Data access implementations using JSON files.
- **Tests (tests/):**
  - Unit tests for core business logic.

## Usage

1. **Install Python 3.7+** if not already installed.
2. **Navigate to the Library directory:**
   ```
   cd Library
   ```
3. **Run the application:**
   ```
   python -m console.main
   ```
4. **Follow the on-screen prompts** to manage books, patrons, and loans.

## License

This project is licensed under the MIT License.