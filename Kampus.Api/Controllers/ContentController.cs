using AutoMapper;
using Kampus.Api.Controllers.Abstract;
using Kampus.BI.Interfaces;
using Kampus.Model.Entities;
using Kampus.Model.Entities.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace Kampus.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    public class ContentController : BaseApiController
    {
        private readonly IContentService _contentService;
        private readonly IMapper _mapper;

        public ContentController(IContentService contentService, IMapper mapper)
        {
            _contentService = contentService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult GetUniversityes()
        {
            try
            {
                var universityes = _contentService.GetUniversityes();

                return Ok(universityes);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult GetUniversity([FromQuery] int universityId)
        {
            try
            {
                var university = _contentService.GetUniversity(universityId);

                return Ok(university);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult GetAllUniversityProfessor([FromQuery] int universityId)
        {
            try
            {
                var profesors = _contentService.GetAllUniversityProfessor(universityId);

                return Ok(profesors);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult CreateUniversity([FromQuery] UniversityDto createdUniversity)
        {
            try
            {
                var universityMap = _mapper.Map<University>(createdUniversity);

                _contentService.CreateUniversity(universityMap);

                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("[action]")]
        public IActionResult UpdateUniversity([FromQuery] UniversityDto updatedUniversity)
        {
            try
            {
                var universityMap = _mapper.Map<University>(updatedUniversity);

                _contentService.UpdateUniversity(universityMap);

                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        [Route("[action]")]
        public IActionResult DeleteUniversity(int universityId)
        {
            try
            {
                _contentService.DeleteUniversity(universityId);
                
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult GetProfessors()
        {
            try
            {
                var professors = _contentService.GetProfessors();

                return Ok(professors);
            }
            catch 
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult GetProfessor([FromQuery] int professorId)
        {
            try
            {
                var professor = _contentService.GetProfessor(professorId);

                return Ok(professor);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult GetProfessorReview([FromQuery] int professorId)
        {
            try
            {
                var reviews = _contentService.GetProfessorReviews(professorId);

                return Ok(reviews);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult CreateProfessor([FromQuery] string universityName,
            [FromQuery] ProfessorDto createdProfessor)
        {
            try
            {
                var professorMap = _mapper.Map<Professor>(createdProfessor);

                professorMap.University = _contentService.GetUniversity(universityName);
                _contentService.CreateProfessor(professorMap);

                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
        
        [HttpPut]
        [Route("[action]")]
        public IActionResult UpdateProfessor([FromQuery] string universityName,
            [FromQuery] ProfessorDto updatedProfessor)
        {
            try
            {
                var professorMap = _mapper.Map<Professor>(updatedProfessor);
                professorMap.University = _contentService.GetUniversity(universityName);
                _contentService.UpdateProfessor(professorMap);

                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        [Route("[action]")]
        public IActionResult DeleteProfessor([FromQuery] int professorId)
        {
            try
            {
                _contentService.DeleteProfessor(professorId);

                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
