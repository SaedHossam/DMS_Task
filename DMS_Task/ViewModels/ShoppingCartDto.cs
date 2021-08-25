using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DMS_Task.ViewModels
{
    public class ShoppingCartDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string Customer { get; set; }
        public ICollection<ShoppingCartItemDto> ShoppingCartItems { get; set; }
    }
}
