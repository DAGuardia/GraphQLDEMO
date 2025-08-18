using GraphQLDEMO.DTO;
using GraphQLDEMO.services.Courses;

namespace GraphQLDEMO.Dataloaders
{
    public class InstructorDataloader : BatchDataLoader<Guid, InstructorDTO>
    {
        private InstructorsRepository repository;
        public InstructorDataloader(IBatchScheduler batchScheduler, DataLoaderOptions options, InstructorsRepository instructorsRepository) : base(batchScheduler, options)
        {
            repository = instructorsRepository;
        }

        protected async override Task<IReadOnlyDictionary<Guid, InstructorDTO>> LoadBatchAsync(IReadOnlyList<Guid> keys, CancellationToken cancellationToken)
        {
            var instructors = await repository.GetMany(keys);
            return instructors.ToDictionary(p => p.Id);
        } 
    }
}
