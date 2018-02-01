using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace B_Api.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        
        [Required]
        public string Name { get; set; }

        [Required]
        public bool IsSupervisor { get; set; }
        [Required]
        
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public virtual ICollection<EmployeeComputer> EmployeeComputers { get; set; }
        public virtual ICollection<TrainingEmployee> TrainingEmployees { get; set; }
    }
}