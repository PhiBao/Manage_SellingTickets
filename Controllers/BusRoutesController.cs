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
        public ActionResult<IEnumerable<Tuyenxe>> GetAllBusRoutes()
        {
            var busRoutes = _busRouteService.GetBusRoutes();

            return Ok(busRoutes);
        }

        // GET api/BusRoutes/id
        [HttpGet("{id}", Name = "GetBusRouteById")]
        public ActionResult<Tuyenxe> GetBusRouteById(int id)
        {
            var busRoute = _busRouteService.GetBusRouteById(id);

            if (busRoute != null)
            {
                return Ok(busRoute);
            }

            return NotFound();
        }

        // POST api/BusRoutes
        [HttpPost]
        public ActionResult<Tuyenxe> CreateBusRoute(Tuyenxe busRoute)
        {
            _busRouteService.CreateBusRoute(busRoute);

            return CreatedAtRoute(nameof(GetBusRouteById), new { id = busRoute.MaTuyenXe }, busRoute);
        }

    }
}