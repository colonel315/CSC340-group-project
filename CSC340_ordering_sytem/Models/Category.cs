using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CSC340_ordering_sytem.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Url { get; set; }

        public virtual ICollection<MenuItem> MenuItems { get; set; }
    }
}