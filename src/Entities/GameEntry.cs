using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace GameSalad.Entities;

public class GameEntry : Entity
{
    [Required]
    public string? Type { get; set; }
    [Required]
    public int? UserId { get; set; }
    [Required]
    public bool? Active { get; set; } = false;
    public string? Data { get; set; }

    [ForeignKey("UserId")]
    public User? User { get; set; }
}
