using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace Fiorello.ViewModels.UserVM
{
    public class RegisterVM
    {
        [System.ComponentModel.DataAnnotations.Required, MaxLength(100)]
        public string FullName { get; set; }
        [System.ComponentModel.DataAnnotations.Required, MaxLength(100)]
        public string UserName { get; set; }
        [System.ComponentModel.DataAnnotations.Required, EmailAddress,DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [System.ComponentModel.DataAnnotations.Required, DataType(DataType.Password)]
        public string Password { get; set; }
        [System.ComponentModel.DataAnnotations.Required, DataType(DataType.Password),Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
