using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class SchoolContext: DbContext
    {
        public SchoolContext(DbContextOptions<SchoolContext> options) : base(options) 
        {
            SavingChanges += SavingChangesEvent;
        }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Enrollment> Enrollments { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("ripas");
            modelBuilder.Entity<Student>(e =>
            {
                e.ToTable("Siswa");
                e.Property(x => x.FirstMidName).IsRequired().HasMaxLength(512);
                e.HasIndex(x => x.NIK);
                e.HasQueryFilter(x => x.DeletedAt == null);
            });

            modelBuilder.Entity<Enrollment>(e =>
            {
                e.HasKey(x => x.EnrollmentID);
            });

            modelBuilder.Entity<Course>(e =>
            {
                e.HasKey(x => x.CourseID);
            });
        }

        public void SavingChangesEvent(object? sender, SavingChangesEventArgs e)
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is Student item
                    && entry.State == EntityState.Deleted)
                {
                    item.DeletedAt = DateTime.Now;
                    entry.State = EntityState.Modified;
                }
            }
        }

    }
}
