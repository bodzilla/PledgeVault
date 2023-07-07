using System;

namespace PledgeVault.Core.Contracts.Entities;

public interface IEntity
{
    int Id { get; init; }

    DateTime EntityCreated { get; init; }

    DateTime? EntityModified { get; set; }

    bool EntityActive { get; set; }
}