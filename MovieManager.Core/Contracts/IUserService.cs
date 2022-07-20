using MovieManager.Core.ViewModels.User;
using MovieManager.Infrastructure.Data.Models;

namespace MovieManager.Core.Contracts
{
    public interface IUserService
    {
        Task<IEnumerable<UserListViewModel>> GetUsers();

        Task<UserEditViewModel> GetUserForEdit(string id);
        
        Task<bool> UpdateUser(UserEditViewModel model);

        Task<User> GetUserById(string id);
    }
}