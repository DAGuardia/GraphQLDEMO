using static GraphQLDEMO.Schema.Courses.Common;

namespace GraphQLDEMO.Schema.Courses.Mutation
{
    public class UpdateCourseResult
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Asignature Asignature { get; internal set; }
        public UpdateCourseResultInstructor Instructor { get; set; }
        public class UpdateCourseResultInstructor
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public double Salary { get; set; }
            public Guid Id { get; internal set; }
        }
    }
    
}
