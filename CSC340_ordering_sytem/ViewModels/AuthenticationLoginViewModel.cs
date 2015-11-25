using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CSC340_ordering_sytem.ViewModels
{
    public class AuthenticationLoginViewModel
    {
        [Required(ErrorMessage = "Please enter an email address.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("Remember Me?")]
        public bool IsPersistant { get; set; }
    }
}