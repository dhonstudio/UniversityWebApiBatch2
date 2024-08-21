using Application.Interfaces;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.DTO;
using Domain.Entities;

namespace Application.Features.StudentFeature
{
    public class StudentFeature
    {
        private readonly IBaseRepository<Student> _studentRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public StudentFeature(IBaseRepository<Student> studentRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public List<Student> GetAllStudents()
        {
            return _studentRepository.GetAll();
        }

        public Student? GetStudentById(int id)
        {
            return _studentRepository.GetById(id);
        }

        public Student CreateStudent(StudentDTO studentDTO)
        {
            //var student = _mapper.Map<Student>(studentDTO);
            var student = new Student
            {
                FirstMidName = studentDTO.FirstMidName,
                LastName = studentDTO.LastName
            };
            
            _studentRepository.Add(student);
            _unitOfWork.SaveChanges();

            return student;
        }
    }
}
