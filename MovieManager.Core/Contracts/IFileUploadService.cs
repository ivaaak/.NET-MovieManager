using MovieManager.Infrastructure.Data.Models;

namespace MovieManager.Core.Contracts
{
    public interface IFileUploadService
    {
        Task SaveFileAsync(ApplicationFile file);
    }
}
