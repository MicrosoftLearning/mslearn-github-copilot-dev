from abc import ABC, abstractmethod
from ..enums.membership_renewal_status import MembershipRenewalStatus

class IPatronService(ABC):
    @abstractmethod
    def renew_membership(self, patron_id: int) -> MembershipRenewalStatus:
        pass
