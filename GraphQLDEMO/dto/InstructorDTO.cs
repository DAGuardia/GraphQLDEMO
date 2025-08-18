namespace GraphQLDEMO.DTO
{
    public class InstructorDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double Salary { get; set; }
        public Guid Id { get; internal set; }
        public IEnumerable<CourseDTO> Courses { get; set; }
    }

}
