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
    public class CustomersController : ControllerBase
    {

        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public CustomersController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        // GET api/customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserReadDto>>> GetAllCustomersAsync()
        {
            var customers = await _userService.GetCustomersAsync();

            return Ok(_mapper.Map<IEnumerable<UserReadDto>>(customers));
        }

        // GET api/customers/{id}
        [HttpGet("{id}", Name = "GetCustomerByIdAsync")]
        public async Task<ActionResult<UserReadDto>> GetCustomerByIdAsync(int id)
        {
            var customer = await _userService.GetCustomerByIdAsync(id);

            if (customer != null)
            {
                return Ok(_mapper.Map<UserReadDto>(customer));
            }

            return NotFound();
        }

        // PUT api/customers/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCustomerAsync(int id, UserUpdateDto customerUpdateDto)
        {
            var customerSelected = await _userService.GetCustomerByIdAsync(id);
            if (customerSelected == null || customerSelected.Vaitro != 3)
            {
                return NotFound();
            }

            _mapper.Map(customerUpdateDto, customerSelected);
            await _userService.UpdateUserAsync(customerSelected);

            return NoContent();
        }

    }
}