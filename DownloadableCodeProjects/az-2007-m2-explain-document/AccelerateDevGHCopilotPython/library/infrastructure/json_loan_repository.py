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
        for idx, l in enumerate(self._json_data.loans):
            if l.id == loan.id:
                self._json_data.loans[idx] = loan
                self._json_data.save_loans(self._json_data.loans)
                return

    def get_loans_by_patron_id(self, patron_id: int):
        return [loan for loan in self._json_data.loans if loan.patron_id == patron_id]
