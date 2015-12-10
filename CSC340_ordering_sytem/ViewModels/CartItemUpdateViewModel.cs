using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CSC340_ordering_sytem.ViewModels
{
    public class CartItemUpdateViewModel
    {
        public int Id { get; set; }

        [Required]
        [RegularExpression("\\d[1-10]", ErrorMessage = "You can have a quantity of 1 to 10 per item.")]
        public int Qty { get; set; }
    }
}