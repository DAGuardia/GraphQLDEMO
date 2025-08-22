using FluentValidation;
using GraphQLDEMO.Schema.Courses.Mutation;

namespace GraphQLDEMO.Schema.Courses.Validators
{
    public class CourseUpdateValidator : AbstractValidator<UpdateCourseCommand>
    {
        public CourseUpdateValidator()
        {
            RuleFor(x => x.CourseId).NotNull();
            RuleFor(x => x.Name).MinimumLength(3).MaximumLength(50).WithMessage("Minimun Length between 3 and 50").WithErrorCode("COURSE_NAME_LENGTH."); ;
        }
    }
}
