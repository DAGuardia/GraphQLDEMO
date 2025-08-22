using FluentValidation;
using GraphQLDEMO.Schema.Courses.Mutation;
using GraphQLDEMO.Schema.Courses.Query;

namespace GraphQLDEMO.Schema.Courses.Validators
{
    public class CourseCreateValidator : AbstractValidator<CreateCourseCommand>
    {
        public CourseCreateValidator()
        {
            RuleFor(x => x.Name).MinimumLength(3).MaximumLength(50).WithMessage("Minimun Length between 3 and 50").WithErrorCode("COURSE_NAME_LENGTH.");
        }
    }
}
