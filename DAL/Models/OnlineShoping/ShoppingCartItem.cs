using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models.OnlineShoping
{
    public class ShoppingCartItem
    {
        public int Id { get; set; }
        public int ShoppingCartId { get; set; }
        public ShoppingCart ShoppingCart { get; set; }
        public int ItemId { get; set; }
        public Item Item { get; set; }
        public double Quantity { get; set; }
    }
}
