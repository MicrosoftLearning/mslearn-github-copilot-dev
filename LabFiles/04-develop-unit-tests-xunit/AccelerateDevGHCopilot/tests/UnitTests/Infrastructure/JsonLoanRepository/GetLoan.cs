using Library.ApplicationCore;
using Library.ApplicationCore.Entities;
using Library.Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using NSubstitute;

namespace Library.UnitTests.Infrastructure.JsonLoanRepositoryTests;

public class GetLoanTest
{
    private readonly ILoanRepository _mockLoanRepository;
    private readonly JsonLoanRepository _jsonLoanRepository;
    private readonly IConfiguration _configuration;
    private readonly JsonData _jsonData;

    public GetLoanTest()
    {
        _mockLoanRepository = Substitute.For<ILoanRepository>();
        _configuration = new ConfigurationBuilder().Build();
        _jsonData = new JsonData(_configuration);
        _jsonLoanRepository = new JsonLoanRepository(_jsonData);
    }

    [Fact]
    public async Task GetLoan_ReturnsLoanWhenLoanIdIsFound()
    {
        // Arrange
        var expectedLoan = new Loan { Id = 1 };
        _jsonData.Loans = new List<Loan> { expectedLoan };
        _jsonData.Patrons = new List<Patron>();
        _jsonData.BookItems = new List<BookItem>();
        var loanId = expectedLoan.Id;

        // Act
        var loan = await _jsonLoanRepository.GetLoan(loanId);

        // Assert
        Assert.NotNull(loan);
        Assert.Equal(expectedLoan.Id, loan.Id);
    }

    [Fact]
    public async Task GetLoan_ReturnsNullWhenLoanIdDoesNotExist()
    {
        // Arrange
        var loanId = 999;
        _jsonData.Loans = new List<Loan>();
        _jsonData.Patrons = new List<Patron>();

        // Act
        var loan = await _jsonLoanRepository.GetLoan(loanId);

        // Assert
        Assert.Null(loan);
    }
}
