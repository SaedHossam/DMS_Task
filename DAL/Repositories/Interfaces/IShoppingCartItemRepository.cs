using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface IShoppingCartItemRepository : IRepository<ShoppingCartItem>
    {
        public ShoppingCartItem GetCartItem(int CustomerId, int ItemId);
    }
}
