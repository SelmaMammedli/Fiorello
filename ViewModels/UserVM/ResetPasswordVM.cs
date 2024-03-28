using System.ComponentModel.DataAnnotations;

namespace Fiorello.ViewModels.UserVM
{
    public class ResetPasswordVM
    {
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
