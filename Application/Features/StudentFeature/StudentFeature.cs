﻿using Application.Interfaces;
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

        public StudentFeature(
            IBaseRepository<Student> studentRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
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
            var student = _mapper.Map<Student>(studentDTO);

            _studentRepository.Add(student);
            _unitOfWork.SaveChanges();

            return student;
        }

        public Student? UpdateStudentById(int id, StudentDTO studentDTO)
        {
            var student = _studentRepository.GetById(id);

            if (student == null)
            {
                return null;
            }

            _mapper.Map(studentDTO, student);
            _studentRepository.Update(student);
            _unitOfWork.SaveChanges();

            return student;
        }

        public Student? DeleteStudentById(int id)
        {
            var student = _studentRepository.GetById(id);

            if (student == null)
            {
                return null;
            }

            _studentRepository.Delete(student);
            _unitOfWork.SaveChanges();

            return student;
        }
    }
}
