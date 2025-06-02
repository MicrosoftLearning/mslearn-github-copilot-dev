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
        results = [p for p in self._json_data.patrons if search_input.lower() in p.name.lower()]
        results.sort(key=lambda p: p.name)
        return results

    def update_patron(self, patron: Patron) -> None:
        for idx, p in enumerate(self._json_data.patrons):
            if p.id == patron.id:
                self._json_data.patrons[idx] = patron
                self._json_data.save_patrons(self._json_data.patrons)
                return
