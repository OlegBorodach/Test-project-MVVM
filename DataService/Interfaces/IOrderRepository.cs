using Model;
using SQLitePCL;

namespace DataService.Interfaces
{
    public interface IOrderRepository:IRepository<Order>
    {
        void Update(Order order);
    }
}
