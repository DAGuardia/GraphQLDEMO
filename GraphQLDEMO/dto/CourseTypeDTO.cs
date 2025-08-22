using static GraphQLDEMO.Schema.Courses.Models.Common;

namespace GraphQLDEMO.DTO
{
    public class CourseDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Asignature Asignature { get; set; }
        public Guid InstructorID { get; set; }
        public InstructorDTO Instructor {get; set;}

        public IEnumerable<StudentDTO> Students {get; set;}
        public string? CreatorId { get; internal set; }
        
    }

}
