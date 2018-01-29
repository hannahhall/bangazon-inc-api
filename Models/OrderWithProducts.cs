using System.Collections.Generic;

namespace B_Api.Models
{
    public class OrderWithProducts: Order
    {
        public OrderWithProducts(Order order)
        {
            this.OrderId = order.OrderId;
            this.CreatedAt = order.CreatedAt;
            this.CustomerId = order.CustomerId;
            this.PaymentTypeId = order.PaymentTypeId;
        }

        public List<Product> Products { get; set; }  = new List<Product>();

        
    }
}