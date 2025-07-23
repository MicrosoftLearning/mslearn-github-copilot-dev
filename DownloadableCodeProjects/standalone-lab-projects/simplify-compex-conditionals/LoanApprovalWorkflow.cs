using System;

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
        public string Notes { get; set; }
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
        public bool IsCitizen { get; set; }
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
                                        if (applicant.IsCitizen)
                                        {
                                            decision.Status = ApprovalStatus.Approved;
                                            decision.InterestRate = 3.5;
                                            decision.ApprovedAmount = applicant.RequestedLoanAmount;
                                            decision.Notes = "Approved at best rate.";
                                        }
                                        else
                                        {
                                            decision.Status = ApprovalStatus.Approved;
                                            decision.InterestRate = 4.0;
                                            decision.ApprovedAmount = applicant.RequestedLoanAmount;
                                            decision.Notes = "Approved for non-citizen at slightly higher rate.";
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
            var applicant = new Applicant
            {
                CreditScore = 760,
                AnnualIncome = 70000,
                EmploymentYears = 3,
                DebtToIncomeRatio = 0.30,
                RequestedLoanAmount = 400000,
                CollateralValue = 350000,
                HasCriminalRecord = false,
                IsCitizen = true
            };

            var evaluator = new LoanEvaluator();
            var decision = evaluator.Evaluate(applicant);

            Console.WriteLine($"Status: {decision.Status}");
            Console.WriteLine($"Interest Rate: {decision.InterestRate}%");
            Console.WriteLine($"Approved Amount: ${decision.ApprovedAmount}");
            Console.WriteLine($"Notes: {decision.Notes}");
        }
    }
}
