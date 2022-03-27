using MovieManager.Data.DataModels;
using MovieManager.Models.User;

namespace MovieManager.Services.ServiceContracts
{
    public interface IUserService
    {
        Task<IEnumerable<UserListViewModel>> GetUsers();

        Task<UserEditViewModel> GetUserForEdit(string id);

        Task<bool> UpdateUser(UserEditViewModel model);

        Task<User> GetUserById(string id);
    }
}