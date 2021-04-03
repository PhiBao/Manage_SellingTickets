using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using backend.Dtos;
using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusesController : ControllerBase
    {

        private readonly IBusService _busService;
        private readonly IMapper _mapper;

        public BusesController(IBusService busService, IMapper mapper)
        {
            _busService = busService;
            _mapper = mapper;
        }

        // GET api/buses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Xe>>> GetAllBusesAsync()
        {
            var buses = await _busService.GetBusesAsync();

            return Ok(buses);
        }

        // GET api/buses/{id}
        [HttpGet("{id}", Name = "GetBusByIdAsync")]
        public async Task<ActionResult<Xe>> GetBusByIdAsync(int id)
        {
            var bus = await _busService.GetBusByIdAsync(id);

            if (bus != null)
            {
                return Ok(bus);
            }

            return NotFound();
        }

        // POST api/buses
        [HttpPost]
        public async Task<ActionResult<Xe>> CreateBusAsync(Xe bus)
        {
            await _busService.CreateBusAsync(bus);

            return CreatedAtRoute(nameof(GetBusByIdAsync), new { id = bus.MaXe }, bus);
        }

        // PUT api/buses/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateBusAsync(int id, BusUpdateDto busUpdateDto)
        {
            var busSelected = await _busService.GetBusByIdAsync(id);
            if (busSelected == null)
            {
                return NotFound();
            }

            _mapper.Map(busUpdateDto, busSelected);
            await _busService.UpdateBusAsync(busSelected);

            return NoContent();
        }

        // DELETE api/buses/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBusAsync(int id)
        {
            var busSelected = await _busService.GetBusByIdAsync(id);
            if (busSelected == null)
            {
                return NotFound();
            }

            await _busService.DeleteBusAsync(busSelected);

            return NoContent();
        }

        // DELETE api/buses/delete?staffId={staffId}
        [HttpDelete("delete")]
        public async Task<ActionResult> DeleteBusByStaffIdAsync(int staffId)
        {
            var busSelected = await _busService.GetBusByStaffIdAsync(staffId);
            if (busSelected == null)
            {
                return NotFound();
            }

            await _busService.DeleteBusAsync(busSelected);

            return NoContent();
        }

    }
}