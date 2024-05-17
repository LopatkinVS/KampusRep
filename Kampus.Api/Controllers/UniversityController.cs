using AutoMapper;
using Azure.Core.Extensions;
using Kampus.Data.Interfaces;
using Kampus.Model.Entities;
using Kampus.Model.Entities.Dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Collections;
using System.Collections.Generic;

namespace Kampus.Api.Controllers
{
    public class UniversityController : Controller
    {
        private readonly IUniversityRepository _universityRepository;
        private readonly IProfessorRepository _professorRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;

        public UniversityController(IUniversityRepository universityRepository,
            IProfessorRepository professorRepository,
            IReviewRepository reviewRepository,
            IMapper mapper)
        {
            _universityRepository = universityRepository;
            _professorRepository = professorRepository;
            _reviewRepository = reviewRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<University>))]
        public IActionResult GetUniversities()
        {
            var universities = _mapper.Map<List<UniversityDto>>(_universityRepository.GetUniversities());

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(universities);
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(University))]
        [ProducesResponseType(404)]
        public IActionResult GetUniversity(int universityId)
        {
            if (!_universityRepository.UniversityExists(universityId))
                return NotFound();

            var university = _mapper.Map<UniversityDto>(_universityRepository.GetUniversityById(universityId));

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(university);
        }

        [HttpGet]
        [ProducesResponseType(200, Type =typeof(IEnumerable<University>))]
        [ProducesResponseType(404)]
        public IActionResult GetAllProffesorsFormUniversity(int universityId)
        {
            if(!_universityRepository.UniversityExists(universityId))
                return NotFound();

            var professors = _mapper.Map<List<ProfessorDto>>(_universityRepository.GetAllProffesorsFormUniversity(universityId));

            if(professors == null)
                return NotFound();

            if(!ModelState.IsValid)
                return BadRequest();

            return Ok(professors);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult CreateUniversity([FromBody] UniversityDto createdUniversity)
        {
            if(createdUniversity == null)
                return BadRequest();

            var university = _universityRepository.GetUniversities()
                .Where(u => u.Name.Trim().ToLower() == 
                createdUniversity.Name.TrimEnd().ToLower())
                .FirstOrDefault();

            if(university != null)
            {
                ModelState.AddModelError("", "University already exist");
                return StatusCode(422, ModelState);
            }

            if(!ModelState.IsValid)
                return BadRequest();

            var universityMap = _mapper.Map<University>(createdUniversity);

            if (!_universityRepository.CreateUniversity(universityMap))
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
        public IActionResult UpdateUniversity(int universityId, [FromBody] UniversityDto updatedUniversity)
        {
            if (_universityRepository.UniversityExists(universityId))
                return NotFound();

            if(updatedUniversity == null)
                return BadRequest();

            if(updatedUniversity.Id != universityId)
                return BadRequest();

            if(!ModelState.IsValid) 
                return BadRequest();

            var universityMap = _mapper.Map<University>(updatedUniversity);

            if (!_universityRepository.UpdateUniversity(universityMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating university");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteUniversity(int universityId)
        {
            if (!_universityRepository.UniversityExists(universityId))
                return NotFound();

            var universityToDelete = _universityRepository.GetUniversityById(universityId);
            var professorsEntities = _professorRepository.GetProfessorsByUniversity(universityId);
            List<Professor> professorsToDelete = new List<Professor>();
            List<Review> reviewsToDelete = new List<Review>();

            foreach(var professorToDelete in professorsEntities)
            {
                professorsEntities.Add(professorToDelete);
                reviewsToDelete.AddRange(_professorRepository.GetAllProfessorReviews(professorToDelete.Id));
            }

            await Task.Run(() =>
            {
                _professorRepository.DeleteProfessors(professorsToDelete);
                _reviewRepository.DeleteReviews(reviewsToDelete);
            });

            if (!_universityRepository.DeleteUniversity(universityToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
