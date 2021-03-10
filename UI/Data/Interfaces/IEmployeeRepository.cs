using System;
using Model;

namespace UI.Data.Interfaces
{
    public interface IEmployeeRepository:IGenericRepository<Employee>
    {
        void Update(Employee employee);
    }
}
