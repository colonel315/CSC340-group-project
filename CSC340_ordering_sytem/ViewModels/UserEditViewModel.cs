using System.ComponentModel.DataAnnotations;

namespace CSC340_ordering_sytem.ViewModels
{
    public class UserEditViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter an email address.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Your passwords must match.")]
        public string ConfirmPassword { get; set; }

        public string Role { get; set; }
    }
}