using System.Runtime.InteropServices;

namespace PledgeVault.Core.Exceptions;

public sealed class InvalidEntityException : PledgeVaultException
{
    public InvalidEntityException([Optional] string message) : base(message ?? "The entity failed validation.")
    {
    }
}