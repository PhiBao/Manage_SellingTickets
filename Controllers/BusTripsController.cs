using System.Collections.Generic;
using AutoMapper;
using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Mvc;
using backend.Dtos;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using System.Linq;
using System;

namespace backend.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class BusTripsController : ControllerBase
    {
        private readonly IBusTripService _busTripService;
        private readonly IMapper _mapper;
        private readonly ITicketService _ticketService;

        public BusTripsController(IBusTripService busTripService, IMapper mapper, ITicketService ticketService)
        {
            _busTripService = busTripService;
            _mapper = mapper;
            _ticketService = ticketService;
        }

        // GET api/BusTrips
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Chuyenxe>>> GetAllBusTripsAsync()
        {
            var busTrips = await _busTripService.GetBusTripsAsync();
            List<BusTripReadDto> res = (List<BusTripReadDto>)_mapper.Map<IEnumerable<BusTripReadDto>>(busTrips);
            for (var idx = 0; idx < res.Count; idx++) {
                string[] dummy = res[idx].NgayXuatBen;
                var soChoNgoi = res[idx].SoChoNgoi;
                for (var i = 0; i < res[idx].NgayXuatBen.Length; i++) {
                    var soChoDaBan = await _ticketService.GetSeatsByBusTripIdAsync(res[idx].MaChuyenXe, res[idx].NgayXuatBen[i]);
                    var soChoTrong = soChoNgoi - soChoDaBan.Count();
                    dummy[i] += " | " + Convert.ToString(soChoTrong);
                }
                res[idx].NgayXuatBen = dummy;
            }

            return Ok(res);
        }

        // GET api/BusTrips/{id}
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

        // GET api/BusTrips/search?dep={a}&dest={b}&date={c}
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<BusTripSearchDto>>> GetBusTripByConditionAsync(int dep, int dest, string date)
        {
            var busTrips = await _busTripService.GetBusTripByConditionAsync(dep, dest, date);

            if (busTrips != null)
            {
                List<BusTripSearchDto> res = (List<BusTripSearchDto>)_mapper.Map<IEnumerable<BusTripSearchDto>>(busTrips);

                for (var idx = 0; idx < res.Count; idx++) {
                    var soChoNgoi = res[idx].SoChoNgoi;
                    var soChoDaBan = await _ticketService.GetSeatsByBusTripIdAsync(res[idx].MaChuyenXe, date + "T" + res[idx].GioXuatBen);
                    res[idx].SoChoTrong = (soChoNgoi - soChoDaBan.Count());
                }

                return Ok(res);
            }

            return NotFound();
        }

        //POST api/bustrips
        [HttpPost]
        public async Task<ActionResult<Chuyenxe>> CreateBusTripAsync(BusTripCreateDto busTrip)
        {
            Chuyenxe busTripModel = _mapper.Map<Chuyenxe>(busTrip);            
            await _busTripService.CreateBusTripAsync(busTripModel);

            return CreatedAtRoute(nameof(GetBusTripByIdAsync), new { id = busTripModel.MaChuyenXe }, busTripModel);
        }

        // DELETE api/bustrips/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBusTripAsync(int id)
        {
            var busTrip = await _busTripService.GetBusTripByIdAsync(id);
            if (busTrip == null)
            {
                return NotFound();
            }

            await _busTripService.DeleteBusTripAsync(busTrip);

            return NoContent();
        }
    }
}