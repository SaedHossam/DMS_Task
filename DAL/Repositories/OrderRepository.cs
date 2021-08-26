using DAL.Models;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(ApplicationDbContext context) : base(context)
        { }

        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;

        public int CreateOrder(int CustomerId)
        {
            var cart = _appContext.ShoppingCarts
                .Include(a => a.ShoppingCartItems)
                .ThenInclude(s => s.Item)
                .Where(c => c.CustomerId == CustomerId)
                .FirstOrDefault();
            Order order = new Order() {
                CustomerId = CustomerId,
                RequestDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(2),
                IsOpen = true,
                IsVisible = true,
                OrderDate = DateTime.Now,
                TotalPrice = 0,
                TotalPriceAfterDiscount = 0,
                TotalPriceAfterTax = 0,
                Tax = 0,
                Discount = 0
            };
            _appContext.Orders.Add(order);
            _appContext.SaveChanges();

            foreach(var i in cart.ShoppingCartItems)
            {
                var totalPrice = i.Item.UnitPrice * i.Quantity;
                var disc = (totalPrice * i.Item.Discount / 100);
                var priceAfterDisc = totalPrice - disc;
                var tax = (priceAfterDisc * i.Item.Tax / 100);
                var priceAfterTax = priceAfterDisc + tax;
                
                // create new order item
                OrderItems orderItem = new OrderItems() { 
                    OrderId = order.Id,
                    ItemId = i.ItemId,
                    Quantity = i.Quantity,
                    TotalPrice = totalPrice,
                    TotalPriceAfterDiscount = priceAfterDisc,
                    TotalPriceAfterTax = priceAfterTax
                };
                _appContext.OrderItems.Add(orderItem);
                // update order data
                order.TotalPrice += totalPrice;
                order.TotalPriceAfterDiscount += priceAfterDisc;
                order.TotalPriceAfterTax += priceAfterTax;
                order.Tax += tax;
                order.Discount += disc;

                // update Item Quantity
                var item = _appContext.Items.Where(it => it.Id == i.ItemId).FirstOrDefault();
                item.AvalibleQuantity -= i.Quantity;
            }
            _appContext.SaveChanges();
            return order.Id;
        }

        public ICollection<Order> GetCustomerOrders(int CustomerId)
        {
            return _appContext.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderItems)
                .ThenInclude(i => i.Item)
                .Where(o => o.CustomerId == CustomerId)
                .ToList();
        }

        public Order GetOrderById(int id)
        {
            return _appContext.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderItems)
                .ThenInclude(o => o.Item)
                .Where(o => o.Id == id)
                .FirstOrDefault();
        }
    }
}
