using PledgeVault.Core.Contracts.Entities;
using System;

namespace PledgeVault.Core.Models;

public sealed class Comment : IEntity
{
    public Comment()
    {
        EntityCreated = DateTime.Now;
        IsEntityActive = true;
    }

    public int Id { get; init; }

    public DateTime EntityCreated { get; init; }

    public DateTime? EntityModified { get; set; }

    public bool IsEntityActive { get; set; }

    public int UserId { get; init; }

    public User User { get; init; }

    public int PledgeId { get; init; }

    public Pledge Pledge { get; init; }

    public string Text { get; set; }
}