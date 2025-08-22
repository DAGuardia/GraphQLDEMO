using Bogus;
using GraphQLDEMO.Schema.Courses.Filters;
using GraphQLDEMO.Schema.Courses.Sorters;
using GraphQLDEMO.services;
using GraphQLDEMO.services.Courses;
using HotChocolate;
using HotChocolate.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using static GraphQLDEMO.Schema.Courses.Models.Common;

namespace GraphQLDEMO.Schema.Courses.Query
{
    public class Query
    {
        private CoursesRepository _coursesRepository;
        public Query(CoursesRepository coursesRepository)
        {
            _coursesRepository = coursesRepository;
        }        

        public async Task<IEnumerable<ISearchResult>> Search(string term, [Service] IDbContextFactory<CourseDbContext> dbContextFactory)
        {
            var courses = await dbContextFactory.CreateDbContext().Courses.Where(x => x.Name.Contains(term)).Select(x => new Course
            {
                Id = x.Id,
                Name = x.Name,
                Asignature = x.Asignature,
                InstructorId = x.InstructorID,
                CreatorID = x.CreatorId
            }).ToListAsync();

            var instructors = await dbContextFactory.CreateDbContext().Instructors.Where(x => x.FirstName.Contains(term) || x.LastName.Contains(term))
                .Select(x => new Instructor
                {
                    Id = x.Id,
                    LastName = x.LastName,
                    FirstName = x.FirstName,
                    Salary = x.Salary,
                }).ToListAsync();

            return new List<ISearchResult>()
                .Concat(courses)
                .Concat(instructors);
        }        
    }
}
