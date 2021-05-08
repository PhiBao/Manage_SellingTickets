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
    public class StaffsController : ControllerBase
    {

        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public StaffsController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        // GET api/staffs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserReadDto>>> GetAllStaffsAsync()
        {
            var staffs = await _userService.GetStaffsAsync();

            return Ok(_mapper.Map<IEnumerable<UserReadDto>>(staffs));
        }

        // GET api/staffs/{id}
        [HttpGet("{id}", Name = "GetStaffByIdAsync")]
        public async Task<ActionResult<UserReadDto>> GetStaffByIdAsync(int id)
        {
            var staff = await _userService.GetStaffByIdAsync(id);

            if (staff != null)
            {
                return Ok(_mapper.Map<UserReadDto>(staff));
            }

            return NotFound();
        }

        // PUT api/staffs/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateStaffAsync(int id, UserUpdateDto staffUpdateDto)
        {
            var staffSelected = await _userService.GetStaffByIdAsync(id);
            if (staffSelected == null || staffSelected.Vaitro != 2)
            {
                return NotFound();
            }

            _mapper.Map(staffUpdateDto, staffSelected);
            await _userService.UpdateUserAsync(staffSelected);

            return NoContent();
        }

        // GET api/staffs/search?name={c}
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<UserReadDto>>> SearchStaffsByName(string name)
        {
            var staffs = await _userService.SearchStaffsByName(name);

            if (staffs != null)
            {
                return Ok(_mapper.Map<IEnumerable<UserReadDto>>(staffs));
            }

            return NotFound();
        }

    }
}