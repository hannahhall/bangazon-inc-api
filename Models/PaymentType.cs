using System.ComponentModel.DataAnnotations;

namespace B_Api.Models
{
    public class PaymentType
    {
        [Key]
        public int PaymentTypeId { get; set; }

        [Required]
        [StringLength(12)]
        public string AccountName { get; set; }

        [Required]
        [StringLength(20)]
        public string AccountNumber { get; set; }

        [Required]
        public int CustomerId { get; set; }        
        public Customer Customer { get; set; }
    }
}