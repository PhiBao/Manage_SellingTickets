using System.Collections.Generic;
using System.Threading.Tasks;
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
        

        // GET api/Seats/search?busTripId=busTripId
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<SeatReadDto>>> GetSeatByBusTripIdAsync(int busTripId) 
        {
            var seats = await _seatService.GetSeatByBusTripIdAsync(busTripId);

            if (seats != null)
            {
                return Ok(_mapper.Map<SeatReadDto>(seats));
            }

            return NotFound();
        }

        //POST api/seats
        [HttpPost]
        public async Task<ActionResult<Chongoi>> CreateSeatAsync(Chongoi seat)
        {
            await _seatService.CreateSeatAsync(seat);

            return CreatedAtRoute(nameof(GetSeatByIdAsync), new { id = seat.MaChoNgoi }, seat);
        }

        // Put api/seats/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateSeatAsync(int id, SeatUpdateDto seatUpdateDto) 
        {
            var seatSelected = await _seatService.GetSeatByIdAsync(id);
            if (seatSelected == null)
            {
                return NotFound();
            }

            _mapper.Map(seatUpdateDto, seatSelected);
            await _seatService.UpdateSeatAsync(seatSelected);

            return NoContent();
        }

    }
}