using GameSalad.Entities;
using GameSalad.Repositories;

namespace GameSalad.ExtensionMethods;

public static class UserFriendList
{
    public static void Follow(this User u, User o, UsersDbContext ctx)
    {
        var item = new UserFollowEntry
        {
            Follower = u,
            Target = o
        };
        ctx.Add(item);
        ctx.SaveChanges();
    }
}

