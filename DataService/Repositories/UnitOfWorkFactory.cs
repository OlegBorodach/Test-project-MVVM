using DataAccess;
using DataService.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataService.Repositories
{
    public class UnitOfWorkFactory<T>:IUnitOfWorkFactory where T : DbContext, new()
    {
        public IUnitOfWork Create()
        {
            return new UnitOfWork<T>();
        }
    }
}
