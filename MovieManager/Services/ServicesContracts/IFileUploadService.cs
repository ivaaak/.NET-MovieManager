using MovieManager.Data.DataModels;

namespace MovieManager.Services.ServicesContracts
{
    public interface IFileUploadService
    {
        Task SaveFileAsync(ApplicationFile file);
    }
}
