using MovieManager.Models;

namespace MovieManager.Services.UserServices
{
    public interface IValidator
    {
        ICollection<string> ValidateUser(RegisterViewModel model);

        ICollection<string> ValidateRepository(RegisterViewModel model);
        
        //CreateRepositoryFormModel
    }
}
