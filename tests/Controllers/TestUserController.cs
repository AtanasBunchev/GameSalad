using GameSalad.Controllers;
using GameSalad.Repositories;

namespace GameSaladTests.Controllers;

public class TestUserController : UserController
{
    public int BearerTokenModifiedCount { get; private set; } = 0;

    public TestUserController(UsersDbContext context)
        : base(context)
    {
    }

    protected override void SetAuthenticationBearerToken(string token)
    {
        this.BearerTokenModifiedCount++;
    }
}
