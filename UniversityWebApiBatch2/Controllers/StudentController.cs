using Application.Features.StudentFeature;
using Domain.DTO;
using Microsoft.AspNetCore.Mvc;

namespace UniversityWebApiBatch2.Controllers
{
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
    }
}
