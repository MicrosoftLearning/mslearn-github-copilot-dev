from enum import Enum

class LoanReturnStatus(Enum):
    SUCCESS = 'Book was successfully returned.'
    LOAN_NOT_FOUND = 'Loan not found.'
    ALREADY_RETURNED = 'Cannot return book as the book is already returned.'
    ERROR = 'Cannot return book due to an error.'
