using FirebaseAdmin.Auth;
using GraphQLDEMO.Dataloaders;
using GraphQLDEMO.services.Courses;
using static GraphQLDEMO.Schema.Courses.Models.Common;

namespace GraphQLDEMO.Schema.Courses.Query
{
    public class Course : ISearchResult
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Asignature Asignature { get; set; }

        [IsProjected]
        public Guid InstructorId{ get; set; }

        [GraphQLNonNullType]
        public async Task<Instructor> Instructor([Service] InstructorDataloader instructorDataloader)
        {
            var dto = await instructorDataloader.LoadAsync(InstructorId, CancellationToken.None);
            return new Instructor()
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Id = dto.Id,
                Salary = dto.Salary,
            };
        }
        public IEnumerable<Student> Students {get; set;}
        [IsProjected(true)]
        public string? CreatorID { get; set; } = string.Empty;
        public async Task<UserType?> Creator([Service] UserDataloader userDataloader) 
        {
            if (CreatorID == null)
                return new UserType();

            return await userDataloader.LoadAsync(CreatorID, CancellationToken.None);
        }
    }

}

