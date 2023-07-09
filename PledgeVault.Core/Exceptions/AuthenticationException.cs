using System.Runtime.InteropServices;

namespace PledgeVault.Core.Exceptions;

public sealed class AuthenticationException : PledgeVaultException
{
    public AuthenticationException([Optional] string message) : base(message ?? "There was an error during authentication.")
    {
    }
}