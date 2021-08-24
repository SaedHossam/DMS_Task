using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class OrderItems
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int ItemId { get; set; }
        public Item Item { get; set; }
        public double Quantity { get; set; }
        public double TotalPrice { get; set; }
        public double TotalPriceAfterDiscount { get; set; }
        // Final Price
        public double TotalPriceAfterTax { get; set; }

    }
}
