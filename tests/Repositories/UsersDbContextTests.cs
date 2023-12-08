using GameSalad.Repositories;
using GameSalad.Entities;
using Microsoft.AspNetCore.Mvc;

namespace GameSaladTests.Repositories;

public class UserDbContextTests
{
    private TestUserDbContext context;

    public UserDbContextTests()
    {
        this.context = new TestUserDbContext();
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
        context.Save();
        Assert.NotNull(item.Id);

        User? item2 = context.GetById(item.Id);
        Assert.NotNull(item2);

        Assert.Equal(item, item2);
    }
}
