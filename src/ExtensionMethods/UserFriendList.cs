using GameSalad.Entities;
using GameSalad.Repositories;

namespace GameSalad.ExtensionMethods;

public static class UserFriendList
{
    public static void Follow(this User u, User o, UsersDbContext ctx)
    {
        var link = ctx.UserFollowEntries
            .Where(e => e.FollowerId == u.Id && e.TargetId == o.Id)
            .FirstOrDefault();
        if(link != null)
            return;

        var item = new UserFollowEntry
        {
            Follower = u,
            Target = o
        };
        ctx.Add(item);
        ctx.SaveChanges();
    }

    public static void Unfollow(this User u, User o, UsersDbContext ctx)
    {
        var link = ctx.UserFollowEntries
            .Where(e => e.FollowerId == u.Id && e.TargetId == o.Id)
            .FirstOrDefault();

        if(link == null)
            return;

        ctx.Remove(link);
        ctx.SaveChanges();
    }
}

