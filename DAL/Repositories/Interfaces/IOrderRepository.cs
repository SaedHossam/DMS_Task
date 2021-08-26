using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        public ICollection<Order> GetCustomerOrders(int CustomerId);
        public int CreateOrder(int CustomerId);
        public Order GetOrderById(int id);
    }
}
