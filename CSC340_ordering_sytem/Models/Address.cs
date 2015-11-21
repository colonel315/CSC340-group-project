using System.ComponentModel.DataAnnotations;

namespace CSC340_ordering_sytem.Models
{
    public class Address
    {
        public int Id { get; set; }

        [Required]
        [MinLength(5, ErrorMessage = "Street must contain at least 5 characters.")]
        [MaxLength(80, ErrorMessage = "Street may not contain more than 80 characters.")]
        public string Street { get; set; }

        [Required]
        [StringLength(2, ErrorMessage = "Please enter the state's two letter abbreviation.")]
        public string State { get; set; }

        [Required]
        [StringLength(5, ErrorMessage = "Zip code is not valid.")]
        public string Zip { get; set; }

        [Required]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}