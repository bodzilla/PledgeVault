using System;
using System.Runtime.InteropServices;

namespace PledgeVault.Core.Exceptions;

public class PledgeVaultException : Exception
{
    public PledgeVaultException([Optional] string message) : base(message ?? "An error occurred while processing the request")
    {
    }
}