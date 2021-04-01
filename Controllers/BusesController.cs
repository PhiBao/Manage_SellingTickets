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
        private readonly ISeatService _seatService;
        private readonly IMapper _mapper;

        public BusesController(IBusService busService, ISeatService seatService, IMapper mapper)
        {
            _busService = busService;
            _seatService = seatService;
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

            for (var i = 0; i < bus.SoChoNgoi; i++)
            {
                _seatService.CreateSeat(new Chongoi 
                {
                    MaXe = bus.MaXe,
                    TinhTrangChoNgoi = false
                });
            }

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

        // DELETE api/accounts/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteBus(int id) 
        {
            var busSelected = _busService.GetBusById(id);
            if (busSelected == null)
            {
                return NotFound();
            }

            _busService.DeleteBus(busSelected);

            return NoContent();
        }

    }
}