using System.Collections.Generic;
using AutoMapper;
using backend.Profiles;
using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Mvc;
using backend.Dtos;
using System;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusTripsController : ControllerBase
    {
        private readonly IBusTripService _busTripService;
        private readonly IMapper _mapper;

        public BusTripsController(IBusTripService busTripService, IMapper mapper)
        {
            _busTripService = busTripService;
            _mapper = mapper;
        }

        // GET api/BusTrips
        [HttpGet]
        public ActionResult<IEnumerable<BusTripReadDto>> GetAllBusTrips()
        {
            var busTrips = _busTripService.GetBusTrips();

            return Ok(_mapper.Map<IEnumerable<BusTripReadDto>>(busTrips));
        }

        // GET api/BusTrips/id
        [HttpGet("{id}", Name = "GetBusTripById")]
        public ActionResult<BusTripReadDto> GetBusTripById(int id)
        {
            var busTrip = _busTripService.GetBusTripById(id);

            if (busTrip != null)
            {
                return Ok(_mapper.Map<BusTripReadDto>(busTrip));
            }

            return NotFound();
        }

        // GET api/BusTrips/search?dep=a&dest=b&date=c
        [HttpGet("search")]
        public ActionResult<IEnumerable<BusTripReadDto>> GetBusTripByCondition(int dep, int dest) 
        {
            var busTrips = _busTripService.GetBusTripByCondition(dep, dest);

            if (busTrips != null)
            {
                return Ok(_mapper.Map<IEnumerable<BusTripReadDto>>(busTrips));
            }

            return NotFound();
        }

        //POST api/bustrips
        [HttpPost]
        public ActionResult<Nguoidung> CreateStaff(Chuyenxe busTrip)
        {
            _busTripService.CreateBusTrip(busTrip);

            return CreatedAtRoute(nameof(GetBusTripById), new { id = busTrip.MaChuyenXe }, busTrip);
        }


    }
}