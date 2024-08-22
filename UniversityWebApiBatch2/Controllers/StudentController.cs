using Application.Features.StudentFeature;
using Domain.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UniversityWebApiBatch2.Controllers
{
    [Authorize(Policy = "AdminOnly")]
    [Route("[controller]")] 
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly StudentFeature _studentFeature;

        public StudentController(StudentFeature studentFeature)
        {
            _studentFeature = studentFeature;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_studentFeature.GetAllStudents());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = _studentFeature.GetStudentById(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create([FromBody] StudentDTO studentDTO)
        {
            var result = _studentFeature.CreateStudent(studentDTO);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] StudentDTO studentDTO)
        {
            var result = _studentFeature.UpdateStudentById(id, studentDTO);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _studentFeature.DeleteStudentById(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost("CreateFromGithub")]
        public async Task<IActionResult> CreateFromGithub([FromBody] StudentCreateFromGithubDTO studentDTO)
        {
            var result = await _studentFeature.CreateFromGithub(studentDTO.Username);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}
