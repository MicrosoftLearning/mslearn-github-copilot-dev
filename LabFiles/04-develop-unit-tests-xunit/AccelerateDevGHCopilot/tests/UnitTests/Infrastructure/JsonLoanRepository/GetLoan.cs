using Library.ApplicationCore;
using Library.ApplicationCore.Entities;
using Library.Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using NSubstitute;

namespace UnitTests.Infrastructure.JsonLoanRepositoryTests;

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

	[Fact(DisplayName = "JsonLoanRepository.GetLoan: Returns loan when ID is found")]
	public async Task GetLoan_ReturnsLoanWhenIdIsFound()
	{
		// Arrange
		var loanId = 1;
		var expectedLoan = new Loan
		{
			Id = loanId,
			BookItemId = 17,
			PatronId = 22,
			LoanDate = DateTime.Parse("2023-12-08T00:40:43.1808862"),
			DueDate = DateTime.Parse("2023-12-22T00:40:43.1808862")
		};
		_mockLoanRepository.GetLoan(loanId).Returns(expectedLoan);

		// Act
		var actualLoan = await _jsonLoanRepository.GetLoan(loanId);

		// Assert
		Assert.NotNull(actualLoan);
		Assert.Equal(expectedLoan.Id, actualLoan!.Id);
	}

	[Fact(DisplayName = "JsonLoanRepository.GetLoan: Returns null when loan ID is not found")]
	public async Task GetLoan_ReturnsNullWhenLoanIdIsNotFound()
	{
		// Arrange
		var loanId = 999;
		var expectedLoan = new Loan
		{
			Id = loanId,
			BookItemId = 101,
			PatronId = 202,
			LoanDate = DateTime.Now,
			DueDate = DateTime.Now.AddDays(14)
		};
		_mockLoanRepository.GetLoan(loanId).Returns(expectedLoan);

		// Act
		var actualLoan = await _jsonLoanRepository.GetLoan(loanId);

		// Assert
		Assert.Null(actualLoan);
	}
}