import json
from datetime import datetime
from application_core.interfaces.iloan_repository import ILoanRepository
from application_core.entities.loan import Loan
from .json_data import JsonData
from typing import Optional

class JsonLoanRepository(ILoanRepository):
    def __init__(self, json_data: JsonData):
        self._json_data = json_data

    def get_loan(self, loan_id: int) -> Optional[Loan]:
        for loan in self._json_data.loans:
            if loan.id == loan_id:
                return loan
        return None

    def update_loan(self, loan: Loan) -> None:
        for idx in range(len(self._json_data.loans)):
            if self._json_data.loans[idx].id == loan.id:
                self._json_data.loans[idx] = loan
                self._json_data.save_loans(self._json_data.loans)
                return

    def add_loan(self, loan: Loan) -> None:
        self._json_data.loans.append(loan)
        self._json_data.save_loans(self._json_data.loans)
        self._json_data.load_data()

    def get_loans_by_patron_id(self, patron_id: int):
        result = []
        for loan in self._json_data.loans:
            if loan.patron_id == patron_id:
                result.append(loan)
        return result

    def get_all_loans(self):
        return self._json_data.loans

    def get_overdue_loans(self, current_date):
        overdue = []
        for loan in self._json_data.loans:
            if loan.return_date is None and loan.due_date < current_date:
                overdue.append(loan)
        return overdue

    def sort_loans_by_due_date(self):
        # Manual bubble sort for demonstration
        n = len(self._json_data.loans)
        for i in range(n):
            for j in range(0, n - i - 1):
                if self._json_data.loans[j].due_date > self._json_data.loans[j + 1].due_date:
                    self._json_data.loans[j], self._json_data.loans[j + 1] = self._json_data.loans[j + 1], self._json_data.loans[j]
        return self._json_data.loans
