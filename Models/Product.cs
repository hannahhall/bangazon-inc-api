using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace B_Api.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

        [Required]
        [StringLength(255)]
        public string Description { get; set; }

        [Required]
        [StringLength(55)]
        public string Title { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public double Price { get; set; }

        [Required]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        [Required]
        public int ProductTypeId { get; set; }

        public ProductType ProductType { get; set; }

        public bool LocalDelivery { get; set; }

        public string Location { get; set; }

        public ICollection<OrderProduct> OrderProducts { get; set; }
    }
}