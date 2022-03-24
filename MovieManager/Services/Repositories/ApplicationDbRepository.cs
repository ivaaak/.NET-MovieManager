using MovieManager.Data;
using MovieManager.Data.Common;
using MovieManager.Services.Repositories;

namespace MovieManager.Services.MovieServices
{
    public class ApplicationDbRepository : Repository, IApplicationDbRepository
    {
        public ApplicationDbRepository(MovieContext context)
        {
            this.Context = context;
        }
    }
}
