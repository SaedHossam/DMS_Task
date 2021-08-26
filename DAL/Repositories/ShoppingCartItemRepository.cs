using DAL.Models;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class ShoppingCartItemRepository : Repository<ShoppingCartItem>, IShoppingCartItemRepository
    {
        
        public ShoppingCartItemRepository(ApplicationDbContext context) : base(context)
        {
        }

        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;

        public ShoppingCartItem GetCartItem(int CustomerId, int ItemId)
        {
            var cartItem = _appContext.ShoppingCartItems.Include(a => a.ShoppingCart).Where(c => c.ItemId == ItemId && c.ShoppingCart.CustomerId == CustomerId).FirstOrDefault();
            return cartItem;
        }

    }
}
