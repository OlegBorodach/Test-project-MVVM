using System;
using DataService.Interfaces;
using Microsoft.EntityFrameworkCore;
using Model;

namespace DataService.Repositories
{
   public class OrderRepository:Repository<Order>, IOrderRepository
    {
        public OrderRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public void Update(Order order)
        {
            var dbEntity = Get(order.OrderId);
            if (dbEntity == null)
                throw new InvalidOperationException($"Сущность {order} не найдена в базе данных");
            dbEntity.Contractor = order.Contractor;
            dbEntity.EmployeeId = order.EmployeeId;
            dbEntity.Date = order.Date;
        }
    }
}
