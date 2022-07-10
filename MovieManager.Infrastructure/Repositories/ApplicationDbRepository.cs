using MovieManager.Infrastructure.Data.Context;

namespace MovieManager.Infrastructure.Repositories
{
    public class ApplicationDbRepository : Repository, IApplicationDbRepository
    {
        public ApplicationDbRepository(MovieContext context)
        {
            this.Context = context;
        }
    }
}
