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
        private readonly ISeatService _seatService;
        private readonly IBusTripService _busTripService;

        public TicketsController(ITicketService ticketService, ISeatService seatService, IBusTripService busTripService, IUserService userService, IMapper mapper)
        {
            _busTripService = busTripService;
            _seatService = seatService;
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

        // POST api/tickets
        [HttpPost]
        public async Task<ActionResult<IEnumerable<TicketReadDto>>> CreateTicketAsync(TicketCreateDto ticket)
        {
            // Vexe ticketModel = _mapper.Map<Vexe>(ticket);
            foreach (var seatId in ticket.MaChoNgoi)
            {
                var seat = await _seatService.GetSeatByIdAsync(seatId);

                if (seat.TinhTrangChoNgoi == true) return BadRequest();
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
                    MaKhNavigation = await _userService.GetUserByIdAsync(ticket.MaKh)
                };
                list.Add(item);
            }

            await _ticketService.CreateTicketAsync(list);

            // Update bustrip
            var busTripSelected = await _busTripService.GetBusTripByIdAsync(ticket.MaChuyenXe);
            if (busTripSelected == null)
            {
                return NotFound();
            }
            BusTripUpdateDto busTripUpdateDto = new BusTripUpdateDto();
            busTripUpdateDto.SoChoDaDat = (busTripSelected.SoChoDaDat == null) ? ticket.MaChoNgoi.Length : busTripSelected.SoChoDaDat + ticket.MaChoNgoi.Length;
            busTripUpdateDto.SoChoTrong = (busTripSelected.SoChoTrong == null) ? ticket.MaChoNgoi.Length : busTripSelected.SoChoTrong - ticket.MaChoNgoi.Length;

            _mapper.Map(busTripUpdateDto, busTripSelected);
            await _busTripService.UpdateBusTripAsync(busTripSelected);

            // Update seat
            SeatUpdateDto seatUpdateDto = new SeatUpdateDto();
            foreach (var seatId in ticket.MaChoNgoi)
            {
                var seat = await _seatService.GetSeatByIdAsync(seatId);
                seatUpdateDto.TinhTrangChoNgoi = true;
                _mapper.Map(seatUpdateDto, seat);

                await _seatService.UpdateSeatAsync(seat);   
            }

            IEnumerable<TicketReadDto> ticketReturn = _mapper.Map<IEnumerable<TicketReadDto>>(list);

            return Ok(ticketReturn);
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

            // Update seat    
            TicketUpdateDto ticketUpdateDto = new TicketUpdateDto();

            ticketUpdateDto.TrangThai = false;
            _mapper.Map(ticketUpdateDto, ticket);

            await _ticketService.UpdateTicketAsync(ticket);

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