using DAL.Models;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ShoppingCartRepository : Repository<ShoppingCart>, IShoppingCartRepository
    {
        public ShoppingCartRepository(ApplicationDbContext context) : base(context)
        {
        }

        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;


        public ShoppingCart GetShoppingCartData(int customerId)
        {
            var cart = _appContext.ShoppingCarts
                .Include(s => s.ShoppingCartItems)
                .ThenInclude(s => s.Item)
                .ThenInclude(s => s.UnitOfMeasure)
                .Include(s => s.Customer)
                .Where(s => s.CustomerId == customerId)
                .FirstOrDefault();

            if (cart == null) { 
                cart = new ShoppingCart() { CustomerId = customerId };
                _appContext.ShoppingCarts.Add(cart);
                _appContext.SaveChanges();
            }

            return cart;
        }

        public int GetShoppingCartId(int CustomerId)
        {
            return GetShoppingCartData(CustomerId).Id;
        }
        public ShoppingCart AddToCart(int ItemId, double Quantity, int customerId)
        {
            //var _shoppingCart = _shoppingCartItem.AddToCart(ItemId, Quantity, customerId);
            var _shoppingCart = GetShoppingCartData(customerId);
            var item = _shoppingCart.ShoppingCartItems.Where(i => i.ItemId == ItemId).FirstOrDefault();
            if(item == null)
            {
                item = new ShoppingCartItem() { ItemId = ItemId, Quantity = Quantity, ShoppingCartId = _shoppingCart.Id };
                _appContext.ShoppingCartItems.Add(item);
            }
            else
            {
                item.Quantity = Quantity;
            }
            _appContext.SaveChanges();
            return _shoppingCart;
        }

        public void DeleteFromCart(int ItemId, int customerId)
        {
        }

        public void ResetCart(int CustomerId)
        {
            var cart = _appContext.ShoppingCarts
                            .Include(s => s.ShoppingCartItems)
                            .Where(s => s.CustomerId == CustomerId)
                            .FirstOrDefault();
            _appContext.ShoppingCartItems.RemoveRange(cart.ShoppingCartItems);
            _appContext.SaveChanges();
        }
    }
}
