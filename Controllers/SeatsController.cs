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
        public async Task<ActionResult<IEnumerable<Chongoi>>> GetAllSeatsAsync()
        {
            var seats = await _seatService.GetSeatsAsync();

            return Ok(seats);
        }

        // GET api/seats/{id}
        [HttpGet("{id}", Name = "GetSeatByIdAsync")]
        public async Task<ActionResult<bool>> GetSeatByIdAsync(int id)
        {
            var seat = await _seatService.GetSeatByIdAsync(id);

            if (seat != null)
            {
                return Ok(seat.TinhTrangChoNgoi == true);
            }

            return NotFound();
        }


        // GET api/Seats/search?busTripId={busTripId}
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<SeatReadDto>>> GetSeatByBusTripIdAsync(int busTripId)
        {
            var seats = await _seatService.GetSeatsByBusTripIdAsync(busTripId);

            if (seats != null)
            {
                return Ok(_mapper.Map<IEnumerable<SeatReadDto>>(seats));
            }

            return NotFound();
        }

    }
}