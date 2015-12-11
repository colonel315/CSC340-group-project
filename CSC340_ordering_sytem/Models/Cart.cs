using System;
using System.Collections.Generic;
using System.Linq;

namespace CSC340_ordering_sytem.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public ICollection<CartItem> CartItems { get; set; }

        public decimal GetSubTotal()
        {
            var total = 0M;

            if (CartItems != null)
            {
                total += CartItems.Sum(item => item.Quantity * item.MenuItem.Price);
            }

            return total;
        }

        public decimal GetTotal()
        {
            return Math.Round(GetSubTotal() + GetTax(),2);
        }

        public decimal GetTax()
        {
            var total = GetSubTotal();
            return total*.06m;
        }
    }
}