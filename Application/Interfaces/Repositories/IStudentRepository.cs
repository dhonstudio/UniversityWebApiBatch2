using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IStudentRepository : IBaseRepository<Student>
    {
        List<Student> AllStudents();
    }
}
