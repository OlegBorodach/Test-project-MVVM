using Model;

namespace DataService.Interfaces
{
   public interface IEmployeeRepository:IRepository<Employee>
   {
       void Update(Employee employee);
   }
}
