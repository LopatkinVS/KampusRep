using AutoMapper;
using Kampus.Api.Controllers.Abstract;
using Kampus.BI.Interfaces;
using Kampus.Model.Entities;
using Kampus.Model.Entities.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Kampus.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/[controller]/[action]")]
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
    }
}
