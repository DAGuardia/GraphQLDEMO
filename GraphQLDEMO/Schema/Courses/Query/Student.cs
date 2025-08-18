
namespace GraphQLDEMO.Schema.Courses.Query
{
    public class Student
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double GPA { get; set; }
        public Guid Id { get; internal set; }
    }

}
