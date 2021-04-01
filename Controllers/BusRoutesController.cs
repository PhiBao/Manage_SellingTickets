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
        public async Task<ActionResult<IEnumerable<Tuyenxe>>> GetAllBusRoutesAsync()
        {
            var busRoutes = await _busRouteService.GetBusRoutesAsync();

            return Ok(busRoutes);
        }

        // GET api/BusRoutes/id
        [HttpGet("{id}", Name = "GetBusRouteByIdAsync")]
        public async Task<ActionResult<Tuyenxe>> GetBusRouteByIdAsync(int id)
        {
            var busRoute = await _busRouteService.GetBusRouteByIdAsync(id);

            if (busRoute != null)
            {
                return Ok(busRoute);
            }

            return NotFound();
        }

        // POST api/BusRoutes
        [HttpPost]
        public async Task<ActionResult<Tuyenxe>> CreateBusRouteAsync(Tuyenxe busRoute)
        {
            await _busRouteService.CreateBusRouteAsync(busRoute);

            return CreatedAtRoute(nameof(GetBusRouteByIdAsync), new { id = busRoute.MaTuyenXe }, busRoute);
        }

    }
}