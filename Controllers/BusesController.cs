using System.Collections.Generic;
using AutoMapper;
using backend.Dtos;
using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    // api/users
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
        public ActionResult<IEnumerable<Xe>> GetAllBuses()
        {
            var buses = _busService.GetBuses();

            return Ok(buses);
        }

        // GET api/buses/id
        [HttpGet("{id}", Name = "GetBusById")]
        public ActionResult<Xe> GetBusById(int id)
        {
            var bus = _busService.GetBusById(id);

            if (bus != null) 
            {
                return Ok(bus);
            }

            return NotFound();
        }

        // POST api/buses
        [HttpPost]
        public ActionResult<Xe> CreateBus(Xe bus)
        {
            _busService.CreateBus(bus);

            return CreatedAtRoute(nameof(GetBusById), new { id = bus.MaXe }, bus);
        }

        // Put api/buses/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateBus(int id, BusUpdateDto busUpdateDto) 
        {
            var busSelected = _busService.GetBusById(id);
            if (busSelected == null)
            {
                return NotFound();
            }

            _mapper.Map(busUpdateDto, busSelected);
            _busService.UpdateBus(busSelected);
            _busService.SaveChanges();

            return NoContent();
        }

    }
}