using System.Runtime.InteropServices;

namespace PledgeVault.Core.Exceptions;

public sealed class InvalidRequestException : PledgeVaultException
{
    public InvalidRequestException([Optional] string message) : base(message)
    {
    }
}