using System;
using System.Collections.Generic;
using System.Text;
using DataAccess;
using Model;

namespace UI.Data.Interfaces
{
    public interface IOrderRepository:IGenericRepository<Order>
    {
        void Update(Order order);
    }
}
