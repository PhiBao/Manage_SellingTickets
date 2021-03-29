using System.Collections.Generic;
using AutoMapper;
using backend.Dtos;
using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    // api/users
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
        public ActionResult<IEnumerable<Nguoidung>> GetAllCustomers()
        {
            var customers = _userService.GetCustomers();

            return Ok(customers);
        }

        // GET api/customers/id
        [HttpGet("{id}", Name = "GetCustomerById")]
        public ActionResult<Nguoidung> GetCustomerById(int id)
        {
            var customer = _userService.GetCustomerById(id);

            if (customer != null) 
            {
                return Ok(customer);
            }

            return NotFound();
        }

        // POST api/customers
        [HttpPost]
        public ActionResult<Nguoidung> CreateCustomer(Nguoidung customer)
        {
            _userService.CreateCustomer(customer);

            return CreatedAtRoute(nameof(GetCustomerById), new { id = customer.MaNd }, customer);
        }

        // Put api/customers/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateCustomer(int id, UserUpdateDto customerUpdateDto) 
        {
            var customerSelected = _userService.GetCustomerById(id);
            if (customerSelected == null || customerSelected.Vaitro != 3)
            {
                return NotFound();
            }

            _mapper.Map(customerUpdateDto, customerSelected);
            _userService.UpdateUser(customerSelected);
            _userService.SaveChanges();

            return NoContent();
        }

    }
}