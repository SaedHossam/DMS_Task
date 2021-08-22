using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models.OnlineShoping
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? RequestDate { get; set; }
        public DateTime? DueDate { get; set; }
        public bool IsOpen { get; set; }
        public bool IsVisible { get; set; }
        public double Tax { get; set; }
        public double Discount { get; set; }
        public double TotalPrice { get; set; }
        public double TotalPriceAfterDiscount { get; set; }
        // Final Price
        public double TotalPriceAfterTax { get; set; }
        public ICollection<OrderItems> OrderItems { get; set; }
    }
}
