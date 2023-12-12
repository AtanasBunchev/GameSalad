using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace GameSalad.Entities;

public class User : Entity
{
    public string? Username { get; set; }
    public string? Password { get; set; }


    [InverseProperty("Target")]
    public List<UserFollowEntry> Followers { get; set; } = new();
    [InverseProperty("Follower")]
    public List<UserFollowEntry> Followed { get; set; } = new();
}
