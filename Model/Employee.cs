using System;
using System.Collections.Generic;

#nullable disable

namespace Model
{
    public partial class Employee
    {
        public Employee()
        {
            Departments = new HashSet<Department>();
            Orders = new HashSet<Order>();
        }

        public int EmployeeId { get; set; }
        public int DepartmentId { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Sex Sex { get; set; }

        public virtual Department Department { get; set; }
        public virtual ICollection<Department> Departments { get; set; }
        public virtual ICollection<Order> Orders { get; set; }

        public override string ToString()
        {
            return $"{Surname} {Name} {Patronymic}";
        }
    }
}
