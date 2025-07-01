from .console_state import ConsoleState
from .common_actions import CommonActions
from application_core.interfaces.ipatron_repository import IPatronRepository
from application_core.interfaces.iloan_repository import ILoanRepository
from application_core.interfaces.iloan_service import ILoanService
from application_core.interfaces.ipatron_service import IPatronService

class ConsoleApp:
    def __init__(
        self,
        loan_service: ILoanService,
        patron_service: IPatronService,
        patron_repository: IPatronRepository,
        loan_repository: ILoanRepository
    ):
        self._current_state: ConsoleState = ConsoleState.PATRON_SEARCH
        self.matching_patrons = []
        self.selected_patron_details = None
        self.selected_loan_details = None
        self._patron_repository = patron_repository
        self._loan_repository = loan_repository
        self._loan_service = loan_service
        self._patron_service = patron_service

    def write_input_options(self, options):
        print("Input Options:")
        if options & CommonActions.RETURN_LOANED_BOOK:
            print(' - "r" to mark as returned')
        if options & CommonActions.EXTEND_LOANED_BOOK:
            print(' - "e" to extend the book loan')
        if options & CommonActions.RENEW_PATRON_MEMBERSHIP:
            print(' - "m" to extend patron\'s membership')
        if options & CommonActions.SEARCH_PATRONS:
            print(' - "s" for new search')
        if options & CommonActions.QUIT:
            print(' - "q" to quit')
        if options & CommonActions.SELECT:
            print(' - type a number to select a list item.')

    def run(self) -> None:
        while True:
            if self._current_state == ConsoleState.PATRON_SEARCH:
                self._current_state = self.patron_search()
            elif self._current_state == ConsoleState.PATRON_SEARCH_RESULTS:
                self._current_state = self.patron_search_results()
            elif self._current_state == ConsoleState.PATRON_DETAILS:
                self._current_state = self.patron_details()
            elif self._current_state == ConsoleState.LOAN_DETAILS:
                self._current_state = self.loan_details()
            elif self._current_state == ConsoleState.QUIT:
                break

    def patron_search(self) -> ConsoleState:
        search_input = input("Enter a string to search for patrons by name: ").strip()
        if not search_input:
            print("No input provided. Please try again.")
            return ConsoleState.PATRON_SEARCH
        self.matching_patrons = self._patron_repository.search_patrons(search_input)
        if not self.matching_patrons:
            print("No matching patrons found.")
            return ConsoleState.PATRON_SEARCH
        return ConsoleState.PATRON_SEARCH_RESULTS

    def patron_search_results(self) -> ConsoleState:
        print("\nMatching Patrons:")
        idx = 1
        for patron in self.matching_patrons:
            print(f"{idx}) {patron.name}")
            idx += 1
        if self.matching_patrons:
            self.write_input_options(
                CommonActions.SELECT | CommonActions.SEARCH_PATRONS | CommonActions.QUIT
            )
        else:
            self.write_input_options(
                CommonActions.SEARCH_PATRONS | CommonActions.QUIT
            )
        selection = input("Enter your choice: ").strip().lower()
        if selection == 'q':
            return ConsoleState.QUIT
        elif selection == 's':
            return ConsoleState.PATRON_SEARCH
        elif selection.isdigit():
            idx = int(selection)
            if 1 <= idx <= len(self.matching_patrons):
                self.selected_patron_details = self.matching_patrons[idx - 1]
                return ConsoleState.PATRON_DETAILS
            else:
                print("Invalid selection. Please enter a valid number.")
                return ConsoleState.PATRON_SEARCH_RESULTS
        else:
            print("Invalid input. Please enter a number, 's', or 'q'.")
            return ConsoleState.PATRON_SEARCH_RESULTS

    def patron_details(self) -> ConsoleState:
        patron = self.selected_patron_details
        print(f"\nName: {patron.name}")
        print(f"Membership Expiration: {patron.membership_end}")
        loans = self._loan_repository.get_loans_by_patron_id(patron.id)
        print("\nBook Loans History:")

        valid_loans = self._print_loans(loans)

        if valid_loans:
            options = (
                CommonActions.RENEW_PATRON_MEMBERSHIP
                | CommonActions.SEARCH_PATRONS
                | CommonActions.QUIT
                | CommonActions.SELECT
            )
            selection = self._get_patron_details_input(options)
            return self._handle_patron_details_selection(selection, patron, valid_loans)
        else:
            print("No valid loans for this patron.")
            options = (
                CommonActions.SEARCH_PATRONS
                | CommonActions.QUIT
            )
            selection = self._get_patron_details_input(options)
            return self._handle_no_loans_selection(selection)

    def _print_loans(self, loans):
        valid_loans = []
        idx = 1
        for loan in loans:
            if not getattr(loan, 'book_item', None) or not getattr(loan.book_item, 'book', None):
                print(f"{idx}) [Invalid loan data: missing book information]")
            else:
                returned = "True" if getattr(loan, 'return_date', None) else "False"
                print(f"{idx}) {loan.book_item.book.title} - Due: {loan.due_date} - Returned: {returned}")
                valid_loans.append((idx, loan))
            idx += 1
        return valid_loans

    def _get_patron_details_input(self, options):
        self.write_input_options(options)
        return input("Enter your choice: ").strip().lower()

    def _handle_patron_details_selection(self, selection, patron, valid_loans):
        if selection == 'q':
            return ConsoleState.QUIT
        elif selection == 's':
            return ConsoleState.PATRON_SEARCH
        elif selection == 'm':
            status = self._patron_service.renew_membership(patron.id)
            print(status)
            self.selected_patron_details = self._patron_repository.get_patron(patron.id)
            return ConsoleState.PATRON_DETAILS
        elif selection.isdigit():
            idx = int(selection)
            if 1 <= idx <= len(valid_loans):
                self.selected_loan_details = valid_loans[idx - 1][1]
                return ConsoleState.LOAN_DETAILS
            print("Invalid selection. Please enter a number shown in the list above.")
            return ConsoleState.PATRON_DETAILS
        else:
            print("Invalid input. Please enter a number, 'm', 's', or 'q'.")
            return ConsoleState.PATRON_DETAILS

    def _handle_no_loans_selection(self, selection):
        if selection == 'q':
            return ConsoleState.QUIT
        elif selection == 's':
            return ConsoleState.PATRON_SEARCH
        else:
            print("Invalid input.")
            return ConsoleState.PATRON_DETAILS

    def loan_details(self) -> ConsoleState:
        loan = self.selected_loan_details
        print(f"\nBook title: {loan.book_item.book.title}")
        print(f"Book Author: {loan.book_item.book.author.name}")
        print(f"Due date: {loan.due_date}")
        returned = "True" if getattr(loan, 'return_date', None) else "False"
        print(f"Returned: {returned}\n")
        options = CommonActions.SEARCH_PATRONS | CommonActions.QUIT
        if not getattr(loan, 'return_date', None):
            options |= CommonActions.RETURN_LOANED_BOOK | CommonActions.EXTEND_LOANED_BOOK
        self.write_input_options(options)
        selection = input("Enter your choice: ").strip().lower()
        if selection == 'q':
            return ConsoleState.QUIT
        elif selection == 's':
            return ConsoleState.PATRON_SEARCH
        elif selection == 'r' and not getattr(loan, 'return_date', None):
            status = self._loan_service.return_loan(loan.id)
            print("Book was successfully returned.")
            print(status)
            self.selected_loan_details = self._loan_repository.get_loan(loan.id)
            return ConsoleState.LOAN_DETAILS
        elif selection == 'e' and not getattr(loan, 'return_date', None):
            status = self._loan_service.extend_loan(loan.id)
            print(status)
            self.selected_loan_details = self._loan_repository.get_loan(loan.id)
            return ConsoleState.LOAN_DETAILS
        else:
            print("Invalid input.")
            return ConsoleState.LOAN_DETAILS

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
        patron_service=patron_service
    )
    app.run()
