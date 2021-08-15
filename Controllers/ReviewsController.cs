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
    public class ReviewsController : ControllerBase
    {

        private readonly IReviewService _reviewService;
        private readonly IMapper _mapper;

        public ReviewsController(IReviewService reviewService, IMapper mapper)
        {
            _reviewService = reviewService;
            _mapper = mapper;
        }

        // GET api/reviews
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReviewReadDto>>> GetAllReviewsAsync()
        {
            var reviews = await _reviewService.GetReviewsAsync();

            return Ok(_mapper.Map<IEnumerable<ReviewReadDto>>(reviews));
        }

        // GET api/reviews/details
        [HttpGet("details")]
        public async Task<ActionResult<IEnumerable<ReviewDetailsDto>>> GetAllDetailsReviewsAsync()
        {
            var reviews = await _reviewService.GetReviewsAsync();

            return Ok(_mapper.Map<IEnumerable<ReviewDetailsDto>>(reviews));
        }

        // GET api/reviews/{id}
        [HttpGet("{id}", Name = "GetReviewByIdAsync")]
        public async Task<ActionResult<ReviewReadDto>> GetReviewByIdAsync(int id)
        {
            var review = await _reviewService.GetReviewByIdAsync(id);

            if (review != null)
            {
                return Ok(_mapper.Map<ReviewReadDto>(review));
            }

            return NotFound();
        }

        // GET api/reviews/search?garageid={garageId}
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<ReviewDetailsDto>>> GetReviewsByGarageIdAsync(int garageId)
        {
            var reviews = await _reviewService.GetReviewsByGarageIdAsync(garageId);

            if (reviews != null)
            {
                return Ok(_mapper.Map<IEnumerable<ReviewDetailsDto>>(reviews));
            }

            return NotFound();
        }

        // GET api/reviews/ticket?ticketid={ticketId}
        [HttpGet("ticket")]
        public async Task<ActionResult<ReviewDetailsDto>> GetReviewByTicketIdAsync(int ticketId)
        {
            var review = await _reviewService.GetReviewByTicketIdAsync(ticketId);

            if (review != null)
            {
                return Ok(_mapper.Map<ReviewDetailsDto>(review));
            }

            return NotFound();
        }

        // POST api/reviews
        [HttpPost]
        public async Task<ActionResult<Danhgia>> CreateReviewAsync(ReviewCreateDto review)
        {
            Danhgia reviewModel = _mapper.Map<Danhgia>(review);
            await _reviewService.CreateReviewAsync(reviewModel);

            return CreatedAtRoute(nameof(GetReviewByIdAsync), new { id = reviewModel.MaDanhGia }, reviewModel);
        }

        [HttpPost("check")]
        public async Task<ActionResult<bool>> CheckAvailableAsync(ReviewCheckDto info)
        {
            return await _reviewService.CheckAvailableAsync(info.MaND, info.MaNhaXe);
        }

        // PUT api/reviews/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateReviewAsync(int id, ReviewUpdateDto reviewUpdateDto)
        {
            var reviewSelected = await _reviewService.GetReviewByIdAsync(id);
            if (reviewSelected == null)
            {
                return NotFound();
            }

            _mapper.Map(reviewUpdateDto, reviewSelected);
            await _reviewService.UpdateReviewAsync(reviewSelected);

            return NoContent();
        }

        // DELETE api/reviews/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteReview(int id)
        {
            var review = await _reviewService.GetReviewByIdAsync(id);

            if (review == null)
            {
                return NotFound();
            }

            await _reviewService.DeleteReviewAsync(review);

            return NoContent();
        }

    }
}