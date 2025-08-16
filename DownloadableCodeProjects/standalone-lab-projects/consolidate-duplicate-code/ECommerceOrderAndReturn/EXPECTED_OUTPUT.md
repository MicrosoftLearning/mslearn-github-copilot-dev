// This file documents the expected behavior that should be maintained after consolidating duplicate code

/*
EXPECTED OUTPUT WHEN RUNNING THE APPLICATION:

=== E-Commerce Order and Return Processing System ===
Starting application tests...

INITIAL INVENTORY STATUS:
[INVENTORY] Current Stock Levels:
  PROD001: 50 units
  PROD002: 100 units
  PROD003: 25 units
  PROD004: 75 units

TEST 1: Processing a valid order
=== Starting Order Processing ===
Processing order: ORD12345
[VALIDATION] Validating order ID...
Security check: Validating Order ID: ORD*****
Security check passed: Order ID is valid
[AUDIT] Logging order activity: PROCESSING_STARTED
[AUDIT] Audit entry validation passed
[AUDIT] Storing: 2025-08-15 22:03:17 | ORDER | ORD12345 | PROCESSING_STARTED
[AUDIT] Details: Order validation passed
[COMPLIANCE] Checking compliance requirements...
[COMPLIANCE] Audit entry is compliant.
[SHIPPING] Calculating shipping cost...
Order ORD12345 shipping cost: $4.00
Order ORD12345 status updated to: Processing
Processing payment for order ORD12345, amount: $75.50
[AUDIT] Logging order activity: PAYMENT_STARTED
[AUDIT] Audit entry validation passed
[AUDIT] Storing: 2025-08-15 22:03:17 | ORDER | ORD12345 | PAYMENT_STARTED
[AUDIT] Details: Amount: $75.50
[COMPLIANCE] Checking compliance requirements...
[COMPLIANCE] Audit entry is compliant.
Payment processed successfully
[AUDIT] Logging order activity: PAYMENT_COMPLETED
[AUDIT] Audit entry validation passed
[AUDIT] Storing: 2025-08-15 22:03:17 | ORDER | ORD12345 | PAYMENT_COMPLETED
[AUDIT] Details: Payment successful
[COMPLIANCE] Checking compliance requirements...
[COMPLIANCE] Audit entry is compliant.
[INVENTORY] Processing inventory reservation for order ORD12345
[INVENTORY] Validating inventory availability...
[INVENTORY] Updated PROD001: 50 → 49 (Change: -1, Reason: RESERVED)
[INVENTORY] Logging inventory transaction...
[INVENTORY] Action: RESERVE, Transaction ID: ORD12345, Product ID: PROD001, Quantity: 1, Details: Order processing, Transaction Type: Restoration
[INVENTORY] Validating inventory availability...
[INVENTORY] Updated PROD002: 100 → 98 (Change: -2, Reason: RESERVED)
[INVENTORY] Logging inventory transaction...
[INVENTORY] Action: RESERVE, Transaction ID: ORD12345, Product ID: PROD002, Quantity: 2, Details: Order processing, Transaction Type: Restoration
[EMAIL] Preparing order confirmation email for CUST001
[EMAIL] Sending to customer CUST001: [E-Commerce] Order Confirmation - Transaction ID: ORD12345
[EMAIL] Content preview: Dear Customer CUST001,

Thank you for your purchas...
[EMAIL] Successfully sent!
[AUDIT] [2025-08-15 15:03:17] EMAIL_SENT | Type: OrderConfirmation | Customer: CUST001 | Transaction: ORD12345
[AUDIT] Logging order activity: PROCESSING_COMPLETED
[AUDIT] Audit entry validation passed
[AUDIT] Storing: 2025-08-15 22:03:17 | ORDER | ORD12345 | PROCESSING_COMPLETED
[AUDIT] Details: Total amount: $75.50, Shipping: $4.00
[COMPLIANCE] Checking compliance requirements...
[COMPLIANCE] Audit entry is compliant.
Order ORD12345 processed successfully!
=== Order Processing Complete ===

TEST 2: Processing a valid return
=== Starting Return Processing ===
Processing return: RET98765
[VALIDATION] Validating return ID...
Security check: Validating Return ID: RET*****
Security check passed: Return ID is valid
[AUDIT] Logging return activity: PROCESSING_STARTED
[AUDIT] Audit entry validation passed
[AUDIT] Storing: 2025-08-15 22:03:17 | RETURN | RET98765 | PROCESSING_STARTED
[AUDIT] Details: Return validation passed
[COMPLIANCE] Checking compliance requirements...
[COMPLIANCE] Audit entry is compliant.
Validating return eligibility for RET98765
Return eligibility validated successfully
[SHIPPING] Calculating return shipping cost...
Return RET98765 shipping cost: $3.00
Return RET98765 status updated to: Approved
Processing refund for return RET98765, amount: $699.99
[AUDIT] Logging return activity: REFUND_STARTED
[AUDIT] Audit entry validation passed
[AUDIT] Storing: 2025-08-15 22:03:17 | RETURN | RET98765 | REFUND_STARTED
[AUDIT] Details: Amount: $699.99
[COMPLIANCE] Checking compliance requirements...
[COMPLIANCE] Audit entry is compliant.
Refund processed successfully
[AUDIT] Logging return activity: REFUND_COMPLETED
[AUDIT] Audit entry validation passed
[AUDIT] Storing: 2025-08-15 22:03:18 | RETURN | RET98765 | REFUND_COMPLETED
[AUDIT] Details: Refund successful
[COMPLIANCE] Checking compliance requirements...
[COMPLIANCE] Audit entry is compliant.
[INVENTORY] Processing inventory restoration for return RET98765
[INVENTORY] Validating inventory availability...
[INVENTORY] Updated PROD001: 49 → 50 (Change: +1, Reason: RESTORED)
[INVENTORY] Logging inventory transaction...
[INVENTORY] Action: RESTORE, Transaction ID: RET98765, Product ID: PROD001, Quantity: 1, Details: Return processing, Transaction Type: Restoration
[EMAIL] Preparing return confirmation email for CUST001
[EMAIL] Sending to customer CUST001: [E-Commerce] Return Confirmation - Transaction ID: RET98765
[EMAIL] Content preview: Dear Customer CUST001,

We appreciate your return!...
[EMAIL] Successfully sent!
[AUDIT] [2025-08-15 15:03:18] EMAIL_SENT | Type: ReturnConfirmation | Customer: CUST001 | Transaction: RET98765
[AUDIT] Logging return activity: PROCESSING_COMPLETED
[AUDIT] Audit entry validation passed
[AUDIT] Storing: 2025-08-15 22:03:18 | RETURN | RET98765 | PROCESSING_COMPLETED
[AUDIT] Details: Refund amount: $699.99, Shipping: $3.00
[COMPLIANCE] Checking compliance requirements...
[COMPLIANCE] Audit entry is compliant.
Return RET98765 processed successfully!
=== Return Processing Complete ===

INVENTORY STATUS AFTER PROCESSING:
[INVENTORY] Current Stock Levels:
  PROD001: 50 units
  PROD002: 98 units
  PROD003: 25 units
  PROD004: 75 units

TEST 3: Processing an invalid order (security test)
=== Starting Order Processing ===
Processing order: INVALID123
[VALIDATION] Validating order ID...
Security check: Validating Order ID: INV*****
Security check failed: Order ID format is invalid
[VALIDATION] Order ID failed security validation.
Order INVALID123 is invalid and cannot be processed.
Exception thrown: 'System.ArgumentException' in ECommerceOrderAndReturn.dll
Error processing order INVALID123: Invalid order ID: INVALID123
=== Order Processing Complete ===

TEST 4: Processing an invalid return (security test)
=== Starting Return Processing ===
Processing return: <script>alert('xss')</script>
[VALIDATION] Validating return ID...
Security check: Validating Return ID: <sc*****
Security check failed: Return ID exceeds maximum length
[VALIDATION] Return ID failed security validation.
Return <script>alert('xss')</script> is invalid and cannot be processed.
Exception thrown: 'System.ArgumentException' in ECommerceOrderAndReturn.dll
Error processing return <script>alert('xss')</script>: Invalid return ID: <script>alert('xss')</script>
=== Return Processing Complete ===

TEST 5: Processing with empty ID
=== Starting Order Processing ===
Processing order: 
[VALIDATION] Validating order ID...
[VALIDATION] Order ID cannot be empty.
Order  is invalid and cannot be processed.
Exception thrown: 'System.ArgumentException' in ECommerceOrderAndReturn.dll
Error processing order : Invalid order ID: 
=== Order Processing Complete ===


=== All tests completed ===
Application is ready for refactoring exercise!

Duplicate code locations found:
1. Validate() method in both OrderProcessor and ReturnProcessor
2. CalculateShipping() method in both OrderProcessor and ReturnProcessor
3. Email template and sending logic in EmailService (SendOrderConfirmation & SendReturnConfirmation)
4. Audit entry creation and logging in AuditService (LogOrderActivity & LogReturnActivity)
5. Inventory validation and update logic in InventoryService (ReserveOrderInventory & RestoreReturnInventory)

These methods demonstrate common real-world duplicate code patterns and should be consolidated during the lab exercise.
Students can use GitHub Copilot to help identify and extract common functionality into shared services or base classes.

*/
