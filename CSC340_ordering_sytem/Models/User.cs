using System.ComponentModel.DataAnnotations;

namespace CSC340_ordering_sytem.Models
{
    public class User
    {
        public int Id { get; set; }
        
        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [Required]
        [MinLength(8, ErrorMessage = "Your password must contain at least 8 characters.")]
        public string Password { get; set; }
    }
}