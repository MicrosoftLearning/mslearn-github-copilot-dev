# Refactoring Hints for GitHub Copilot Exercise

## Goal

Consolidate duplicate code between multiple classes to improve maintainability and reduce code duplication.

## Duplicate Methods to Extract

### 1. Core Validation and Shipping Logic (Primary Focus)

Both `OrderProcessor` and `ReturnProcessor` have identical methods:

- **Validation Logic**: Basic null/empty validation, security validation, business rule validation
- **Shipping Calculation Logic**: Base shipping rates, weight-based calculations, free shipping thresholds

### 2. Service Layer Duplications (Advanced Real-World Examples)

**EmailService duplications:**
- `BuildEmailTemplate()` - Same template building pattern for orders and returns
- `FormatEmailSubject()` - Identical subject formatting logic  
- `SendEmail()` and `LogEmailActivity()` - Duplicated sending and audit logging

**AuditService duplications:**
- `CreateAuditEntry()` - Same audit entry creation pattern
- `ValidateAuditEntry()` - Identical validation logic for all audit entries
- `StoreAuditEntry()` and `CheckComplianceRequirements()` - Duplicated storage and compliance logic

**InventoryService duplications:**
- `ValidateInventoryAvailability()` - Same validation pattern for orders and returns
- `UpdateInventoryLevel()` - Identical update logic (only direction differs)
- `LogInventoryTransaction()` - Duplicated transaction logging pattern

## Suggested Refactoring Approach

### Option 1: Create Shared Services
```csharp
// Services/ValidationService.cs
public class ValidationService
{
    public bool ValidateId(string id, string idType) { /* ... */ }
}

// Services/ShippingService.cs  
public class ShippingService
{
    public decimal CalculateOrderShipping(Order order) { /* ... */ }
    public decimal CalculateReturnShipping(Return returnRequest) { /* ... */ }
}
```

### Option 2: Create Base Class
```csharp
// ProcessorBase.cs
public abstract class ProcessorBase
{
    protected bool Validate(string id) { /* ... */ }
    protected abstract decimal CalculateShipping(string id, object details);
}
```

### Option 3: Extract to Utility Class
```csharp
// Utils/ProcessingUtils.cs
public static class ProcessingUtils
{
    public static bool ValidateId(string id, string idType) { /* ... */ }
    // Shipping methods as needed
}
```

## Testing After Refactoring
1. Run `dotnet run` 
2. Compare output with `EXPECTED_OUTPUT.md`
3. Ensure all 5 test scenarios behave identically
4. Verify no functionality is lost

## Learning Objectives
- Identify code duplication patterns
- Choose appropriate refactoring strategies
- Maintain functionality while improving code structure
- Use GitHub Copilot to assist with code extraction and refactoring
