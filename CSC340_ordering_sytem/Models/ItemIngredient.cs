using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSC340_ordering_sytem.Models
{
    public class ItemIngredient
    {
        [Key, Column(Order = 0)]
        public int MenuItemId { get; set; }
        public MenuItem MenuItem { get; set; }

        [Key, Column(Order = 1)]
        public int IngredientId { get; set; }
        public Ingredient Ingredient { get; set; }

        [Required]
        [Range(0, 9999, ErrorMessage = "You must order at least 1 of this item.")]
        public int Quantity { get; set; }
    }
}