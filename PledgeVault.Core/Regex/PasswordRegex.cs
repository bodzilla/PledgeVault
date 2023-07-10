using PledgeVault.Core.Exceptions;
using System.Text.RegularExpressions;

namespace PledgeVault.Core.Regex;

public static partial class PasswordRegex
{
    /// <summary>
    /// ^ : Start of the string.
    /// (?=.*[a-z]): This is a lookahead assertion that requires at least one lowercase letter.
    /// (?=.*[A-Z]): This is a lookahead assertion that requires at least one uppercase letter.
    /// (?=.*\d): This is a lookahead assertion that requires at least one digit.
    /// (?=.*[^a-zA-Z\d]): This is a lookahead assertion that requires at least one character that is not a letter or digit (a special character).
    /// .{8,}: This part of the pattern requires that the entire string be at least eight characters long.
    /// $ : End of the string.
    /// </summary>
    [GeneratedRegex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z\d]).{8,}$", RegexOptions.CultureInvariant)]
    private static partial System.Text.RegularExpressions.Regex MinimumRequirements();

    public static void MatchIfRawPasswordMeetsMinimumRequirements(string password)
    {
        if (!MinimumRequirements().IsMatch(password)) throw new AuthenticationException($"The {nameof(password)} does not meet the minimum requirements.");
    }
}