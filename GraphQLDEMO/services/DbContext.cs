using GraphQLDEMO.DTO;
using Microsoft.EntityFrameworkCore;

namespace GraphQLDEMO.services
{
    public class CourseDbContext :DbContext
    {
        public CourseDbContext(DbContextOptions<CourseDbContext> options) : base(options)
        {
        }
        public DbSet<CourseDTO> Courses { get; set; }
        public DbSet<StudentDTO> Students { get; set; }
        public DbSet<InstructorDTO> Instructors { get; set; }
    }
}
