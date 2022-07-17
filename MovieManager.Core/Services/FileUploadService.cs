using MovieManager.Core.Contracts;
using MovieManager.Infrastructure.Data.Models;
using MovieManager.Infrastructure.Repositories;

namespace MovieManager.Core.Services
{
    public class FileUploadService : IFileUploadService
    {
        private readonly IApplicationDbRepository repo;

        public FileUploadService(IApplicationDbRepository _repo)
        {
            repo = _repo;
        }

        public async Task SaveFileAsync(ApplicationFile file)
        {
            await repo.AddAsync(file);
            await repo.SaveChangesAsync();
        }
    }
}
