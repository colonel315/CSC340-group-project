using System.ComponentModel.DataAnnotations;
using System.Linq;
using CSC340_ordering_sytem.DAL;

namespace CSC340_ordering_sytem.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }
        
        [Required]
        public string LastName { get; set; }

        [Required]
        public string Role { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [Required]
        [MinLength(8, ErrorMessage = "Your password must contain at least 8 characters.")]
        public string Password { get; set; }

        public static User FindUserByEmailAndPassword(string email, string password, OrderingSystemDbContext db)
        {
            return db.Users.FirstOrDefault(x => x.Email.Equals(email) && x.Password.Equals(password));
        }

        public string GetFullName()
        {
            return $"{FirstName} {LastName}";
        }
    }
}