using PledgeVault.Core.Contracts.Entities;
using System;
using System.Collections.Generic;

namespace PledgeVault.Core.Models;

public sealed class User : IEntity
{
    public User()
    {
        EntityCreated = DateTime.Now;
        EntityActive = true;
        Pledges = new List<Pledge>();
        Comments = new List<Comment>();
        Resources = new List<Resource>();
    }

    public int Id { get; init; }

    public DateTime EntityCreated { get; init; }

    public DateTime? EntityModified { get; set; }

    public bool EntityActive { get; set; }

    public string Email { get; set; }

    public string Username { get; set; }

    public string EncryptedPassword { get; set; }

    public ICollection<Pledge> Pledges { get; init; }

    public ICollection<Comment> Comments { get; init; }

    public ICollection<Resource> Resources { get; init; }
}