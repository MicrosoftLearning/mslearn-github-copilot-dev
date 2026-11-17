namespace Library.ApplicationCore.Enums;

public static class EnumHelper
{
    private static readonly Dictionary<LoanExtensionStatus, string> LoanExtensionDescriptions = new()
    {
        [LoanExtensionStatus.Success] = "Book loan extension was successful.",
        [LoanExtensionStatus.LoanNotFound] = "Loan not found.",
        [LoanExtensionStatus.LoanExpired] = "Cannot extend book loan as it already has expired. Return the book instead.",
        [LoanExtensionStatus.MembershipExpired] = "Cannot extend book loan due to expired patron's membership.",
        [LoanExtensionStatus.LoanReturned] = "Cannot extend book loan as the book is already returned.",
        [LoanExtensionStatus.Error] = "Cannot extend book loan due to an error."
    };

    private static readonly Dictionary<LoanReturnStatus, string> LoanReturnDescriptions = new()
    {
        [LoanReturnStatus.Success] = "Book was successfully returned.",
        [LoanReturnStatus.LoanNotFound] = "Loan not found.",
        [LoanReturnStatus.AlreadyReturned] = "Cannot return book as the book is already returned.",
        [LoanReturnStatus.Error] = "Cannot return book due to an error."
    };

    private static readonly Dictionary<MembershipRenewalStatus, string> MembershipRenewalDescriptions = new()
    {
        [MembershipRenewalStatus.Success] = "Membership renewal was successful.",
        [MembershipRenewalStatus.PatronNotFound] = "Patron not found.",
        [MembershipRenewalStatus.TooEarlyToRenew] = "It is too early to renew the membership.",
        [MembershipRenewalStatus.LoanNotReturned] = "Cannot renew membership due to an outstanding loan.",
        [MembershipRenewalStatus.Error] = "Cannot renew membership due to an error."
    };

    public static string GetDescription(Enum value)
    {
        if (value == null)
            return string.Empty;

        return value switch
        {
            LoanExtensionStatus status => GetDescription(status, LoanExtensionDescriptions),
            LoanReturnStatus status => GetDescription(status, LoanReturnDescriptions),
            MembershipRenewalStatus status => GetDescription(status, MembershipRenewalDescriptions),
            _ => value.ToString()
        };
    }

    private static string GetDescription<TEnum>(TEnum value, Dictionary<TEnum, string> descriptions)
        where TEnum : struct, Enum =>
        descriptions.TryGetValue(value, out string? description) ? description : value.ToString();
}