using AutoMapper;
using DAL;
using DMS_Task.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace DMS_Task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ItemController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        // GET: api/Item/
        [HttpGet]
        public ActionResult<IEnumerable<ItemListDto>> GetJobs()
        {
            var items = _unitOfWork.Item.ItemsList();
            return Ok(_mapper.Map<IEnumerable<ItemListDto>>(items));
        }

        // GET: api/Item/avalible
        [HttpGet("avalible")]
        public ActionResult<IEnumerable<ItemListDto>> GetAvalibleItems()
        {
            var items = _unitOfWork.Item.AvalibleItems();
            return Ok(_mapper.Map<IEnumerable<ItemListDto>>(items));
        }

        // GET: api/Item/:id
        [HttpGet("{id}")]
        public ActionResult<ItemListDto> GetJob(int id)
        {
            var item = _unitOfWork.Item.ItemData(id);

            if (item == null)
            {
                return NotFound();
            }

            return _mapper.Map<ItemListDto>(item);
        }


        [HttpGet("Search/{term}")]
        public ActionResult<IEnumerable<ItemListDto>> Search(string term)
        {
            var res = _unitOfWork.Item.ItemsList().Where(i => i.Name.Contains(term, StringComparison.InvariantCultureIgnoreCase)).ToList();
            return Ok(_mapper.Map<IEnumerable<ItemListDto>>(res));

        }

        // PUT: api/Item/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        //[Authorize(Roles = "Company")]
        public IActionResult PutJob(int id, [FromBody] ItemEditDto itemEdit)
        {
            var oldItem = _unitOfWork.Item.ItemData(id);
            
            _mapper.Map(itemEdit, oldItem);

            try
            {
                _unitOfWork.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }


        // POST: api/Jobs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        //[Authorize(Roles = "Company")]
        public ActionResult<ItemCreateDto> PostJob([FromBody] ItemCreateDto item)
        {
            if (item == null || !ModelState.IsValid)
                return BadRequest();

            var Item = _mapper.Map<Item>(item);
            Item.IsVisible = true;
            Item.AvalibleQuantity = item.Quantity;

            _unitOfWork.Item.Add(Item);
            _unitOfWork.SaveChanges();
            return Ok(Item);
        }

        // DELETE: api/Jobs/5
        [HttpDelete("{id}")]
        public IActionResult DeleteJob(int id)
        {
            var item = _unitOfWork.Item.Get(id);
            if (item == null)
            {
                return NotFound();
            }
            item.IsVisible = false;
            _unitOfWork.SaveChanges();
            return NoContent();
        }


        [HttpPost("Upload"), DisableRequestSizeLimit]
        //[Authorize(Roles = "Employee")]
        public async Task<IActionResult> Upload(IFormFile file)
        {

            Guid obj = Guid.NewGuid();
            var fileName = obj.ToString() + ".png";

            try
            {
                var formCollection = await Request.ReadFormAsync();
                var folderName = Path.Combine("Resources", "images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if (file.Length > 0)
                {

                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    //employee.Photo = dbPath;
                    _unitOfWork.SaveChanges();
                    return Ok(new { dbPath });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }


        private bool ItemExists(int id)
        {
            return _unitOfWork.Item.Get(id) != null;
        }
    }
}
