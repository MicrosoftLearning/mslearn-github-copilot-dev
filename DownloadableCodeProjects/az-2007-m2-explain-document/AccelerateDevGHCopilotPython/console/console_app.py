from .console_state import ConsoleState
from .common_actions import CommonActions
from application_core.interfaces.ipatron_repository import IPatronRepository
from application_core.interfaces.iloan_repository import ILoanRepository
from application_core.interfaces.iloan_service import ILoanService
from application_core.interfaces.ipatron_service import IPatronService

class ConsoleApp:
    def __init__(self, loan_service: ILoanService, patron_service: IPatronService, patron_repository: IPatronRepository, loan_repository: ILoanRepository):
        self._current_state = ConsoleState.PATRON_SEARCH
        self.matching_patrons = []
        self.selected_patron_details = None
        self.selected_loan_details = None
        self._patron_repository = patron_repository
        self._loan_repository = loan_repository
        self._loan_service = loan_service
        self._patron_service = patron_service

    def run(self):
        while True:
            if self._current_state == ConsoleState.PATRON_SEARCH:
                # Implement patron search logic
                pass
            elif self._current_state == ConsoleState.PATRON_SEARCH_RESULTS:
                # Implement search results logic
                pass
            elif self._current_state == ConsoleState.PATRON_DETAILS:
                # Implement patron details logic
                pass
            elif self._current_state == ConsoleState.LOAN_DETAILS:
                # Implement loan details logic
                pass
            elif self._current_state == ConsoleState.QUIT:
                break
