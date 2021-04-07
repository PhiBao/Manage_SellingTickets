using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using backend.Dtos;
using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RevenuesController : ControllerBase
    {

        private readonly IRevenueService _revenueService;

        public RevenuesController(IRevenueService revenueService)
        {
            _revenueService = revenueService;
        }

        // GET api/revenues
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Doanhthungay>>> GetRevenuesAsync()
        {
             var revenues = await _revenueService.GetRevenuesAsync();

             return Ok(revenues);
        }

        // GET api/revenues/{id}
        [HttpGet("{id}", Name = "GetRevenueByIdAsync")]
        public async Task<ActionResult<Doanhthungay>> GetRevenueByIdAsync(int id)
        {
            var revenue = await _revenueService.GetRevenueByIdAsync(id);

            if (revenue != null)
            {
                return Ok(revenue);
            }

            return NotFound();
        }

        // POST api/revenues/{role}
        [HttpPost]
        public async Task<ActionResult<Doanhthungay>> CreateRevenueAsync(Doanhthungay revenue)
        {
            await _revenueService.CreateRevenueAsync(revenue);

            return CreatedAtRoute(nameof(GetRevenueByIdAsync), new { id = revenue.MaDoanhThuNgay }, revenue);
        }

    }
}