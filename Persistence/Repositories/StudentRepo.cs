using Application.Interfaces.Repositories;
using Domain.Entities;

namespace Persistence.Repositories
{
    public class StudentRepo : BaseRepository<Student>, IStudentRepository
    {
        public StudentRepo(SchoolContext schoolContext) : base(schoolContext)
        {
        }

        public List<Student> AllStudents()
        {
            return SchoolContext.Students.ToList();
        }
    }
}
