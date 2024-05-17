using AutoMapper;
using Azure.Core.Extensions;
using Kampus.Data.Interfaces;
using Kampus.Model.Entities;
using Kampus.Model.Entities.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;

namespace Kampus.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;

        public UserController(IUserRepository userRepository,
            IReviewRepository reviewRepository,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _reviewRepository = reviewRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
        public IActionResult GetUsers()
        {
            if(!ModelState.IsValid) 
                return BadRequest();

            var users = _mapper.Map<List<UserDto>>(_userRepository.GetUsers());

            return Ok(users);
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(404)]
        public IActionResult GetUser(int userId)
        {
            if(!_userRepository.UserExists(userId))
                return NotFound();

            var user = _mapper.Map<UserDto>(_userRepository.GetUserById(userId));

            if(!ModelState.IsValid) 
                return BadRequest();

            return Ok(user);
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Review>))]
        [ProducesResponseType(404)]
        public IActionResult GetAllUserReviews(int userId)
        {
            if(!_userRepository.UserExists(userId))
                return NotFound();

            var reviews = _mapper.Map<List<ReviewDto>>(_userRepository.GetAllUserReviews(userId));

            if(!ModelState.IsValid)
                return BadRequest();

            return Ok(reviews);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult CreateUser([FromBody] UserDto createdUser)
        {
            if(createdUser == null)
                return BadRequest();

            var user = _userRepository.GetUsers()
                .Where(u => u.LastName.Trim().ToLower() 
                == createdUser.LastName.TrimEnd().ToLower())
                .FirstOrDefault();

            if(user != null)
            {
                ModelState.AddModelError("", "User already exists");
                return StatusCode(422, ModelState);
            }

            if(!ModelState.IsValid)
                return BadRequest();

            var userMap = _mapper.Map<User>(createdUser);

            if (!_userRepository.CreateUser(userMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok();
        }

        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateUser(int userId, [FromBody] UserDto updatedUser)
        {
            if(updatedUser == null)
                return BadRequest();

            if(!_userRepository.UserExists(userId))
                return NotFound();

            if(userId != updatedUser.Id)
                return BadRequest();

            if(!ModelState.IsValid)
                return BadRequest();

            var userMap = _mapper.Map<User>(updatedUser);

            if (!_userRepository.UpdateUser(userMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            if(!_userRepository.UserExists(userId))
                return NotFound();

            if(!ModelState.IsValid)
                return BadRequest();

            var userToDelete = _userRepository.GetUserById(userId);
            var reviewsEntities = _userRepository.GetAllUserReviews(userId);
            List<Review> reviewsToDelete = new List<Review>();

            foreach(var review in reviewsEntities)
                reviewsToDelete.Add(review);

            await Task.Run(() =>
            {
                _reviewRepository.DeleteReviews(reviewsToDelete);
            });

            if (!_userRepository.DeleteUser(userToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
