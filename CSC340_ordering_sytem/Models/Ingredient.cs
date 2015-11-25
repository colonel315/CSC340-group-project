using System.ComponentModel.DataAnnotations;

namespace CSC340_ordering_sytem.Models
{
    public class Ingredient
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}