using System.Collections.Generic;
using System.Threading.Tasks;
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
    public class TicketsController : ControllerBase
    {

        private readonly ITicketService _ticketService;
        private readonly IMapper _mapper;
        private readonly ISeatService _seatService;
        private readonly IBusTripService _busTripService;

        public TicketsController(ITicketService ticketService, ISeatService seatService, IBusTripService busTripService, IMapper mapper)
        {
            _busTripService = busTripService;
            _seatService = seatService;
            _ticketService = ticketService;
            _mapper = mapper;
        }

        // GET api/tickets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vexe>>> GetAllTicketsAsync()
        {
            var tickets = await _ticketService.GetTicketsAsync();

            return Ok(tickets);
        }

        // GET api/tickets/{id}
        [HttpGet("{id}", Name = "GetTicketByIdAsync")]
        public async Task<ActionResult<Vexe>> GetTicketByIdAsync(int id)
        {
            var ticket = await _ticketService.GetTicketByIdAsync(id);

            if (ticket != null)
            {
                return Ok(ticket);
            }

            return NotFound();
        }

        // GET api/tickets/search?userid={userid}
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Vexe>>> GetTicketsByUserIdAsync(int userId)
        {
            var ticket = await _ticketService.GetTicketsByUserIdAsync(userId);

            if (ticket != null)
            {
                return Ok(ticket);
            }

            return NotFound();
        }

        // POST api/tickets
        [HttpPost]
        public async Task<ActionResult<Vexe>> CreateTicketAsync(Vexe ticket)
        {
            var seat = await _seatService.GetSeatByIdAsync(ticket.MaChoNgoi);

            if (seat.TinhTrangChoNgoi == true) return BadRequest();

            await _ticketService.CreateTicketAsync(ticket);

            // Update bustrip
            var busTripSelected = await _busTripService.GetBusTripByIdAsync(seat.MaChuyenXe);
            if (busTripSelected == null)
            {
                return NotFound();
            }
            BusTripUpdateDto busTripUpdateDto = new BusTripUpdateDto();
            busTripUpdateDto.SoChoDaDat = (busTripSelected.SoChoDaDat == null) ? 1 : busTripSelected.SoChoDaDat + 1;
            busTripUpdateDto.SoChoTrong = (busTripSelected.SoChoTrong == null) ? 1 : busTripSelected.SoChoTrong - 1;

            _mapper.Map(busTripUpdateDto, busTripSelected);
            await _busTripService.UpdateBusTripAsync(busTripSelected);

            // Update seat    
            SeatUpdateDto seatUpdateDto = new SeatUpdateDto();

            seatUpdateDto.TinhTrangChoNgoi = true;
            _mapper.Map(seatUpdateDto, seat);

            await _seatService.UpdateSeatAsync(seat);

            return CreatedAtRoute(nameof(GetTicketByIdAsync), new { id = ticket.MaVe }, ticket);
        }

        // DELETE api/accounts/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTicket(int id)
        {
            var ticket = await _ticketService.GetTicketByIdAsync(id);

            if (ticket == null)
            {
                return NotFound();
            }

            await _ticketService.DeleteTicketAsync(ticket);

            var seat = await _seatService.GetSeatByIdAsync(ticket.MaChoNgoi);

            // Update bustrip
            var busTripSelected = await _busTripService.GetBusTripByIdAsync(seat.MaChuyenXe);
            if (busTripSelected == null)
            {
                return NotFound();
            }
            BusTripUpdateDto busTripUpdateDto = new BusTripUpdateDto();
            busTripUpdateDto.SoChoDaDat = (busTripSelected.SoChoDaDat == null) ? 1 : busTripSelected.SoChoDaDat - 1;
            busTripUpdateDto.SoChoTrong = (busTripSelected.SoChoTrong == null) ? 1 : busTripSelected.SoChoTrong + 1;

            _mapper.Map(busTripUpdateDto, busTripSelected);
            await _busTripService.UpdateBusTripAsync(busTripSelected);

            // Update seat    
            SeatUpdateDto seatUpdateDto = new SeatUpdateDto();

            seatUpdateDto.TinhTrangChoNgoi = false;
            _mapper.Map(seatUpdateDto, seat);

            await _seatService.UpdateSeatAsync(seat);

            return NoContent();
        }

    }
}