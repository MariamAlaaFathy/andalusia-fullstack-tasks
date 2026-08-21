using FullStackSession6.Model;
using TaskEight.Model;

namespace TaskEight.Services.Interfaces
{
    public interface IUsersService
    {
        public Task<PagedResult<Users>> GetUsers(UserFilterParams paginationParams);
        public Task<Users> GetUserById(int id);
        public Task<Users> CreateUser(Users user);
        public Task<Users> UpdateUser(int id, Users user);
        public Task DeleteUser(int id);
    }
}
