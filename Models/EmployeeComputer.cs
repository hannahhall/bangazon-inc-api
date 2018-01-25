using System;
using System.ComponentModel.DataAnnotations;

namespace B_Api.Models
{
    public class EmployeeComputer
    {
        [Key]
        public int EmployeeComputerId { get; set; }

        [Required]
        public int EmployeeId { get; set; }
        public Employee  Employee { get; set; }

        [Required]
        public int ComputerId { get; set; }
        public Computer Computer { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}