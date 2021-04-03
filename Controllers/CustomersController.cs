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
        public async Task<ActionResult<IEnumerable<Nguoidung>>> GetAllCustomersAsync()
        {
            var customers = await _userService.GetCustomersAsync();

            return Ok(customers);
        }

        // GET api/customers/{id}
        [HttpGet("{id}", Name = "GetCustomerByIdAsync")]
        public async Task<ActionResult<Nguoidung>> GetCustomerByIdAsync(int id)
        {
            var customer = await _userService.GetCustomerByIdAsync(id);

            if (customer != null)
            {
                return Ok(customer);
            }

            return NotFound();
        }

        // POST api/customers
        [HttpPost]
        public async Task<ActionResult<Nguoidung>> CreateCustomerAsync(Nguoidung customer)
        {
            await _userService.CreateCustomerAsync(customer);

            return CreatedAtRoute(nameof(GetCustomerByIdAsync), new { id = customer.MaNd }, customer);
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

        // DELETE api/customers/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCustomerAsync(int id)
        {
            var user = await _userService.GetCustomerByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            await _userService.DeleteUserAsync(user);

            return NoContent();
        }

    }
}