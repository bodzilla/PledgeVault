using System;

namespace PledgeVault.Core.Contracts;

internal interface IEntity
{
    int Id { get; set; }

    DateTime EntityCreated { get; set; }

    DateTime? EntityModified { get; set; }

    bool EntityActive { get; set; }
}