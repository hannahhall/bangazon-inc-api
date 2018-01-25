using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace B_Api.Models
{
    public class ProductType
    {
        [Key]
        public int ProductTypeId { get; set; }

        [Required]
        [StringLength(255)]
        public string Label { get; set; }
       
        public ICollection<Product> Products;
    }
}