using GameSalad.Repositories;
using GameSalad.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GameSaladTests.Repositories;

public class UsersDbContextTests : IDisposable
{
    private TestUsersDbContext context;

    public UsersDbContextTests()
    {
        this.context = new TestUsersDbContext();
    }

    public void Dispose()
    {
        this.context.Dispose();
    }


    public static User GetValidUser()
    {
        return new User
        {
            Username = "user",
            Password = "pass"
        };
    }

    [Fact]
    public void AddUserAndGetUserByIdTest()
    {
        User item = GetValidUser();
        context.Add(item);
        context.SaveChanges();

        var item2 = Assert.IsType<User>(context.GetUserById(item.Id));
        Assert.Equal(item, item2);
    }

    [Fact]
    public void AddAndRemoveUserTest()
    {
        User item = GetValidUser();
        context.Add(item);
        context.SaveChanges();

        var item2 = Assert.IsType<User>(context.GetUserById(item.Id));
        Assert.Equal(item, item2);

        context.Remove(item);
        context.SaveChanges();

        User? item3 = context.GetUserById(item.Id);
        Assert.Null(item3);
    }

    [Fact]
    public void AddUserAndFindByUsernameTest()
    {
        User item = GetValidUser();
        context.Add(item);
        context.SaveChanges();

        var item2 = Assert.IsType<User>(context.GetUserById(item.Id));
        Assert.Equal(item, item2);
    }
}
