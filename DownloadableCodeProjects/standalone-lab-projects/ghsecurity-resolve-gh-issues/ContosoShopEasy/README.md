# ContosoShopEasy E-Commerce Platform

## Overview

ContosoShopEasy is a realistic e-commerce application designed specifically for educational purposes in a GitHub Copilot training course. The application demonstrates common security vulnerabilities found in real-world applications while maintaining a functional e-commerce workflow.

## Purpose

This application serves as starter code for teaching developers how to:
- Identify security vulnerabilities in code
- Use GitHub Copilot to resolve security issues
- Apply security best practices to e-commerce applications
- Understand the impact of insecure coding practices

## Architecture

The application follows a layered architecture pattern with the following components:

### Models (`Models/`)
- **Product.cs**: Comprehensive product model with realistic e-commerce properties
- **User.cs**: User model with authentication and profile information  
- **Order.cs**: Order management with items, payment info, and status tracking
- **Category.cs**: Product categorization system

### Services (`Services/`)
- **ProductService.cs**: Product catalog and search functionality
- **UserService.cs**: User registration, authentication, and management
- **PaymentService.cs**: Payment processing and validation
- **OrderService.cs**: Order creation and lifecycle management

### Data (`Data/`)
- **ProductRepository.cs**: In-memory product database with 40+ realistic products
- **UserRepository.cs**: User storage and retrieval with pre-populated test users
- **OrderRepository.cs**: Order persistence and querying

### Security (`Security/`)
- **SecurityValidator.cs**: Input validation and security checks (with intentional vulnerabilities)

## Intentional Security Vulnerabilities

The application contains the following security issues for educational purposes:

### 1. SQL Injection Vulnerabilities
- Product search functionality accepts unsanitized input
- Debug logging shows vulnerable SQL query construction
- User input validation bypasses dangerous characters

### 2. Weak Password Security
- Uses MD5 hashing for password storage (cryptographically weak)
- Minimal password complexity requirements
- Plaintext password logging during registration and login

### 3. Sensitive Data Exposure
- Full credit card numbers stored and logged
- CVV codes stored in payment records
- Debug logging exposes sensitive user information
- Admin credentials hardcoded and displayed in security audit

### 4. Hardcoded Credentials
- Admin username/password hardcoded in SecurityValidator
- Configuration values embedded in source code
- Predictable session token generation

### 5. Input Validation Issues
- Accepts potentially dangerous characters without proper sanitization
- Weak email validation (only checks for @ and .)
- No file upload restrictions
- XSS vulnerability through insufficient input sanitization

### 6. Information Disclosure
- Verbose error messages expose system internals
- Debug logging enabled in production-like environment
- Security audit displays sensitive configuration

## Sample Data

### Pre-populated Users
- **john_doe** (password: "hello") - Customer
- **jane_smith** (password: "test") - Customer  
- **admin** (password: "password") - Admin
- **mike_wilson** (password: "123456") - Customer
- **sarah_johnson** (password: "123456") - Employee

### Product Catalog
40+ realistic products across categories:
- Electronics (MacBook Pro, iPhone 15 Pro, etc.)
- Home & Garden (Dyson V15, Ninja Foodi, etc.)
- Clothing & Fashion (Levi's jeans, Nike shoes, etc.)
- Books & Media (Psychology of Money, Atomic Habits, etc.)
- Sports & Recreation (Peloton Bike, YETI products, etc.)

## Running the Application

### Prerequisites
- .NET 9.0 SDK
- Visual Studio Code or Visual Studio

### Build and Run
```bash
cd ContosoShopEasy
dotnet build
dotnet run
```

### Expected Output
The application will:
1. Display security audit results (showing vulnerabilities)
2. Demonstrate product search with SQL injection attempts
3. Register new users with weak password validation
4. Perform login attempts including admin backdoor
5. Create sample orders with realistic pricing
6. Process payments with security issues
7. Display order management and statistics

### Verification Points
- **Total products**: 40 items in catalog
- **Total users**: 8 registered users (5 pre-existing + 3 newly registered)
- **Total revenue**: Calculated from existing and new orders
- **Security issues**: Clearly visible in debug output and logs

## Learning Objectives

After working with this codebase, developers should be able to:

1. **Identify** common security vulnerabilities in e-commerce applications
2. **Understand** the impact of insecure coding practices
3. **Apply** secure coding principles using GitHub Copilot
4. **Implement** proper input validation and sanitization
5. **Secure** authentication and password handling
6. **Protect** sensitive data and payment information
7. **Remove** hardcoded secrets and credentials

## Lab Exercise Workflow

1. **Initial Assessment**: Run the application and observe security issues
2. **Vulnerability Analysis**: Use GitHub Copilot to identify security problems
3. **Code Remediation**: Apply fixes for each vulnerability category
4. **Testing & Validation**: Verify fixes don't break functionality
5. **Security Review**: Confirm all issues are resolved

## Important Notes

- This application is for **educational purposes only**
- Contains **intentional security vulnerabilities**
- Should **never be used in production**
- Demonstrates **realistic e-commerce scenarios** for effective learning
- Provides **verifiable output** for lab validation
- **No GitHub Secret Scanning alerts**: Hardcoded secrets have been simplified to avoid triggering GitHub Secret Scanning while maintaining educational value for other security vulnerabilities

## File Structure

```
ContosoShopEasy/
├── Models/
│   ├── Product.cs
│   ├── User.cs
│   ├── Order.cs
│   └── Category.cs
├── Services/
│   ├── ProductService.cs
│   ├── UserService.cs
│   ├── PaymentService.cs
│   └── OrderService.cs
├── Data/
│   ├── ProductRepository.cs
│   ├── UserRepository.cs
│   └── OrderRepository.cs
├── Security/
│   └── SecurityValidator.cs
├── Program.cs
├── ContosoShopEasy.csproj
└── README.md
```

## Contributing

This project is designed for educational use in Microsoft Learn training courses. The security vulnerabilities are intentional and should not be "fixed" in this repository as they serve as learning examples.