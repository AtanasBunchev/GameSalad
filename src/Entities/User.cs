using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace GameSalad.Entities;

public class User : Entity
{
    public string? Username { get; set; }
    public string? Password { get; set; }
}
