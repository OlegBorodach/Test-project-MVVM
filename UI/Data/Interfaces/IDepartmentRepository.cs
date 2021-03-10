using System.Collections.Generic;
using System.Threading.Tasks;
using Model;

namespace UI.Data.Interfaces
{
   public interface IDepartmentRepository:IGenericRepository<Department>
   {
       void Update(Department department);
   }
}
