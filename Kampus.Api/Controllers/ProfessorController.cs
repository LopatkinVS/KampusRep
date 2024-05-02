using AutoMapper;
using Kampus.Data.Interfaces;
using Kampus.Model.Entities;
using Kampus.Model.Entities.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace Kampus.Api.Controllers
{
    [Route("api/[controller]")]
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


    }
}
