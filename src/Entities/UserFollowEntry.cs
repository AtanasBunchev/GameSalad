using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace GameSalad.Entities;

public class UserFollowEntry : Entity
{
    public int FollowerId { get; set; }
    public int TargetId { get; set; }

    [ForeignKey("FollowerId")]
    public User Follower { get; set; } = null!;
    [ForeignKey("TargetId")]
    public User Target { get; set; } = null!;
}
