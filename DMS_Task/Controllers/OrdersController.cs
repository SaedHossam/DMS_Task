using AutoMapper;
using DAL;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DMS_Task.ViewModels;

namespace DMS_Task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        public OrdersController(IMapper mapper, IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        [HttpPost]
        [Authorize(Roles = "Employee")]
        public async Task<ActionResult> PostOrderAsync()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user == null)
                return BadRequest("Can't find User!\nlogin and try again.");

            var customer = _unitOfWork.Customer.Find(c => c.UserId == user.Id).FirstOrDefault();
            if (customer == null)
                return BadRequest("Can't find User!\nlogin and try again.");

            var cart = _unitOfWork.ShoppingCart.GetShoppingCartData(customer.Id);
            if (cart.ShoppingCartItems.Count <= 0)
                return BadRequest("Can't make Order with Empty Cart!");

            var order_id = _unitOfWork.Order.CreateOrder(customer.Id);
            // reset cart
            _unitOfWork.ShoppingCart.ResetCart(customer.Id);
            return Ok(order_id);
        }

        [HttpGet]
        [Authorize(Roles = "Employee")]
        public async Task<ActionResult<IEnumerable<OrderListDto>>> GetOrders()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user == null)
                return BadRequest("Can't find User!\nlogin and try again.");

            var customer = _unitOfWork.Customer.Find(c => c.UserId == user.Id).FirstOrDefault();
            if (customer == null)
                return BadRequest("Can't find User!\nlogin and try again.");

            var orders = _unitOfWork.Order.GetCustomerOrders(customer.Id);
            return Ok(_mapper.Map<IEnumerable<OrderListDto>>(orders));
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Employee")]
        public async Task<ActionResult<OrderListDto>> GetOrderById(int id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user == null)
                return BadRequest("Can't find User!\nlogin and try again.");

            var customer = _unitOfWork.Customer.Find(c => c.UserId == user.Id).FirstOrDefault();
            if (customer == null)
                return BadRequest("Can't find User!\nlogin and try again.");
            var o = _unitOfWork.Order.Get(id);
            if (o == null)
                return BadRequest("Order Not Found!");
            var orders = _unitOfWork.Order.GetOrderById(id);
            return Ok(_mapper.Map<OrderListDto>(orders));
        }
    }
}
