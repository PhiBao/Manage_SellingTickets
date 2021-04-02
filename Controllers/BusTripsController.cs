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

        // GET api/BusTrips/id
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

        // GET api/BusTrips/search?dep=a&dest=b&date=c
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

        //POST api/bustrips
        [HttpPost]
        public async Task<ActionResult<Chuyenxe>> CreateBusTripAsync(Chuyenxe busTrip)
        {
            await _busTripService.CreateBusTripAsync(busTrip);

            for (var i = 0; i < busTrip.SoChoTrong; i++)
            {
                await _seatService.CreateSeatAsync(new Chongoi 
                {
                    MaChuyenXe = busTrip.MaChuyenXe,
                    TinhTrangChoNgoi = false
                });
            }

            return CreatedAtRoute(nameof(GetBusTripByIdAsync), new { id = busTrip.MaChuyenXe }, busTrip);
        }

        // Put api/buses/{id}
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
    }
}