import unittest
from unittest.mock import MagicMock
from application_core.services.patron_service import PatronService
from application_core.entities.patron import Patron
from application_core.enums.membership_renewal_status import MembershipRenewalStatus
from datetime import datetime, timedelta

class PatronServiceTest(unittest.TestCase):
    def setUp(self):
        self.mock_repo = MagicMock()
        self.service = PatronService(self.mock_repo)

    def test_renew_membership_success(self):
        patron = Patron(id=1, name="John Doe", membership_end=datetime.now()-timedelta(days=1), membership_start=datetime.now()-timedelta(days=365))
        self.mock_repo.get_patron.return_value = patron
        self.mock_repo.update_patron.return_value = None
        status = self.service.renew_membership(1)
        self.assertEqual(status, MembershipRenewalStatus.SUCCESS)

    def test_renew_membership_patron_not_found(self):
        self.mock_repo.get_patron.return_value = None
        status = self.service.renew_membership(1)
        self.assertEqual(status, MembershipRenewalStatus.PATRON_NOT_FOUND)

if __name__ == "__main__":
    unittest.main()