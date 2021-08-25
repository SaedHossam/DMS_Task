using AutoMapper;
using DAL;
using DAL.Models;
using DMS_Task.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DMS_Task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UOMController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UOMController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }


        // GET: api/UOM
        [HttpGet]
        public ActionResult<IEnumerable<UnitOfMeasureDto>> GetUnitOfMeasure()
        {
            var units = _mapper.Map<IEnumerable<UnitOfMeasureDto>>(_unitOfWork.UnitOfMeasure.GetAll().Where(u => u.IsVisible == true));
            return Ok(units);
        }

        // GET: api/UOM/5
        [HttpGet("{id}")]
        public ActionResult<UnitOfMeasureDto> GetUnitOfMeasure(int id)
        {
            var UOM = _mapper.Map<UnitOfMeasureDto>(_unitOfWork.UnitOfMeasure.Get(id));

            if (UOM == null)
            {
                return NotFound();
            }

            return UOM;
        }

        // PUT: api/UOM/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutUnitOfMeasure(int id,[FromBody] UnitOfMeasureDto unitOfMeasureDto)
        {
            if (id != unitOfMeasureDto.Id)
            {
                return BadRequest();
            }

            var unitOfMeasure = _mapper.Map<UnitOfMeasure>(unitOfMeasureDto);
            unitOfMeasure.IsVisible = true;
            _unitOfWork.UnitOfMeasure.Update(unitOfMeasure);

            try
            {
                _unitOfWork.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CareerLevelExists(id))
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

        // POST: api/UOM
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public IActionResult PostUnitOfMeasure([FromBody] UnitOfMeasureCreateDto unitOfMeasureDto)
        {
            if (unitOfMeasureDto == null || !ModelState.IsValid)
                return BadRequest();

            var unitOfMeasure = _mapper.Map<UnitOfMeasure>(unitOfMeasureDto);
            unitOfMeasure.IsVisible = true;
            _unitOfWork.UnitOfMeasure.Add(unitOfMeasure);
            _unitOfWork.SaveChanges();
            return CreatedAtAction("GetUnitOfMeasure", new { id = unitOfMeasure.Id }, unitOfMeasureDto);
        }

        // DELETE: api/UOM/5
        [HttpDelete("{id}")]
        public IActionResult DeleteUnitOfMeasure(int id)
        {
            var unitOfMeasure = _unitOfWork.UnitOfMeasure.Get(id);
            if (unitOfMeasure == null)
            {
                return NotFound();
            }

            unitOfMeasure.IsVisible = false;
            _unitOfWork.SaveChanges();

            return NoContent();
        }

        private bool CareerLevelExists(int id)
        {
            return _unitOfWork.UnitOfMeasure.Get(id) != null;
        }

    }
}
