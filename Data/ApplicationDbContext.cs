using Microsoft.EntityFrameworkCore;
using B_Api.Models;

namespace B_Api.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        public DbSet<EmployeeComputer> EmployeeComputer { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Computer> Computer { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderProduct> OrderProduct { get; set; }
        public DbSet<PaymentType> PaymentType { get; set; }
        public DbSet<ProductType> ProductType { get; set; }
        public DbSet<TrainingEmployee> TrainingEmployee { get; set; }
        public DbSet<TrainingProgram> TrainingProgram { get; set; }
        public DbSet<Product> Product { get; set; }
    }
}