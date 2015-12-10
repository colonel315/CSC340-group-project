using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using CSC340_ordering_sytem.DAL;

namespace CSC340_ordering_sytem.Models.Helpers
{
    public class CustomerHelper
    {
        static OrderingSystemDbContext _db = new OrderingSystemDbContext();
        public static List<CartItem> GetCartItems(int customerId)
        {
            var customer = (Customer)UsersHelper.GetUserById(customerId);
            if (customer?.CartId == null)
            {
                return new List<CartItem>();
            }
	        return _db.CartItems.Where(x => x.CartId == customer.Id)
                        .Include("MenuItem").ToList();
        }

        public static decimal GetCartTotal(List<CartItem> cartItem)
        {
            if (cartItem.Count == 0)
            {
                return (decimal) 0.00;
            }

            decimal total = 0;

            foreach (var item in cartItem)
            {
                total += (item.MenuItem.Price*item.Quantity);
            }

            return total;
        }
    }
}