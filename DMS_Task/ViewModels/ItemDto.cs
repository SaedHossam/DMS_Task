using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DMS_Task.ViewModels
{
    public class ItemListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string UnitOfMeasure { get; set; }
        public int UnitOfMeasureId { get; set; }
        public string ImageUrl { get; set; }
        public double Quantity { get; set; }
        public double UnitPrice { get; set; }
        public double Discount { get; set; }
        public double Tax { get; set; }
        public double AvalibleQuantity { get; set; }
        public double LimitPerCustomer { get; set; }
    }


    public class ItemCreateDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int UnitOfMeasureId { get; set; }
        public string ImageUrl { get; set; }
        public double Quantity { get; set; }
        public double UnitPrice { get; set; }
        public double Discount { get; set; }
        public double Tax { get; set; }
        public double LimitPerCustomer { get; set; }
    }

    public class ItemEditDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public int UnitOfMeasureId { get; set; }
        public double Quantity { get; set; }
        public double UnitPrice { get; set; }
        public double Discount { get; set; }
        public double Tax { get; set; }
        public double LimitPerCustomer { get; set; }
    }
}
