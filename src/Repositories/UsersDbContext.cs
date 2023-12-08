using Microsoft.EntityFrameworkCore;
using GameSalad.Entities;

namespace GameSalad.Repositories;

public class UsersDbContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public UsersDbContext()
    {
        this.Users = this.Set<User>();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseSqlServer(@"Server=db;Database=GameSalad;User Id=sa;Password=df682008-f174-48b2-9a76-b99e7fc799ee");
    }
}
