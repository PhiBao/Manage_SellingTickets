using System.Collections.Generic;
using AutoMapper;
using backend.Dtos;
using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
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
        public ActionResult<IEnumerable<BusStationReadDto>> GetAllBusStations()
        {
            var busStations = _busStationService.GetBusStations();

            return Ok(_mapper.Map<IEnumerable<BusStationReadDto>>(busStations));
        }

        // GET api/BusStations/id
        [HttpGet("{id}", Name = "GetBusStationById")]
        public ActionResult<Benxe> GetBusStationById(int id)
        {
            var busStation = _busStationService.GetBusStationById(id);

            if (busStation != null)
            {
                return Ok(busStation);
            }

            return NotFound();
        }

        // POST api/busstations
        [HttpPost]
        public ActionResult<Benxe> CreateBusStation(Benxe busStation)
        {
            _busStationService.CreateBusStation(busStation);

            return CreatedAtRoute(nameof(GetBusStationById), new { id = busStation.MaBx }, busStation);
        }

    }
}