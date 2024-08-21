using Application.Interfaces.Repositories;
using Domain.Entities;

namespace Application.Features.StudentFeature
{
    public class StudentFeature
    {
        private readonly IBaseRepository<Student> _studentRepository;

        public StudentFeature(IBaseRepository<Student> studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public List<Student> GetAllStudents()
        {
            return _studentRepository.GetAll();
        }
    }
}
