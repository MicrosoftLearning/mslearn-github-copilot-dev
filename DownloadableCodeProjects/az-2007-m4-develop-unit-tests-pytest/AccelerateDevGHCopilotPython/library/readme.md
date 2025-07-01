# Library App

## Description

Library App is a Python-based console application for managing a library's books, patrons, and loans. It supports searching for patrons and books, checking out and returning books, extending loans, and renewing patron memberships. Data is persisted in JSON files, and the application is structured using a clean separation of entities, repositories, services, and console UI.

## Project Structure

- application_core/
    - entities/
        - author.py
        - book.py
        - book_item.py
        - loan.py
        - patron.py
    - enums/
        - loan_extension_status.py
        - loan_return_status.py
        - membership_renewal_status.py
    - interfaces/
        - iloan_repository.py
        - iloan_service.py
        - ipatron_repository.py
        - ipatron_service.py
    - services/
        - loan_service.py
        - patron_service.py
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
    - test_loan_service.py
    - test_patron_service.py
    - __init__.py
- readme.md

## Key Classes and Interfaces

- **Entities**
    - `Author`, `Book`, `BookItem`, `Loan`, `Patron`: Data models for library domain objects.
- **Enums**
    - `LoanExtensionStatus`, `LoanReturnStatus`, `MembershipRenewalStatus`: Status codes for operations.
- **Interfaces**
    - `ILoanRepository`, `ILoanService`, `IPatronRepository`, `IPatronService`: Abstract base classes defining contracts for repositories and services.
- **Services**
    - `LoanService`: Handles loan operations (checkout, return, extend).
    - `PatronService`: Handles patron operations (renew membership, search).
- **Repositories**
    - `JsonLoanRepository`, `JsonPatronRepository`: Implement data access using JSON files.
    - `JsonData`: Loads and saves all data from/to JSON files.
- **Console UI**
    - `ConsoleApp`: Main application loop and user interaction.
    - `common_actions.py`, `console_state.py`: Define UI actions and states.

## Usage

1. **Install Requirements**  
   No external dependencies are required beyond Python 3.7+.

2. **Run the Application**  
   From the `console` directory (or project root), run:
   ```
   python -m console.main
   ```
   Follow the on-screen prompts to search for patrons, manage loans, and check book availability.

3. **Run Tests**  
   From the project root, run:
   ```
   python -m unittest discover tests
   ```

## License

This project is licensed under the MIT License.