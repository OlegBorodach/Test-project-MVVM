using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Model;
using UI.Data.Interfaces;

namespace UI.Data
{
   public class OrderRepository:GenericRepository<Order, TestDBContext>, IOrderRepository
    {
        public OrderRepository(Func<TestDBContext> context) : base(context)
        {
        }

        public void Update(Order order)
        {
            using var context = ContextCreator.Invoke();
            var dbEntity = context.Orders.Find(order.OrderId);
            if (dbEntity == null)
                throw new InvalidOperationException($"Сущность с Id={order.OrderId} не найдена в базе данных");
            dbEntity.Contractor = order.Contractor;
            dbEntity.Date = order.Date;
            dbEntity.EmployeeId = order.EmployeeId;
            context.SaveChanges();
        }
    }
}
