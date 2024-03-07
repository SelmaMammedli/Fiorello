using System.ComponentModel.DataAnnotations;

namespace Fiorello.ViewModels.UserVM
{
    public class LoginVM
    {
        [Required,StringLength(50)]
        public string UserNameOrEmail { get; set; }

        [Required,StringLength(50),DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }

    }
}
