# My Microsoft Learn Lab Work — GitHub Copilot xUnit Testing

> **Learning exercise:** [Develop unit tests using GitHub Copilot](https://learn.microsoft.com/)  
> **Repository:** This repository is my fork of [MicrosoftLearning/mslearn-github-copilot-dev](https://github.com/MicrosoftLearning/mslearn-github-copilot-dev). The starter application and lab materials are provided by Microsoft Learning; the additions below document my own work completing the lab.

## Overview

I completed the **Develop unit tests using GitHub Copilot** hands-on exercise using C#, .NET 9, xUnit, and GitHub Copilot. I extended the existing library application's test project to exercise the JSON loan repository, and verified that the full test suite passed.

## My changes

- Created `tests/UnitTests/Infrastructure/JsonLoanRepository/GetLoan.cs`, with a `GetLoanTest` class.
- Added a test for `JsonLoanRepository.GetLoan()` when an existing loan ID is found.
- Added a test confirming that `GetLoan()` returns `null` when a loan ID does not exist.
- Followed the **Arrange–Act–Assert** pattern.
- Added a project reference from `UnitTests` to `Library.Infrastructure`.
- Configured the test project to copy JSON files from `src/Library.Console/Json` into the test output directory.
- Used **GitHub Copilot Ask** to explore the existing tests and **Agent** to help implement the changes.

## Test results

After merging my changes into my fork's `main` branch, I ran:

```bash
cd LabFiles/04-develop-unit-tests-xunit/AccelerateDevGHCopilot
dotnet test tests/UnitTests/UnitTests.csproj --verbosity normal
```

Result:

```text
Test summary: total: 19, failed: 0, succeeded: 19, skipped: 0
Build succeeded with 5 warning(s)
```

The five compiler warnings originated in existing application source files; they did not prevent the tests from passing.

## Development workflow

1. Inspected the existing service tests and repository implementations.
2. Planned the infrastructure test approach with GitHub Copilot.
3. Added the `GetLoanTest` class and updated test project configuration.
4. Implemented tests for both found and missing loan IDs.
5. Built the project and ran the complete suite.
6. Committed the changes on `feature/copilot-xunit-infrastructure-tests`.
7. Pushed the branch to my fork, opened a pull request, and merged it into `main`.
8. Re-ran all 19 tests on the updated local `main` branch.

## Technologies and skills

**C# · .NET 9 · xUnit · NSubstitute · GitHub Copilot · JSON · Automated Testing · Arrange–Act–Assert · Git · GitHub · Pull Requests**

## Links

- [My merged pull request #1 — Add xUnit infrastructure tests for JSON loan repository](https://github.com/tabner0320/mslearn-github-copilot-dev/pull/1)
- [New GetLoan.cs tests](LabFiles/04-develop-unit-tests-xunit/AccelerateDevGHCopilot/tests/UnitTests/Infrastructure/JsonLoanRepository/GetLoan.cs)
- [UnitTests.csproj configuration](LabFiles/04-develop-unit-tests-xunit/AccelerateDevGHCopilot/tests/UnitTests/UnitTests.csproj)
- [Original Microsoft Learning repository](https://github.com/MicrosoftLearning/mslearn-github-copilot-dev)

---

*This document describes my work on a Microsoft Learn lab; it is not a claim of authorship of the original Microsoft Learning application or curriculum.*
