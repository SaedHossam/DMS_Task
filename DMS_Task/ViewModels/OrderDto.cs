using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DMS_Task.ViewModels
{
    public class OrderListDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        //public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? DueDate { get; set; }
        public double Tax { get; set; }
        public double Discount { get; set; }
        public double TotalPrice { get; set; }
        public double TotalPriceAfterDiscount { get; set; }
        // Final Price
        public double TotalPriceAfterTax { get; set; }
        public ICollection<OrderItemDto> OrderItems { get; set; }

    }

    public class OrderItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public double Quantity { get; set; }
    }

}
