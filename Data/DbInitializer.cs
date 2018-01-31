using System;
using System.Linq;
using B_Api.Models;

namespace B_Api.Data
{
    public class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Customer.Any())
            {
                return;
            }

            var customers = new Customer[]
            {
                new Customer{Name="Larry Larrison"},
                new Customer{Name="Moe Morrison"},                
                new Customer{Name="Gary Garrison"},
                new Customer{Name="Jerry Jerrison"}
            };
            foreach (Customer c in customers)
            {
                context.Customer.Add(c);
            }
            context.SaveChanges();

            var productTypes = new ProductType[]
            {
                new ProductType{Label="Toy"},
                new ProductType{Label="Electronic"},
                new ProductType{Label="Clothes"}
            };
            foreach (ProductType pt in productTypes)
            {
                context.ProductType.Add(pt);
            }
            context.SaveChanges();
            
            var products = new Product[]
            {
                new Product
                {
                    Quantity = 5, 
                    Title = "Puzzle", 
                    ProductType = context.ProductType.First(),
                    Customer = context.Customer.First(),
                    DateCreated = DateTime.Now,
                    Price = 3.50,
                    Description = "It's a puzzle"   
                },
                new Product
                {
                    Quantity = 5,
                    Title = "Ball",
                    ProductType = context.ProductType.First(),
                    Customer = context.Customer.First(),
                    DateCreated = DateTime.Now,
                    Price = 3.50,
                    Description = "It's a ball"
                },
                new Product
                {
                    Quantity = 5,
                    Title = "Dynamite",
                    ProductType = context.ProductType.First(),
                    Customer = context.Customer.First(),
                    DateCreated = DateTime.Now,
                    Price = 3.50,
                    Description = "It's dynamite"
                }
            };
            foreach (var p in products)
            {
                context.Product.Add(p);
            }
            context.SaveChanges();

            var paymentTypes = new PaymentType[]
            {
                new PaymentType
                {
                    AccountName = "Visa",
                    AccountNumber = "1234123412341234",
                    Customer = context.Customer.Last()
                }
            };
            foreach (var pt in paymentTypes)
            {
                context.PaymentType.Add(pt);
            }
            context.SaveChanges();

            var orders = new Order[]
            {
                new Order
                {
                    Customer = context.Customer.Last(),
                    PaymentType = context.PaymentType.Single()
                },
                new Order
                {
                    Customer = context.Customer.Last(),
                }
            };
            foreach (var o in orders)
            {
                context.Order.Add(o);
            }
            context.SaveChanges();

            var orderProducts = new OrderProduct[]
            {
                new OrderProduct
                {
                    Order = context.Order.First(),
                    Product = context.Product.First()
                },
                new OrderProduct
                {
                    Order = context.Order.Last(),
                    Product = context.Product.Last()
                }
            };
            foreach (var op in orderProducts)
            {
                context.OrderProduct.Add(op);   
            }
            context.SaveChanges();

            var departments = new Department[]
            {
                new Department{Name="HR"},
                new Department{Name="IT"},
                new Department{Name="Marketing"}
            };
            foreach (var d in departments)
            {
                context.Department.Add(d);
            }
            context.SaveChanges();

            var employees = new Employee[]
            {
                new Employee
                {
                    Name="Bob", 
                    Department=context.Department.First(),
                    IsSupervisor=true
                },
                new Employee
                {
                    Name="Rob",
                    Department=context.Department.First(),
                    IsSupervisor=false
                },
                new Employee
                {
                    Name="Bobert", 
                    Department=context.Department.Last(), 
                    IsSupervisor=true
                }
            };
            foreach (var e in employees)
            {
                context.Employee.Add(e);
            }
            context.SaveChanges();

            var computers = new Computer[]
            {
                new Computer{Make="Mac", Model="Pro", PurchaseDate=DateTime.Now},
                new Computer{Make="Dell", Model="Laptop", PurchaseDate=DateTime.Now},
            };
            foreach (var c in computers)
            {
                context.Computer.Add(c);
            }
            context.SaveChanges();

            var employeeComputers = new EmployeeComputer[]
            {
                new EmployeeComputer
                {
                    Computer = context.Computer.First(),
                    Employee = context.Employee.First()
                },
                new EmployeeComputer
                {
                    Computer = context.Computer.Last(),
                    Employee = context.Employee.Last()
                }
            };
            foreach (var ec in employeeComputers)
            {
                context.EmployeeComputer.Add(ec);
            }
            context.SaveChanges();


            var trainingPrograms = new TrainingProgram[]
            {
                new TrainingProgram
                {
                    Name="Training Program 1", 
                    StartDate=DateTime.Parse("2018-09-30"), 
                    EndDate=DateTime.Parse("2018-10-04")
                },
                new TrainingProgram
                {
                    Name="Training Program 2",
                    StartDate=DateTime.Parse("2017-09-30"),
                    EndDate=DateTime.Parse("2017-10-04")
                }
            };
            foreach (var tp in trainingPrograms)
            {
                context.TrainingProgram.Add(tp);
            }
            context.SaveChanges();

            var trainingEmployees = new TrainingEmployee[]
            {
                new TrainingEmployee
                {
                    Employee = context.Employee.First(),
                    TrainingProgram = context.TrainingProgram.First()
                },
                new TrainingEmployee
                {
                    Employee = context.Employee.Last(),
                    TrainingProgram = context.TrainingProgram.First()
                }
            };
            foreach (var te in trainingEmployees)
            {
                context.TrainingEmployee.Add(te);
            }
            context.SaveChanges();
        }
    }
}