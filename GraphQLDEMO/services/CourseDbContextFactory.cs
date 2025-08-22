using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace GraphQLDEMO.services
{
    public class CourseDbContextFactory : IDesignTimeDbContextFactory<CourseDbContext>
    {
        public CourseDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CourseDbContext>();

            // misma cadena de conexión que usás en Program.cs
            optionsBuilder.UseSqlite("Data Source=course.db");

            return new CourseDbContext(optionsBuilder.Options);
        }
    }
}