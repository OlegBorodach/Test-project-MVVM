using System;
using DataAccess;
using Model;
using UI.Data.Interfaces;

namespace UI.Data
{
    public class EmployeeRepository:GenericRepository<Employee, TestDBContext>, IEmployeeRepository
    {
        public EmployeeRepository(Func<TestDBContext> context) : base(context)
        {
        }

        public void Update(Employee employee)
        {
            using var context = ContextCreator.Invoke();
            var dbEntity = context.Employees.Find(employee.EmployeeId);
            if (dbEntity == null)
                throw new InvalidOperationException($"Сущность с Id={employee.EmployeeId} не найдена в базе данных");
            dbEntity.Surname = employee.Surname;
            dbEntity.Name = employee.Name;
            dbEntity.Surname = employee.Patronymic;
            dbEntity.DateOfBirth = employee.DateOfBirth;
            dbEntity.Sex = employee.Sex;
            dbEntity.DepartmentId = employee.DepartmentId;
            context.SaveChanges();
        }
    }
}
