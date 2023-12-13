using GameSalad.Entities;
using GameSalad.Repositories;

namespace GameSalad.ExtensionMethods;

public static class UserFriendList
{
    public static void Follow(this User self, User other, UsersDbContext ctx)
    {
        if (self == other)
            return;

        var link = ctx.UserFollowEntries
            .Where(e =>
                e.FollowerId == self.Id
                && e.TargetId == other.Id)
            .FirstOrDefault();

        if (link != null)
            return;

        var item = new UserFollowEntry
        {
            Follower = self,
            Target = other
        };
        ctx.Add(item);
        ctx.SaveChanges();
    }

    public static void Unfollow(this User self, User other, UsersDbContext ctx)
    {
        var link = ctx.UserFollowEntries
            .Where(e =>
                e.FollowerId == self.Id
                && e.TargetId == other.Id)
            .FirstOrDefault();

        if (link == null)
            return;

        ctx.Remove(link);
        ctx.SaveChanges();
    }
}

