using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataService.Interfaces;
using Microsoft.EntityFrameworkCore;
using Model;

namespace DataService.Repositories
{
   public class DepartmentRepository:Repository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public void Update(Department department)
        {
            var dbEntity = Get(department.DepartmentId);
            if (dbEntity == null)
                throw new InvalidOperationException($"Сущность {department} не найдена в базе данных");
            dbEntity.Name = department.Name;
            dbEntity.EmployeeId = department.EmployeeId;
        }
    }
}
