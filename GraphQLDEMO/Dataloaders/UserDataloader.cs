using FirebaseAdmin;
using FirebaseAdmin.Auth;
using GraphQLDEMO.DTO;
using GraphQLDEMO.Schema.Courses;
using GraphQLDEMO.Schema.Courses.Query;
using GraphQLDEMO.services.Courses;

namespace GraphQLDEMO.Dataloaders
{
    public class UserDataloader : BatchDataLoader<string, UserType>
    {
        private FirebaseAuth _firebaseApp { get; set; }
        public static int MAX_FIREBASE_BATCH { get; } = 100;

        public UserDataloader(IBatchScheduler batchScheduler, FirebaseApp firebaseApp) : base(batchScheduler, new DataLoaderOptions() { MaxBatchSize = MAX_FIREBASE_BATCH})
        {
            _firebaseApp = FirebaseAuth.GetAuth(firebaseApp);
        }

        protected async override Task<IReadOnlyDictionary<string, UserType>> LoadBatchAsync(IReadOnlyList<string> userIds, CancellationToken cancellationToken)
        {
            var Users = await _firebaseApp.GetUsersAsync(userIds.Select(x => new UidIdentifier(x)).ToList());
            return Users.Users.Select(z => new UserType() { Id = z.Uid, PhotoUrl = z.PhotoUrl, UserName = z.Email}).ToDictionary(p => p.Id);
        } 
    }
}
