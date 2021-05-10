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
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HistoryReadDto>>> GetHistoriesAsync()
        {
            var histories = await _historyService.GetHistoriesAsync();

            return Ok(_mapper.Map<IEnumerable<HistoryReadDto>>(histories));
        }

        // POST api/histories
        [HttpPost]
        public async Task<ActionResult<IEnumerable<HistoryReadDto>>> CreateHistoryAsync(HistoryCreateDto history)
        {
            Lichsutimkiem historyModel = _mapper.Map<Lichsutimkiem>(history);
            await _historyService.CreateHistoryAsync(historyModel);

            var histories = await _historyService.GetHistoriesAsync();

            return Ok(_mapper.Map<IEnumerable<HistoryReadDto>>(histories));
        }
    }
}