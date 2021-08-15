using System.Collections.Generic;
using System.Globalization;
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
    public class RevenuesController : ControllerBase
    {

        private readonly IRevenueService _revenueService;
        private readonly IMapper _mapper;

        public RevenuesController(IRevenueService revenueService, IMapper mapper)
        {
            _revenueService = revenueService;
            _mapper = mapper;
        }

        // GET api/revenues
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RevenueReadDto>>> GetRevenuesAsync(string month)
        {
            var revenues = await _revenueService.GetRevenuesAsync();

            return Ok(_mapper.Map<IEnumerable<RevenueReadDto>>(revenues));
        }

        // GET api/revenues/month?month={a}
        [HttpGet("month")]
        public async Task<ActionResult<IEnumerable<RevenueReadDto>>> GetRevenuesInMonthAsync(string month)
        {
            var revenues = await _revenueService.GetRevenuesInMonthAsync(month);

            return Ok(_mapper.Map<IEnumerable<RevenueReadDto>>(revenues));
        }

        // GET api/revenues/year?year={a}
        [HttpGet("year")]
        public async Task<ActionResult<IEnumerable<RevenueHelper>>> GetRevenuesInYearAsync(string year)
        {
            var revenues = await _revenueService.GetRevenuesInYearAsync(year);

            return Ok(revenues);
        }

        // GET api/revenues/{id}
        [HttpGet("{id}", Name = "GetRevenueByIdAsync")]
        public async Task<ActionResult<RevenueReadDto>> GetRevenueByIdAsync(int id)
        {
            var revenue = await _revenueService.GetRevenueByIdAsync(id);

            if (revenue != null)
            {
                return Ok(_mapper.Map<RevenueReadDto>(revenue));
            }

            return NotFound();
        }

        // GET api/revenues/date?date={a}
        [HttpGet("date")]
        public async Task<ActionResult<RevenueReadDto>> GetRevenueByDayAsync(string date)
        {
            var revenue = await _revenueService.GetRevenueByDayAsync(date);

            if (revenue != null)
            {
                return Ok(_mapper.Map<RevenueReadDto>(revenue));
            }

            return NotFound();
        }

        // POST api/revenues/{role}
        [HttpPost]
        public async Task<ActionResult<RevenueReadDto>> CreateRevenueAsync(RevenueCreateDto revenue)
        {
            string date = revenue.Ngay.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            if (await _revenueService.GetRevenueByDayAsync(date) != null)
            {
                return BadRequest();
            }
            Doanhthungay revenueModel = _mapper.Map<Doanhthungay>(revenue);
            await _revenueService.CreateRevenueAsync(revenueModel);


            return CreatedAtRoute(nameof(GetRevenueByIdAsync), new { id = revenueModel.MaDoanhThuNgay },
                _mapper.Map<RevenueReadDto>(revenueModel));
        }

        // DELETE api/revenues/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRevenueAsync(int id)
        {
            var garage = await _revenueService.GetRevenueByIdAsync(id);

            if (garage == null)
            {
                return NotFound();
            }

            await _revenueService.DeleteRevenueAsync(garage);

            return NoContent();
        }

    }
}