using static GraphQLDEMO.Schema.Courses.Common;

namespace GraphQLDEMO.Schema.Courses.Mutation
{
    public class CreateCourseCommand
    {
        public string Name { get; set; }
        public Asignature Asignature { get; set; }
        public Guid InstructorId { get; set; }
    }
   
    
}
