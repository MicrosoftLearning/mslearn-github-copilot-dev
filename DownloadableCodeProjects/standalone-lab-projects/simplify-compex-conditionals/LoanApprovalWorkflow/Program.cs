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

    public class Applicant
    {
        public int CreditScore { get; set; }
        public double AnnualIncome { get; set; }
        public int EmploymentYears { get; set; }
        public double DebtToIncomeRatio { get; set; }
        public double RequestedLoanAmount { get; set; }
        public double CollateralValue { get; set; }
        public bool HasCriminalRecord { get; set; }
        public bool IsFirstTimeBorrower { get; set; }
    }

    public class LoanEvaluator
    {
        public LoanDecision Evaluate(Applicant applicant)
        {
            var decision = new LoanDecision();

            if (applicant.CreditScore >= 750)
            {
                if (applicant.AnnualIncome >= 60000)
                {
                    if (applicant.EmploymentYears >= 2)
                    {
                        if (applicant.DebtToIncomeRatio <= 0.35)
                        {
                            if (applicant.RequestedLoanAmount <= 500000)
                            {
                                if (applicant.CollateralValue >= 0.8 * applicant.RequestedLoanAmount)
                                {
                                    if (!applicant.HasCriminalRecord)
                                    {
                                        if (!applicant.IsFirstTimeBorrower)
                                        {
                                            decision.Status = ApprovalStatus.Approved;
                                            decision.InterestRate = 3.5;
                                            decision.ApprovedAmount = applicant.RequestedLoanAmount;
                                            decision.Notes = "Approved at best rate for existing customer.";
                                        }
                                        else
                                        {
                                            decision.Status = ApprovalStatus.Approved;
                                            decision.InterestRate = 4.0;
                                            decision.ApprovedAmount = applicant.RequestedLoanAmount;
                                            decision.Notes = "Approved for first-time borrower at slightly higher rate.";
                                        }
                                    }
                                    else
                                    {
                                        decision.Status = ApprovalStatus.ConditionallyApproved;
                                        decision.InterestRate = 4.5;
                                        decision.ApprovedAmount = applicant.RequestedLoanAmount;
                                        decision.Notes = "Conditional approval pending background review.";
                                    }
                                }
                                else
                                {
                                    decision.Status = ApprovalStatus.Approved;
                                    decision.InterestRate = 4.25;
                                    decision.ApprovedAmount = 0.8 * applicant.CollateralValue;
                                    decision.Notes = "Approved with lower amount due to insufficient collateral.";
                                }
                            }
                            else
                            {
                                decision.Status = ApprovalStatus.Approved;
                                decision.InterestRate = 4.0;
                                decision.ApprovedAmount = 500000;
                                decision.Notes = "Approved with cap at $500,000.";
                            }
                        }
                        else
                        {
                            decision.Status = ApprovalStatus.Approved;
                            decision.InterestRate = 5.0;
                            decision.ApprovedAmount = applicant.RequestedLoanAmount * 0.8;
                            decision.Notes = "Approved with high debt ratio penalty.";
                        }
                    }
                    else
                    {
                        decision.Status = ApprovalStatus.Approved;
                        decision.InterestRate = 5.25;
                        decision.ApprovedAmount = applicant.RequestedLoanAmount * 0.75;
                        decision.Notes = "Approved with short employment history penalty.";
                    }
                }
                else
                {
                    decision.Status = ApprovalStatus.Approved;
                    decision.InterestRate = 5.5;
                    decision.ApprovedAmount = applicant.RequestedLoanAmount * 0.7;
                    decision.Notes = "Approved with low income cap.";
                }
            }
            else if (applicant.CreditScore >= 650)
            {
                if (applicant.AnnualIncome >= 50000)
                {
                    if (applicant.DebtToIncomeRatio <= 0.4)
                    {
                        decision.Status = ApprovalStatus.Approved;
                        decision.InterestRate = 6.5;
                        decision.ApprovedAmount = Math.Min(applicant.RequestedLoanAmount, 250000);
                        decision.Notes = "Approved at higher interest rate.";
                    }
                    else
                    {
                        decision.Status = ApprovalStatus.ConditionallyApproved;
                        decision.InterestRate = 7.0;
                        decision.ApprovedAmount = applicant.RequestedLoanAmount * 0.6;
                        decision.Notes = "Conditional approval with debt reduction plan.";
                    }
                }
                else
                {
                    decision.Status = ApprovalStatus.Declined;
                    decision.Notes = "Declined due to low income.";
                }
            }
            else
            {
                decision.Status = ApprovalStatus.Declined;
                decision.Notes = "Declined due to low credit score.";
            }

            return decision;
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
                Console.WriteLine($"  Annual Income: ${applicant.AnnualIncome:N0}");
                Console.WriteLine($"  Employment Years: {applicant.EmploymentYears}");
                Console.WriteLine($"  Debt-to-Income Ratio: {applicant.DebtToIncomeRatio:P1}");
                Console.WriteLine($"  Requested Amount: ${applicant.RequestedLoanAmount:N0}");
                Console.WriteLine($"  Collateral Value: ${applicant.CollateralValue:N0}");
                Console.WriteLine($"  Criminal Record: {applicant.HasCriminalRecord}");
                Console.WriteLine($"  First Time Borrower: {applicant.IsFirstTimeBorrower}");
                Console.WriteLine();
                Console.WriteLine($"  RESULT:");
                Console.WriteLine($"  Status: {decision.Status}");
                Console.WriteLine($"  Interest Rate: {decision.InterestRate}%");
                Console.WriteLine($"  Approved Amount: ${decision.ApprovedAmount:N0}");
                Console.WriteLine($"  Notes: {decision.Notes}");
                Console.WriteLine(new string('-', 60));
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Creates a collection of test applicants demonstrating different loan approval outcomes
        /// </summary>
        /// <returns>List of applicants with varying profiles</returns>
        private static List<Applicant> CreateTestApplicants()
        {
            return new List<Applicant>
            {
                // Outcome 1: Best case approval - Excellent credit, existing customer, good collateral
                // Expected: Approved at 3.5% interest rate with full requested amount
                new Applicant
                {
                    CreditScore = 780,
                    AnnualIncome = 85000,
                    EmploymentYears = 5,
                    DebtToIncomeRatio = 0.25,
                    RequestedLoanAmount = 300000,
                    CollateralValue = 280000, // 93% of requested amount (>80% required)
                    HasCriminalRecord = false,
                    IsFirstTimeBorrower = false // Existing customer gets best rate
                },

                // Outcome 2: First-time borrower premium - Good profile but first-time borrower
                // Expected: Approved at 4.0% due to first-time borrower status
                new Applicant
                {
                    CreditScore = 780,
                    AnnualIncome = 85000,
                    EmploymentYears = 5,
                    DebtToIncomeRatio = 0.25,
                    RequestedLoanAmount = 300000,
                    CollateralValue = 280000, // 93% of requested amount (>80% required)
                    HasCriminalRecord = false,
                    IsFirstTimeBorrower = true // First-time borrower gets slightly higher rate
                },

                // Outcome 3: Conditional approval - Good profile but has criminal record
                // Expected: Conditionally approved at 4.5% pending background review
                new Applicant
                {
                    CreditScore = 760,
                    AnnualIncome = 75000,
                    EmploymentYears = 4,
                    DebtToIncomeRatio = 0.30,
                    RequestedLoanAmount = 250000,
                    CollateralValue = 220000, // 88% of requested amount
                    HasCriminalRecord = true, // This triggers conditional approval
                    IsFirstTimeBorrower = false
                },

                // Outcome 4: Approved with penalties - High debt-to-income ratio
                // Expected: Approved at 5.0% with 80% of requested amount due to high debt ratio
                new Applicant
                {
                    CreditScore = 770,
                    AnnualIncome = 70000,
                    EmploymentYears = 3,
                    DebtToIncomeRatio = 0.40, // Above 0.35 threshold
                    RequestedLoanAmount = 200000,
                    CollateralValue = 180000,
                    HasCriminalRecord = false,
                    IsFirstTimeBorrower = false
                },

                // Outcome 5: Mid-tier approval - Good credit (650-749 range)
                // Expected: Approved at 6.5% with cap at $250,000
                new Applicant
                {
                    CreditScore = 680,
                    AnnualIncome = 55000,
                    EmploymentYears = 3,
                    DebtToIncomeRatio = 0.35,
                    RequestedLoanAmount = 180000,
                    CollateralValue = 160000,
                    HasCriminalRecord = false,
                    IsFirstTimeBorrower = false
                },

                // Outcome 6: Declined - Low credit score
                // Expected: Declined due to credit score below 650
                new Applicant
                {
                    CreditScore = 620, // Below 650 threshold
                    AnnualIncome = 60000,
                    EmploymentYears = 2,
                    DebtToIncomeRatio = 0.30,
                    RequestedLoanAmount = 150000,
                    CollateralValue = 140000,
                    HasCriminalRecord = false,
                    IsFirstTimeBorrower = true
                }
            };
        }
    }
}
