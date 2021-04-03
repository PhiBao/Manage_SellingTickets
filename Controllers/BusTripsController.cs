using System.Collections.Generic;
using AutoMapper;
using backend.Profiles;
using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Mvc;
using backend.Dtos;
using System;
using System.Threading.Tasks;

namespace backend.Controllers
{
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

        // GET api/BusTrips/search?dep={a}&dest={b}
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<BusTripReadDto>>> GetBusTripByConditionAsync(int dep, int dest)
        {
            var busTrips = await _busTripService.GetBusTripByConditionAsync(dep, dest);

            if (busTrips != null)
            {
                return Ok(_mapper.Map<IEnumerable<BusTripReadDto>>(busTrips));
            }

            return NotFound();
        }

        // GET api/BusTrips/find?staffId={staffId}
        [HttpGet("find")]
        public async Task<ActionResult<IEnumerable<int>>> GetBusTripIdByStaffIdAsync(int staffId)
        {
            var busTripId = await _busTripService.GetBusTripIdByStaffIdAsync(staffId);

            return Ok(busTripId);
        }

        // GET api/BusTrips/check?busRouteId={busRouteId}
        [HttpGet("check")]
        public async Task<ActionResult<IEnumerable<int>>> GetBusTripIdByBusRouteIdAsync(int busRouteId)
        {
            var busTripId = await _busTripService.GetBusTripIdByBusRouteIdAsync(busRouteId);

            return Ok(busTripId);
        }

        // GET api/BusTrips/seek?busId={busId}
        [HttpGet("seek")]
        public async Task<ActionResult<IEnumerable<int>>> GetBusTripIdByBusIdAsync(int busId)
        {
            var busTripId = await _busTripService.GetBusTripIdByBusIdAsync(busId);

            return Ok(busTripId);
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

        // PUT api/buses/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateBusTripAsync(int id, BusTripUpdateDto busTripUpdateDto)
        {
            var busTripSelected = await _busTripService.GetBusTripByIdAsync(id);
            if (busTripSelected == null)
            {
                return NotFound();
            }
            busTripUpdateDto.SoChoDaDat = (busTripSelected.SoChoDaDat == null) ? 1 : busTripSelected.SoChoDaDat + 1;
            busTripUpdateDto.SoChoTrong = (busTripSelected.SoChoTrong == null) ? 1 : busTripSelected.SoChoTrong - 1;

            _mapper.Map(busTripUpdateDto, busTripSelected);
            await _busTripService.UpdateBusTripAsync(busTripSelected);

            return NoContent();
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