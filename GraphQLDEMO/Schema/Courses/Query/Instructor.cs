namespace GraphQLDEMO.Schema.Courses.Query
{
    public class Instructor : ISearchResult
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double Salary { get; set; }        
    }

}
