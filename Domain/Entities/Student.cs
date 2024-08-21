using Domain.Common;

namespace Domain.Entities
{
    public class Student : BaseEntity
    {
        public int ID { get; set; }
        public string FirstMidName { get; set; }
        public string LastName { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? NIK { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
