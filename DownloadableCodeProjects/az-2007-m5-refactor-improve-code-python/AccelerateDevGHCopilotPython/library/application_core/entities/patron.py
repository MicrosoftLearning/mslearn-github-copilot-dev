from dataclasses import dataclass, field
from typing import List, Optional
from datetime import datetime
# from .loan import Loan  # Use string annotation to avoid circular import

@dataclass
class Patron:
    id: int
    name: str
    membership_end: datetime
    membership_start: datetime
    image_name: Optional[str] = None
    loans: List['Loan'] = field(default_factory=list)
