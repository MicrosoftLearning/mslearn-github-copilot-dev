from abc import ABC, abstractmethod
from typing import List, Optional
from ..entities.patron import Patron

class IPatronRepository(ABC):
    @abstractmethod
    def get_patron(self, patron_id: int) -> Optional[Patron]:
        pass

    @abstractmethod
    def search_patrons(self, search_input: str) -> List[Patron]:
        pass

    @abstractmethod
    def update_patron(self, patron: Patron) -> None:
        pass
