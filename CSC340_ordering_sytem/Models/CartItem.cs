using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSC340_ordering_sytem.Models
{
    public class CartItem
    {
        public int Id { get; set; }

        [Required]
        public int MenuItemId { get; set; }

        [ForeignKey("MenuItemId")]
        public MenuItem MenuItem { get; set; }

        [Required]
        [Range(0, 9999, ErrorMessage = "You must order at least 1 of this item.")]
        public int Quantity { get; set; }

        public int CartId { get; set; }
        public Cart Cart { get; set; }
    }
}