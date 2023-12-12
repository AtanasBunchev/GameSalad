using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace GameSalad.ViewModels.FriendList;

public class IndexVM
{
    public List<Entities.User> Following { get; set; } = new List<Entities.User>();
    public List<Entities.User> Followers { get; set; } = new List<Entities.User>();
}
