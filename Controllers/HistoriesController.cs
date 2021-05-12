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
    public class HistoriesController : ControllerBase
    {
        private readonly IHistoryService _historyService;
        private readonly IMapper _mapper;

        public HistoriesController(IHistoryService historyService, IMapper mapper)
        {
            _historyService = historyService;
            _mapper = mapper;
        }

        // GET api/histories
        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<HistoryReadDto>>> GetHistoriesByUserIdAsync(int userId)
        {
            var histories = await _historyService.GetHistoriesByUserIdAsync(userId);

            return Ok(_mapper.Map<IEnumerable<HistoryReadDto>>(histories));
        }

        // POST api/histories
        [HttpPost]
        public async Task<ActionResult<IEnumerable<HistoryReadDto>>> CreateHistoryAsync(HistoryCreateDto history)
        {
            Lichsutimkiem historyModel = _mapper.Map<Lichsutimkiem>(history);
            await _historyService.CreateHistoryAsync(historyModel);

            var histories = await _historyService.GetHistoriesByUserIdAsync(history.MaNd);

            return Ok(_mapper.Map<IEnumerable<HistoryReadDto>>(histories));
        }

        // DELETE api/histories/{userId}
        [HttpDelete("{userId}")]
        public async Task<ActionResult> DeleteAccountAsync(int userId)
        {
            await _historyService.DeleteHistoriesByUserIdAsync(userId);

            return NoContent();
        }
    }
}