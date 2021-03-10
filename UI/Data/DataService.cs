using System;
using System.Collections.Generic;
using System.Text;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using UI.Data.Interfaces;

namespace UI.Data
{
    public class DataService
    {
        private readonly Func<TestDBContext> _contextCreator;

        public IDepartmentRepository DepartmentRepository => new DepartmentRepository(_contextCreator);
        public IEmployeeRepository EmployeeRepository => new EmployeeRepository(_contextCreator);

        public IOrderRepository OrderRepository => new OrderRepository(_contextCreator);

        public DataService(Func<TestDBContext> contextCreator)
        {
            _contextCreator = contextCreator;
        }
    }
}
