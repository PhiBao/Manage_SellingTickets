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
        public async Task<ActionResult<Benxe>> GetBusStationByIdAsync(int id)
        {
            var busStation = await _busStationService.GetBusStationByIdAsync(id);

            if (busStation != null)
            {
                return Ok(busStation);
            }

            return NotFound();
        }

        // POST api/busstations
        [HttpPost]
        public async Task<ActionResult<Benxe>> CreateBusStationAsync(Benxe busStation)
        {
            await _busStationService.CreateBusStationAsync(busStation);

            return CreatedAtRoute(nameof(GetBusStationByIdAsync), new { id = busStation.MaBx }, busStation);
        }

    }
}