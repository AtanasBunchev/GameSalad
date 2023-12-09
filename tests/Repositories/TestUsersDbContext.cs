using GameSalad.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;

namespace GameSaladTests.Repositories;

public class TestUsersDbContext : UsersDbContext
{
    private SqliteConnection? dbConn;

    public TestUsersDbContext()
    {
        this.Database.Migrate();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Pass an open connection to preserve the in-memory database for the object's lifetime
        // See https://github.com/dotnet/efcore/issues/9842
        this.dbConn = new SqliteConnection(@"Data Source=:memory:");
        this.dbConn.Open();
        optionsBuilder.UseSqlite(this.dbConn);
    }

    public override void Dispose()
    {
        base.Dispose();
        if(this.dbConn != null)
            this.dbConn.Dispose();
    }
}
