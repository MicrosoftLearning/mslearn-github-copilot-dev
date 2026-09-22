using Microsoft.Extensions.DependencyInjection;
using Library.Infrastructure.Data;
using Library.ApplicationCore;
using Microsoft.Extensions.Configuration;

var services = new ServiceCollection();

var configuration = new ConfigurationBuilder()
.SetBasePath(Directory.GetCurrentDirectory())
.AddJsonFile("appSettings.json")
.Build();

services.AddSingleton<IConfiguration>(configuration);

// Set up the patron data access class for dependency injection.
services.AddScoped<IPatronRepository, JsonPatronRepository>();
// Set up the loan data access class for dependency injection.
services.AddScoped<ILoanRepository, JsonLoanRepository>();
// Set up the loan business logic class for dependency injection.
services.AddScoped<ILoanService, LoanService>();
// Set up the patron business logic class for dependency injection.
services.AddScoped<IPatronService, PatronService>();

services.AddSingleton<JsonData>();
services.AddSingleton<ConsoleApp>();

var servicesProvider = services.BuildServiceProvider();

var consoleApp = servicesProvider.GetRequiredService<ConsoleApp>();
consoleApp.Run().Wait();
