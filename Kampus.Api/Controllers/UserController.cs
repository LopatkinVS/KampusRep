using AutoMapper;
using Kampus.Api.Controllers.Abstract;
using Kampus.BI.Interfaces;
using Kampus.Model.Entities;
using Kampus.Model.Entities.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.Runtime.CompilerServices;

namespace Kampus.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    public class UserController : BaseApiController
    {
        private readonly IUserService _userService;
        private readonly IContentService _contentService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService,
            IContentService contentService,
            IMapper mapper)
        {
            _userService = userService;
            _contentService = contentService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult GetUsers()
        {
            try
            {
                var users = _userService.GetUsers();

                return Ok(users);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult GetUser([FromQuery] int userId)
        {
            try
            {
                var user = _userService.GetUser(userId);

                return Ok(user);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult GetUserReviews([FromQuery] int userId)
        {
            try
            {
                var reviews = _userService.GetUserReviews(userId);

                return Ok(reviews);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult CreateUser([FromQuery] UserDto createdUser)
        {
            try
            {
                var userMap = _mapper.Map<User>(createdUser);

                _userService.CreateUser(userMap);

                return Ok(userMap);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("[action]")]
        public IActionResult UpdateUser([FromQuery] UserDto updatedUser)
        {
            try
            {
                var userMap = _mapper.Map<User>(updatedUser);

                _userService.UpdateUser(userMap);

                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        [Route("[action]")]
        public IActionResult DeleteUser([FromQuery] int userId)
        {
            try
            {
                _userService.DeleteReview(userId);

                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult GetReview(int reiewId)
        {
            try
            {
                var review = _userService.GetReview(reiewId);

                return Ok(review);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult CreateReview([FromQuery] string professorName,
            [FromQuery] int userId,
            [FromQuery] Review createdReview)
        {
            try
            {
                var reviewMap = _mapper.Map<Review>(createdReview);
                reviewMap.Professor = _contentService.GetProfessor(professorName);
                reviewMap.User = _userService.GetUser(userId);
                
                _userService.CreateReview(reviewMap);

                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("[action]")]
        public IActionResult UpdateReview([FromQuery] string professorName,
            [FromQuery] int userId,
            [FromQuery] Review updatedReview)
        {
            try
            {
                var reviewMap = _mapper.Map<Review>(updatedReview);
                reviewMap.Professor = _contentService.GetProfessor(professorName);
                reviewMap.User = _userService.GetUser(userId);

                _userService.UpdateReview(reviewMap);

                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        [Route("[action]")]
        public IActionResult DeleteReview([FromQuery] int reviewId)
        {
            try
            {
                _userService.DeleteReview(reviewId);

                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
