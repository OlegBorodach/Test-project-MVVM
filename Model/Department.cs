using System.Collections.Generic;

#nullable disable

namespace Model
{
    public partial class Department
    {
        public Department()
        {
            Employees = new HashSet<Employee>();
        }

        public int DepartmentId { get; set; }
        public int? EmployeeId { get; set; }
        public string Name { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}
