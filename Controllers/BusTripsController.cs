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
        public async Task<ActionResult<IEnumerable<Chuyenxe>>> GetAllBusTripsAsync()
        {
            var busTrips = await _busTripService.GetBusTripsAsync();

            return Ok(_mapper.Map<IEnumerable<BusTripReadDto>>(busTrips));
        }

        // GET api/BusTrips/{id}
        [HttpGet("{id}", Name = "GetBusTripByIdAsync")]
        public async Task<ActionResult<BusTripReadDto>> GetBusTripByIdAsync(int id)
        {
            var busTrip = await _busTripService.GetBusTripByIdAsync(id);

            if (busTrip != null)
            {
                return Ok(_mapper.Map<BusTripReadDto>(busTrip));
            }

            return NotFound();
        }

        // GET api/BusTrips/revenue?date={a}
        [HttpGet("revenue")]
        public async Task<ActionResult<IEnumerable<RevenueByDayDto>>> GetRevenueByDayAsync(string date)
        {
            var revenues = await _busTripService.GetRevenueByDayAsync(date);

            if (revenues != null)
            {
                return Ok(_mapper.Map<IEnumerable<RevenueByDayDto>>(revenues));
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
        public async Task<ActionResult<Chuyenxe>> CreateBusTripAsync(BusTripCreateDto busTrip)
        {
            Chuyenxe busTripModel = _mapper.Map<Chuyenxe>(busTrip);
            
            if (await _busTripService.CreateBusTripAsync(busTripModel) == false) return BadRequest();

            var SoChoTrong = busTripModel.SoChoTrong.GetValueOrDefault();
            await _seatService.CreateSeatAsync(SoChoTrong, busTripModel.MaChuyenXe);

            return CreatedAtRoute(nameof(GetBusTripByIdAsync), new { id = busTripModel.MaChuyenXe }, busTripModel);
        }

        // DELETE api/bustrips/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBusTripAsync(int id)
        {
            var busTrip = await _busTripService.GetBusTripByIdAsync(id);
            if (busTrip == null)
            {
                return NotFound();
            }

            await _busTripService.DeleteBusTripAsync(busTrip);

            return NoContent();
        }
    }
}