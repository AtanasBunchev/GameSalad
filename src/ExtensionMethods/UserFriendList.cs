using GameSalad.Entities;
using GameSalad.Repositories;

namespace GameSalad.ExtensionMethods;

public static class UserFriendList
{
    // TODO decide on argument order
    // It's quite the dilema as i prefer ctx to be first.
    // But then user1.follow(ctx, user2) reads worse than the opposite...
    // user1 in context follows user2 / user1 follows user2 in contex

    public static void Follow(this User u, UsersDbContext ctx, User other)
    {
        var item = new UserFollowEntry
        {
            Follower = u,
            Target = other
        };
        ctx.Add(item);
        ctx.SaveChanges();
    }
}

