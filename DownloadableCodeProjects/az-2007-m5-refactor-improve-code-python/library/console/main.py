import sys
from pathlib import Path

# Add the parent directory to sys.path
sys.path.append(str(Path(__file__).resolve().parent.parent))

from application_core.services.loan_service import LoanService
from application_core.services.patron_service import PatronService
from infrastructure.json_data import JsonData
from infrastructure.json_loan_repository import JsonLoanRepository
from infrastructure.json_patron_repository import JsonPatronRepository
from console.console_app import ConsoleApp


def main():
    json_data = JsonData()
    patron_repo = JsonPatronRepository(json_data)
    loan_repo = JsonLoanRepository(json_data)
    loan_service = LoanService(loan_repo)
    patron_service = PatronService(patron_repo)

    app = ConsoleApp(
        loan_service=loan_service,
        patron_service=patron_service,
        patron_repository=patron_repo,
        loan_repository=loan_repo,
        json_data=json_data  # <-- pass json_data here
    )
    app.run()


if __name__ == "__main__":
    main()
