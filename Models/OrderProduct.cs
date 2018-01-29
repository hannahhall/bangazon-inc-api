using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace B_Api.Models
{
    public class OrderProduct
    {
        [Key]
        public int OrderProductId { get; set; }
        [Required]
        public int OrderId { get; set; }
        [JsonIgnore]
        public Order Order { get; set; }
        [Required]
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}