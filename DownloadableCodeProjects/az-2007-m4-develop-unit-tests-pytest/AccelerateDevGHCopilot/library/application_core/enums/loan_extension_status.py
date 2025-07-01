from enum import Enum

class LoanExtensionStatus(Enum):
    SUCCESS = 'Book loan extension was successful.'
    LOAN_NOT_FOUND = 'Loan not found.'
    LOAN_EXPIRED = 'Cannot extend book loan as it already has expired. Return the book instead.'
    MEMBERSHIP_EXPIRED = "Cannot extend book loan due to expired patron's membership."
    LOAN_RETURNED = 'Cannot extend book loan as the book is already returned.'
    ERROR = 'Cannot extend book loan due to an error.'
