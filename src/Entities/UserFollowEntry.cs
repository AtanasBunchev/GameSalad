using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace GameSalad.Entities;

public class UserFollowEntry : Entity
{
    public User? Follower { get; set; }
    public User? Target { get; set; }
}
