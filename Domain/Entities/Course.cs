using Domain.Common;

namespace Domain.Entities
{
    public class Course : BaseEntity
    {
        public int CourseID { get; set; }
        public string Title { get; set; }
        public int Credits { get; set; }
    }
}
