using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;
using CSC340_ordering_sytem.Models;

namespace CSC340_ordering_sytem.ViewModels
{
    public class MenuItemSaveViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        public ICollection<ItemIngredient> ItemIngredients { get; set; }

        [Required]
        [RegularExpression(@"^\$?(\d{1,3},?(\d{3},?)*\d{3}(.\d{0,3})?|\d{1,3}(.\d{2})?)$", ErrorMessage = "Invalid Price.")]
        public decimal Price { get; set; }

        public string CurrentImageUrl { get; set; }
        public HttpPostedFileBase Image { get; set; }
    }
}