using System;
using System.Collections.Generic;

namespace LoanApprovalSystem
{
    public enum ApprovalStatus
    {
        Approved,
        ConditionallyApproved,
        Declined
    }

    public class LoanDecision
    {
        public ApprovalStatus Status { get; set; }
        public double InterestRate { get; set; }
        public double ApprovedAmount { get; set; }
        public string? Notes { get; set; }
    }

    public enum EmploymentType { Salaried, SelfEmployed, Contract, Retired, Unemployed }
    public enum LoanPurpose { HomePurchase, Refinance, HomeEquity, Personal, Business }
    public enum PropertyType { PrimaryResidence, SecondHome, Investment, Commercial }

    public class Applicant
    {
        public int CreditScore { get; set; }
        public double AnnualIncome { get; set; }
        public double VerifiedIncome { get; set; } // Income after verification
        public int EmploymentYears { get; set; }
        public EmploymentType EmploymentStatus { get; set; }
        public double DebtToIncomeRatio { get; set; }
        public double RequestedLoanAmount { get; set; }
        public double PropertyValue { get; set; } // Appraised value
        public double DownPayment { get; set; }
        public double LiquidAssets { get; set; } // Cash reserves
        public int CreditHistoryLength { get; set; } // Years of credit history
        public bool HasBankruptcyHistory { get; set; }
        public int MonthsSinceBankruptcy { get; set; }
        public bool HasForeclosureHistory { get; set; }
        public int MonthsSinceForeclosure { get; set; }
        public LoanPurpose PurposeOfLoan { get; set; }
        public PropertyType PropertyCategory { get; set; }
        public bool IsVeteran { get; set; }
        public bool IsFirstTimeHomebuyer { get; set; }
        public double MonthlyDebtPayments { get; set; }
        public int NumberOfInquiries { get; set; } // Recent credit inquiries
    }

    public class LoanEvaluator
    {
        public LoanDecision Evaluate(Applicant applicant)
        {
            // Security: Input validation to prevent null reference and invalid data attacks
            if (applicant == null)
            {
                throw new ArgumentNullException(nameof(applicant), "Applicant cannot be null");
            }

            if (!IsValidApplicantData(applicant))
            {
                return new LoanDecision
                {
                    Status = ApprovalStatus.Declined,
                    Notes = "Application declined due to invalid or incomplete financial data"
                };
            }

            var decision = new LoanDecision();
            
            // Security: Safe financial ratio calculations with bounds checking
            double loanToValueRatio = SafeDivide(applicant.RequestedLoanAmount, applicant.PropertyValue);
            double monthlyIncome = SafeDivide(applicant.VerifiedIncome, 12);
            double monthlyPayment = CalculateMonthlyPayment(applicant.RequestedLoanAmount, 4.5);
            double paymentToIncomeRatio = SafeDivide(monthlyPayment, monthlyIncome);
            double liquidityRatio = SafeDivide(applicant.LiquidAssets, applicant.RequestedLoanAmount);

            // Level 1: Credit Score Primary Assessment - Foundation of loan approval
            if (applicant.CreditScore >= 740)
            {
                // Level 2: Income Verification and Stability - High credit tier analysis
                if (applicant.VerifiedIncome >= applicant.AnnualIncome * 0.95) // Income verification within 5%
                {
                    // Level 3: Employment Stability - Career consistency evaluation
                    if (applicant.EmploymentStatus == EmploymentType.Salaried && applicant.EmploymentYears >= 2)
                    {
                        // Level 4: Debt Service Coverage - Financial capacity analysis
                        if (applicant.DebtToIncomeRatio <= 0.36 && paymentToIncomeRatio <= 0.28)
                        {
                            // Level 5: Loan-to-Value Assessment - Risk mitigation analysis
                            if (loanToValueRatio <= 0.80)
                            {
                                // Level 6: Liquidity and Reserve Analysis - Financial cushion evaluation
                                if (liquidityRatio >= 0.03) // 3% of loan amount in liquid assets
                                {
                                    // Level 7: Credit History Depth - Long-term creditworthiness
                                    if (applicant.CreditHistoryLength >= 7)
                                    {
                                        // Level 8: Loan Purpose and Property Type - Risk categorization
                                        if (applicant.PurposeOfLoan == LoanPurpose.HomePurchase && 
                                            applicant.PropertyCategory == PropertyType.PrimaryResidence)
                                        {
                                            // Prime borrower - best terms
                                            decision.Status = ApprovalStatus.Approved;
                                            decision.InterestRate = 3.25;
                                            decision.ApprovedAmount = applicant.RequestedLoanAmount;
                                            decision.Notes = "Prime borrower approved at best available rate.";
                                        }
                                        else if (applicant.PropertyCategory == PropertyType.SecondHome)
                                        {
                                            // Second home premium
                                            decision.Status = ApprovalStatus.Approved;
                                            decision.InterestRate = 3.75;
                                            decision.ApprovedAmount = applicant.RequestedLoanAmount;
                                            decision.Notes = "Approved for second home with rate premium.";
                                        }
                                        else
                                        {
                                            // Investment property higher rates
                                            decision.Status = ApprovalStatus.Approved;
                                            decision.InterestRate = 4.25;
                                            decision.ApprovedAmount = applicant.RequestedLoanAmount;
                                            decision.Notes = "Investment property approved with higher rate.";
                                        }
                                    }
                                    else
                                    {
                                        // Shorter credit history penalty
                                        decision.Status = ApprovalStatus.Approved;
                                        decision.InterestRate = 3.75;
                                        decision.ApprovedAmount = applicant.RequestedLoanAmount;
                                        decision.Notes = "Approved with rate adjustment for limited credit history.";
                                    }
                                }
                                else
                                {
                                    // Insufficient liquid reserves
                                    decision.Status = ApprovalStatus.ConditionallyApproved;
                                    decision.InterestRate = 4.0;
                                    decision.ApprovedAmount = applicant.RequestedLoanAmount;
                                    decision.Notes = "Conditional approval pending additional reserves requirement.";
                                }
                            }
                            else if (loanToValueRatio <= 0.90)
                            {
                                // Higher LTV requires mortgage insurance
                                decision.Status = ApprovalStatus.Approved;
                                decision.InterestRate = 4.0;
                                decision.ApprovedAmount = applicant.RequestedLoanAmount;
                                decision.Notes = "Approved with mortgage insurance required for high LTV.";
                            }
                            else
                            {
                                // LTV too high - reduce loan amount
                                decision.Status = ApprovalStatus.Approved;
                                decision.InterestRate = 4.25;
                                decision.ApprovedAmount = applicant.PropertyValue * 0.90;
                                decision.Notes = "Approved with reduced amount due to LTV limits.";
                            }
                        }
                        else if (applicant.DebtToIncomeRatio <= 0.43) // Qualified mortgage limits
                        {
                            // Higher DTI but within QM guidelines
                            decision.Status = ApprovalStatus.Approved;
                            decision.InterestRate = 4.5;
                            decision.ApprovedAmount = applicant.RequestedLoanAmount * 0.85;
                            decision.Notes = "Approved with rate premium for elevated debt ratios.";
                        }
                        else
                        {
                            // DTI exceeds qualified mortgage guidelines
                            decision.Status = ApprovalStatus.ConditionallyApproved;
                            decision.InterestRate = 5.0;
                            decision.ApprovedAmount = applicant.RequestedLoanAmount * 0.75;
                            decision.Notes = "Conditional approval requiring debt reduction plan.";
                        }
                    }
                    else if (applicant.EmploymentStatus == EmploymentType.SelfEmployed && applicant.EmploymentYears >= 2)
                    {
                        // Self-employed borrowers require additional documentation
                        decision.Status = ApprovalStatus.ConditionallyApproved;
                        decision.InterestRate = 4.25;
                        decision.ApprovedAmount = applicant.RequestedLoanAmount * 0.90;
                        decision.Notes = "Conditional approval pending additional income documentation for self-employed.";
                    }
                    else
                    {
                        // Employment stability concerns
                        decision.Status = ApprovalStatus.ConditionallyApproved;
                        decision.InterestRate = 4.75;
                        decision.ApprovedAmount = applicant.RequestedLoanAmount * 0.80;
                        decision.Notes = "Conditional approval due to employment history concerns.";
                    }
                }
                else
                {
                    // Income verification discrepancy
                    decision.Status = ApprovalStatus.ConditionallyApproved;
                    decision.InterestRate = 4.5;
                    decision.ApprovedAmount = Math.Min(applicant.RequestedLoanAmount, applicant.VerifiedIncome * 4);
                    decision.Notes = "Conditional approval pending income re-verification.";
                }
            }
            else if (applicant.CreditScore >= 620) // FHA minimum
            {
                // Level 2: Government Program Eligibility - Alternative approval paths
                if (applicant.IsFirstTimeHomebuyer || applicant.IsVeteran)
                {
                    // Level 3: Program-specific DTI allowances - Enhanced qualification criteria
                    if (applicant.DebtToIncomeRatio <= 0.41) // FHA/VA allowances
                    {
                        // Level 4: Down Payment and LTV Analysis - Program requirements
                        if ((applicant.IsVeteran && loanToValueRatio <= 1.0) || 
                            (applicant.IsFirstTimeHomebuyer && applicant.DownPayment >= applicant.PropertyValue * 0.035))
                        {
                            // Level 5: Credit Event Recovery Analysis - Past financial difficulties
                            if (!applicant.HasBankruptcyHistory && !applicant.HasForeclosureHistory)
                            {
                                // Level 6: Recent Credit Activity - Current credit management
                                if (applicant.NumberOfInquiries <= 6) // Reasonable shopping activity
                                {
                                    // Government program approval
                                    decision.Status = ApprovalStatus.Approved;
                                    decision.InterestRate = applicant.IsVeteran ? 3.5 : 4.0;
                                    decision.ApprovedAmount = applicant.RequestedLoanAmount;
                                    decision.Notes = applicant.IsVeteran ? 
                                        "VA loan approved with government backing." : 
                                        "FHA loan approved for first-time homebuyer.";
                                }
                                else
                                {
                                    // Too much recent credit activity
                                    decision.Status = ApprovalStatus.ConditionallyApproved;
                                    decision.InterestRate = 4.5;
                                    decision.ApprovedAmount = applicant.RequestedLoanAmount;
                                    decision.Notes = "Conditional approval pending explanation of recent credit inquiries.";
                                }
                            }
                            else if (applicant.HasBankruptcyHistory && applicant.MonthsSinceBankruptcy >= 24)
                            {
                                // Bankruptcy recovery period met
                                decision.Status = ApprovalStatus.Approved;
                                decision.InterestRate = 5.0;
                                decision.ApprovedAmount = applicant.RequestedLoanAmount * 0.90;
                                decision.Notes = "Approved with rate premium due to bankruptcy history.";
                            }
                            else if (applicant.HasForeclosureHistory && applicant.MonthsSinceForeclosure >= 36)
                            {
                                // Foreclosure waiting period met
                                decision.Status = ApprovalStatus.Approved;
                                decision.InterestRate = 5.25;
                                decision.ApprovedAmount = applicant.RequestedLoanAmount * 0.85;
                                decision.Notes = "Approved with premium for foreclosure history.";
                            }
                            else
                            {
                                // Credit events too recent
                                decision.Status = ApprovalStatus.Declined;
                                decision.Notes = "Declined due to recent bankruptcy or foreclosure.";
                            }
                        }
                        else
                        {
                            // Insufficient down payment
                            decision.Status = ApprovalStatus.ConditionallyApproved;
                            decision.InterestRate = 4.75;
                            decision.ApprovedAmount = applicant.PropertyValue * 0.965; // Maximum FHA
                            decision.Notes = "Conditional approval requiring increased down payment.";
                        }
                    }
                    else
                    {
                        // DTI too high even for government programs
                        decision.Status = ApprovalStatus.Declined;
                        decision.Notes = "Declined due to debt-to-income ratio exceeding program limits.";
                    }
                }
                else
                {
                    // Non-government conventional loan at lower credit tier
                    if (applicant.DebtToIncomeRatio <= 0.38 && loanToValueRatio <= 0.75)
                    {
                        decision.Status = ApprovalStatus.Approved;
                        decision.InterestRate = 5.5;
                        decision.ApprovedAmount = Math.Min(applicant.RequestedLoanAmount, 400000);
                        decision.Notes = "Approved with conservative terms for credit profile.";
                    }
                    else
                    {
                        decision.Status = ApprovalStatus.Declined;
                        decision.Notes = "Declined due to credit score and debt ratio combination.";
                    }
                }
            }
            else
            {
                // Credit score below lending thresholds
                decision.Status = ApprovalStatus.Declined;
                decision.Notes = "Declined due to credit score below minimum lending standards.";
            }

            return decision;
        }

        /// <summary>
        /// Security: Validates applicant data to prevent malicious or invalid inputs
        /// </summary>
        private bool IsValidApplicantData(Applicant applicant)
        {
            // Validate credit score range (300-850 is typical FICO range)
            if (applicant.CreditScore < 300 || applicant.CreditScore > 850)
                return false;

            // Validate financial amounts are positive and reasonable
            if (applicant.AnnualIncome <= 0 || applicant.AnnualIncome > 10_000_000)
                return false;

            if (applicant.VerifiedIncome <= 0 || applicant.VerifiedIncome > 10_000_000)
                return false;

            if (applicant.RequestedLoanAmount <= 0 || applicant.RequestedLoanAmount > 50_000_000)
                return false;

            if (applicant.PropertyValue <= 0 || applicant.PropertyValue > 100_000_000)
                return false;

            // Validate ratios are within reasonable bounds
            if (applicant.DebtToIncomeRatio < 0 || applicant.DebtToIncomeRatio > 1.0)
                return false;

            // Validate employment years is reasonable
            if (applicant.EmploymentYears < 0 || applicant.EmploymentYears > 50)
                return false;

            // Validate credit history length
            if (applicant.CreditHistoryLength < 0 || applicant.CreditHistoryLength > 50)
                return false;

            // Validate down payment doesn't exceed property value
            if (applicant.DownPayment < 0 || applicant.DownPayment > applicant.PropertyValue)
                return false;

            return true;
        }

        /// <summary>
        /// Security: Safe division operation to prevent division by zero errors
        /// </summary>
        private double SafeDivide(double numerator, double denominator)
        {
            if (Math.Abs(denominator) < 0.01) // Effectively zero
                return 0; // Return 0 instead of throwing exception for demo purposes
            
            return numerator / denominator;
        }

        /// <summary>
        /// Security: Enhanced payment calculation with input validation and overflow protection
        /// </summary>
        private double CalculateMonthlyPayment(double loanAmount, double annualRate)
        {
            // Input validation
            if (loanAmount <= 0 || annualRate < 0 || annualRate > 30) // Max 30% interest rate
            {
                throw new ArgumentException("Invalid loan amount or interest rate");
            }

            // Handle zero interest rate case
            if (Math.Abs(annualRate) < 0.001)
            {
                return loanAmount / 360; // Simple division for 0% interest
            }

            double monthlyRate = annualRate / 100 / 12;
            int numberOfPayments = 360; // 30 years
            
            try
            {
                double factor = Math.Pow(1 + monthlyRate, numberOfPayments);
                return loanAmount * (monthlyRate * factor) / (factor - 1);
            }
            catch (OverflowException)
            {
                // Handle mathematical overflow
                throw new ArgumentException("Loan calculation resulted in mathematical overflow");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var applicants = CreateTestApplicants();
            var evaluator = new LoanEvaluator();

            Console.WriteLine("=== Loan Approval Workflow Test Results ===\n");

            for (int i = 0; i < applicants.Count; i++)
            {
                var applicant = applicants[i];
                var decision = evaluator.Evaluate(applicant);

                Console.WriteLine($"Applicant {i + 1}:");
                Console.WriteLine($"  Credit Score: {applicant.CreditScore}");
                // Security: In production, consider masking/logging sensitive financial data appropriately
                Console.WriteLine($"  Annual Income: ${applicant.AnnualIncome:N0}");
                Console.WriteLine($"  Verified Income: ${applicant.VerifiedIncome:N0}");
                Console.WriteLine($"  Employment: {applicant.EmploymentStatus} ({applicant.EmploymentYears} years)");
                Console.WriteLine($"  Debt-to-Income Ratio: {applicant.DebtToIncomeRatio:P1}");
                Console.WriteLine($"  Requested Amount: ${applicant.RequestedLoanAmount:N0}");
                Console.WriteLine($"  Property Value: ${applicant.PropertyValue:N0}");
                Console.WriteLine($"  Down Payment: ${applicant.DownPayment:N0}");
                Console.WriteLine($"  Liquid Assets: ${applicant.LiquidAssets:N0}");
                Console.WriteLine($"  Credit History: {applicant.CreditHistoryLength} years");
                Console.WriteLine($"  Loan Purpose: {applicant.PurposeOfLoan}");
                Console.WriteLine($"  Property Type: {applicant.PropertyCategory}");
                Console.WriteLine($"  Veteran: {applicant.IsVeteran}");
                Console.WriteLine($"  First-Time Homebuyer: {applicant.IsFirstTimeHomebuyer}");
                Console.WriteLine();
                Console.WriteLine($"  RESULT:");
                Console.WriteLine($"  Status: {decision.Status}");
                Console.WriteLine($"  Interest Rate: {decision.InterestRate}%");
                Console.WriteLine($"  Approved Amount: ${decision.ApprovedAmount:N0}");
                Console.WriteLine($"  Notes: {decision.Notes}");
                Console.WriteLine(new string('-', 60));
                Console.WriteLine();
            }

            // Run security tests to demonstrate security measures
            SecurityTest.RunSecurityTests();
        }

        /// <summary>
        /// Creates a collection of test applicants demonstrating realistic loan approval scenarios
        /// </summary>
        /// <returns>List of applicants with varying financial profiles</returns>
        private static List<Applicant> CreateTestApplicants()
        {
            return new List<Applicant>
            {
                // Scenario 1: Prime borrower - Excellent credit, stable employment, primary residence
                // Expected: Approved at 3.25% - best available rate
                new Applicant
                {
                    CreditScore = 780,
                    AnnualIncome = 120000,
                    VerifiedIncome = 118000, // 98% verification
                    EmploymentYears = 8,
                    EmploymentStatus = EmploymentType.Salaried,
                    DebtToIncomeRatio = 0.25,
                    RequestedLoanAmount = 400000,
                    PropertyValue = 500000, // LTV = 80%
                    DownPayment = 100000,
                    LiquidAssets = 25000, // 6.25% of loan amount
                    CreditHistoryLength = 12,
                    HasBankruptcyHistory = false,
                    MonthsSinceBankruptcy = 0,
                    HasForeclosureHistory = false,
                    MonthsSinceForeclosure = 0,
                    PurposeOfLoan = LoanPurpose.HomePurchase,
                    PropertyCategory = PropertyType.PrimaryResidence,
                    IsVeteran = false,
                    IsFirstTimeHomebuyer = false,
                    MonthlyDebtPayments = 2500,
                    NumberOfInquiries = 2
                },

                // Scenario 2: Second home purchase - High credit score, investment property
                // Expected: Approved at 3.75% with second home premium
                new Applicant
                {
                    CreditScore = 760,
                    AnnualIncome = 150000,
                    VerifiedIncome = 145000,
                    EmploymentYears = 6,
                    EmploymentStatus = EmploymentType.Salaried,
                    DebtToIncomeRatio = 0.32,
                    RequestedLoanAmount = 320000,
                    PropertyValue = 400000, // LTV = 80%
                    DownPayment = 80000,
                    LiquidAssets = 15000,
                    CreditHistoryLength = 10,
                    HasBankruptcyHistory = false,
                    MonthsSinceBankruptcy = 0,
                    HasForeclosureHistory = false,
                    MonthsSinceForeclosure = 0,
                    PurposeOfLoan = LoanPurpose.HomePurchase,
                    PropertyCategory = PropertyType.SecondHome,
                    IsVeteran = false,
                    IsFirstTimeHomebuyer = false,
                    MonthlyDebtPayments = 4000,
                    NumberOfInquiries = 3
                },

                // Scenario 3: VA loan candidate - Veteran with good credit
                // Expected: Approved at 3.5% with VA loan benefits
                new Applicant
                {
                    CreditScore = 680,
                    AnnualIncome = 75000,
                    VerifiedIncome = 73000,
                    EmploymentYears = 4,
                    EmploymentStatus = EmploymentType.Salaried,
                    DebtToIncomeRatio = 0.38,
                    RequestedLoanAmount = 280000,
                    PropertyValue = 280000, // LTV = 100% (VA allows)
                    DownPayment = 0, // VA loan - no down payment required
                    LiquidAssets = 12000,
                    CreditHistoryLength = 8,
                    HasBankruptcyHistory = false,
                    MonthsSinceBankruptcy = 0,
                    HasForeclosureHistory = false,
                    MonthsSinceForeclosure = 0,
                    PurposeOfLoan = LoanPurpose.HomePurchase,
                    PropertyCategory = PropertyType.PrimaryResidence,
                    IsVeteran = true,
                    IsFirstTimeHomebuyer = false,
                    MonthlyDebtPayments = 2375,
                    NumberOfInquiries = 4
                },

                // Scenario 4: Self-employed borrower - Good credit but employment complexity
                // Expected: Conditional approval pending additional documentation
                new Applicant
                {
                    CreditScore = 750,
                    AnnualIncome = 95000,
                    VerifiedIncome = 85000, // Self-employed income harder to verify
                    EmploymentYears = 3,
                    EmploymentStatus = EmploymentType.SelfEmployed,
                    DebtToIncomeRatio = 0.30,
                    RequestedLoanAmount = 350000,
                    PropertyValue = 450000, // LTV = 78%
                    DownPayment = 100000,
                    LiquidAssets = 20000,
                    CreditHistoryLength = 9,
                    HasBankruptcyHistory = false,
                    MonthsSinceBankruptcy = 0,
                    HasForeclosureHistory = false,
                    MonthsSinceForeclosure = 0,
                    PurposeOfLoan = LoanPurpose.HomePurchase,
                    PropertyCategory = PropertyType.PrimaryResidence,
                    IsVeteran = false,
                    IsFirstTimeHomebuyer = false,
                    MonthlyDebtPayments = 2375,
                    NumberOfInquiries = 5
                },

                // Scenario 5: First-time homebuyer with FHA loan - Lower credit, minimal down payment
                // Expected: Approved at 4.0% with FHA program
                new Applicant
                {
                    CreditScore = 640,
                    AnnualIncome = 65000,
                    VerifiedIncome = 64000,
                    EmploymentYears = 3,
                    EmploymentStatus = EmploymentType.Salaried,
                    DebtToIncomeRatio = 0.40,
                    RequestedLoanAmount = 240000,
                    PropertyValue = 250000, // LTV = 96%
                    DownPayment = 10000, // 4% down payment
                    LiquidAssets = 8000,
                    CreditHistoryLength = 6,
                    HasBankruptcyHistory = false,
                    MonthsSinceBankruptcy = 0,
                    HasForeclosureHistory = false,
                    MonthsSinceForeclosure = 0,
                    PurposeOfLoan = LoanPurpose.HomePurchase,
                    PropertyCategory = PropertyType.PrimaryResidence,
                    IsVeteran = false,
                    IsFirstTimeHomebuyer = true,
                    MonthlyDebtPayments = 2167,
                    NumberOfInquiries = 6
                },

                // Scenario 6: Bankruptcy recovery - Good recovery but recent bankruptcy
                // Expected: Approved with rate premium due to credit event
                new Applicant
                {
                    CreditScore = 650,
                    AnnualIncome = 80000,
                    VerifiedIncome = 79000,
                    EmploymentYears = 5,
                    EmploymentStatus = EmploymentType.Salaried,
                    DebtToIncomeRatio = 0.35,
                    RequestedLoanAmount = 200000,
                    PropertyValue = 250000, // LTV = 80%
                    DownPayment = 50000,
                    LiquidAssets = 15000,
                    CreditHistoryLength = 4, // Credit rebuilding after bankruptcy
                    HasBankruptcyHistory = true,
                    MonthsSinceBankruptcy = 30, // 2.5 years since bankruptcy
                    HasForeclosureHistory = false,
                    MonthsSinceForeclosure = 0,
                    PurposeOfLoan = LoanPurpose.HomePurchase,
                    PropertyCategory = PropertyType.PrimaryResidence,
                    IsVeteran = true, // Eligible for VA loan
                    IsFirstTimeHomebuyer = false,
                    MonthlyDebtPayments = 2333,
                    NumberOfInquiries = 8
                },

                // Scenario 7: High DTI but strong credit - Debt consolidation opportunity
                // Expected: Conditional approval requiring debt reduction
                new Applicant
                {
                    CreditScore = 720,
                    AnnualIncome = 90000,
                    VerifiedIncome = 88000,
                    EmploymentYears = 4,
                    EmploymentStatus = EmploymentType.Salaried,
                    DebtToIncomeRatio = 0.48, // Above qualified mortgage limits
                    RequestedLoanAmount = 300000,
                    PropertyValue = 380000, // LTV = 79%
                    DownPayment = 80000,
                    LiquidAssets = 12000,
                    CreditHistoryLength = 8,
                    HasBankruptcyHistory = false,
                    MonthsSinceBankruptcy = 0,
                    HasForeclosureHistory = false,
                    MonthsSinceForeclosure = 0,
                    PurposeOfLoan = LoanPurpose.Refinance,
                    PropertyCategory = PropertyType.PrimaryResidence,
                    IsVeteran = false,
                    IsFirstTimeHomebuyer = false,
                    MonthlyDebtPayments = 3600,
                    NumberOfInquiries = 12
                },

                // Scenario 8: Below minimum credit score - Declined
                // Expected: Declined due to credit score below lending standards
                new Applicant
                {
                    CreditScore = 580, // Below FHA minimum
                    AnnualIncome = 55000,
                    VerifiedIncome = 54000,
                    EmploymentYears = 2,
                    EmploymentStatus = EmploymentType.Salaried,
                    DebtToIncomeRatio = 0.42,
                    RequestedLoanAmount = 180000,
                    PropertyValue = 200000, // LTV = 90%
                    DownPayment = 20000,
                    LiquidAssets = 5000,
                    CreditHistoryLength = 3,
                    HasBankruptcyHistory = false,
                    MonthsSinceBankruptcy = 0,
                    HasForeclosureHistory = true,
                    MonthsSinceForeclosure = 18, // Too recent
                    PurposeOfLoan = LoanPurpose.HomePurchase,
                    PropertyCategory = PropertyType.PrimaryResidence,
                    IsVeteran = false,
                    IsFirstTimeHomebuyer = true,
                    MonthlyDebtPayments = 1925,
                    NumberOfInquiries = 15
                }
            };
        }
    }
}
