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
    public class BusStationsController : ControllerBase
    {

        private readonly IBusStationService _busStationService;
        private readonly IMapper _mapper;

        public BusStationsController(IBusStationService busStationService, IMapper mapper)
        {
            _busStationService = busStationService;
            _mapper = mapper;
        }

        // GET api/BusStations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BusStationReadDto>>> GetAllBusStationsAsync()
        {
            var busStations = await _busStationService.GetBusStationsAsync();

            return Ok(_mapper.Map<IEnumerable<BusStationReadDto>>(busStations));
        }

        // GET api/BusStations/{id}
        [HttpGet("{id}", Name = "GetBusStationByIdAsync")]
        public async Task<ActionResult<BusStationReadDto>> GetBusStationByIdAsync(int id)
        {
            var busStation = await _busStationService.GetBusStationByIdAsync(id);

            if (busStation != null)
            {
                return Ok(_mapper.Map<BusStationReadDto>(busStation));
            }

            return NotFound();
        }

        // POST api/busstations
        [HttpPost]
        public async Task<ActionResult<Benxe>> CreateBusStationAsync(BusStationCreateDto busStation)
        {
            Benxe busStationModel = _mapper.Map<Benxe>(busStation);
            await _busStationService.CreateBusStationAsync(busStationModel);

            return CreatedAtRoute(nameof(GetBusStationByIdAsync), new { id = busStationModel.MaBx }, busStationModel);
        }

        // DELETE api/busstations/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBusStationAsync(int id)
        {
            var busStation = await _busStationService.GetBusStationByIdAsync(id);

            if (busStation == null)
            {
                return NotFound();
            }

            await _busStationService.DeleteBusStationAsync(busStation);

            return NoContent();
        }


    }
}