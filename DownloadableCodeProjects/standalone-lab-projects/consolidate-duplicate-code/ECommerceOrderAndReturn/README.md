# E-Commerce Order and Return Processing System

## Overview

This is a sample e-commerce application designed for a GitHub Copilot training course focused on consolidating duplicate code. The application demonstrates real-world scenarios while containing intentional code duplication that students will learn to refactor.

## Purpose

- **Training Focus**: Learn to identify and consolidate duplicate code using GitHub Copilot
- **Real-world Context**: Represents actual e-commerce order and return processing logic
- **Security Awareness**: Includes basic security validation patterns
- **Testing**: Provides verifiable output before and after refactoring

## Project Structure

```text
├── Models/
│   ├── Order.cs           # Order data model with items and status
│   └── Return.cs          # Return data model with refund details
├── Security/
│   └── SecurityValidator.cs # Security validation for IDs and input
├── Configuration/
│   └── AppConfig.cs       # Application configuration settings
├── Services/
│   ├── EmailService.cs    # Email notifications (contains duplicate logic)
│   ├── InventoryService.cs # Inventory management (contains duplicate logic)
│   └── AuditService.cs    # Audit logging (contains duplicate logic)
├── OrderProcessor.cs      # Processes customer orders (contains duplicate code)
├── ReturnProcessor.cs     # Processes product returns (contains duplicate code)
└── Program.cs             # Main application with test scenarios
```

## Features

- **Order Processing**: Complete order workflow with payment and inventory
- **Return Processing**: Return eligibility validation and refund processing
- **Security Validation**: Input validation with XSS/injection prevention
- **Error Handling**: Proper exception handling and logging
- **Configuration**: Centralized configuration for business rules
- **Testing**: Multiple test scenarios with expected outputs

## Running the Application

```bash
dotnet run
```

## Expected Output

The application runs 5 test scenarios:

1. Valid order processing
2. Valid return processing  
3. Invalid order ID (should fail)
4. Malicious input (should fail security check)
5. Empty ID (should fail validation)

## Lab Exercise Goals

Students will use GitHub Copilot to:

1. Identify the duplicate validation and shipping calculation logic
2. Extract common functionality into shared services
3. Refactor both processors to use the shared services
4. Verify the application still produces the same output after refactoring

## Security Features

- Input validation with pattern matching
- XSS/SQL injection prevention
- Data masking for logging
- Length validation to prevent buffer overflows
- Suspicious pattern detection

## Business Rules

- Order IDs must follow pattern: `^[A-Z]{3}\d{5}$` (e.g., ORD12345)
- Return IDs must follow pattern: `^[A-Z]{3}\d{5}$` (e.g., RET98765)  
- Free shipping on orders over $50
- Returns allowed within 30 days
- Maximum refund amount: $1,000
- Weight-based shipping calculation available

This application provides a realistic foundation for learning code consolidation techniques while maintaining security best practices and business logic integrity.
