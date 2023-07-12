using System.Text.RegularExpressions;

namespace PledgeVault.Core.Regex;

public static partial class EmailRegex
{
    /// <summary>
    /// ^ : Start of the string.
    /// [a-zA-Z0-9._%+-]+ matches one or more alphanumeric characters, dots, underscores, percent signs, pluses, or hyphens.
    /// @ is the exact symbol.
    /// [a-zA-Z0-9.-]+ matches one or more alphanumeric characters, dots, or hyphens.
    /// \. is the dot symbol.
    /// [a-zA-Z]{2,} matches two or more alphabetical characters.
    /// $ : End of the string.
    /// </summary>
    [GeneratedRegex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", RegexOptions.CultureInvariant)]
    private static partial System.Text.RegularExpressions.Regex ValidEmail();

    public static bool IsValidEmail(string str) => ValidEmail().IsMatch(str);
}