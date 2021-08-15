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
    public class BusRoutesController : ControllerBase
    {

        private readonly IBusRouteService _busRouteService;
        private readonly IMapper _mapper;

        public BusRoutesController(IBusRouteService busRouteService, IMapper mapper)
        {
            _busRouteService = busRouteService;
            _mapper = mapper;
        }

        // GET api/BusRoutes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BusRouteReadDto>>> GetAllBusRoutesAsync()
        {
            var busRoutes = await _busRouteService.GetBusRoutesAsync();

            return Ok(_mapper.Map<IEnumerable<BusRouteReadDto>>(busRoutes));
        }

        // GET api/BusRoutes/{id}
        [HttpGet("{id}", Name = "GetBusRouteByIdAsync")]
        public async Task<ActionResult<BusRouteReadDto>> GetBusRouteByIdAsync(int id)
        {
            var busRoute = await _busRouteService.GetBusRouteByIdAsync(id);

            if (busRoute != null)
            {
                return Ok(_mapper.Map<BusRouteReadDto>(busRoute));
            }

            return NotFound();
        }

        // GET api/BusRoutes/{id}
        [HttpGet("garage")]
        public async Task<ActionResult<IEnumerable<BusRouteReadDto>>> GetBusRoutesByGarageIdAsync(int garageId)
        {
            var busRoutes = await _busRouteService.GetBusRoutesByGarageIdAsync(garageId);

            if (busRoutes != null)
            {
                return Ok(_mapper.Map<IEnumerable<BusRouteReadDto>>(busRoutes));
            }

            return NotFound();
        }

        // POST api/BusRoutes
        [HttpPost]
        public async Task<ActionResult<Tuyenxe>> CreateBusRouteAsync(BusRouteCreateDto busRoute)
        {
            Tuyenxe busRouteModel = _mapper.Map<Tuyenxe>(busRoute);
            await _busRouteService.CreateBusRouteAsync(busRouteModel);

            return CreatedAtRoute(nameof(GetBusRouteByIdAsync), new { id = busRouteModel.MaTuyenXe }, busRouteModel);
        }

        // PUT api/BusRoutes/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateBusRouteAsync(int id, BusRouteUpdateDto busRouteUpdateDto)
        {
            var busRouteSelected = await _busRouteService.GetBusRouteByIdAsync(id);
            if (busRouteSelected == null)
            {
                return NotFound();
            }

            _mapper.Map(busRouteUpdateDto, busRouteSelected);

            await _busRouteService.UpdateBusRouteAsync(busRouteSelected);

            return NoContent();
        }

        // DELETE api/busRoutes/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBusRouteAsync(int id)
        {
            var busRouteSelected = await _busRouteService.GetBusRouteByIdAsync(id);
            if (busRouteSelected == null)
            {
                return NotFound();
            }

            await _busRouteService.DeleteBusRouteAsync(busRouteSelected);

            return NoContent();
        }

        // GET api/busroutes/search?name={c}
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<BusRouteReadDto>>> SearchBusRoutesByName(int depId, int destId )
        {
            var busRoutes = await _busRouteService.SearchBusRoutesByName(depId, destId);

            if (busRoutes != null)
            {
                return Ok(_mapper.Map<IEnumerable<BusRouteReadDto>>(busRoutes));
            }

            return NotFound();
        }

    }
}