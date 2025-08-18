using GraphQLDEMO.DTO;
using GraphQLDEMO.Schema.Courses.Query;
using Microsoft.EntityFrameworkCore;

namespace GraphQLDEMO.services.Courses
{
    public class InstructorsRepository
    {
        private readonly IDbContextFactory<CourseDbContext> _dbContextFactory;

        public InstructorsRepository(IDbContextFactory<CourseDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
        public async Task<InstructorDTO?> Get(Guid instructorId)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                return await context.Instructors.FirstOrDefaultAsync(z => z.Id == instructorId);
            }
        }

        public async Task<IEnumerable<InstructorDTO>> GetMany(IReadOnlyList<Guid> keys)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                return await context.Instructors.Where(z => keys.Contains(z.Id)).ToListAsync();
            }
        }
    }
}
