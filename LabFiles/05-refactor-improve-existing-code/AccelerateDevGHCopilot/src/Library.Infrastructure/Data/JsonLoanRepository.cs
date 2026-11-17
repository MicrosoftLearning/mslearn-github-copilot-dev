using Library.ApplicationCore;
using Library.ApplicationCore.Entities;

namespace Library.Infrastructure.Data;

public class JsonLoanRepository : ILoanRepository
{
    private readonly JsonData _jsonData;

    public JsonLoanRepository(JsonData jsonData)
    {
        _jsonData = jsonData;
    }

    public async Task<Loan?> GetLoan(int id)
    {
        await _jsonData.EnsureDataLoaded();

        return _jsonData.Loans!
            .Where(loan => loan.Id == id)
            .Select(_jsonData.GetPopulatedLoan)
            .FirstOrDefault();
    }

    public async Task UpdateLoan(Loan loan)
    {
        List<Loan> loans = _jsonData.Loans!;
        Loan? existingLoan = loans.Where(l => l.Id == loan.Id).FirstOrDefault();

        if (existingLoan != null)
        {
            loans[loans.IndexOf(existingLoan)] = loan;

            await _jsonData.SaveLoans(loans);

            await _jsonData.LoadData();
        }
    }
}