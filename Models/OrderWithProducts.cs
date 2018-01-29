using System.Collections.Generic;

namespace B_Api.Models
{
    public class OrderWithProducts
    {
        
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public int? PaymentTypeId { get; set; }
        public List<Product> Products { get; set; }  = new List<Product>();

        
    }
}