using GameSalad.Controllers;
using GameSalad.Repositories;
using System.IdentityModel.Tokens.Jwt;

namespace GameSaladTests.Controllers;

public class TestUserController : UserController
{
    public int BearerTokenModifiedCount { get; private set; } = 0;
    public JwtSecurityToken? LastBearerToken { get; private set; } = null;

    public TestUserController(UsersDbContext context)
        : base(context)
    {
    }

    protected override void SetAuthenticationBearerToken(JwtSecurityToken token)
    {
        this.BearerTokenModifiedCount++;
        this.LastBearerToken = token;
    }
}
