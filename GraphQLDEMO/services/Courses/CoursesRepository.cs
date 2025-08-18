using GraphQLDEMO.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace GraphQLDEMO.services.Courses
{
    public class CoursesRepository
    {
        private readonly IDbContextFactory<CourseDbContext> _dbContextFactory;

        public CoursesRepository(IDbContextFactory<CourseDbContext> dbContextFactory) 
        {
            _dbContextFactory = dbContextFactory; 
        }
        public async Task<List<CourseDTO>> GetAll()
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                return await context.Courses.ToListAsync();
            }
        }
        public async Task<CourseDTO?> Get(Guid courseID)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                return await context.Courses.FirstOrDefaultAsync(z => z.Id == courseID);
            }
        }
        public async Task<CourseDTO> Create(CourseDTO courseDTO)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                context.Add(courseDTO);
                try
                {
                    await context.SaveChangesAsync();  

                }
                catch (Exception ex)
                {

                    throw;
                }
                await context.SaveChangesAsync();  
            }
            return courseDTO;
        }
        public async Task<CourseDTO> Update(CourseDTO courseDTO)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                context.Update(courseDTO);
                await context.SaveChangesAsync();
            }
            return courseDTO;
        }
        public async Task<bool> Delete(Guid courseId)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                context.Courses.Remove(new CourseDTO() { Id = courseId});
                return await context.SaveChangesAsync() > 0;
            }
        }
    }
}
