
using AppAny.HotChocolate.FluentValidation;
using Bogus.DataSets;
using FirebaseAdminAuthentication.DependencyInjection.Models;
using GraphQLDEMO.DTO;
using GraphQLDEMO.MIddlewares;
using GraphQLDEMO.Schema.Courses.Models;
using GraphQLDEMO.Schema.Courses.Query;
using GraphQLDEMO.Schema.Courses.Validators;
using GraphQLDEMO.services.Courses;
using HotChocolate.Authorization;
using HotChocolate.Subscriptions;
using System.Security.Claims;
using static GraphQLDEMO.Schema.Courses.Models.Common;

namespace GraphQLDEMO.Schema.Courses.Mutation
{
    public class Mutation
    {
        private readonly CoursesRepository _coursesRepository;
        public Mutation(CoursesRepository coursesRepository) 
        {
            _coursesRepository = coursesRepository;
        }
        [Authorize]
        [UseUser]
        public async Task<CreateCourseResult> CreateCourse([UseFluentValidation] CreateCourseCommand courseCommand, [Service] ITopicEventSender topicEvent, [GlobalState("User")] UserModel user)
        {
            var course = new CourseDTO()
            {
                Id = Guid.NewGuid(),
                Asignature = courseCommand.Asignature,
                Name = courseCommand.Name,
                InstructorID = courseCommand.InstructorId,
                CreatorId = user.Id,
            };
            course = await _coursesRepository.Create(course);

            await topicEvent.SendAsync(nameof(Subscription.Subscription.CourseCreated), course);
            return new CreateCourseResult()
            {
                Id = course.Id,
                Asignature = course.Asignature,
                Name = course.Name,
                Instructor = new CreateCourseResult.CreateCourseResultInstructor() { Id = course.InstructorID }
            };
        }

        [Authorize]
        public async Task<UpdateCourseResult> UpdateCourse([UseFluentValidation] UpdateCourseCommand updateCourseCommand, [Service] ITopicEventSender topicEvent, [GlobalState("User")] UserModel user)  
        {
            var thisCourse = await _coursesRepository.Get(updateCourseCommand.CourseId);
            if (thisCourse == null)
                throw new GraphQLException(new Error("Not Found","NOT_FOUND"));
            if (thisCourse.CreatorId != user.Id)
                throw new GraphQLException(new Error("Cannot update", "PERMISSION_INVALID"));

            var course = new CourseDTO()
            {
                Id = updateCourseCommand.CourseId,
                Asignature = updateCourseCommand.Asignature,
                Name = updateCourseCommand.Name,
                InstructorID = updateCourseCommand.InstructorId

            };
            course = await _coursesRepository.Update(course);

            var updateCourseResult = new UpdateCourseResult() { 
                Name = updateCourseCommand.Name, 
                Asignature = updateCourseCommand.Asignature, 
                Id = course.Id, 
                Instructor = new UpdateCourseResult.UpdateCourseResultInstructor() 
                { 
                    Id = updateCourseCommand.InstructorId 
                } 
            };
            var topic = $"{course.Id}_{nameof(Subscription.Subscription.CourseUpdated)}";
            await topicEvent.SendAsync(topic, updateCourseResult);
            return updateCourseResult;
        }
        [Authorize(Policy = "IsAdmin")]
        public async Task<bool> DeleteCourse(Guid courseId)
        {
            try
            {
                return await _coursesRepository.Delete(courseId);
            }
            catch (Exception)
            {
                return false;
            }
            
        }
    }
}
;