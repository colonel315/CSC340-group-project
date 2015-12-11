using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CSC340_ordering_sytem.Models
{
    public class CreditCard
    {
        public int Id { get; set; }

        [Required]
        [StringLength(22, ErrorMessage = "Invalid customer token.")]
        public string Token { get; set; }

        [Required]
        [DisplayName("Suffix")]
        [StringLength(4, ErrorMessage = "Invalid credit card suffix. Please enter the last 4 digits.")]
        [RegularExpression(@"[0-9]{4}", ErrorMessage = "Invalid credit card suffix. Please enter the last 4 digits.")]
        public string CardSuffix { get; set; }

        [Required]
        [RegularExpression(@"[0-9]{2}", ErrorMessage = "Invalid card expiration Month")]
        public string ExpMonth { get; set; }

        [Required]
        [RegularExpression(@"[0-9]{4}", ErrorMessage = "Invalid card expiration Year")]
        public string ExpYear { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}