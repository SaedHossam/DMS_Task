using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface IShoppingCartRepository: IRepository<ShoppingCart>
    {
        public ShoppingCart GetShoppingCartData(int customerId);
        public int GetShoppingCartId(int CustomerId);
        public ShoppingCart AddToCart(int ItemId, double Quantity, int customerId);

        public void DeleteFromCart(int ItemId, int customerId);
        public void ResetCart(int CustomerId);
    }
}
