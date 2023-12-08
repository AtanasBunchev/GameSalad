using GameSalad.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GameSaladTests.Repositories;

public class TestUserDbContext : UsersDbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(@"Data Source=:memory:");
    }
}
