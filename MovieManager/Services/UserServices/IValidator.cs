using MovieManagerMVC.Models;

namespace MovieManagerMVC.Services.UserServices
{
    public interface IValidator
    {
        ICollection<string> ValidateUser(RegisterViewModel model);

        ICollection<string> ValidateRepository(RegisterViewModel model);
        
        //CreateRepositoryFormModel
    }
}
