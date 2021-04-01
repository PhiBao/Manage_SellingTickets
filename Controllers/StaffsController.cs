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
        public async Task<ActionResult<IEnumerable<Nguoidung>>> GetAllStaffsAsync()
        {
            var staffs = await _userService.GetStaffsAsync();

            return Ok(staffs);
        }

        // GET api/staffs/{id}
        [HttpGet("{id}", Name = "GetStaffByIdAsync")]
        public async Task<ActionResult<Nguoidung>> GetStaffByIdAsync(int id)
        {
            var staff = await _userService.GetStaffByIdAsync(id);

            if (staff != null)
            {
                return Ok(staff);
            }

            return NotFound();
        }

        //POST api/staffs
        [HttpPost]
        public async Task<ActionResult<Nguoidung>> CreateStaffAsync(Nguoidung staff)
        {
            await _userService.CreateStaffAsync(staff);

            return CreatedAtRoute(nameof(GetStaffByIdAsync), new { id = staff.MaNd }, staff);
        }

        // Put api/staffs/{id}
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

    }
}