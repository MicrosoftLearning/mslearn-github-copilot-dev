# E-Commerce Order Processing System

## Overview

This is a sample e-commerce order processing application designed for educational purposes in a training course that teaches developers how to refactor large functions into smaller, single-purpose functions using GitHub Copilot.

## Project Structure

The application follows a layered architecture pattern commonly used in real-world .NET applications:

```
src/
├── ECommerce.ApplicationCore/          # Domain layer (entities, interfaces, business logic)
│   ├── Entities/                       # Domain models
│   │   ├── Order.cs
│   │   ├── OrderItem.cs
│   │   ├── PaymentInfo.cs
│   │   ├── OrderStatus.cs
│   │   ├── ShippingDetails.cs
│   │   └── OrderResult.cs
│   ├── Interfaces/                     # Service abstractions
│   │   ├── IInventoryService.cs
│   │   ├── IPaymentGateway.cs
│   │   ├── IShippingService.cs
│   │   ├── INotificationService.cs
│   │   ├── ISecurityValidator.cs
│   │   └── IAuditLogger.cs
│   └── Services/
│       └── OrderProcessor.cs          # MAIN LARGE METHOD TO REFACTOR
├── ECommerce.Infrastructure/           # Infrastructure layer (service implementations)
│   └── Services/
│       ├── SecurityValidator.cs
│       ├── AuditLogger.cs
│       └── ExternalServices.cs
└── ECommerce.Console/                  # Presentation layer (console app)
    └── Program.cs
```

## Key Features

This application demonstrates real-world e-commerce scenarios with:

### Business Logic

- **Order Processing**: Complete order lifecycle from validation to completion
- **Inventory Management**: Stock checking and reservation
- **Payment Processing**: Secure payment validation and processing
- **Shipping Management**: Shipment scheduling and tracking
- **Customer Notifications**: Order confirmations and alerts

### Security Features

- **Input Validation**: Comprehensive validation of all user inputs
- **Email Security**: Email format validation and suspicious domain detection
- **Payment Security**: Credit card validation and fraud detection
- **Risk Assessment**: Automated risk scoring for suspicious orders
- **Data Masking**: Sensitive information masking in logs

### Audit & Compliance

- **Audit Logging**: Complete audit trail of all order processing activities
- **Security Events**: Logging of security-related events and failures
- **Error Handling**: Comprehensive error handling with cleanup procedures

### Real-World Architecture

- **Dependency Injection**: Proper service abstractions and implementations
- **Separation of Concerns**: Clear separation between domain, infrastructure, and presentation layers
- **Interface-Based Design**: All services implement interfaces for testability
- **Layered Architecture**: Follows Clean Architecture principles

## The Refactoring Challenge

### Current State: Large Function

The `OrderProcessor.ProcessOrder()` method is intentionally large (200+ lines) and contains multiple responsibilities:

1. **Input Validation** - Validating order data, email formats, payment info
2. **Security Checks** - Risk assessment, fraud detection, suspicious pattern detection  
3. **Inventory Management** - Stock checking, reservation, and release
4. **Payment Processing** - Payment validation, processing, and error handling
5. **Shipping Management** - Shipping validation, scheduling, and tracking
6. **Notification Management** - Sending confirmations and alerts
7. **Order Finalization** - Status updates, completion tracking, and cleanup
8. **Audit Logging** - Security and business event logging
9. **Error Handling** - Exception management and recovery procedures

### Refactoring Goals

Students should refactor this large method into smaller, focused methods such as:

- `ValidateOrderInput()`
- `PerformSecurityChecks()`
- `ReserveInventory()`
- `ProcessPayment()`
- `ScheduleShipping()`
- `SendNotifications()`
- `FinalizeOrder()`

## Testing

### Running the Application

```bash
# Build the solution
dotnet build ECommerceOrderProcessing.sln

# Run the console application
cd src/ECommerce.Console
dotnet run

# Or use the provided batch file (Windows)
run.bat
```

### Test Cases Included

The application includes four comprehensive test cases:

1. **Valid Order** - Complete successful order processing
2. **Invalid Email** - Tests email validation
3. **Declined Payment** - Tests payment failure handling
4. **Suspicious Order** - Tests security risk assessment

### Expected Output

- All tests should pass (4/4)
- Successful orders generate tracking numbers
- Invalid orders are rejected with appropriate error messages
- Audit log file is created with detailed event tracking

## Security Considerations

This sample application includes basic security practices suitable for educational purposes:

- Input validation and sanitization
- Sensitive data masking in logs
- Basic fraud detection patterns
- Secure error handling without information leakage
- Audit trail for compliance

## File Outputs

- **Console Output**: Real-time processing information and test results
- **order_audit_log.txt**: Complete audit trail of all processing activities

## Educational Value

This project provides an excellent foundation for learning:

- How to identify code that needs refactoring
- Breaking down large functions into single-responsibility methods
- Maintaining functionality while improving code structure
- Using GitHub Copilot to assist with refactoring tasks
- Testing to ensure behavior remains consistent after refactoring
- Understanding real-world software architecture patterns

## Course Integration

Before refactoring:

1. Run the application and document the output
2. Note the processing time and functionality
3. Review the audit log contents
4. Study the `OrderProcessor.ProcessOrder()` method structure

After refactoring:

1. Verify the output matches the original
2. Confirm all test cases still pass
3. Ensure audit logging continues to work correctly
4. Validate that the processing behavior is identical

This ensures students can confidently refactor the code while maintaining all existing functionality and learning proper software architecture principles.
