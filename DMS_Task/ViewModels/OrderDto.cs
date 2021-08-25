using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DMS_Task.ViewModels
{
    public class OrderListDto
    {
        public int Id { get; set; }
        public string Customer { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? RequestDate { get; set; }
        public DateTime? DueDate { get; set; }
        public double Tax { get; set; }
        public double Discount { get; set; }
        public double TotalPrice { get; set; }
        public double TotalPriceAfterDiscount { get; set; }
        // Final Price
        public double TotalPriceAfterTax { get; set; }
    }

    public class OrderDetailsDto
    {
        public int Id { get; set; }
        public string Customer { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? RequestDate { get; set; }
        public DateTime? DueDate { get; set; }
        public double Tax { get; set; }
        public double Discount { get; set; }
        public double TotalPrice { get; set; }
        public double TotalPriceAfterDiscount { get; set; }
        // Final Price
        public double TotalPriceAfterTax { get; set; }
        //public ICollection<OrderItems> OrderItems { get; set; }
    }

    public class OrderCreateDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string Customer { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? RequestDate { get; set; }
        public DateTime? DueDate { get; set; }
        public double Tax { get; set; }
        public double Discount { get; set; }
        public double TotalPrice { get; set; }
        public double TotalPriceAfterDiscount { get; set; }
        // Final Price
        public double TotalPriceAfterTax { get; set; }
        //public ICollection<OrderItems> OrderItems { get; set; }
    }


}
