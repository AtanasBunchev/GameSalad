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

        User? item2 = context.GetById(item.Id);
        Assert.Equal(item, item2);
    }
}
