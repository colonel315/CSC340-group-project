using System.Collections.Generic;

namespace CSC340_ordering_sytem.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public ICollection<CartItem> CartItems { get; set; }
    }
}