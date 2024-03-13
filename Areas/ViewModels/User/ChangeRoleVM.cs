using Fiorello.Models;
using Microsoft.AspNetCore.Identity;

namespace Fiorello.Areas.ViewModels.User
{
    public class ChangeRoleVM
    {
        public List<IdentityRole> Roles { get; set; }
        public IList<string> UserRoles { get; set;}
        public AppUser User { get; set; }
    }
}
