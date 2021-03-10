using System;
using System.Collections.Generic;
using System.Text;

namespace DataService.Interfaces
{
    public interface IUnitOfWork 
    {
        IDepartmentRepository DepartmentRepository { get; }
        IEmployeeRepository EmployeeRepository { get; }
        IOrderRepository OrderRepository { get; }
    }
}
