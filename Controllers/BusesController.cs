using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using backend.Dtos;
using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [EnableCors("AllowOrigin")]
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
        public async Task<ActionResult<IEnumerable<BusReadDto>>> GetAllBusesAsync()
        {
            var buses = await _busService.GetBusesAsync();

            return Ok(_mapper.Map<IEnumerable<BusReadDto>>(buses));
        }

        // GET api/buses/{id}
        [HttpGet("{id}", Name = "GetBusByIdAsync")]
        public async Task<ActionResult<BusReadDto>> GetBusByIdAsync(int id)
        {
            var bus = await _busService.GetBusByIdAsync(id);

            if (bus != null)
            {
                return Ok(_mapper.Map<BusReadDto>(bus));
            }

            return NotFound();
        }

        // POST api/buses
        [HttpPost]
        public async Task<ActionResult<Xe>> CreateBusAsync(BusCreateDto bus)
        {
            Xe busModel = _mapper.Map<Xe>(bus);
            await _busService.CreateBusAsync(busModel);

            return CreatedAtRoute(nameof(GetBusByIdAsync), new { id = busModel.MaXe }, busModel);
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

        // GET api/buses/search?name={c}
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<BusReadDto>>> SearchBusesByName(string name)
        {
            var buses = await _busService.SearchBusesByName(name);

            if (buses != null)
            {
                return Ok(_mapper.Map<IEnumerable<BusReadDto>>(buses));
            }

            return NotFound();
        }

    }
}