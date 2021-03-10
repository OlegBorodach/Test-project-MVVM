using System;
using System.Collections.Generic;
using System.Text;
using DataService.Interfaces;
using Microsoft.EntityFrameworkCore;
using Model;

namespace DataService.Repositories
{
    public class EmployeeRepository:Repository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public void Update(Employee employee)
        {
            var dbEntity = Get(employee.EmployeeId);
            if (dbEntity == null)
                throw new InvalidOperationException($"Сущность {employee} не найдена в базе данных");
            dbEntity.Name = employee.Name;
            dbEntity.Surname = employee.Surname;
            dbEntity.Patronymic = employee.Patronymic;
            dbEntity.DateOfBirth = employee.DateOfBirth;
            dbEntity.Sex = employee.Sex;
            dbEntity.DepartmentId = employee.DepartmentId;
        }
    }
}
