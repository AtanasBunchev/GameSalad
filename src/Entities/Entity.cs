using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace GameSalad.Entities;

public class Entity
{
    [Key]
    public int Id { get; set; }
}
