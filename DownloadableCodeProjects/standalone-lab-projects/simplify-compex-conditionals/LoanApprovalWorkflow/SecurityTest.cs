using System;

namespace LoanApprovalSystem
{
    /// <summary>
    /// Security testing class to demonstrate the security measures in the loan approval system
    /// </summary>
    public class SecurityTest
    {
        public static void RunSecurityTests()
        {
            Console.WriteLine("\n=== LOAN APPROVAL SECURITY TESTING ===");
            
            var evaluator = new LoanEvaluator();

            // Test 1: Null applicant protection
            Console.WriteLine("\n--- Test 1: Null Applicant Protection ---");
            try
            {
                #pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type - testing security
                var result = evaluator.Evaluate(null);
                #pragma warning restore CS8625
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"✓ Security measure working: {ex.Message}");
            }

            // Test 2: Invalid credit score bounds
            Console.WriteLine("\n--- Test 2: Invalid Credit Score Bounds ---");
            var invalidCreditApplicant = CreateValidApplicant();
            invalidCreditApplicant.CreditScore = 900; // Above valid range (300-850)
            Console.WriteLine("Testing with credit score of 900 (above valid range)...");
            var decision = evaluator.Evaluate(invalidCreditApplicant);
            Console.WriteLine($"Result: {decision.Status} - {decision.Notes}");

            // Test 3: Negative financial values
            Console.WriteLine("\n--- Test 3: Negative Financial Values ---");
            var negativeIncomeApplicant = CreateValidApplicant();
            negativeIncomeApplicant.AnnualIncome = -50000; // Negative income
            Console.WriteLine("Testing with negative annual income...");
            decision = evaluator.Evaluate(negativeIncomeApplicant);
            Console.WriteLine($"Result: {decision.Status} - {decision.Notes}");

            // Test 4: Extreme loan amount
            Console.WriteLine("\n--- Test 4: Extreme Loan Amount ---");
            var extremeLoanApplicant = CreateValidApplicant();
            extremeLoanApplicant.RequestedLoanAmount = 100_000_000; // $100M loan
            Console.WriteLine("Testing with $100M loan request (above reasonable limits)...");
            decision = evaluator.Evaluate(extremeLoanApplicant);
            Console.WriteLine($"Result: {decision.Status} - {decision.Notes}");

            // Test 5: Invalid debt-to-income ratio
            Console.WriteLine("\n--- Test 5: Invalid Debt-to-Income Ratio ---");
            var invalidDTIApplicant = CreateValidApplicant();
            invalidDTIApplicant.DebtToIncomeRatio = 1.5; // 150% DTI (impossible)
            Console.WriteLine("Testing with 150% debt-to-income ratio...");
            decision = evaluator.Evaluate(invalidDTIApplicant);
            Console.WriteLine($"Result: {decision.Status} - {decision.Notes}");

            // Test 6: Down payment exceeding property value
            Console.WriteLine("\n--- Test 6: Down Payment Exceeding Property Value ---");
            var invalidDownPaymentApplicant = CreateValidApplicant();
            invalidDownPaymentApplicant.PropertyValue = 300000;
            invalidDownPaymentApplicant.DownPayment = 400000; // Down payment > property value
            Console.WriteLine("Testing with down payment exceeding property value...");
            decision = evaluator.Evaluate(invalidDownPaymentApplicant);
            Console.WriteLine($"Result: {decision.Status} - {decision.Notes}");

            // Test 7: Extreme employment years
            Console.WriteLine("\n--- Test 7: Extreme Employment Years ---");
            var extremeEmploymentApplicant = CreateValidApplicant();
            extremeEmploymentApplicant.EmploymentYears = 75; // 75 years of employment
            Console.WriteLine("Testing with 75 years of employment...");
            decision = evaluator.Evaluate(extremeEmploymentApplicant);
            Console.WriteLine($"Result: {decision.Status} - {decision.Notes}");

            // Test 8: Valid applicant (should pass all security checks)
            Console.WriteLine("\n--- Test 8: Valid Applicant (Security Baseline) ---");
            var validApplicant = CreateValidApplicant();
            Console.WriteLine("Testing with valid applicant data...");
            decision = evaluator.Evaluate(validApplicant);
            Console.WriteLine($"Result: {decision.Status} - Rate: {decision.InterestRate}% - Amount: ${decision.ApprovedAmount:N0}");
            Console.WriteLine($"Notes: {decision.Notes}");

            // Test 9: Edge case - Zero property value (division by zero protection)
            Console.WriteLine("\n--- Test 9: Zero Property Value (Division Protection) ---");
            var zeroPropertyApplicant = CreateValidApplicant();
            zeroPropertyApplicant.PropertyValue = 0; // Should trigger safe division
            Console.WriteLine("Testing with zero property value (tests safe division)...");
            decision = evaluator.Evaluate(zeroPropertyApplicant);
            Console.WriteLine($"Result: {decision.Status} - {decision.Notes}");

            Console.WriteLine("\n=== LOAN APPROVAL SECURITY TESTING COMPLETE ===\n");
        }

        /// <summary>
        /// Creates a valid baseline applicant for security testing
        /// </summary>
        private static Applicant CreateValidApplicant()
        {
            return new Applicant
            {
                CreditScore = 720,
                AnnualIncome = 80000,
                VerifiedIncome = 78000,
                EmploymentYears = 5,
                EmploymentStatus = EmploymentType.Salaried,
                DebtToIncomeRatio = 0.30,
                RequestedLoanAmount = 300000,
                PropertyValue = 375000,
                DownPayment = 75000,
                LiquidAssets = 15000,
                CreditHistoryLength = 8,
                HasBankruptcyHistory = false,
                MonthsSinceBankruptcy = 0,
                HasForeclosureHistory = false,
                MonthsSinceForeclosure = 0,
                PurposeOfLoan = LoanPurpose.HomePurchase,
                PropertyCategory = PropertyType.PrimaryResidence,
                IsVeteran = false,
                IsFirstTimeHomebuyer = false,
                MonthlyDebtPayments = 2000,
                NumberOfInquiries = 3
            };
        }
    }
}
