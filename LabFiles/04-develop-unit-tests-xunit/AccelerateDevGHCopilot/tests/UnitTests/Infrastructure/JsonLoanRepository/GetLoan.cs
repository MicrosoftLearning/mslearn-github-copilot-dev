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
        var jsonDirectory = Path.GetFullPath(
            Path.Combine(AppContext.BaseDirectory, "../../../../../src/Library.Console/Json"));
        _configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["JsonPaths:Authors"] = Path.Combine(jsonDirectory, "Authors.json"),
                ["JsonPaths:Books"] = Path.Combine(jsonDirectory, "Books.json"),
                ["JsonPaths:BookItems"] = Path.Combine(jsonDirectory, "BookItems.json"),
                ["JsonPaths:Patrons"] = Path.Combine(jsonDirectory, "Patrons.json"),
                ["JsonPaths:Loans"] = Path.Combine(jsonDirectory, "Loans.json")
            })
            .Build();
        _jsonData = new JsonData(_configuration);
        _jsonLoanRepository = new JsonLoanRepository(_jsonData);
    }

    [Fact]
    public async Task GetLoan_ReturnsLoanWhenLoanIdIsFound()
    {
        // Arrange
        var loanId = 1;
        var expectedLoan = new Loan { Id = loanId };
        _mockLoanRepository.GetLoan(loanId).Returns(expectedLoan);

        // Act
        var actualLoan = await _jsonLoanRepository.GetLoan(loanId);

        // Assert
        Assert.NotNull(actualLoan);
        Assert.Equal(expectedLoan.Id, actualLoan.Id);
    }


    [Fact]
    public async Task GetLoan_ReturnsNullWhenIdIsNotFound()
    {
        // Arrange
        var loanId = 999; // Assuming this ID does not exist in the JSON data

        // Act
        var actualLoan = await _jsonLoanRepository.GetLoan(loanId);

        // Assert
        Assert.Null(actualLoan);
    }

}
