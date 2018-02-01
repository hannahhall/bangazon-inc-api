using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace B_Api.Models
{
    public class Computer
    {
        [Key]
        public int ComputerId { get; set; }
        [Required]
        public string Make { get; set; }
        [Required]
        public string Model { get; set; }
        
        [Required]
        public DateTime PurchaseDate { get; set; }
        public DateTime DecomissionDate { get; set; }

        public virtual ICollection<EmployeeComputer> EmployeeComputers { get; set; }

    }
}