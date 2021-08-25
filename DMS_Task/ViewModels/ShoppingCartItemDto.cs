using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DMS_Task.ViewModels
{
    public class ShoppingCartItemDto
    {
        public int Id { get; set; }
        public int ShoppingCartId { get; set; }
        public int ItemId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double OrderedQuantity { get; set; }
        public double AvalibleQuantity { get; set; }
        public double LimitPerCustomer { get; set; }
        public string UnitOfMeasure { get; set; }
        public string ImageUrl { get; set; }
        public double UnitPrice { get; set; }
        public double Discount { get; set; }
        public double Tax { get; set; }
        public double FinalPrice { get; set; }
    }

    public class ShoppingCartItemAddDto
    {
        public int ItemId { get; set; }
        public double Quantity { get; set; }
    }
}
