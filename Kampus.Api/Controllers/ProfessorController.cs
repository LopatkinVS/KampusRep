using AutoMapper;
using Kampus.Data.Interfaces;
using Kampus.Model.Entities;
using Kampus.Model.Entities.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace Kampus.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProfessorController : Controller
    {
        private readonly IProfessorRepository _professorRepository;
        private readonly IUniversityRepository _universityRepository;
        private readonly IMapper _mapper;

        public ProfessorController(IMapper mapper, IProfessorRepository professorRepository, 
            IUniversityRepository universityRepository)
        {
            _universityRepository = universityRepository;
            _professorRepository = professorRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Professor>))]
        public IActionResult GetProfessors()
        {
            var professors = _mapper.Map<List<ProfessorDto>>(_professorRepository.GetProfessors());

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(professors);
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(Professor))]
        [ProducesResponseType(400)]
        public IActionResult GetProfessor(int professorId)
        {
            if(!_professorRepository.ProfessorExists(professorId))
                return NotFound();

            var professor = _mapper.Map<ProfessorDto>(_professorRepository.GetProfessor(professorId));

            if(!ModelState.IsValid) 
                return BadRequest();

            return Ok(professor);
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Professor>))]
        [ProducesResponseType(400)]
        public IActionResult GetProfessorByUniversity(int universityId)
        {
            if(!_universityRepository.UniversityExists(universityId))
                return NotFound();

            var professors = _mapper.Map<List<ProfessorDto>>
                (_professorRepository.GetProfessorsByUniversity(universityId));

            if(!ModelState.IsValid)
                return BadRequest();

            return Ok(professors);
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Review>))]
        [ProducesResponseType(400)]
        public IActionResult GetAllProfessorReview(int professorId)
        {
            if(!_professorRepository.ProfessorExists(professorId))
                return NotFound();

            var reviews = _mapper.Map<List<ReviewDto>>(_professorRepository.GetAllProfessorReviews(professorId));

            if(!ModelState.IsValid)
                return BadRequest();

            return Ok(reviews);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateProfessor([FromQuery] int univercityId, [FromBody] ProfessorDto professorCreated)
        {
            if(professorCreated == null)
                return BadRequest();

            var professors = _professorRepository.GetProfessors()
                .Where(p => p.LastName.Trim().ToUpper() == 
                professorCreated.LastName.Trim().ToUpper())
                .FirstOrDefault();

            if (professors != null)
            {
                ModelState.AddModelError("", "Professor already exists");
                return StatusCode(422, ModelState);
            }

            if(!ModelState.IsValid) 
                return BadRequest();

            var professorMap = _mapper.Map<Professor>(professorCreated);

            professorMap.University = _universityRepository.GetUniversityById(univercityId);

            if (!_professorRepository.CreateProfessor(professorMap))
                return BadRequest();

            return Ok(professorMap);
        }

        [HttpPut]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateProfessor(int professorId, [FromBody] ProfessorDto updatedProfessor)
        {
            if(updatedProfessor == null)
                return BadRequest();

            if(professorId != updatedProfessor.Id)
                return BadRequest();

            if(!_professorRepository.ProfessorExists(professorId))
                return NotFound();

            if(!ModelState.IsValid)
                return BadRequest();

            var professorMap = _mapper.Map<Professor>(updatedProfessor);

            if (!_professorRepository.UpdateProfessor(professorMap))
            {
                ModelState.AddModelError("", "Something went wrond while updating professor");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteProfessor([FromBody] int professorId)
        {
            if (_professorRepository.ProfessorExists(professorId))
                return NotFound();

            var professorToDelete = _professorRepository.GetProfessor(professorId);

            if (!_professorRepository.DeleteProfessor(professorToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting professor");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
