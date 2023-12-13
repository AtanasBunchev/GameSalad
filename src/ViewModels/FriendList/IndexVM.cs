using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;


namespace GameSalad.ViewModels.FriendList;

public class IndexVM
{
    public List<Entities.User> Following { get; set; } = new ();
    public List<Entities.User> Followers { get; set; } = new ();

    [DisplayName("Username: ")]
    public string? Username { get; set; }

    public string? FollowedUser { get; set; }
    public string? UnfollowedUser { get; set; }
}
