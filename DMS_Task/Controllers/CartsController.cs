using AutoMapper;
using DAL;
using DAL.Models;
using DMS_Task.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DMS_Task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        public CartsController(IMapper mapper, IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        // GET: api/Item/
        [HttpGet]
        [Authorize(Roles = "Employee")]
        public async Task<ActionResult<ShoppingCartDto>> GetCartItemsAsync()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            if (user == null)
                return BadRequest("Can't find User!\nlogin and try again.");

            var customer = _unitOfWork.Customer.Find(c => c.UserId == user.Id).FirstOrDefault();
            if (customer == null)
                return BadRequest("Can't find User!\nlogin and try again.");

            var cart = _unitOfWork.ShoppingCart.GetShoppingCartData(customer.Id);
            var cartItems = _mapper.Map<ShoppingCartDto>(cart);
            foreach(var i in cartItems.ShoppingCartItems)
            {
                var x = i.UnitPrice - (i.UnitPrice * i.Discount / 100);
                i.FinalPrice = x + (x * i.Tax / 100);
                i.Discount = i.UnitPrice * i.Discount / 100;
                i.Tax = (i.UnitPrice - i.Discount) * i.Tax / 100;
            }
            return Ok(cartItems);
        }


        // Add Item To Cart

        // POST: api/Jobs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Employee")]
        public async Task<ActionResult<ShoppingCartDto>> PostCartAsync([FromBody] ShoppingCartItemAddDto cartItem)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user == null)
                return BadRequest("Can't find User!\nlogin and try again.");

            var customer = _unitOfWork.Customer.Find(c => c.UserId == user.Id).FirstOrDefault();
            if (customer == null)
                return BadRequest("Can't find User!\nlogin and try again.");

            if (cartItem == null || !ModelState.IsValid)
                return BadRequest();
            
            // Check ordered quantity
            if (cartItem.Quantity <= 0)
                return BadRequest("Enter a valid quantity");
            
            var item = _unitOfWork.Item.Get(cartItem.ItemId);
            if (item == null)
                return BadRequest("Item Not Found");

            var m = Math.Min(item.AvalibleQuantity, item.LimitPerCustomer);
            if (m < cartItem.Quantity)
                return BadRequest("Ordered Quantity are more than avalible");

            var cart = _unitOfWork.ShoppingCart.AddToCart(cartItem.ItemId, cartItem.Quantity, customer.Id);
            
            return Ok(_mapper.Map<ShoppingCartDto>(cart));
        }

        //// GET: api/Item/
        //[HttpGet("avalible")]
        //public ActionResult<IEnumerable<ItemListDto>> GetAvalibleItems()
        //{
        //    var items = _unitOfWork.Item.AvalibleItems();
        //    return Ok(_mapper.Map<IEnumerable<ItemListDto>>(items));
        //}

        //// GET: api/Item/:id
        //[HttpGet("{id}")]
        //public ActionResult<ItemListDto> GetJob(int id)
        //{
        //    var item = _unitOfWork.Item.ItemData(id);

        //    if (item == null)
        //    {
        //        return NotFound();
        //    }

        //    return _mapper.Map<ItemListDto>(item);
        //}


        //[HttpGet("Search/{term}")]
        //public ActionResult<IEnumerable<ItemListDto>> Search(string term)
        //{
        //    var res = _unitOfWork.Item.ItemsList().Where(i => i.Name.Contains(term, StringComparison.InvariantCultureIgnoreCase)).ToList();
        //    return Ok(_mapper.Map<IEnumerable<ItemListDto>>(res));

        //}

        //// PUT: api/Item/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        ////[Authorize(Roles = "Company")]
        //public IActionResult PutJob(int id, [FromBody] ItemEditDto itemEdit)
        //{
        //    var oldItem = _unitOfWork.Item.ItemData(id);

        //    _mapper.Map(itemEdit, oldItem);

        //    try
        //    {
        //        _unitOfWork.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ItemExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }
        //    return NoContent();
        //}


        // POST: api/Jobs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        //[Authorize(Roles = "Company")]
        //public ActionResult<ItemCreateDto> PostJob([FromBody] ItemCreateDto item)
        //{
        //    if (item == null || !ModelState.IsValid)
        //        return BadRequest();

        //    var Item = _mapper.Map<Item>(item);
        //    Item.IsVisible = true;
        //    Item.AvalibleQuantity = item.Quantity;

        //    _unitOfWork.Item.Add(Item);
        //    _unitOfWork.SaveChanges();
        //    return Ok(Item);
        //}

        //// DELETE: api/Jobs/5
        //[HttpDelete("{id}")]
        //public IActionResult DeleteJob(int id)
        //{
        //    var item = _unitOfWork.Item.Get(id);
        //    if (item == null)
        //    {
        //        return NotFound();
        //    }
        //    item.IsVisible = false;
        //    _unitOfWork.SaveChanges();
        //    return NoContent();
        //}


        


        private bool ItemExists(int id)
        {
            return _unitOfWork.ShoppingCart.Get(id) != null;
        }
    }
}
