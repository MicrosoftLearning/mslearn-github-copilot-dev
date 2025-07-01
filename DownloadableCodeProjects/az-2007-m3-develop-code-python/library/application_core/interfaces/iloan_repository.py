from abc import ABC, abstractmethod
from typing import Optional
from ..entities.loan import Loan

class ILoanRepository(ABC):
    @abstractmethod
    def get_loan(self, loan_id: int) -> Optional[Loan]:
        pass

    @abstractmethod
    def update_loan(self, loan: Loan) -> None:
        pass

    @abstractmethod
    def add_loan(self, loan: Loan) -> None:
        pass

    @abstractmethod
    def get_loans_by_patron_id(self, patron_id: int):
        pass
