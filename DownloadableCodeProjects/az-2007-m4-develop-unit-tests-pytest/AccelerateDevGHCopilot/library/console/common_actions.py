from enum import Flag, auto

class CommonActions(Flag):
    REPEAT = 0
    SELECT = auto()
    QUIT = auto()
    SEARCH_PATRONS = auto()
    SEARCH_BOOKS = auto()
    RENEW_PATRON_MEMBERSHIP = auto()
    RETURN_LOANED_BOOK = auto()
    EXTEND_LOANED_BOOK = auto()
