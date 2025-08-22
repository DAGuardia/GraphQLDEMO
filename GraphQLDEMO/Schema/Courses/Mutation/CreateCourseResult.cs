using static GraphQLDEMO.Schema.Courses.Models.Common;

namespace GraphQLDEMO.Schema.Courses.Mutation
{
    public class CreateCourseResult
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Asignature Asignature { get; internal set; }
        public CreateCourseResultInstructor Instructor { get; set; }

        public class CreateCourseResultInstructor
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public double Salary { get; set; }
            public Guid Id { get; internal set; }
        }
    }
   
    
}
