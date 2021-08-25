using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int UnitOfMeasureId { get; set; }
        public UnitOfMeasure UnitOfMeasure { get; set; }
        public string ImageUrl { get; set; }
        public double Quantity { get; set; }
        public double AvalibleQuantity { get; set; }
        public double UnitPrice { get; set; }
        public double Discount { get; set; }
        public double Tax { get; set; }
        public ICollection<OrderItems> OrderItems { get; set; }
        public bool IsVisible { get; set; }
        public double LimitPerCustomer { get; set; }
    }
}
