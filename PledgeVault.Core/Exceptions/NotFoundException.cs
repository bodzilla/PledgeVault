using System.Runtime.InteropServices;

namespace PledgeVault.Core.Exceptions;

public sealed class NotFoundException : PledgeVaultException
{
    public NotFoundException([Optional] string message) : base(message ?? "The entity was not found.")
    {
    }
}