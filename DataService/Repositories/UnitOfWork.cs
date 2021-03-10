using DataAccess;
using DataService.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataService.Repositories
{
    public class UnitOfWork<T> : IUnitOfWork where T : DbContext, new()
    {
        public IDepartmentRepository DepartmentRepository => new DepartmentRepository(new T());
        public IEmployeeRepository EmployeeRepository => new EmployeeRepository(new T());
        public IOrderRepository OrderRepository => new OrderRepository(new T());
    }
}
