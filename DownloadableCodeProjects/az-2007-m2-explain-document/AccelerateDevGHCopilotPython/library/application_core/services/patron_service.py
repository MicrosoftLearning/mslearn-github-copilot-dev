from ..interfaces.ipatron_service import IPatronService
from ..interfaces.ipatron_repository import IPatronRepository
from ..enums.membership_renewal_status import MembershipRenewalStatus
from datetime import datetime, timedelta

class PatronService(IPatronService):
    def __init__(self, patron_repository: IPatronRepository):
        self._patron_repository = patron_repository

    def renew_membership(self, patron_id: int) -> MembershipRenewalStatus:
        patron = self._patron_repository.get_patron(patron_id)
        if patron is None:
            return MembershipRenewalStatus.PATRON_NOT_FOUND
        if patron.membership_end >= datetime.now() + timedelta(days=30):
            return MembershipRenewalStatus.TOO_EARLY_TO_RENEW
        if any(l.return_date is None and l.due_date < datetime.now() for l in getattr(patron, 'loans', [])):
            return MembershipRenewalStatus.LOAN_NOT_RETURNED
        patron.membership_end = patron.membership_end + timedelta(days=365)
        try:
            self._patron_repository.update_patron(patron)
            return MembershipRenewalStatus.SUCCESS
        except Exception:
            return MembershipRenewalStatus.ERROR
