using Microsoft.EntityFrameworkCore;
using MovieManager.Data.DataModels;
using MovieManager.Models.User;
using MovieManager.Services.Repositories;
using MovieManager.Services.ServiceContracts;

namespace MovieManager.Services
{
    public class UserService : IUserService
    {
        private readonly IApplicationDbRepository repo;

        public UserService(IApplicationDbRepository _repo)
        {
            repo = _repo;
        }

        public async Task<User> GetUserById(string id)
        {
            return await repo.GetByIdAsync<User>(id);
        }

        public async Task<UserEditViewModel> GetUserForEdit(string id)
        {
            var user = await repo.GetByIdAsync<User>(id);

            return new UserEditViewModel()
            {
                Id = user.Id,
            };
        }

        public async Task<IEnumerable<UserListViewModel>> GetUsers()
        {
            return await repo.All<User>()
                .Select(u => new UserListViewModel()
                {
                    Email = u.Email,
                    Id = u.Id,
                    Name = u.UserName
                })
                .ToListAsync();
        }

        public async Task<bool> UpdateUser(UserEditViewModel model)
        {
            bool result = false;
            var user = await repo.GetByIdAsync<User>(model.Id);

            if (user != null)
            {
                user.UserName = model.FirstName;

                await repo.SaveChangesAsync();
                result = true;
            }

            return result;
        }
    }
}