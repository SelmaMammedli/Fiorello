using Fiorello.Models;

namespace Fiorello.Areas.ViewModels.User
{
    public class UserDetailVM
    {
        public AppUser User { get; set; }
        public IList<string> UserRoles { get; set; }
    }
}
