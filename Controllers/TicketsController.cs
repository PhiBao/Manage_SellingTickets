using System;
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
    public class TicketsController : ControllerBase
    {

        private readonly ITicketService _ticketService;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IBusTripService _busTripService;

        public TicketsController(ITicketService ticketService, IBusTripService busTripService, IUserService userService, IMapper mapper)
        {
            _busTripService = busTripService;
            _ticketService = ticketService;
            _userService = userService;
            _mapper = mapper;
        }

        // GET api/tickets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TicketReadDto>>> GetAllTicketsAsync()
        {
            var tickets = await _ticketService.GetTicketsAsync();

            return Ok(_mapper.Map<IEnumerable<TicketReadDto>>(tickets));
        }

        // GET api/tickets/{id}
        [HttpGet("{id}", Name = "GetTicketByIdAsync")]
        public async Task<ActionResult<TicketReadDto>> GetTicketByIdAsync(int id)
        {
            var ticket = await _ticketService.GetTicketByIdAsync(id);

            if (ticket != null)
            {
                return Ok(_mapper.Map<TicketReadDto>(ticket));
            }

            return NotFound();
        }

        // GET api/tickets/search?userid={userid}
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<TicketReadDto>>> GetTicketsByUserIdAsync(int userId)
        {
            var ticket = await _ticketService.GetTicketsByUserIdAsync(userId);

            if (ticket != null)
            {
                return Ok(_mapper.Map<IEnumerable<TicketReadDto>>(ticket));
            }

            return NotFound();
        }

        // GET api/tickets/seats?bustripid={bustripId}&date={date}
        [HttpGet("seats")]
        public async Task<ActionResult<IEnumerable<int>>> GetSeatsByBusTripIdAsync(int busTripId, string date)
        {
            var seats = await _ticketService.GetSeatsByBusTripIdAsync(busTripId, date);

            if (seats != null)
            {
                return Ok(seats);
            }

            return NotFound();
        }

        // POST api/tickets
        [HttpPost]
        public async Task<ActionResult<IEnumerable<TicketReadDto>>> CreateTicketAsync(TicketCreateDto ticket)
        {
            if (ticket.DaThanhToan > await _busTripService.GetPriceByBusTripAsync(ticket.MaChuyenXe)) { 
                return BadRequest();
            }
            
            foreach (var seatId in ticket.MaChoNgoi)
            {
                if (await _ticketService.CheckAvailableAsync(ticket.MaChuyenXe, ticket.NgayDi, seatId) == true) return BadRequest();
            }

            List<Vexe> list = new List<Vexe>();
            foreach (var seatId in ticket.MaChoNgoi)
            {
                Vexe item = new Vexe
                {
                    MaChoNgoi = seatId,
                    MaChuyenXe = ticket.MaChuyenXe,
                    MaKh = ticket.MaKh,
                    GhiChu = ticket.GhiChu,
                    NgayDi = ticket.NgayDi,
                    DaThanhToan = ticket.DaThanhToan,
                    MaKhNavigation = await _userService.GetUserByIdAsync(ticket.MaKh),
                    MaChuyenXeNavigation = await _busTripService.GetBusTripByIdAsync(ticket.MaChuyenXe),
                };
                list.Add(item);
            }

            await _ticketService.CreateTicketAsync(list);

            IEnumerable<TicketReadDto> ticketReturn = _mapper.Map<IEnumerable<TicketReadDto>>(list);

            return Ok(ticketReturn);
        }

        // DELETE api/tickets/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTicketAsync(int id)
        {
            var ticket = await _ticketService.GetTicketByIdAsync(id);

            if (ticket == null)
            {
                return NotFound();
            }

            // Update seat    
            TicketUpdateDto ticketUpdateDto = new TicketUpdateDto();

            ticketUpdateDto.TrangThai = false;
            _mapper.Map(ticketUpdateDto, ticket);

            await _ticketService.UpdateTicketAsync(ticket);

            return NoContent();
        }

        // GET api/tickets/revenue?date={a}
        [HttpGet("revenue")]
        public async Task<ActionResult<IEnumerable<RevenueByDay>>> GetRevenueByDayAsync(string date)
        {
            var revenues = await _ticketService.GetRevenueByDayAsync(date);

            if (revenues != null)
            {
                return Ok(revenues);
            }

            return NotFound();
        }

    }
}