using System.Runtime.InteropServices;

namespace PledgeVault.Core.Exceptions;

public sealed class EntityValidationException : PledgeVaultException
{
    public EntityValidationException([Optional] string message) : base(message ?? "The entity failed validation.")
    {
    }
}