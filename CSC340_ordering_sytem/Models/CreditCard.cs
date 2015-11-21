using System.ComponentModel.DataAnnotations;

namespace CSC340_ordering_sytem.Models
{
    public class CreditCard
    {
        public int Id { get; set; }

        [Required]
        [StringLength(10, ErrorMessage = "Invalid customer token.")]
        public string Token { get; set; }

        [Required]
        [StringLength(4, ErrorMessage = "Invalid credit card suffix. Please enter the last 4 digits.")]
        [RegularExpression(@"[0-9]{4}", ErrorMessage = "Invalid credit card suffix. Please enter the last 4 digits.")]
        public string CardSuffix { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}