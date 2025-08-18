using GraphQLDEMO.Schema.Courses.Query;
using HotChocolate.Data.Filters;

namespace GraphQLDEMO.Schema.Courses.Filters
{
    public class CourseFilter : FilterInputType<Course>
    {
        protected override void Configure(IFilterInputTypeDescriptor<Course> descriptor)
        {
            descriptor.Ignore(p => p.Students);

            base.Configure(descriptor);

        }
    }
}
