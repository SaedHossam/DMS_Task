using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models.OnlineShoping
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public ICollection<ShoppingCartItem> ShoppingCartItems { get; set; }
    }
}

// Front End => Cart == null => create, add
// checkout => move cart item into new order, remove all items from cart