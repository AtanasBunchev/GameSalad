using GameSalad.Repositories;
using GameSalad.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GameSaladTests.Repositories;

public class UserDbContextTests : IDisposable
{
    private TestUsersDbContext context;

    public UserDbContextTests()
    {
        this.context = new TestUsersDbContext();
        this.context.Database.Migrate();
    }

    public void Dispose()
    {
        this.context.Dispose();
    }

    [Fact]
    public void AddUserAndGetByIdTest()
    {
        User item = new User
        {
            Username = "user",
            Password = "pass"
        };
        context.Add(item);

        User? item2 = context.GetById(item.Id);
        Assert.Equal(item, item2);
    }
}
