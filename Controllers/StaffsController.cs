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
        public ActionResult<IEnumerable<Nguoidung>> GetAllStaffs()
        {
            var staffs = _userService.GetStaffs();

            return Ok(staffs);
        }

        // GET api/staffs/{id}
        [HttpGet("{id}", Name = "GetStaffById")]
        public ActionResult<Nguoidung> GetStaffById(int id)
        {
            var staff = _userService.GetStaffById(id);

            if (staff != null)
            {
                return Ok(staff);
            }

            return NotFound();
        }

        //POST api/staffs
        [HttpPost]
        public ActionResult<Nguoidung> CreateStaff(Nguoidung staff)
        {
            _userService.CreateStaff(staff);

            return CreatedAtRoute(nameof(GetStaffById), new { id = staff.MaNd }, staff);
        }

        // Put api/staffs/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateStaff(int id, UserUpdateDto staffUpdateDto) 
        {
            var staffSelected = _userService.GetStaffById(id);
            if (staffSelected == null || staffSelected.Vaitro != 2)
            {
                return NotFound();
            }

            _mapper.Map(staffUpdateDto, staffSelected);
            _userService.UpdateUser(staffSelected);
            _userService.SaveChanges();

            return NoContent();
        }

    }
}