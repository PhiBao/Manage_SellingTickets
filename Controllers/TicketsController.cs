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

        public TicketsController(ITicketService ticketService, ISeatService seatService, IMapper mapper)
        {
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

        // GET api/tickets/id
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

        // POST api/tickets
        [HttpPost]
        public async Task<ActionResult<Vexe>> CreateTicketAsync(Vexe ticket)
        {
            await _ticketService.CreateTicketAsync(ticket);

            var seat = await _seatService.GetSeatByIdAsync(ticket.MaChoNgoi);
            seat.TinhTrangChoNgoi = true;

            await _seatService.UpdateSeatAsync(seat);

            return CreatedAtRoute(nameof(GetTicketByIdAsync), new { id = ticket.MaVe }, ticket);
        }

        // DELETE api/accounts/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTicket(int id)
        {
            var ticketSelected = await _ticketService.GetTicketByIdAsync(id);

            if (ticketSelected == null)
            {
                return NotFound();
            }

            await _ticketService.DeleteTicketAsync(ticketSelected);

            return NoContent();
        }

        // DELETE api/tickets/remove?userId={userId}
        [HttpDelete("remove")]
        public async Task<ActionResult> DeleteTicketsByUserIdAsync(int userId)
        {
            var ticketSelected = await _ticketService.GetTicketsByUserIdAsync(userId);

            if (ticketSelected == null)
            {
                return NotFound();
            }

            foreach (var ticket in ticketSelected)
            {
                await _ticketService.DeleteTicketAsync(ticket);
            }

            return NoContent();
        }

        // DELETE api/tickets/delete?busTripId={busTripId}
        [HttpDelete("delete")]
        public async Task<ActionResult> DeleteTicketsByBusTripIdAsync(int busTripId)
        {
            var ticketSelected = await _ticketService.GetTicketsByUserIdAsync(busTripId);

            if (ticketSelected == null)
            {
                return NotFound();
            }

            foreach (var ticket in ticketSelected)
            {
                await _ticketService.DeleteTicketAsync(ticket);
            }

            return NoContent();
        }

    }
}