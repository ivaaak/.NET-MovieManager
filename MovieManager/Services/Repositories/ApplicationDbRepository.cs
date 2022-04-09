using MovieManager.Data;
using MovieManager.Data.Common;

namespace MovieManager.Services.Repositories
{
    public class ApplicationDbRepository : Repository, IApplicationDbRepository
    {
        public ApplicationDbRepository(MovieContext context)
        {
            this.Context = context;
        }
    }
}
