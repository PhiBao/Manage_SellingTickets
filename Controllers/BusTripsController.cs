using System.Collections.Generic;
using AutoMapper;
using backend.Profiles;
using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Mvc;
using backend.Dtos;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;

namespace backend.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class BusTripsController : ControllerBase
    {
        private readonly IBusTripService _busTripService;
        private readonly ISeatService _seatService;
        private readonly IMapper _mapper;

        public BusTripsController(IBusTripService busTripService, ISeatService seatService, IMapper mapper)
        {
            _busTripService = busTripService;
            _seatService = seatService;
            _mapper = mapper;
        }

        // GET api/BusTrips
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BusTripReadDto>>> GetAllBusTripsAsync()
        {
            var busTrips = await _busTripService.GetBusTripsAsync();

            return Ok(_mapper.Map<IEnumerable<BusTripReadDto>>(busTrips));
        }

        // GET api/BusTrips/{id}
        [HttpGet("{id}", Name = "GetBusTripByIdAsync")]
        public async Task<ActionResult<Chuyenxe>> GetBusTripByIdAsync(int id)
        {
            var busTrip = await _busTripService.GetBusTripByIdAsync(id);

            if (busTrip != null)
            {
                return Ok(busTrip);
            }

            return NotFound();
        }
        // GET api/BusTrips/revenue
        [HttpGet("revenue")]
        public async Task<ActionResult<IEnumerable<RevenueHelperDto>>> GetRevenueByDayAsync()
        {
            var date = DateTime.Now;
            var revenues = await _busTripService.GetRevenueByDayAsync(date);

            if (revenues != null)
            {
                return Ok(revenues);
            }

            return NotFound();
        }

        // GET api/BusTrips/search?dep={a}&dest={b}&date={c}
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<BusTripReadDto>>> GetBusTripByConditionAsync(int dep, int dest, string date)
        {
            var busTrips = await _busTripService.GetBusTripByConditionAsync(dep, dest, date);

            if (busTrips != null)
            {
                return Ok(_mapper.Map<IEnumerable<BusTripReadDto>>(busTrips));
            }

            return NotFound();
        }

        //POST api/bustrips
        [HttpPost]
        public async Task<ActionResult<Chuyenxe>> CreateBusTripAsync(Chuyenxe busTrip)
        {
            await _busTripService.CreateBusTripAsync(busTrip);

            var SoChoTrong = busTrip.SoChoTrong.GetValueOrDefault();
            await _seatService.CreateSeatAsync(SoChoTrong, busTrip.MaChuyenXe);

            return CreatedAtRoute(nameof(GetBusTripByIdAsync), new { id = busTrip.MaChuyenXe }, busTrip);
        }

        // DELETE api/bustrips/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBusTripAsync(int id)
        {
            var user = await _busTripService.GetBusTripByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            await _busTripService.DeleteBusTripAsync(user);

            return NoContent();
        }
    }
}