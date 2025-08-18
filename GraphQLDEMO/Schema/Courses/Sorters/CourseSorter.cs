using GraphQLDEMO.Schema.Courses.Query;
using HotChocolate.Data.Sorting;

namespace GraphQLDEMO.Schema.Courses.Sorters
{
    public class CourseSorter : SortInputType<Course>
    {
        protected override void Configure(ISortInputTypeDescriptor<Course> descriptor)
        {
            descriptor.Ignore(x => x.InstructorId);
            descriptor.Ignore(x => x.Id);
            descriptor.Field(x => x.Name).Name("CourseName");
            base.Configure(descriptor);
        }
    }
}
