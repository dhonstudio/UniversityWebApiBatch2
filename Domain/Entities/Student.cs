using Domain.Common;
using Sieve.Attributes;

namespace Domain.Entities
{
    public class Student : BaseEntity
    {
        [Sieve(CanSort = true)]
        public int ID { get; set; }
        [Sieve(CanFilter =true, CanSort =true)]
        public string FirstMidName { get; set; }
        public string LastName { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? NIK { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
