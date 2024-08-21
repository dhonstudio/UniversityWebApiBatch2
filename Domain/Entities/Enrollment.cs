using Domain.Common;

namespace Domain.Entities
{
    public class Enrollment : BaseEntity
    {
        public int EnrollmentID { get; set; }
        public int CourseID { get; set; }
        public int StudentID { get; set; }
        public Grade Grade { get; set; }
    }

    public enum Grade { A, B, C, D, E, F }
}
