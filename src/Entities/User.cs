using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace GameSalad.Entities;

public class User : Entity
{
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;


    [InverseProperty("Target")]
    public List<UserFollowEntry> Followers { get; set; } = new();
    [InverseProperty("Follower")]
    public List<UserFollowEntry> Followed { get; set; } = new();
}
