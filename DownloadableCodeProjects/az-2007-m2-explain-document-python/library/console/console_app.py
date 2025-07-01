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
        print("Matching Patrons:")
        for idx, patron in enumerate(self.matching_patrons, 1):
            print(f"{idx}) {patron.name}")
        return ConsoleState.PATRON_SEARCH_RESULTS

    def patron_search_results(self) -> ConsoleState:
        print("\nInput Options:")
        print(" - Type a number to select a patron from the list")
        print(" - Type 's' to search again")
        print(" - Type 'q' to quit")
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
        print("\nBook Loans:")

        # Filter and display valid loans
        valid_loans = []
        for idx, loan in enumerate(loans, 1):
            if not getattr(loan, 'book_item', None) or not getattr(loan.book_item, 'book', None):
                print(f"{idx}) [Invalid loan data: missing book information]")
            else:
                returned = "True" if getattr(loan, 'return_date', None) else "False"
                print(f"{idx}) {loan.book_item.book.title} - Due: {loan.due_date} - Returned: {returned}")
                valid_loans.append((idx, loan))
        if valid_loans:
            print("Type a number to select a loan from the list")
        if not valid_loans:
            print("No valid loans for this patron.")
            print("Input Options:")
            print(" - Type 's' to search again")
            print(" - Type 'q' to quit")
            selection = input("Enter your choice: ").strip().lower()
            if selection == 'q':
                return ConsoleState.QUIT
            elif selection == 's':
                return ConsoleState.PATRON_SEARCH
            else:
                print("Invalid input.")
                return ConsoleState.PATRON_DETAILS
        else:
            print("Input Options:")
            print(" - Type 'm' to renew membership")
            print(" - Type 's' to search again")
            print(" - Type 'q' to quit")
            selection = input("Enter your choice: ").strip().lower()
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

    def loan_details(self) -> ConsoleState:
        loan = self.selected_loan_details
        print(f"\nBook title: {loan.book_item.book.title}")
        print(f"Book Author: {loan.book_item.book.author.name}")
        print(f"Due date: {loan.due_date}")
        returned = "True" if getattr(loan, 'return_date', None) else "False"
        print(f"Returned: {returned}\n")
        print("Input Options:")
        print(" - Type 'r' to return book")
        print(" - Type 'e' to extend loan")
        print(" - Type 's' to search again")
        print(" - Type 'q' to quit")
        selection = input("Enter your choice: ").strip().lower()
        if selection == 'q':
            return ConsoleState.QUIT
        elif selection == 's':
            return ConsoleState.PATRON_SEARCH
        elif selection == 'r':
            status = self._loan_service.return_loan(loan.id)
            print("Book was successfully returned.")
            print(status)
            self.selected_loan_details = self._loan_repository.get_loan(loan.id)
            return ConsoleState.LOAN_DETAILS
        elif selection == 'e':
            status = self._loan_service.extend_loan(loan.id)
            print(status)
            self.selected_loan_details = self._loan_repository.get_loan(loan.id)
            return ConsoleState.LOAN_DETAILS
        else:
            print("Invalid input.")
            return ConsoleState.LOAN_DETAILS
