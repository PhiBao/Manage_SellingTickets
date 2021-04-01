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
    public class SeatsController : ControllerBase
    {

        private readonly ISeatService _seatService;
        private readonly IMapper _mapper;

        public SeatsController(ISeatService seatService, IMapper mapper)
        {
            _seatService = seatService;
            _mapper = mapper;
        }

        // GET api/seats
        [HttpGet]
        public ActionResult<IEnumerable<Chongoi>> GetAllSeats()
        {
            var seats = _seatService.GetSeats();

            return Ok(seats);
        }

        // GET api/seats/{id}
        [HttpGet("{id}", Name = "GetSeatById")]
        public ActionResult<bool> GetSeatById(int id)
        {
            var seat = _seatService.GetSeatById(id);

            if (seat != null)
            {
                return Ok(seat.TinhTrangChoNgoi == true);
            }

            return NotFound();
        }

        // Put api/seats/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateSeat(int id, SeatUpdateDto seatUpdateDto) 
        {
            var seatSelected = _seatService.GetSeatById(id);
            if (seatSelected == null)
            {
                return NotFound();
            }

            _mapper.Map(seatUpdateDto, seatSelected);
            _seatService.UpdateSeat(seatSelected);
            _seatService.SaveChanges();

            return NoContent();
        }

    }
}