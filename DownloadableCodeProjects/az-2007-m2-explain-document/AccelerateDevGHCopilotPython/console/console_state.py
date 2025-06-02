from enum import Enum

class ConsoleState(Enum):
    PATRON_SEARCH = 1
    PATRON_SEARCH_RESULTS = 2
    PATRON_DETAILS = 3
    LOAN_DETAILS = 4
    QUIT = 5
