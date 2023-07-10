using PledgeVault.Core.Exceptions;
using PledgeVault.Core.Regex;

namespace PledgeVault.Core.Security;

public static class AuthenticationManager
{
    public static string HashPassword(string password) => BCrypt.Net.BCrypt.EnhancedHashPassword(password);

    public static bool IsPasswordMatch(string password, string hashedPassword)
    {
        if (password is null) throw new AuthenticationException($"The {nameof(password)} cannot be null.");
        return BCrypt.Net.BCrypt.EnhancedVerify(password, hashedPassword);
    }

    /// <summary>
    /// Validates the username to ensure it meets the minimum requirements using <see cref="UsernameRegex"/>.
    /// </summary>
    /// <param name="username">The raw password.</param>
    public static void ValidateUsernameMeetsMinimumRequirements(string username) => UsernameRegex.MatchIfUsernameMeetsMinimumRequirements(username);

    /// <summary>
    /// Validates the password to ensure it meets the minimum requirements using <see cref="PasswordRegex"/>.
    /// </summary>
    /// <param name="password">The raw password.</param>
    public static void ValidateRawPasswordMeetsMinimumRequirements(string password) => PasswordRegex.MatchIfRawPasswordMeetsMinimumRequirements(password);
}