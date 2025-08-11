// SecurityValidator.cs - Handles security validation for orders and returns
using System;
using System.Text.RegularExpressions;

namespace EcommerceApp.Security;

public class SecurityValidator
{
    private static readonly Regex IdPattern = new Regex(@"^[A-Z]{3}\d{5}$", RegexOptions.Compiled);
    private static readonly string[] SuspiciousPatterns = { "<script", "javascript:", "onload=", "onerror=", "drop table", "union select" };

    public static bool IsValidId(string id, string idType)
    {
        Console.WriteLine($"Security check: Validating {idType} ID: {MaskSensitiveData(id)}");

        // Basic null/empty check
        if (string.IsNullOrWhiteSpace(id))
        {
            Console.WriteLine($"Security check failed: {idType} ID is null or empty");
            return false;
        }

        // Length validation (prevent buffer overflow attempts)
        if (id.Length > 20)
        {
            Console.WriteLine($"Security check failed: {idType} ID exceeds maximum length");
            return false;
        }

        // Pattern validation (e.g., ORD12345 or RET98765)
        if (!IdPattern.IsMatch(id))
        {
            Console.WriteLine($"Security check failed: {idType} ID format is invalid");
            return false;
        }

        // Check for suspicious patterns (basic XSS/SQL injection prevention)
        foreach (string pattern in SuspiciousPatterns)
        {
            if (id.ToLowerInvariant().Contains(pattern))
            {
                Console.WriteLine($"Security check failed: {idType} ID contains suspicious pattern");
                return false;
            }
        }

        Console.WriteLine($"Security check passed: {idType} ID is valid");
        return true;
    }

    private static string MaskSensitiveData(string data)
    {
        if (string.IsNullOrEmpty(data) || data.Length <= 4)
            return "****";

        return data.Substring(0, 3) + "*****";
    }
}
