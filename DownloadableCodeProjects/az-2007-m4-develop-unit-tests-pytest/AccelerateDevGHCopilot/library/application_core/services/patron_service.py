from ..interfaces.ipatron_service import IPatronService
from ..interfaces.ipatron_repository import IPatronRepository
from ..entities.patron import Patron
from ..enums.membership_renewal_status import MembershipRenewalStatus
from datetime import datetime, timedelta

class PatronService(IPatronService):
    EXTEND_BY_DAYS = 365

    def __init__(self, patron_repository: IPatronRepository):
        self._patron_repository = patron_repository

    def renew_membership(self, patron_id: int) -> MembershipRenewalStatus:
        patron = self._patron_repository.get_patron(patron_id)
        if patron is None:
            return MembershipRenewalStatus.PATRON_NOT_FOUND
        if patron.membership_end < datetime.now():
            patron.membership_end = datetime.now() + timedelta(days=self.EXTEND_BY_DAYS)
        else:
            patron.membership_end = patron.membership_end + timedelta(days=self.EXTEND_BY_DAYS)
        self._patron_repository.update_patron(patron)
        return MembershipRenewalStatus.SUCCESS

    def find_patron_by_name(self, name: str):
        results = []
        all_patrons = self._patron_repository.get_all_patrons()
        for patron in all_patrons:
            if patron.name.lower() == name.lower():
                results.append(patron)
        return results
