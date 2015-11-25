using System.Collections.Generic;

namespace CSC340_ordering_sytem.Models
{
    public class Customer : User
    {
        public ICollection<Address> Addresses { get; set; }
        public ICollection<CreditCard> CreditCards { get; set; }
        public ICollection<Order> Orders { get; set; }

        public int CartId { get; set; }
        public Cart Cart { get; set; }
    }
}