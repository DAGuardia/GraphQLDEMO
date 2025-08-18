using GraphQLDEMO.Schema.Courses.Mutation;
using GraphQLDEMO.Schema.Courses.Query;
using HotChocolate.Execution;
using HotChocolate.Subscriptions;

namespace GraphQLDEMO.Schema.Courses.Subscription
{
    public class Subscription
    {
        [Subscribe]
        public CreateCourseResult CourseCreated([EventMessage] CreateCourseResult course) => course;
        [SubscribeAndResolve]
        public ValueTask<ISourceStream<UpdateCourseResult>> CourseUpdated(Guid id, [Service] ITopicEventReceiver reciever)
        {
            var topic = $"{id}_{nameof(CourseUpdated)}";
            return reciever.SubscribeAsync<UpdateCourseResult>(topic);
        }


      
    }
}
