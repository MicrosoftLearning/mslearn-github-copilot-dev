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

import pytest

@pytest.fixture
def mock_patron_repo():
    class MockRepo:
        def __init__(self):
            self.patrons = {}
            self.updated = False
        def get_patron(self, patron_id):
            return self.patrons.get(patron_id)
        def update_patron(self, patron):
            self.updated = True
        def get_all_patrons(self):
            return list(self.patrons.values())
    return MockRepo()

@pytest.mark.parametrize("patron_id,exists,expected_status", [
    (1, True, MembershipRenewalStatus.SUCCESS),
    (2, False, MembershipRenewalStatus.PATRON_NOT_FOUND),
])
def test_renew_membership_param(mock_patron_repo, patron_id, exists, expected_status):
    from application_core.services.patron_service import PatronService
    from application_core.entities.patron import Patron
    from datetime import datetime, timedelta
    repo = mock_patron_repo
    if exists:
        repo.patrons[patron_id] = Patron(id=patron_id, name="Test", membership_end=datetime.now()-timedelta(days=1), membership_start=datetime.now()-timedelta(days=365))
    service = PatronService(repo)
    status = service.renew_membership(patron_id)
    assert status == expected_status

def test_find_patron_by_name(mock_patron_repo):
    from application_core.services.patron_service import PatronService
    from application_core.entities.patron import Patron
    repo = mock_patron_repo
    repo.patrons[1] = Patron(id=1, name="Alice", membership_end=datetime.now(), membership_start=datetime.now())
    repo.patrons[2] = Patron(id=2, name="Bob", membership_end=datetime.now(), membership_start=datetime.now())
    service = PatronService(repo)
    results = service.find_patron_by_name("Alice")
    assert len(results) == 1
    assert results[0].name == "Alice"

def test_renew_membership_assertion(mock_patron_repo):
    from application_core.services.patron_service import PatronService
    service = PatronService(mock_patron_repo)
    with pytest.raises(AssertionError):
        # This will fail because we assert False
        assert service.renew_membership(999) == MembershipRenewalStatus.SUCCESS