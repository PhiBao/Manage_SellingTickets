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
    public class GaragesController : ControllerBase
    {

        private readonly IGarageService _garageService;
        private readonly IMapper _mapper;

        public GaragesController(IGarageService garageService, IMapper mapper)
        {
            _garageService = garageService;
            _mapper = mapper;
        }

        // GET api/Garages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GarageReadDto>>> GetAllGaragesAsync()
        {
            var garages = await _garageService.GetGaragesAsync();

            return Ok(_mapper.Map<IEnumerable<GarageReadDto>>(garages));
        }

        // GET api/Garages/{id}
        [HttpGet("{id}", Name = "GetGarageByIdAsync")]
        public async Task<ActionResult<GarageReadDto>> GetGarageByIdAsync(int id)
        {
            var garage = await _garageService.GetGarageByIdAsync(id);

            if (garage != null)
            {
                return Ok(_mapper.Map<GarageReadDto>(garage));
            }

            return NotFound();
        }

        // POST api/garages
        [HttpPost]
        public async Task<ActionResult<Nhaxe>> CreateGarageAsync(GarageCreateDto garage)
        {
            Nhaxe garageModel = _mapper.Map<Nhaxe>(garage);
            await _garageService.CreateGarageAsync(garageModel);

            return CreatedAtRoute(nameof(GetGarageByIdAsync), new { id = garageModel.MaNhaXe }, garageModel);
        }

        // PUT api/garages/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateGarageAsync(int id, GarageUpdateDto garageUpdateDto)
        {
            var check = await _garageService.GetGarageByIdAsync(id);
            var garageSelected = await _garageService.GetGarageByIdAsync(id);
            if (garageSelected == null)
            {
                return NotFound();
            }

            _mapper.Map(garageUpdateDto, garageSelected);

            await _garageService.UpdateGarageAsync(garageSelected);

            return NoContent();
        }

        // DELETE api/garages/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteGarageAsync(int id)
        {
            var garage = await _garageService.GetGarageByIdAsync(id);

            if (garage == null)
            {
                return NotFound();
            }

            await _garageService.DeleteGarageAsync(garage);

            return NoContent();
        }

    }
}