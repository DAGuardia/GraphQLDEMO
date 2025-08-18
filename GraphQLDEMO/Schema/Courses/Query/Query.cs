using Bogus;
using GraphQLDEMO.services.Courses;
using System;
using static GraphQLDEMO.Schema.Courses.Common;

namespace GraphQLDEMO.Schema.Courses.Query
{
    public class Query
    {
        private CoursesRepository _coursesRepository;

        public Query(CoursesRepository coursesRepository)
        {
            _coursesRepository = coursesRepository;
        }
        [UsePaging(DefaultPageSize = 10, IncludeTotalCount = true)]
        public async Task<IEnumerable<Course>> GetCourses()
        {
            var list = await _coursesRepository.GetAll();
            return list.Select(x => new Course
            {
                Id = x.Id,
                Name = x.Name,
                Asignature = x.Asignature,
                InstructorId = x.InstructorID
            }).ToList();
        }
        [UseOffsetPaging(IncludeTotalCount = true, DefaultPageSize = 10)]
        public async Task<IEnumerable<Course>> GetOffsetCourses()
        {
            var list = await _coursesRepository.GetAll();
            return list.Select(x => new Course
            {
                Id = x.Id,
                Name = x.Name,
                Asignature = x.Asignature,
                InstructorId = x.InstructorID
            }).ToList();
        }
        public async Task<Course?> GetCourse(Guid id)
        {
            var course = await _coursesRepository.Get(id);
            if (course == null) return null;
            return new Course()
            {
                Id = course.Id,
                Name = course.Name,
                Asignature = course.Asignature                
            };
        }
    }
}
