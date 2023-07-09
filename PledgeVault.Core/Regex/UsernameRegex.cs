using System.Security.Authentication;
using System.Text.RegularExpressions;

namespace PledgeVault.Core.Regex;

public static partial class UsernameRegex
{
    /// <summary>
    /// ^ : Start of the string.
    /// [a-zA-Z0-9] is the character set that matches any uppercase letter (A-Z), lowercase letter (a-z), or number (0-9).
    /// + is a quantifier that means one or more of the preceding element.
    /// $ : End of the string.
    /// </summary>
    [GeneratedRegex(@"^[a-zA-Z0-9]+$", RegexOptions.CultureInvariant)]
    private static partial System.Text.RegularExpressions.Regex Requirements();

    public static void MatchIfUsernameMeetsMinimumRequirements(string username)
    {
        if (!Requirements().IsMatch(username)) throw new AuthenticationException($"The {nameof(username)} can only be letters and numbers.");
    }
}