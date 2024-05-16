using AutoMapper;
using Kampus.Data.Interfaces;
using Kampus.Model.Entities;
using Kampus.Model.Entities.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace Kampus.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReviewController : Controller
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IUserRepository _userRepository;
        private readonly IProfessorRepository _professorRepository;
        private readonly IMapper _mapper;

        public ReviewController(IReviewRepository reviewRepository, IUserRepository userRepository,
            IProfessorRepository professorRepository, IMapper mapper)
        {
            _reviewRepository = reviewRepository;
            _userRepository = userRepository;
            _professorRepository = professorRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Review>))]
        public IActionResult GetReviews()
        {
            var reviews = _mapper.Map<List<ReviewDto>>(_reviewRepository.GetReviews());

            if(!ModelState.IsValid) 
                return BadRequest();

            return Ok(reviews);
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(Review))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetReview(int reviewId) 
        {
            if(!_reviewRepository.ReviewExists(reviewId))
                return NotFound();

            var review = _mapper.Map<ReviewDto>(_reviewRepository.GetReview(reviewId));

            if(!ModelState.IsValid)
                return BadRequest();

            return Ok(review);
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Review>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetReviewsByUser(int userId)
        {
            if(!_userRepository.UserExists(userId))
                return NotFound();

            var reviews = _mapper.Map<List<ReviewDto>>(_reviewRepository.GetReviewByUser(userId));

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(reviews);
        }

        [HttpGet]
        [ProducesResponseType(200, Type =typeof(IEnumerable<Review>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetReviewByProfessor(int professorId)
        {
            if(!_professorRepository.ProfessorExists(professorId))
                return NotFound();

            var reviews = _mapper.Map<List<ReviewDto>>(_reviewRepository.GetReviewByProfessor(professorId));

            if(!ModelState.IsValid)
                return BadRequest();

            return Ok(reviews);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult CreateReview([FromQuery] int professorId,
            [FromQuery] int userId,
            [FromBody] ReviewDto createdReview)
        {
            if (!_professorRepository.ProfessorExists(professorId) && 
                !_userRepository.UserExists(userId))
                return NotFound();

            var review = _reviewRepository.GetReviews()
                .Where(r => r.Title.Trim().ToUpper()==
                createdReview.Title.TrimEnd().ToUpper())
                .FirstOrDefault();

            if(review != null)
            {
                ModelState.AddModelError("", "Review already exists");
                return StatusCode(422, ModelState);
            }

            if(!ModelState.IsValid)
                return BadRequest();

            var reviewMap = _mapper.Map<Review>(createdReview);

            reviewMap.Professor = _professorRepository.GetProfessor(professorId);
            reviewMap.User = _userRepository.GetUserById(userId);

            if (!_reviewRepository.CreateReview(reviewMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok();
        }

        [HttpPut]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateReview(int reviewId, [FromBody]ReviewDto updatedReview)
        {
            if(updatedReview == null)
                return BadRequest();

            if(reviewId != updatedReview.Id)
                return BadRequest();

            if (!_reviewRepository.ReviewExists(reviewId))
                return NotFound();

            if(!ModelState.IsValid)
                return BadRequest();

            var reviewMap = _mapper.Map<Review>(updatedReview);

            if (!_reviewRepository.UpdateReview(reviewMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteReview(int reviewId)
        {
            if (!_reviewRepository.ReviewExists(reviewId))
                return NotFound();

            var reviewToDelete = _reviewRepository.GetReview(reviewId);
            
            if(!ModelState.IsValid)
                return BadRequest();

            if (!_reviewRepository.DeleteReview(reviewToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
