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
        context.SaveChanges();

        User? item2 = context.GetById(item.Id);
        Assert.Equal(item, item2);
    }

    [Fact]
    public void AddAndRemoveUserTest()
    {
        User item = new User
        {
            Username = "user",
            Password = "pass"
        };
        context.Add(item);
        context.SaveChanges();

        User? item2 = context.GetById(item.Id);
        Assert.Equal(item, item2);

        context.Remove(item);
        context.SaveChanges();

        User? item3 = context.GetById(item.Id);
        Assert.Null(item3);
    }

    [Fact]
    public void AddUserAndFindByUsernameTest()
    {
        User item = new User
        {
            Username = "user",
            Password = "pass"
        };
        context.Add(item);
        context.SaveChanges();

        User? item2 = context.FindByUsername(item.Username);
        Assert.Equal(item, item2);
    }
}
