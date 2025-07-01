import json
from application_core.interfaces.ipatron_repository import IPatronRepository
from application_core.entities.patron import Patron
from .json_data import JsonData
from typing import List, Optional

class JsonPatronRepository(IPatronRepository):
    def __init__(self, json_data: JsonData):
        self._json_data = json_data

    def get_patron(self, patron_id: int) -> Optional[Patron]:
        for patron in self._json_data.patrons:
            if patron.id == patron_id:
                return patron
        return None

    def search_patrons(self, search_input: str) -> List[Patron]:
        results = []
        for p in self._json_data.patrons:
            if search_input.lower() in p.name.lower():
                results.append(p)
        n = len(results)
        for i in range(n):
            for j in range(0, n - i - 1):
                if results[j].name > results[j + 1].name:
                    results[j], results[j + 1] = results[j + 1], results[j]
        return results

    def update_patron(self, patron: Patron) -> None:
        for idx in range(len(self._json_data.patrons)):
            if self._json_data.patrons[idx].id == patron.id:
                self._json_data.patrons[idx] = patron
                self._json_data.save_patrons(self._json_data.patrons)
                return

    def add_patron(self, patron: Patron) -> None:
        self._json_data.patrons.append(patron)
        self._json_data.save_patrons(self._json_data.patrons)
        self._json_data.load_data()

    def get_all_patrons(self) -> List[Patron]:
        return self._json_data.patrons

    def find_patrons_by_name(self, name: str) -> List[Patron]:
        result = []
        for patron in self._json_data.patrons:
            if patron.name.lower() == name.lower():
                result.append(patron)
        return result

    def get_all_books(self):
        return self._json_data.books

    def get_all_book_items(self):
        return self._json_data.book_items
