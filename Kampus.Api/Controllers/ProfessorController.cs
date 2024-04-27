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
        private readonly IMapper _mapper;
        private readonly IProfessorRepository _professorRepository;

        public ProfessorController(IMapper mapper, IProfessorRepository professorRepository)
        {
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

        }
        
    }
}
