using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CSC340_ordering_sytem.Models
{
    public class Order
    {
        [Key]
        public string OrderNumber { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        public Address ShippingAddress { get; set; }

        [Required]
        public CreditCard CreditCard { get; set; }

        [Required]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        [Required]
        public int CartId { get; set; }
        public Cart Cart { get; set; }
    }
}