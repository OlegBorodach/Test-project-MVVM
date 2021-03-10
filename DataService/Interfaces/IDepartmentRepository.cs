using Model;

namespace DataService.Interfaces
{
    public interface IDepartmentRepository:IRepository<Department>
    {
        void Update(Department department);
    }
}
