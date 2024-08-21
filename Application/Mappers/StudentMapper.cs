using AutoMapper;
using Domain.DTO;
using Domain.Entities;

namespace Application.Mappers
{
    public class StudentMapper : Profile
    {
        public StudentMapper()
        {
            CreateMap<StudentDTO, Student>();

            CreateMap<Student, StudentResponseDTO>()
                .ForMember(x => x.FullName, src => src.MapFrom(x => $"{x.FirstMidName} {x.LastName}"));
        }
    }
}
