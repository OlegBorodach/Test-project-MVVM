using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Model;
using UI.Data.Interfaces;

namespace UI.Data
{
   public class DepartmentRepository:GenericRepository<Department, TestDBContext>, IDepartmentRepository
    {
        public DepartmentRepository(Func<TestDBContext> context) : base(context)
        {
        }

        public override Department GetById(int id)
        {
            using var context = ContextCreator.Invoke();
            return context.Departments.Include(department => department.Employee)
                .FirstOrDefault(d => d.DepartmentId == id);
        }

        public async Task<IEnumerable<Department>> GetAllAsyncInclude()
        {
            await using var context = ContextCreator.Invoke();
            return await context.Set<Department>().Include(d => d.Employee).ToListAsync();
        }

        public void Update(Department department)
        {
            using var context = ContextCreator.Invoke();
            var dbEntity = context.Departments.Find(department.DepartmentId);
            if (dbEntity == null)
                throw new InvalidOperationException($"Сущность {department} не найдена в базе данных");
            dbEntity.Name = department.Name;
            dbEntity.EmployeeId = department.EmployeeId;
            context.SaveChanges();
        }
    }
}
