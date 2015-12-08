using System;
using System.ComponentModel.DataAnnotations;

namespace CSC340_ordering_sytem.ViewModels
{
    public class CreditCardCreateViewModel
    {
        [Required]
        [StringLength(10, ErrorMessage = "Invalid customer token.")]
        public string Token { get; set; }

        [Required]
        [StringLength(4, ErrorMessage = "Invalid credit card suffix. Please enter the last 4 digits.")]
        [RegularExpression(@"[0-9]{16}", ErrorMessage = "Invalid credit card suffix. Please enter the last 4 digits.")]
        public string Number { get; set; }

        public DateTime expDate { get; set; }
        /*[Required]
        [StringLength(2, ErrorMessage = "Please enter the two digit expiration date for the card.")]
        [RegularExpression(@"[0-9]{2}", ErrorMessage = "Invalid card expiration Month")]
        public string ExpMonth { get; set; }

        [Required]
        [StringLength(2, ErrorMessage = "Please enter the expiration date for the card.")]
        [RegularExpression(@"[0-9]{2}", ErrorMessage = "Invalid card expiration Month")]
        public string ExpYear { get; set; }*/
    }
}