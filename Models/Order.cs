using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace B_Api.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public int? PaymentTypeId { get; set; }
        public PaymentType PaymentType { get; set; }
        [Required]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}